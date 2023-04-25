using FluentValidation.Results;

namespace WeCare.Application.ViewModels;

public class OpportunityInvitationForm
{
    public string? InvitationMessage { get; set; }
    public string? ResponseMessage { get; set; }
    public long OpportunityId { get; set; }
    public long CandidateId { get; set; }
}