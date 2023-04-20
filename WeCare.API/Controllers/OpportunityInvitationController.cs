using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/opportunity-invitation")]
public class OpportunityInvitationController : ControllerBase
{

    private readonly OpportunityInvitationService _invitationService;

    public OpportunityInvitationController(OpportunityInvitationService invitationService)
    {
        _invitationService = invitationService;
    }


    [HttpPost]
    public async ValueTask<ActionResult<OpportunityInvitationViewModel>> Create(OpportunityInvitationForm form)
    {
        return Ok(await _invitationService.Save(form));
    }
    
}