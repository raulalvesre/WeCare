using FluentValidation.Results;
using WeCare.Application.Validators;

namespace WeCare.Application.ViewModels;

public class PasswordRecoveryForm
{
    public string Token { get; set; }
    public string Password { get; set; }
    public string PasswordConfirmation { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new PasswordRecoveryFormValidator().ValidateAsync(this);
    }
}