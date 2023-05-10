using WeCare.Domain.Core;

namespace WeCare.Domain.Models;

public class IssueReport
{
    public long Id { get; set; }
    public IssueStatus Status { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ResolutionNotes { get; set; }
    public long ReportedUserId { get; set; }
    public long ReporterId { get; set; }
    public long OpportunityId { get; set; }
    public DateTime? ResolutionDate { get; set; }
    public DateTime CreationDate { get; set; }

    public User ReportedUser { get; set; }
    public User Reporter { get; set; }
    public VolunteerOpportunity Opportunity { get; set; }
    public IssueMessageThread MessageThread { get; set; }
}