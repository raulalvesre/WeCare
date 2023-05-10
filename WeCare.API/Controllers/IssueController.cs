using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;

namespace WeCare.API.Controllers;

[ApiController]
public class IssueController : ControllerBase
{
    private readonly IssueService _issueService;

    public IssueController(IssueService issueService)
    {
        _issueService = issueService;
    }

    [HttpGet("{id:long}")]
    public async ValueTask<ActionResult<IssueReportViewModel>> GetById(long id)
    {
        return Ok(await _issueService.GetById(id));
    }
    
    [HttpGet("search")]
    public async ValueTask<ActionResult<Pagination<IssueReportViewModel>>> GetPage([FromQuery] IssueSearchParams searchParams)
    {
        return Ok(await _issueService.GetPage(searchParams));
    }

    [HttpPost]
    public async ValueTask<ActionResult<IssueReportViewModel>> Save(IssueReportForm form)
    {
        return Ok(await _issueService.Save(form));
    }
    
    [HttpPut("{id:long}")]
    public async ValueTask<ActionResult<IssueReportViewModel>> Update(long id, IssueReportForm form)
    {
        await _issueService.Update(id, form);
        return NoContent();
    }
    
    [HttpPut("{id:long}")]
    public async ValueTask<ActionResult<IssueReportViewModel>> Delete(long id)
    {
        await _issueService.Delete(id);
        return NoContent();
    }
    
    [HttpPut("{id:long}/close")]
    public async ValueTask<ActionResult<IssueReportViewModel>> Close(long id, IssueResolutionForm form)
    {
        await _issueService.ResolveIssue(id, form);
        return NoContent();
    }

}