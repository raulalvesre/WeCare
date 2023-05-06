using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/candidate-qualification")]
public class CandidateQualificationController : ControllerBase
{

    private readonly CandidateQualificationService _candidateQualificationService;


    public CandidateQualificationController(CandidateQualificationService candidateQualificationService)
    {
        _candidateQualificationService = candidateQualificationService;
    }

    [HttpGet]
    public async ValueTask<ActionResult<CandidateQualification>> GetAll()
    {
        return Ok(await _candidateQualificationService.GetAll());
    } 
    
    [HttpGet("search")]
    public async ValueTask<ActionResult<Pagination<CandidateQualification>>> Search([FromQuery] CandidateQualificationSearchParams searchParams)
    {
        return Ok(await _candidateQualificationService.GetPage(searchParams));
    } 
    
}