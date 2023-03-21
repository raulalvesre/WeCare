using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/opportunity-cause")]
public class OpportunityCauseController : ControllerBase
{

    private readonly OpportunityCauseService _opportunityCauseService;


    public OpportunityCauseController(OpportunityCauseService opportunityCauseService)
    {
        _opportunityCauseService = opportunityCauseService;
    }

    [HttpGet]
    public async ValueTask<ActionResult<OpportunityCauseViewModel>> GetAll()
    {
        return Ok(await _opportunityCauseService.GetAll());
    } 
    
    [HttpGet("search")]
    public async ValueTask<ActionResult<OpportunityCauseViewModel>> Search([FromQuery] OpportunityCauseSearchParams searchParamses)
    {
        return Ok(await _opportunityCauseService.GetPage(searchParamses));
    } 
    
}