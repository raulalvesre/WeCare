using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class PasswordRecoveryFormValidator : AbstractValidator<PasswordRecoveryForm>
{
    public PasswordRecoveryFormValidator()
    {
        RuleFor(x => x.Password)
            .Equal(x => x.PasswordConfirmation)
            .WithMessage("Senhas tem que ser iguais");
    }
}