using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Interfaces;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/opportunity-invitation")]
public class OpportunityInvitationController : ControllerBase
{

    private readonly OpportunityInvitationService _invitationService;
    private readonly ICurrentUser _currentUser;

    public OpportunityInvitationController(OpportunityInvitationService invitationService, ICurrentUser currentUser)
    {
        _invitationService = invitationService;
        _currentUser = currentUser;
    }

    [HttpGet("{id:long}")]
    public async ValueTask<ActionResult<OpportunityInvitationViewModel>> GetById(long id)
    {
        return Ok(await _invitationService.GetById(id));
    }
    
    [HttpGet("search")]
    public async ValueTask<ActionResult<OpportunityInvitationViewModel>> Search([FromQuery] OpportunityInvitationSearchParams searchParams)
    {
        return Ok(await _invitationService.GetPage(searchParams));
    }

    [Authorize(Roles = "INSTITUTION")]
    [HttpPost]
    public async ValueTask<ActionResult<OpportunityInvitationViewModel>> Create(OpportunityInvitationForm form)
    {
        return Ok(await _invitationService.Save(form));
    }
    
    [Authorize(Roles = "INSTITUTION")]
    [HttpPatch("{invitationId}/cancel")]
    public async ValueTask<ActionResult<OpportunityInvitationViewModel>> Cancel(long invitationId)
    {
        await _invitationService.Cancel(invitationId);
        return NoContent();
    }
    
    [Authorize(Roles = "CANDIDATE")]
    [HttpPatch("{invitationId}/accept")]
    public async ValueTask<ActionResult<OpportunityInvitationViewModel>> Accept(long invitationId, OpportunityInvitationResponseForm form)
    {
        await _invitationService.Accept(invitationId, _currentUser.GetUserId(), form);
        return NoContent();

    }

    [Authorize(Roles = "CANDIDATE")]
    [HttpPatch("{invitationId}/deny")]
    public async ValueTask<ActionResult<OpportunityInvitationViewModel>> Deny(long invitationId,
        OpportunityInvitationResponseForm form)
    {
        await _invitationService.Deny(invitationId, _currentUser.GetUserId(), form);
        return NoContent();
    }

}