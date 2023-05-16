using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Interfaces;
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
    private readonly OpportunityRegistrationService _opportunityRegistrationService;
    private readonly ICurrentUser _currentUser;

    public VolunteerOpportunityController(VolunteerOpportunityService volunteerOpportunityService,
        ICurrentUser currentUser,
        OpportunityRegistrationService opportunityRegistrationService)
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
        var candidatePage = await _volunteerOpportunityService.GetPage(searchParams);
        return Ok(candidatePage);
    }

    [Authorize(Roles = "INSTITUTION")]
    [HttpPost]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Save([FromForm] VolunteerOpportunityForm form)
    {
        return Ok(await _volunteerOpportunityService.Save(_currentUser.GetUserId(), form));
    }
    
    [Authorize(Roles = "INSTITUTION")]
    [HttpPut("{opportunityId:long}")]
    public async ValueTask<ActionResult<VolunteerOpportunityViewModel>> Update(long opportunityId, [FromForm] VolunteerOpportunityForm form)
    {
        return Ok(await _volunteerOpportunityService.Update(_currentUser.GetUserId(), opportunityId, form));
    }
    
    [Authorize(Roles = "INSTITUTION")]
    [HttpDelete("{opportunityId:long}")]
    public async ValueTask<ActionResult> Delete(long opportunityId)
    {
        await _volunteerOpportunityService.Delete(_currentUser.GetUserId(), opportunityId);
        return NoContent();
    }
    
    [Authorize(Roles = "INSTITUTION")]
    [HttpGet("{opportunityId:long}/registrations")]
    public async ValueTask<ActionResult<Pagination<RegistrationForInstitutionViewModel>>> GetOpportunityRegistrations(long opportunityId, [FromQuery] RegistrationStatus status) 
    {
        var searchParams = new OpportunityRegistrationSearchParams()
        {
            Status = status,
            OpportunityId = opportunityId
        };
        
        return Ok(await _opportunityRegistrationService.GetPageForInstitution(searchParams));
    }
    
    [Authorize(Roles = "CANDIDATE")]
    [HttpPost("{opportunityId:long}/register")]
    public async ValueTask<ActionResult> RegisterCurrentCandidate(long opportunityId)
    {
        await _opportunityRegistrationService.RegisterCandidate(opportunityId, _currentUser.GetUserId());
        return NoContent();
    }
    
    [Authorize(Roles = "CANDIDATE")]
    [HttpPatch("{opportunityId:long}/cancel-registration")]
    public async ValueTask<ActionResult> CancelCurrentCandidateRegistration(long opportunityId)
    {
        await _opportunityRegistrationService.CancelRegistration(opportunityId, _currentUser.GetUserId());
        return NoContent();
    }
    

}