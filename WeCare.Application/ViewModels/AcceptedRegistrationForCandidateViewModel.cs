using WeCare.Domain.Core;

namespace WeCare.Application.ViewModels;

public class AcceptedRegistrationForCandidateViewModel
{
    public long Id { get; set; }
    public RegistrationStatus Status { get; set; }
    public OpportunityForAcceptedRegistrationViewModel Opportunity { get; set; }

}