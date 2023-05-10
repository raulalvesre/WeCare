namespace WeCare.Domain.Models;

public class IssueMessageThread
{
    public long Id { get; set; }
    public long IssueReportId { get; set; }

    public IssueReport IssueReport { get; set; }
    public ICollection<IssueMessage> Messages { get; set; }
}