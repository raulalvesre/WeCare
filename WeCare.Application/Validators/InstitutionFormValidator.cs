using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class InstitutionFormValidator : AbstractValidator<InstitutionForm>
{
    public InstitutionFormValidator()
    {
        RuleFor(institution => institution.Name)
            .NotEmpty()
            .WithMessage("O nome da instituição não pode ser vazio");

        RuleFor(institution => institution.Email)
            .NotEmpty()
            .WithMessage("E-mail necessário")
            .EmailAddress()
            .WithMessage("O e-mail de ser válido");
    }
}