using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.Validators;
using WeCare.Domain.Core;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/participation-certificate")]
public class ParticipationCertificateController : ControllerBase
{
    private readonly ParticipationCertificateService _participationCertificateService;

    public ParticipationCertificateController(ParticipationCertificateService participationCertificateService)
    {
        _participationCertificateService = participationCertificateService;
    }
    
    [HttpGet("{id:long}")]
    public async ValueTask<ActionResult<ParticipationCertificateViewModel>> GetById(long id)
    {
        return Ok(await _participationCertificateService.GetById(id));
    } 
    
    [HttpGet("search")]
    [Authorize]
    public async ValueTask<ActionResult<Pagination<ParticipationCertificateViewModel>>> GetPage([FromQuery] ParticipationCertificateSearchParams searchParams)
    {
        return Ok(await _participationCertificateService.GetPage(searchParams));
    }

    [HttpPost]
    [Authorize(Roles = "INSTITUTION")]
    public async ValueTask<ActionResult<ParticipationCertificateViewModel>> Save(ParticipationCertificateForm form)
    {
        return Ok(await _participationCertificateService.Save(form));
    }
}