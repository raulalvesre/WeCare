using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class RegistrationFeedbackMessageFormValidator : AbstractValidator<RegistrationFeedbackMessageForm>
{
    public RegistrationFeedbackMessageFormValidator()
    {
        RuleFor(x => x.FeedbackMessage)
            .MaximumLength(1024)
            .WithMessage("Mensagem pode ter no máximo 1024 caracteres");
    }
}