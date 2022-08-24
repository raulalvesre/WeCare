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
            .NotEmpty()
            .WithMessage("É necessário uma senha")
            .MinimumLength(6)
            .WithMessage("A senha precisa ter no minínmo 6 caracteres")
            .MaximumLength(500)
            .WithMessage("A senha precisa ter no máximo 500 caracteres");

        //TODO quando manda null n vem a mensagem de null
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("É necessário um nome")
            .MinimumLength(3)
            .WithMessage("O nome precisa ter no minímo 3 caracteres")
            .MaximumLength(500)
            .WithMessage("O nome pode ter no máximo 500 caracteres");


    }
}