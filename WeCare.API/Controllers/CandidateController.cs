using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidateController : ControllerBase
{
    private readonly CandidateService _candidateService;

    public CandidateController(CandidateService candidateService)
    {
        _candidateService = candidateService;
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

    [HttpPost]
    public async ValueTask<ActionResult<CandidateViewModel>> Save(CandidateForm form)
    {
        return Ok(await _candidateService.Save(form));
    }
    
    [HttpPut("{id:long}")]
    public async ValueTask<ActionResult<CandidateViewModel>> Update(long id, CandidateForm form)
    {
        return Ok(await _candidateService.Update(id, form));
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<ActionResult> Delete(long id)
    {
        await _candidateService.Delete(id);
        return NoContent();
    }
}