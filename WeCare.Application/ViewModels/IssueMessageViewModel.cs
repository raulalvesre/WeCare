using WeCare.Domain.Models;

namespace WeCare.Application.ViewModels;

public class IssueMessageViewModel
{
    public IssueMessageViewModel(IssueMessage issueMessage)
    {
        Id = issueMessage.Id;
        IssueReportId = issueMessage.IssueReportId;
        SenderId = issueMessage.SenderId;
        SenderPhoto = issueMessage.Sender.Photo;
        SenderName = issueMessage.Sender.Name;
        Content = issueMessage.Content;
        Timestamp = issueMessage.Timestamp;
    }

    public long Id { get; set; }
    public long IssueReportId { get; set; }
    public long SenderId { get; set; }
    public byte[] SenderPhoto { get; set; }
    public string SenderName { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}