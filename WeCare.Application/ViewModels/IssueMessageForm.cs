using FluentValidation.Results;
using WeCare.Application.Validators;

namespace WeCare.Application.ViewModels;

public class IssueMessageForm
{
    public string Content { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new IssueMessageFormValidator().ValidateAsync(this);
    }
}