using WeCare.Domain.Core;

namespace WeCare.Application.ViewModels;

public class OpportunityInvitationViewModel
{
    public long Id { get; set; }
    public InvitationStatus Status { get; set; }
    public string InvitationMessage { get; set; }
    public string ResponseMessage { get; set; }
    public long OpportunityId { get; set; }
    public long CandidateId { get; set; }
}