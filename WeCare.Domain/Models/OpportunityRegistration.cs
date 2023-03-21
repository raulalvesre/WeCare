using WeCare.Domain.Core;

namespace WeCare.Domain.Models;

public class OpportunityRegistration
{
    public long Id { get; set; }
    public OpportunityStatus Status { get; set; } = OpportunityStatus.PENDING;
    
    public long OpportunityId { get; set; }
    public VolunteerOpportunity? Opportunity { get; set; }
    public long CandidateId { get; set; }
    public Candidate? Candidate { get; set; }
}