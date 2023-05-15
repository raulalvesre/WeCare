using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class IssueReportViewModel
{
    public long Id { get; set; }
    public IssueStatus Status { get; set; }
    public long OpportunityId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ResolutionNotes { get; set; }
    public long ReportedUserId { get; set; }
    public long ReporterId { get; set; }
    public DateTime CreationDate { get; set; }
}