using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/admin/candidate")]
public class CandidateAdminController : ControllerBase
{
    private readonly CandidateService _candidateService;

    public CandidateAdminController(CandidateService candidateService) {
        _candidateService = candidateService;
    }
    
    [HttpGet("{candidateId:long}")]
    public async Task<ActionResult<CandidateViewModel>> GetById(long candidateId)
    {
        return Ok(await _candidateService.GetById(candidateId));
    }
    
    [HttpGet("search")]
    public async Task<ActionResult<CandidateViewModel>> GetPage([FromQuery] CandidateSearchParams searchParams)
    {
        return Ok(await _candidateService.GetPage(searchParams));
    }

    //TODO ajudar swagger c qual erros retornados
    [HttpPost]
    public async ValueTask<ActionResult<CandidateViewModel>> Create(CandidateAdminForm form)
    {
        //TODO Mudar pra created
        return Ok(await _candidateService.Save(form));
    }

    [HttpPut("{candidateId:long}")]
    public async ValueTask<ActionResult<CandidateViewModel>> Edit(long candidateId, CandidateAdminForm form)
    {
        return Ok(await _candidateService.Update(candidateId, form));
    }

    [HttpDelete("{candidateId:long}")]
    public async ValueTask<ActionResult> Delete(long candidateId)
    {
        await _candidateService.Remove(candidateId);
        return NoContent();
    }
    
}