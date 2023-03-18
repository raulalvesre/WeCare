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
    
    
    [HttpGet("{opportunityId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> GetById(long opportunityId)
    {
        //TODO get institution id from jwt
        long jwtId = 1L;
        return Ok(await _volunteerOpportunityService.GetByInstitutionIdAndOpportunityId(jwtId, opportunityId));
    }
    
    [HttpGet("search")]
    public async ValueTask<ActionResult<Pagination<VolunteerOpportunityViewModel>>> Search([FromQuery] VolunteerOpportunitySearchParam searchParams)
    {
        //TODO get institution id from jwt
        long jwtId = 1L;
        var candidatePage = await _volunteerOpportunityService.GetPage(jwtId, searchParams);
        return Ok(candidatePage);
    }

    [HttpPost]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Save([FromForm] VolunteerOpportunityForm form)
    {
        //TODO get institution id from jwt
        long jwtId = 1L;
        return Ok(await _volunteerOpportunityService.Save(jwtId, form));
    }

    [HttpPut("{opportunityId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Update(long opportunityId, [FromForm] VolunteerOpportunityForm form)
    {
        //TODO get institution id from jwt
        long jwtId = 1L;
        return Ok(await _volunteerOpportunityService.Update(jwtId, opportunityId, form));
    }
    
    [HttpDelete("{opportunityId:long}")]
    public async ValueTask<ActionResult> Delete(long opportunityId)
    {
        //TODO get institution id from jwt
        long jwtId = 1L;
        await _volunteerOpportunityService.Delete(jwtId, opportunityId);
        return NoContent();
    }
}