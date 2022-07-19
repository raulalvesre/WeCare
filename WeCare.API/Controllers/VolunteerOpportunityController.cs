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

    [HttpPost]
    public async ValueTask<ActionResult<VolunteerOpportunityResponse>> Save(VolunteerOpportunityForm form)
    {
        //TODO pegar o id da instituição de um token JWT por exemplo
        return Ok(await _volunteerOpportunityService.Save(1, form));
    }
}