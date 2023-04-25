using FluentValidation;
using WeCare.Application.ViewModels;

namespace WeCare.Application.Validators;

public class OpportunityInvitationFormValidator : AbstractValidator<OpportunityInvitationForm>
{
    public OpportunityInvitationFormValidator()
    {
        RuleFor(x => x.InvitationMessage)
            .MaximumLength(512);
            
        RuleFor(x => x.OpportunityId)
            .NotNull();
        
        RuleFor(x => x.CandidateId)
            .NotNull();
    }
}