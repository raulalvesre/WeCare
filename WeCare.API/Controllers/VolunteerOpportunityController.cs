using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;

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
    
    
    [HttpGet("{id:long:min(0)}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> GetById(long id)
    {
        return Ok(await _volunteerOpportunityService.GetById(id));
    }
    
    [HttpGet("search")]
    public async ValueTask<ActionResult<Pagination<VolunteerOpportunityViewModel>>> Search([FromQuery] VolunteerOpportunitySearchParam searchParams)
    {
        var candidatePage = await _volunteerOpportunityService.GetPage(searchParams);
        return Ok(candidatePage);
    }

    [HttpPost("{institutionId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Save(long institutionId, [FromForm] VolunteerOpportunityForm form)
    {
        return Ok(await _volunteerOpportunityService.Save(institutionId, form));
    }
}