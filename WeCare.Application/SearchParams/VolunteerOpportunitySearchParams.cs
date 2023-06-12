using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.SearchParams;

public class VolunteerOpportunitySearchParams : PaginationFilterParamsBase<VolunteerOpportunity>
{
    public long? InstitutionId { get; set; }
    public DateTime? PeriodStart { get; set; }
    public DateTime? PeriodEnd { get; set; }
    public string? City { get; set; }
    public State? State { get; set; }
    public long? CandidateNotRegistered { get; set; }
    public long? CandidateNotInvited { get; set; }
    public IEnumerable<string> Causes { get; set; } = new List<string>();
    public IEnumerable<long> DesirableQualifications = new List<long>();
    public OpportunityOrderBy? OrderBy { get; set; }
    public SortDirection? OrderDirection { get; set; }

    protected override void Filter()
    {
        PreQuery(ops => ops.Include(op => op.Causes));

        And(x => x.Enabled);

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
        
        if (CandidateNotRegistered.HasValue)
            And(x => x.Registrations.All(y => y.CandidateId != CandidateNotRegistered));

        if (CandidateNotInvited.HasValue)
            And(x => x.Invitations.All(y => y.CandidateId != CandidateNotInvited));

        if (Causes.Any())
        {
            And(vo => vo.Causes
                .Any(oc => Causes.Contains(oc.Code))
            );
        }
        
        if (DesirableQualifications.Any())
        {
            And(vo => vo.DesirableQualifications
                .Any(x => DesirableQualifications.Contains(x.Id))
            );
        }

        if (OrderBy.HasValue && OrderDirection.HasValue)
            ApplyOrderBy();
    }

    private void ApplyOrderBy()
    {
        Direction = OrderDirection == SortDirection.Ascending ? SortDirection.Ascending : SortDirection.Descending;

        switch (OrderBy)
        {
            case OpportunityOrderBy.Id:
                OrderByExpression = x => x.Id;
                break;
            case OpportunityOrderBy.Name:
                OrderByExpression = op => op.Name;
                break;
            case OpportunityOrderBy.Description:
                OrderByExpression = op => op.Description;
                break;
            case OpportunityOrderBy.OpportunityDate:
                OrderByExpression = op => op.OpportunityDate;
                break;
            case OpportunityOrderBy.City:
                OrderByExpression = op => op.City;
                break;
            case OpportunityOrderBy.State:
                OrderByExpression = op => op.State;
                break;
            case OpportunityOrderBy.CreationDate:
                OrderByExpression = op => op.CreationDate;
                break;
            case OpportunityOrderBy.InstitutionId:
                OrderByExpression = op => op.InstitutionId;
                break;
        }
    }
}