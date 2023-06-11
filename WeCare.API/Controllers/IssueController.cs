using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeCare.Application.SearchParams;
using WeCare.Application.Services;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;

namespace WeCare.API.Controllers;

[ApiController]
[Route("api/issue")]
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
    [Authorize(Roles = "INSTITUTION,CANDIDATE")]
    public async ValueTask<ActionResult<IssueReportViewModel>> Save(IssueReportForm form)
    {
        return Ok(await _issueService.Save(form));
    }
    
    [Authorize]
    [HttpDelete("{id:long}")]
    public async ValueTask<ActionResult> Delete(long id)
    {
        await _issueService.Delete(id);
        return NoContent();
    }
    
    [HttpPatch("{id:long}/resolve")]
    [Authorize(Roles = "ADMIN")]
    public async ValueTask<ActionResult> Resolve(long id, IssueResolutionForm form)
    {
        await _issueService.ResolveIssue(id, form);
        return NoContent();
    }

    [Authorize]
    [HttpGet("{id:long}/messages")]
    public async ValueTask<ActionResult<IEnumerable<IssueMessageViewModel>>> GetMessagesPage(long id, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
    {
        var searchParams = new IssueMessageSearchParams
        {
            IssueId = id,
            PageSize = pageSize,
            PageNumber = pageNumber
        };
        
        return Ok(await _issueService.GetMessagesPage(searchParams));
    }

    [Authorize]
    [HttpPost("{id:long}/messages")]
    public async ValueTask<ActionResult> CreateMessage(long id, IssueMessageForm form)
    {
        await _issueService.CreateMessage(id, form);
        return NoContent();
    }

}