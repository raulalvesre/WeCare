using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.SearchParams;

public class VolunteerOpportunitySearchParam : PaginationFilterParamsBase<VolunteerOpportunity>
{
    public long? InstitutionId { get; set; }
    public DateTime? PeriodStart { get; set; }
    public DateTime? PeriodEnd { get; set; }
    public string? City { get; set; }
    public State? State { get; set; }
    public IEnumerable<string> Causes { get; set; } = new List<string>();

    protected override void Filter()
    {
        PreQuery(ops => ops.Include(op => op.Causes));

        if (InstitutionId.HasValue)
            And(x => x.InstitutionId == InstitutionId);

        if (PeriodStart.HasValue && PeriodEnd.HasValue)
            And(x => x.OpportunityDate >= PeriodStart && x.OpportunityDate <= PeriodEnd);

        if (PeriodStart.HasValue)
            And(x => x.OpportunityDate >= PeriodStart);

        if (PeriodEnd.HasValue)
            And(x => x.OpportunityDate <= PeriodEnd);

        if (!string.IsNullOrEmpty(City))
            And(x => EF.Functions.Like(x.City, $"%{City}%"));

        if (State.HasValue)
            And(x => x.State == State);

        if (Causes.Any())
        {
            And(vo => vo.Causes
                .Any(oc => Causes.Contains(oc.Name))
            );
        }
    }
}