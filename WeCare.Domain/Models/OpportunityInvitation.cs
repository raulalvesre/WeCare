using WeCare.Domain.Core;

namespace WeCare.Domain.Models;

public class OpportunityInvitation
{
    public long Id { get; set; }
    public InvitationStatus Status { get; set; }
    public string InvitationMessage { get; set; }
    public string ResponseMessage { get; set; }
    public long OpportunityId { get; set; }
    public VolunteerOpportunity? Opportunity { get; set; }
    public long CandidateId { get; set; }
    public Candidate? Candidate { get; set; }


    public bool HasBeenCanceled() => Status == InvitationStatus.CANCELED;

    public bool IsPending() => Status == InvitationStatus.PENDING;

}