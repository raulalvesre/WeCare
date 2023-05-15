using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.SearchParams;

public class IssueMessageSearchParams : PaginationFilterParamsBase<IssueMessage>
{
    public long? Id { get; set; }
    public DateTime? PeriodStart { get; set; }
    public DateTime? PeriodEnd { get; set; }

    
    protected override void Filter()
    {
        if (Id.HasValue)
            And(x => x.Id == Id);
        
        if (PeriodStart.HasValue && PeriodEnd.HasValue)
            And(x => x.Timestamp >= PeriodStart && x.Timestamp <= PeriodEnd);

        if (PeriodStart.HasValue)
            And(x => x.Timestamp >= PeriodStart);

        if (PeriodEnd.HasValue)
            And(x => x.Timestamp <= PeriodEnd);
    }
}