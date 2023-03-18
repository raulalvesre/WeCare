using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/admin/volunteer-opportunity")]
public class VolunteerOpportunityAdminController : ControllerBase
{
    private readonly VolunteerOpportunityService _volunteerOpportunityService;

    public VolunteerOpportunityAdminController(VolunteerOpportunityService volunteerOpportunityService)
    {
        _volunteerOpportunityService = volunteerOpportunityService;
    }
    
    
    [HttpGet("{opportunityId:long:min(0)}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> GetById(long opportunityId)
    {
        return Ok(await _volunteerOpportunityService.GetById(opportunityId));
    }
    
    [HttpGet("{institutionId:long}/search")]
    public async ValueTask<ActionResult<Pagination<VolunteerOpportunityViewModel>>> Search(long institutionId, [FromQuery] VolunteerOpportunitySearchParam searchParams)
    {
        var candidatePage = await _volunteerOpportunityService.GetPage(institutionId, searchParams);
        return Ok(candidatePage);
    }

    [HttpPost("{institutionId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Save(long institutionId, [FromForm] VolunteerOpportunityForm form)
    {
        return Ok(await _volunteerOpportunityService.Save(institutionId, form));
    }

    [HttpPut("{opportunityId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Update(long opportunityId, [FromForm] VolunteerOpportunityForm form)
    {
        return Ok(await _volunteerOpportunityService.Update(opportunityId, form));
    }
    
    [HttpDelete("{opportunityId:long}")]
    public async ValueTask<ActionResult> Delete(long opportunityId)
    {
        await _volunteerOpportunityService.Delete(opportunityId);
        return NoContent();
    }
}