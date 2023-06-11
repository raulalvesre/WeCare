using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.SearchParams;

public class IssueMessageSearchParams : PaginationFilterParamsBase<IssueMessage>
{
    public long? IssueId { get; set; }
    public DateTime? PeriodStart { get; set; }
    public DateTime? PeriodEnd { get; set; }

    
    protected override void Filter()
    {
        PreQuery(x => x.Include(y => y.Sender));
        
        if (IssueId.HasValue)
            And(x => x.IssueReportId == IssueId);
        
        if (PeriodStart.HasValue && PeriodEnd.HasValue)
            And(x => x.Timestamp >= PeriodStart && x.Timestamp <= PeriodEnd);

        if (PeriodStart.HasValue)
            And(x => x.Timestamp >= PeriodStart);

        if (PeriodEnd.HasValue)
            And(x => x.Timestamp <= PeriodEnd);
    }
}