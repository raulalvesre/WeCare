using FluentValidation.Results;
using WeCare.Application.Validators;

namespace WeCare.Application.ViewModels;

public class RegistrationFeedbackMessageForm
{
    public string FeedbackMessage { get; set; }
    
    public Task<ValidationResult> ValidateAsync()
    {
        return new RegistrationFeedbackMessageFormValidator().ValidateAsync(this);
    }
}