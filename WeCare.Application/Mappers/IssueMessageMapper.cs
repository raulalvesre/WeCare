using WeCare.Application.ViewModels;
using WeCare.Domain.Models;

namespace WeCare.Application.Mappers;

public class IssueMessageMapper
{
    public IssueMessage ToModel(long issueId, IssueMessageForm form, long senderId)
    {
        return new IssueMessage
        {
            Content = form.Content,
            SenderId = senderId,
            IssueReportId = issueId
        };
    }
    
}