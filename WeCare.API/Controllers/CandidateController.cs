using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Services;
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
    public async ValueTask<ActionResult> GetById(long id)
    {
        var candidate = await _candidateService.GetById(id);
        return candidate is not null
            ? Ok(candidate)
            : NotFound();
    }
}