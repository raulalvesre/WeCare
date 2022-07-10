using FluentValidation.Results;
using WeCare.Application.Validators;

namespace WeCare.Application.ViewModels;

public class CandidateForm
{
    public string Name { get; set; }
    public string Email { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new CandidateFormValidator().ValidateAsync(this);
    }
}