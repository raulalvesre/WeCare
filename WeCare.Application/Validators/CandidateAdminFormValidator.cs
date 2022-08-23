using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class CandidateAdminFormValidator : AbstractValidator<CandidateAdminForm>
{
    public CandidateAdminFormValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("É necessário um email")
            .EmailAddress()
            .WithMessage("O email deve ser válido");

        RuleFor(x => x.Password)
            .MinimumLength(6)
            .WithMessage("A senha precisa ter no minínmo 6 caracteres")
            .MaximumLength(500)
            .WithMessage("A senha precisa ter no máximo 500 caracteres");
    }
}