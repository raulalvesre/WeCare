using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class OpportunityInvitationResponseFormValidator : AbstractValidator<OpportunityInvitationResponseForm>
{
    public OpportunityInvitationResponseFormValidator()
    {
        RuleFor(x => x.ResponseMessage)
            .MaximumLength(500);
    }
}