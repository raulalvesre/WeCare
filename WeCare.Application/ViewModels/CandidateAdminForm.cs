using FluentValidation.Results;
using WeCare.Application.Validators;

namespace WeCare.Application.ViewModels;

public class CandidateAdminForm
{
    public string Email { get; set; }
    public string Password { get; set; }
    

    public Task<ValidationResult> ValidateAsync() {
        return new CandidateAdminFormValidator().ValidateAsync(this);
    }
}