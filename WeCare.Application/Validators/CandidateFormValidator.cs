using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class CandidateFormValidator : AbstractValidator<CandidateForm>
{
    public CandidateFormValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("O nome do candidato não pode ser vazio")
            .MaximumLength(200)
            .WithMessage("O nome do candidato deve ter no máximo 200 caracteres");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("É necessário um email")
            .EmailAddress()
            .WithMessage("O email deve ser válido");
    }
}