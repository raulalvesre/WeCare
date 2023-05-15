namespace WeCare.Domain.Models;

public class IssueMessage
{
    public long Id { get; set; }
    public long IssueReportId { get; set; }
    public long SenderId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }

    public User Sender { get; set; }
    public IssueReport IssueReport { get; set; }
}