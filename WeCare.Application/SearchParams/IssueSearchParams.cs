using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.SearchParams;

public class IssueSearchParams : PaginationFilterParamsBase<IssueReport>
{
    public IssueStatus? Status { get; set; }
    public long? ReporterId { get; set; }
    public long? ReportedUserId { get; set; }
    public string? Description { get; set; }
    public string? ResolutionNotes { get; set; }
    public DateTime? PeriodStart { get; set; }
    public DateTime? PeriodEnd { get; set; }
    
    protected override void Filter()
    {
        PreQuery(q => q.Include(x => x.Reporter)
            .Include(x => x.ReportedUser));
        
        if (Status.HasValue)
            And(x => x.Status.Equals(Status));
        
        if (ReporterId.HasValue)
            And(x => x.ReporterId == ReporterId);
        
        if (ReportedUserId.HasValue)
            And(x => x.ReportedUserId == ReportedUserId);
        
        if (!string.IsNullOrEmpty(Description))
            And(x => EF.Functions.Like(x.Description, $"%{Description}%"));
        
        if (!string.IsNullOrEmpty(ResolutionNotes))
            And(x => EF.Functions.Like(x.ResolutionNotes, $"%{ResolutionNotes}%"));
        
        if (PeriodStart.HasValue && PeriodEnd.HasValue)
            And(x => x.Opportunity.OpportunityDate >= PeriodStart && x.Opportunity.OpportunityDate <= PeriodEnd);

        if (PeriodStart.HasValue)
            And(x => x.Opportunity.OpportunityDate >= PeriodStart);

        if (PeriodEnd.HasValue)
            And(x => x.Opportunity.OpportunityDate <= PeriodEnd);
    }
}