using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class IssueResolutionFormValidator : AbstractValidator<IssueResolutionForm>
{
    public IssueResolutionFormValidator()
    {
        RuleFor(x => x.ResolutionNotes)
            .NotEmpty()
            .WithMessage("Mensagem de resolução não pode estar vazia")
            .MaximumLength(1024)
            .WithMessage("Mensagem de resolução tem que ter no máximo 1024 caracteres");
    }
}