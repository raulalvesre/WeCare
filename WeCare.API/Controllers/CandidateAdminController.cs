using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    public async ValueTask<ActionResult<CandidateViewModel>> Save(CandidateAdminForm form)
    {
        return Ok(await _candidateService.Save(form));
    }

    


}