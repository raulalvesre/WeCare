using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class IssueMessageFormValidator : AbstractValidator<IssueMessageForm>
{
    public IssueMessageFormValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Mensagem não pode estar vazia")
            .MaximumLength(1024)
            .WithMessage("A mensagem pode ter no maximo 1024 caracteres");
    }
}