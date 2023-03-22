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
            And(x => x.OpportunityId == OpportunityId);
        
        if (CandidateId.HasValue)
            And(x => x.CandidateId == CandidateId);
    }
}