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
        PreQuery(x => x.Include(x => x.Candidate.CausesCandidateIsInterestedIn)
            .Include(x => x.Candidate.Qualifications)
            .Include(x => x.Opportunity.Institution));
        if (Status.HasValue)
            And(x => x.Status == Status);

        if (OpportunityId.HasValue)
        {
            PreQuery(q => q.Include(x => x.Candidate));
            And(x => x.OpportunityId == OpportunityId);
        }

        if (CandidateId.HasValue)
        {
            PreQuery(q =>
            {
                return q.Include(x => x.Opportunity)
                    .ThenInclude(x => x.Causes)
                    .Include(x => x.Opportunity)
                    .ThenInclude(x => x.Institution);
            });
            And(x => x.CandidateId == CandidateId);
        }
    }
}