using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.API.Extensions;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/volunteer-opportunity")]
[Authorize(Roles = "INSTITUTION")]
public class VolunteerOpportunityController : ControllerBase
{
    private readonly VolunteerOpportunityService _volunteerOpportunityService;
    private readonly AspNetUser _aspNetUser;

    public VolunteerOpportunityController(VolunteerOpportunityService volunteerOpportunityService, AspNetUser aspNetUser)
    {
        _volunteerOpportunityService = volunteerOpportunityService;
        _aspNetUser = aspNetUser;
    }
    
    
    [HttpGet("{opportunityId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> GetById(long opportunityId)
    {
        return Ok(await _volunteerOpportunityService.GetByInstitutionIdAndOpportunityId(_aspNetUser.GetUserId(), opportunityId));
    }
    
    [HttpGet("search")]
    public async ValueTask<ActionResult<Pagination<VolunteerOpportunityViewModel>>> Search([FromQuery] VolunteerOpportunitySearchParam searchParams)
    {
        var candidatePage = await _volunteerOpportunityService.GetPage(_aspNetUser.GetUserId(), searchParams);
        return Ok(candidatePage);
    }

    [HttpPost]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Save([FromForm] VolunteerOpportunityForm form)
    {
        return Ok(await _volunteerOpportunityService.Save(_aspNetUser.GetUserId(), form));
    }

    [HttpPut("{opportunityId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Update(long opportunityId, [FromForm] VolunteerOpportunityForm form)
    {
        return Ok(await _volunteerOpportunityService.Update(_aspNetUser.GetUserId(), opportunityId, form));
    }
    
    [HttpDelete("{opportunityId:long}")]
    public async ValueTask<ActionResult> Delete(long opportunityId)
    {
        await _volunteerOpportunityService.Delete(_aspNetUser.GetUserId(), opportunityId);
        return NoContent();
    }
}