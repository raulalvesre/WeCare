using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class IssueMessageViewModel
{
    public IssueMessageViewModel(IssueMessage issueMessage)
    {
        Id = issueMessage.Id;
        IssueReportId = issueMessage.IssueReportId;
        SenderId = issueMessage.SenderId;
        Content = issueMessage.Content;
        Timestamp = issueMessage.Timestamp;
    }

    public long Id { get; set; }
    public long IssueReportId { get; set; }
    public long SenderId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}