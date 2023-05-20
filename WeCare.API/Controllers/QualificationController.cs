using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/qualification")]
public class QualificationController : ControllerBase
{

    private readonly QualificationService _qualificationService;


    public QualificationController(QualificationService qualificationService)
    {
        _qualificationService = qualificationService;
    }

    [HttpGet]
    public async ValueTask<ActionResult<Qualification>> GetAll()
    {
        return Ok(await _qualificationService.GetAll());
    } 
    
    [HttpGet("search")]
    public async ValueTask<ActionResult<Pagination<Qualification>>> Search([FromQuery] CandidateQualificationSearchParams searchParams)
    {
        return Ok(await _qualificationService.GetPage(searchParams));
    } 
    
}