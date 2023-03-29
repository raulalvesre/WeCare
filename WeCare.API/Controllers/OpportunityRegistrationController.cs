using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/opportunity-registration")]
public class OpportunityRegistrationController : ControllerBase
{
    private readonly OpportunityRegistrationService _opportunityRegistrationService;

    public OpportunityRegistrationController(OpportunityRegistrationService opportunityRegistrationService)
    {
        _opportunityRegistrationService = opportunityRegistrationService;
    }
    
    [Authorize(Roles = "INSTITUTION")]
    [HttpPatch("{registrationId}/accept")]
    public async ValueTask<ActionResult> AcceptRegistration(long registrationId, RegistrationFeedbackMessageForm form)
    {
        await _opportunityRegistrationService.AcceptRegistration(registrationId, form.FeedbackMessage);
        return NoContent();
    }
    
    [Authorize(Roles = "INSTITUTION")]
    [HttpPatch("{registrationId:long}/deny")]
    public async ValueTask<ActionResult> DenyRegistration(long registrationId, RegistrationFeedbackMessageForm form)
    {
        await _opportunityRegistrationService.DenyRegistration(registrationId, form.FeedbackMessage);
        return NoContent();
    }
    
}