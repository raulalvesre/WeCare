using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidatesController : ControllerBase
{
    private readonly CandidateService _candidateService;

    public CandidatesController(CandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    [HttpGet("{id:long:min(0)}")]
    public async ValueTask<ActionResult<Candidate>> GetById(long id)
    {
        return Ok(await _candidateService.GetById(id));
    }
    
    [HttpGet("search")]
    public async ValueTask<ActionResult<Pagination<Candidate>>> Search([FromQuery] CandidateSearchParams searchParams)
    {
        var candidatePage = await _candidateService.GetPage(searchParams);
        return Ok(candidatePage);
    }

    [HttpPost]
    public async ValueTask<ActionResult<Candidate>> Save(CandidateForm form)
    {
        return Ok(await _candidateService.Save(form));
    }
}