using FluentValidation;
using FluentValidation.Results;
using WeCare.Application.Validators;

namespace WeCare.Application.ViewModels;

public class IssueResolutionForm
{
    public string ResolutionNotes { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new IssueResolutionFormValidator().ValidateAsync(this);
    }
}