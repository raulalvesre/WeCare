using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.SearchParams;

public class ParticipationCertificateSearchParams : PaginationFilterParamsBase<ParticipationCertificate>
{
    public long? CandidateId { get; set; }
    public long? OpportunityId { get; set; }
    public DateTime? PeriodStart { get; set; }
    public DateTime? PeriodEnd { get; set; }

    protected override void Filter()
    {
        PreQuery(x => x.Include(x => x.Registration).Include(x => x.DisplayedQualifications));
        
        if (CandidateId.HasValue)
            And(x => x.Registration.CandidateId == CandidateId);
        
        if (OpportunityId.HasValue)
            And(x => x.Registration.OpportunityId == OpportunityId);
        
        if (PeriodStart.HasValue && PeriodEnd.HasValue)
            And(x => x.CreationDate >= PeriodStart && x.CreationDate <= PeriodEnd);

        if (PeriodStart.HasValue)
            And(x => x.CreationDate >= PeriodStart);

        if (PeriodEnd.HasValue)
            And(x => x.CreationDate <= PeriodEnd);
    }
}