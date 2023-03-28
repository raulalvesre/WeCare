using WeCare.Domain.Core;
using static WeCare.Domain.Core.OpportunityStatus;

namespace WeCare.Domain.Models;

public class OpportunityRegistration
{
    public long Id { get; set; }
    public OpportunityStatus Status { get; set; } = PENDING;
    
    public long OpportunityId { get; set; }
    public VolunteerOpportunity? Opportunity { get; set; }
    public long CandidateId { get; set; }
    public Candidate? Candidate { get; set; }

    public bool IsCanceled() => Status == CANCELED;

    public bool AlreadyHasBeenDeniedOrAccepted() => Status == DENIED || Status == ACCEPTED;

}