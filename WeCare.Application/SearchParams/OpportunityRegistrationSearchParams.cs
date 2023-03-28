using Microsoft.EntityFrameworkCore;
using WeCare.Domain;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.SearchParams;

public class OpportunityRegistrationSearchParams : PaginationFilterParamsBase<OpportunityRegistration>
{
    public long? OpportunityId { get; set; }
    public long? CandidateId { get; set; }
    
    protected override void Filter()
    {
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