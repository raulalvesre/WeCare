using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/volunteer-opportunity")]
public class VolunteerOpportunityController : ControllerBase
{
    private readonly VolunteerOpportunityService _volunteerOpportunityService;

    public VolunteerOpportunityController(VolunteerOpportunityService volunteerOpportunityService)
    {
        _volunteerOpportunityService = volunteerOpportunityService;
    }
    
    
    [HttpGet("{institutionId:long}/{opportunityId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> GetById(long institutionId, long opportunityId)
    {
        return Ok(await _volunteerOpportunityService.GetByInstitutionIdAndOpportunityId(institutionId, opportunityId));
    }
    
    [HttpGet("{institutionId:long}/search")]
    public async ValueTask<ActionResult<Pagination<VolunteerOpportunityViewModel>>> Search(long institutionId, [FromQuery] VolunteerOpportunitySearchParam searchParams)
    {
        searchParams.InstitutionId = institutionId;
        var candidatePage = await _volunteerOpportunityService.GetPage(searchParams);
        return Ok(candidatePage);
    }

    [HttpPost("{institutionId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Save(long institutionId, [FromForm] VolunteerOpportunityForm form)
    {
        return Ok(await _volunteerOpportunityService.Save(institutionId, form));
    }

    [HttpPut("{institutionId:long}/{opportunityId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Update(long institutionId, long opportunityId, [FromForm] VolunteerOpportunityForm form)
    {
        return Ok(await _volunteerOpportunityService.Update(institutionId, opportunityId, form));
    }
    
    [HttpDelete("{institutionId:long}/{opportunityId:long}")]
    public async ValueTask<ActionResult> Delete(long institutionId, long opportunityId)
    {
        await _volunteerOpportunityService.Delete(institutionId, opportunityId);
        return NoContent();
    }
    
}