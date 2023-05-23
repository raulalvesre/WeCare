using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class RegistrationViewModel
{
    public long Id { get; set; }
    public RegistrationStatus Status { get; set; }
    public string FeedbackMessage { get; set; }
    public long CandidateId { get; set; }
    public long OpportunityId { get; set; }

    public RegistrationViewModel(OpportunityRegistration registration)
    {
        Id = registration.Id;
        Status = registration.Status;
        FeedbackMessage = registration.FeedbackMessage;
        CandidateId = registration.CandidateId;
        OpportunityId = registration.OpportunityId;
    }
}