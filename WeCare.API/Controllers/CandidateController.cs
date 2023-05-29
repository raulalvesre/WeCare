using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Interfaces;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/candidate")]
public class CandidateController : ControllerBase
{
    private readonly CandidateService _candidateService;
    private readonly OpportunityRegistrationService _opportunityRegistrationService;
    private readonly VolunteerOpportunityService _volunteerOpportunityService;
    private readonly ICurrentUser _currentUser;

    public CandidateController(CandidateService candidateService,
        OpportunityRegistrationService opportunityRegistrationService,
        VolunteerOpportunityService volunteerOpportunityService, 
        ICurrentUser currentUser)
    {
        _candidateService = candidateService;
        _opportunityRegistrationService = opportunityRegistrationService;
        _volunteerOpportunityService = volunteerOpportunityService;
        _currentUser = currentUser;
    }

    [HttpGet("{id:long:min(0)}")]
    public async ValueTask<ActionResult<CandidateViewModel>> GetById(long id)
    {
        return Ok(await _candidateService.GetById(id));
    }
    
    [HttpGet("search")]
    public async ValueTask<ActionResult<Pagination<CandidateViewModel>>> Search([FromQuery] CandidateSearchParams searchParams)
    {
        var candidatePage = await _candidateService.GetPage(searchParams);
        return Ok(candidatePage);
    }
    
    [Authorize(Roles = "CANDIDATE")]
    [HttpPut("{candidateId:long}")]
    public async ValueTask<ActionResult<CandidateViewModel>> Update(long candidateId, CandidateUpdateForm updateForm)
    {
        return Ok(await _candidateService.Update(candidateId, updateForm));
    }

    [Authorize(Roles = "CANDIDATE")]
    [HttpDelete("{candidateId:long}")]
    public async ValueTask<ActionResult> Delete(long candidateId)
    {
        await _candidateService.Delete(candidateId);
        return NoContent();
    }
    
    [Authorize(Roles = "CANDIDATE")]
    [HttpPatch("{candidateId:long}/upload-photo")]
    public async ValueTask<ActionResult> AddPhoto(long candidateId, [FromForm] ImageUploadForm form)
    {
        await _candidateService.AddPhoto(candidateId, form);
        return NoContent();
    }
    
    [Authorize(Roles = "CANDIDATE")]
    [HttpGet("{candidateId:long}/pending-registrations")]
    public async ValueTask<ActionResult<Pagination<RegistrationForCandidateViewModel>>> GetCurrentCandidatePendingRegistrationsPage(long candidateId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var searchParams = new OpportunityRegistrationSearchParams()
        {
            Status = RegistrationStatus.PENDING,
            CandidateId = candidateId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        return Ok(await _opportunityRegistrationService.GetPageForCandidate(searchParams));
    }  
    
    [Authorize(Roles = "CANDIDATE")]
    [HttpGet("{candidateId:long}/accepted-registrations")]
    public async ValueTask<ActionResult<Pagination<AcceptedRegistrationForCandidateViewModel>>> GetCurrentCandidateAcceptedRegistrationsPage(long candidateId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var searchParams = new OpportunityRegistrationSearchParams()
        {
            Status = RegistrationStatus.ACCEPTED,
            CandidateId = candidateId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        return Ok(await _opportunityRegistrationService.GetAcceptedRegistrationsPageForCandidate(searchParams));
    }  
    
    [Authorize(Roles = "CANDIDATE")]
    [HttpGet("{candidateId:long}/denied-registrations")]
    public async ValueTask<ActionResult<Pagination<RegistrationForCandidateViewModel>>> GetCurrentCandidateDeniedRegistrationsPage(long candidateId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var searchParams = new OpportunityRegistrationSearchParams()
        {
            Status = RegistrationStatus.DENIED,
            CandidateId = candidateId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        return Ok(await _opportunityRegistrationService.GetPageForCandidate(searchParams));
    }  
    
    
    [Authorize(Roles = "CANDIDATE")]
    [HttpGet("{candidateId:long}/canceled-registrations")]
    public async ValueTask<ActionResult<Pagination<RegistrationForCandidateViewModel>>> GetCurrentCandidateCanceledRegistrationsPage(long candidateId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var searchParams = new OpportunityRegistrationSearchParams()
        {
            Status = RegistrationStatus.CANCELED,
            CandidateId = candidateId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        
        return Ok(await _opportunityRegistrationService.GetPageForCandidate(searchParams));
    }

    [Authorize(Roles = "CANDIDATE")]
    [HttpGet("recommended-opportunities")]
    public async ValueTask<ActionResult<Pagination<VolunteerOpportunityViewModel>>> GetRecommendedOpportunitiesPage(int pageNumber = 1, int pageSize = 10)
    {
        return Ok(await _volunteerOpportunityService.GetRecommendedOpportunitiesPageForCandidate(_currentUser.GetUserId(), pageNumber, pageSize));
    }
}