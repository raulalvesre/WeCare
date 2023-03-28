using WeCare.Domain.Core;

namespace WeCare.Application.ViewModels;

public class OpportunityRegistrationWithOpportunityViewModel
{
    public long Id { get; set; }
    public OpportunityStatus Status { get; set; }
    public VolunteerOpportunityViewModel Opportunity { get; set; }
}