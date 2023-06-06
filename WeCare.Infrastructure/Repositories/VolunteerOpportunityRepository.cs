using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class VolunteerOpportunityRepository : BaseRepository<VolunteerOpportunity>
{
    public VolunteerOpportunityRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }

    public Task<VolunteerOpportunity?> GetByIdIncludingCauses(long id)
    {
        return Query
            .Include(x => x.Causes)
            .FirstOrDefaultAsync(x => x.Id == id && x.Enabled);
    }

    public Task<VolunteerOpportunity?> GetByInstitutionIdAndIdIncludingCauses(long institutionId, long opportunityId)
    {
        return Query
            .Include(x => x.Causes)
            .FirstOrDefaultAsync(x => x.InstitutionId == institutionId && x.Id == opportunityId && x.Enabled);
    }

    public async Task<Pagination<VolunteerOpportunity>> GetRecommendedOpportunitiesPageForCandidate(long candidateId,
        int pageNumber = 1, int pageSize = 10)
    {
        int skip = (pageNumber - 1) * pageSize;
        
        var baseQuery = $@"
            WITH candidate_interests AS (
                SELECT c.causes_candidate_is_interested_in_id AS cause_id
                FROM candidate_opportunity_cause c
                WHERE candidates_interested_in_id = {candidateId}
            ),
            candidate_qualifications AS (
                SELECT cq.qualifications_id AS qualification_id
                FROM candidate_qualification cq
                WHERE cq.candidates_id = {candidateId}
            ),
            opportunities_with_common_causes AS (
                SELECT vo.id AS opportunity_id, COALESCE(COUNT(oc.causes_id), 0) AS common_causes_count
                FROM candidate_interests ci
                      JOIN opportunity_cause_volunteer_opportunity oc ON ci.cause_id = oc.causes_id
                      RIGHT JOIN volunteer_opportunities vo ON vo.id = oc.volunteer_opportunities_id
                GROUP BY vo.id
            ),
            opportunities_with_common_qualifications AS (
                SELECT vo.id AS opportunity_id, COALESCE(COUNT(oc.desirable_qualifications_id), 0) AS common_qualifications_count
                FROM candidate_qualifications cq
                         JOIN qualification_volunteer_opportunity oc ON cq.qualification_id = oc.desirable_qualifications_id
                         RIGHT JOIN volunteer_opportunities vo ON vo.id = oc.opportunities_id
                GROUP BY vo.id
            )
            SELECT op.*
            FROM opportunities_with_common_causes opcc
                JOIN opportunities_with_common_qualifications opcq ON opcq.opportunity_id = opcc.opportunity_id
                JOIN volunteer_opportunities op ON (op.id = opcc.opportunity_id AND op.id = opcq.opportunity_id)
                JOIN users u on u.id = {candidateId}
            WHERE op.enabled
            ORDER BY CASE
                 WHEN u.city = op.city THEN 0
                 WHEN u.city <> op.city AND u.state = op.state THEN 1
                 ELSE 3
            END, opcc.common_causes_count DESC, opcq.common_qualifications_count DESC";

        var query = FormattableStringFactory.Create($"{baseQuery} OFFSET {skip} LIMIT {pageSize}");

        var opportunityIds = WeCareDatabaseContext.VolunteerOpportunities
            .FromSqlInterpolated(query)
            .Select(x => x.Id)
            .ToHashSet();

        var opportunitiesDict = WeCareDatabaseContext.VolunteerOpportunities
            .Include(x => x.Causes)
            .Include(x => x.DesirableQualifications)
            .Where(x => opportunityIds.Contains(x.Id))
            .ToDictionary(x => x.Id);
        
        var opportunities = opportunityIds.Select(id => opportunitiesDict[id]).ToList();
        
        var count = WeCareDatabaseContext.VolunteerOpportunities
            .FromSqlRaw(baseQuery)
            .Count();

        int totalPages = count != 0 ? (int) Math.Ceiling(count * 1.0 / pageSize) : 0;

        return new Pagination<VolunteerOpportunity>(pageNumber, pageSize, count, totalPages, opportunities);
    }
    
    public  Task<VolunteerOpportunity?> GetByIdAsync(long id)
    {
        return Query
            .Include(x => x.Causes)
            .Include(x => x.Institution)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}