using Microsoft.EntityFrameworkCore;
using WeCare.Domain;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.SearchParams;

public class OpportunityRegistrationSearchParams : PaginationFilterParamsBase<OpportunityRegistration>
{
    public RegistrationStatus? Status { get; set; }
    public long? OpportunityId { get; set; }
    public long? CandidateId { get; set; }

    protected override void Filter()
    {
        PreQuery(q =>
        {
            return q.Include(x => x.Opportunity)
                .ThenInclude(x => x.Causes)
                .Include(x => x.Opportunity)
                .ThenInclude(x => x.Institution)
                .Include(x => x.Candidate)
                .ThenInclude(x => x.CausesCandidateIsInterestedIn)
                .Include(x => x.Candidate)
                .ThenInclude(x => x.Qualifications);
        });
        
        if (Status.HasValue)
            And(x => x.Status == Status);

        if (OpportunityId.HasValue)
        {
            And(x => x.OpportunityId == OpportunityId);
        }

        if (CandidateId.HasValue)
        {
            And(x => x.CandidateId == CandidateId);
        }
    }
}