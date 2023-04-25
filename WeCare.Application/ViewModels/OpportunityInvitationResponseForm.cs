using FluentValidation.Results;
using WeCare.Application.Validators;

namespace WeCare.Application.ViewModels;

public class OpportunityInvitationResponseForm
{
    public string ResponseMessage { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new OpportunityInvitationResponseFormValidator().ValidateAsync(this);
    }
}