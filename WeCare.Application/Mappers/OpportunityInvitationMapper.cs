using WeCare.Application.ViewModels;
using WeCare.Domain.Models;
using static WeCare.Domain.Core.InvitationStatus;

namespace WeCare.Application.Mappers;

public class OpportunityInvitationMapper
{

    public OpportunityInvitation ToModel(OpportunityInvitationForm form)
    {
        return new OpportunityInvitation
        {
            InvitationMessage = form.InvitationMessage,
            Status = PENDING,
            OpportunityId = form.OpportunityId,
            CandidateId = form.CandidateId
        };
    }
    
    public OpportunityInvitationViewModel FromModel(OpportunityInvitation opportunityInvitation)
    {
        return new OpportunityInvitationViewModel
        {
            Id = opportunityInvitation.Id,
            Status = opportunityInvitation.Status,
            InvitationMessage = opportunityInvitation.InvitationMessage,
            ResponseMessage = opportunityInvitation.ResponseMessage,
            OpportunityId = opportunityInvitation.OpportunityId,
            CandidateId = opportunityInvitation.CandidateId
        };
    }
    
}