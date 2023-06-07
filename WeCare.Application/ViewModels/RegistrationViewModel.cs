using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class RegistrationViewModel
{
    public long Id { get; set; }
    public RegistrationStatus Status { get; set; }
    public string FeedbackMessage { get; set; }
    public CandidateViewModel Candidate { get; set; }
    public VolunteerOpportunityViewModel Opportunity { get; set; }

    public RegistrationViewModel(OpportunityRegistration registration)
    {
        Id = registration.Id;
        Status = registration.Status;
        FeedbackMessage = registration.FeedbackMessage;
        Candidate = new CandidateViewModel(registration.Candidate);
        Opportunity = new VolunteerOpportunityViewModel(registration.Opportunity);
    }
}