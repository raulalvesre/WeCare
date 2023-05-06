using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.SearchParams;

public class OpportunityInvitationSearchParams : PaginationFilterParamsBase<OpportunityInvitation>
{
    
    public long? OpportunityId { get; set; }
    public long? CandidateId { get; set; }
    public InvitationStatus? Status { get; set; }
    
    protected override void Filter()
    {
        PreQuery(q => q.Include(x => x.Candidate)
            .ThenInclude(x => x.Qualifications)
            .Include(x => x.Opportunity)
            .ThenInclude(x => x.Causes));

        if (OpportunityId.HasValue)
            And(x => x.OpportunityId == OpportunityId);

        if (CandidateId.HasValue)
            And(x => x.CandidateId == CandidateId);
        
        if (Status.HasValue)
            And(x => x.Status == Status);
    }
}