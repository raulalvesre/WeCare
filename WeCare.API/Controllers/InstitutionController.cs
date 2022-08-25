using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/admin/institution")]
public class InstitutionController : ControllerBase
{
    private readonly InstitutionService _institutionService;

    public InstitutionController(InstitutionService institutionService)
    {
        _institutionService = institutionService;
    }

    [HttpGet("{institutionId:long:min(1)}")]
    public async Task<ActionResult<InstitutionViewModel>> GetById(long institutionId)
    {
        var institution = await _institutionService.GetById(institutionId);

        return Ok(institution);
    }

    [HttpPost]
    public async ValueTask<ActionResult> Create(InstitutionForm form)
    {
        var institution = await _institutionService.Save(form);

        return CreatedAtAction(nameof(GetById), new { id = institution.Id }, institution);
    }
}