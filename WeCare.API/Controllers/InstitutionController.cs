using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstitutionController : ControllerBase
{
    private readonly InstitutionService _institutionService;
    
    public InstitutionController(InstitutionService institutionService)
    {
        _institutionService = institutionService;
    }

    [HttpGet("{id:long:min(0)}")]
    public async ValueTask<ActionResult<InstitutionViewModel>> GetById(long id)
    {
        return Ok(await _institutionService.GetById(id));
    }
    
    [HttpGet("search")]
    public async ValueTask<ActionResult<Pagination<InstitutionViewModel>>> Search([FromQuery] InstitutionSearchParams searchParams)
    {
        var institutionPage = await _institutionService.GetPage(searchParams);
        return Ok(institutionPage);
    }

    [HttpPost]
    public async ValueTask<ActionResult<InstitutionViewModel>> Save(InstitutionForm form)
    {
        return Ok(await _institutionService.Save(form));
    }
    
    [HttpPut("id:long")]
    public async ValueTask<ActionResult<InstitutionViewModel>> Update(long id, InstitutionForm form)
    {
        return Ok(await _institutionService.Update(id, form));
    }

    [HttpPut("id:long")]
    public async ValueTask<ActionResult> Delete(long id)
    {
        await _institutionService.Delete(id);
        return NoContent();
    }
}