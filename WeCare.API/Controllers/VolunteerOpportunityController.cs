using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/volunteer-opportunities")]
public class VolunteerOpportunityController : ControllerBase
{
    private readonly VolunteerOpportunityService _volunteerOpportunityService;

    public VolunteerOpportunityController(VolunteerOpportunityService volunteerOpportunityService)
    {
        _volunteerOpportunityService = volunteerOpportunityService;
    }

    [HttpPost("{institutionId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Save(long institutionId, [FromForm] VolunteerOpportunityForm form)
    {
        return Ok(await _volunteerOpportunityService.Save(institutionId, form));
    }
}