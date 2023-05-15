using FluentValidation.Results;
using WeCare.Application.Validators;

namespace WeCare.Application.ViewModels;

public class IssueReportForm
{
    public string Title { get; set; }
    public string Description { get; set; }
    public long ReportedUserId { get; set; }
    public long OpportunityId { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new IssueFormValidator().ValidateAsync(this);
    }
}