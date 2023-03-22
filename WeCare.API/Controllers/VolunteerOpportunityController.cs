using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.API.Extensions;
using WeCare.Application.Interfaces;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/volunteer-opportunity")]
[Authorize(Roles = "INSTITUTION")]
public class VolunteerOpportunityController : ControllerBase
{
    private readonly VolunteerOpportunityService _volunteerOpportunityService;
    private readonly OpportunityRegistrationService _opportunityRegistrationService;
    private readonly ICurrentUser _currentUser;

    public VolunteerOpportunityController(VolunteerOpportunityService volunteerOpportunityService, ICurrentUser currentUser, OpportunityRegistrationService opportunityRegistrationService)
    {
        _volunteerOpportunityService = volunteerOpportunityService;
        _currentUser = currentUser;
        _opportunityRegistrationService = opportunityRegistrationService;
    }
    
    
    [HttpGet("{opportunityId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> GetById(long opportunityId)
    {
        return Ok(await _volunteerOpportunityService.GetByInstitutionIdAndOpportunityId(_currentUser.GetUserId(), opportunityId));
    }
    
    [HttpGet("search")]
    public async ValueTask<ActionResult<Pagination<VolunteerOpportunityViewModel>>> Search([FromQuery] VolunteerOpportunitySearchParams searchParams)
    {
        var candidatePage = await _volunteerOpportunityService.GetPage(_currentUser.GetUserId(), searchParams);
        return Ok(candidatePage);
    }

    [HttpPost]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Save([FromForm] VolunteerOpportunityForm form)
    {
        return Ok(await _volunteerOpportunityService.Save(_currentUser.GetUserId(), form));
    }

    [HttpPut("{opportunityId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Update(long opportunityId, [FromForm] VolunteerOpportunityForm form)
    {
        return Ok(await _volunteerOpportunityService.Update(_currentUser.GetUserId(), opportunityId, form));
    }
    
    [HttpDelete("{opportunityId:long}")]
    public async ValueTask<ActionResult> Delete(long opportunityId)
    {
        //TODO add ICurrentUser
        await _volunteerOpportunityService.Delete(_currentUser.GetUserId(), opportunityId);
        return NoContent();
    }

    //TODO only candidate role, maybe new controller?
    [HttpPost("{opportunityId:long}/register")]
    public async ValueTask<ActionResult> RegisterCurrentCandidate(long opportunityId)
    {
        await _opportunityRegistrationService.RegisterCandidate(opportunityId, _currentUser.GetUserId());
        return NoContent();
    }

    [HttpPut("{opportunityId:long}/registrations")]
    public async ValueTask<ActionResult<Pagination<AddressViewModel>>> GetRegistrationsPage([FromQuery] OpportunityRegistrationSearchParams searchParams) 
    {
        return Ok(await _opportunityRegistrationService.GetPage(searchParams));
    }
}