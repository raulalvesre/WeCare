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
    private readonly ICurrentUser _currentUser;

    public CandidateController(CandidateService candidateService,
        OpportunityRegistrationService opportunityRegistrationService,
        ICurrentUser currentUser)
    {
        _candidateService = candidateService;
        _opportunityRegistrationService = opportunityRegistrationService;
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
    [HttpPut]
    public async ValueTask<ActionResult<CandidateViewModel>> UpdateCurrentCandidate(CandidateForm form)
    {
        return Ok(await _candidateService.Update(_currentUser.GetUserId(), form));
    }

    [Authorize(Roles = "CANDIDATE")]
    [HttpDelete]
    public async ValueTask<ActionResult> DeleteCurrentCandidate()
    {
        await _candidateService.Delete(_currentUser.GetUserId());
        return NoContent();
    }
    
    [Authorize(Roles = "CANDIDATE")]
    [HttpPatch("upload-photo")]
    public async ValueTask<ActionResult> AddPhoto([FromForm] ImageUploadForm form)
    {
        await _candidateService.AddPhoto(_currentUser.GetUserId(), form);
        return NoContent();
    }
    
    [Authorize(Roles = "CANDIDATE")]
    [HttpGet("registrations")]
    public async ValueTask<ActionResult<Pagination<RegistrationForCandidateViewModel>>> GetCurrentCandidateRegistrationsPage()
    {
        var searchParams = new OpportunityRegistrationSearchParams()
        {
            CandidateId = _currentUser.GetUserId()
        };
        
        return Ok(await _opportunityRegistrationService.GetPageForCandidate(searchParams));
    }  
    
}