using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Interfaces;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/opportunity-registration")]
public class OpportunityRegistrationController : ControllerBase
{
    private readonly OpportunityRegistrationService _opportunityRegistrationService;
    private readonly ICurrentUser _currentUser;

    public OpportunityRegistrationController(OpportunityRegistrationService opportunityRegistrationService, ICurrentUser currentUser)
    {
        _opportunityRegistrationService = opportunityRegistrationService;
        _currentUser = currentUser;
    }
    
    [Authorize(Roles = "CANDIDATE")]
    [HttpGet("my-registrations")]
    public async ValueTask<ActionResult<Pagination<OpportunityRegistrationWithCandidateViewModel>>> GetCurrentCandidateRegistrationsPage()
    {
        var searchParams = new OpportunityRegistrationSearchParams()
        {
            CandidateId = _currentUser.GetUserId()
        };
        
        return Ok(await _opportunityRegistrationService.GetPageForCandidate(searchParams));
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
    
    [Authorize(Roles = "INSTITUTION")]
    [HttpGet("{opportunityId:long}/registrations")]
    public async ValueTask<ActionResult<Pagination<OpportunityRegistrationWithCandidateViewModel>>> GetOpportunityRegistrations(long opportunityId) 
    {
        var searchParams = new OpportunityRegistrationSearchParams()
        {
            OpportunityId = opportunityId
        };
        
        return Ok(await _opportunityRegistrationService.GetPageForInstitution(searchParams));
    }

    [Authorize(Roles = "INSTITUTION")]
    [HttpPatch("accept-registration/{registrationId}")]
    public async ValueTask<ActionResult> AcceptRegistration(long registrationId)
    {
        await _opportunityRegistrationService.AcceptRegistration(registrationId);
        return NoContent();
    }
    
    [Authorize(Roles = "INSTITUTION")]
    [HttpPatch("deny-registration/{registrationId:long}")]
    public async ValueTask<ActionResult> DenyRegistration(long registrationId)
    {
        await _opportunityRegistrationService.DenyRegistration(registrationId);
        return NoContent();
    }
    
}