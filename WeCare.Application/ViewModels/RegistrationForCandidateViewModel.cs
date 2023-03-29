using WeCare.Domain.Core;

namespace WeCare.Application.ViewModels;

public class RegistrationForCandidateViewModel
{
    public long Id { get; set; }
    public OpportunityStatus Status { get; set; }
    public OpportunityForRegistrationViewModel Opportunity { get; set; }
}