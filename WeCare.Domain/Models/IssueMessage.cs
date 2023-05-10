namespace WeCare.Domain.Models;

public class IssueMessage
{
    public long Id { get; set; }
    public long MessageThreadId { get; set; }
    public long SenderId { get; set; }
    public long RecipientId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }

    public User Sender { get; set; }
    public User Recipient { get; set; }
    public IssueMessageThread IssueMessageThread { get; set; }
}