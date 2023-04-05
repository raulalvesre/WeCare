using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    public CandidateController(CandidateService candidateService,
        OpportunityRegistrationService opportunityRegistrationService)
    {
        _candidateService = candidateService;
        _opportunityRegistrationService = opportunityRegistrationService;
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
    public async ValueTask<ActionResult<CandidateViewModel>> Update(long candidateId, CandidateForm form)
    {
        return Ok(await _candidateService.Update(candidateId, form));
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
    [HttpGet("{candidateId:long}/registrations")]
    public async ValueTask<ActionResult<Pagination<RegistrationForCandidateViewModel>>> GetCurrentCandidateRegistrationsPage(long candidateId, [FromQuery] OpportunityStatus status)
    {
        var searchParams = new OpportunityRegistrationSearchParams()
        {
            Status = status,
            CandidateId = candidateId
        };
        
        return Ok(await _opportunityRegistrationService.GetPageForCandidate(searchParams));
    }  
    
}