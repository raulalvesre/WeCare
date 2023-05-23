using WeCare.Domain.Core;
using static WeCare.Domain.Core.RegistrationStatus;

namespace WeCare.Domain.Models;

public class OpportunityRegistration
{
    public long Id { get; set; }
    public RegistrationStatus Status { get; set; } = PENDING;
    public string FeedbackMessage { get; set; }
    
    public long OpportunityId { get; set; }
    public VolunteerOpportunity? Opportunity { get; set; }
    public long CandidateId { get; set; }
    public Candidate? Candidate { get; set; }

    public bool IsCanceled() => Status == CANCELED;

    public bool AlreadyHasBeenDeniedOrAccepted() => Status == DENIED || Status == ACCEPTED;

    public bool IsNotAccepted() => Status != ACCEPTED;

}