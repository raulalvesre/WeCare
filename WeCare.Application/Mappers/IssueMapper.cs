using WeCare.Application.ViewModels;
using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Application.Mappers;

public class IssueMapper
{
    public IssueReportViewModel FromModel(IssueReport model)
    {
        return new IssueReportViewModel
        {
            Id = model.Id,
            Status = model.Status,
            Title = model.Title,
            Description = model.Description,
            ResolutionNotes = model.ResolutionNotes,
            ReportedUserId = model.ReportedUserId,
            ReporterId = model.ReporterId,
            OpportunityId = model.OpportunityId,
            CreationDate = model.CreationDate,
        };
    }

    public IssueReport ToModel(IssueReportForm form, User reportedUser, User reporter, VolunteerOpportunity opportunity)
    {
        return new IssueReport
        {
            Status = IssueStatus.OPEN,
            Title = form.Title,
            Description = form.Description,
            ReportedUser = reportedUser,
            Reporter = reporter,
            Opportunity = opportunity
        };
    }

    public void Merge(IssueReport issueReport, IssueReportForm issueReportForm, User reportedUser, User reporter,  VolunteerOpportunity opportunity)
    {
        issueReport.Description = issueReportForm.Description;
        issueReport.ReportedUser = reportedUser;
        issueReport.Reporter = reporter;
        issueReport.Opportunity = opportunity;
    }
}