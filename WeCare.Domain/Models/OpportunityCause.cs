namespace WeCare.Domain.Models;

public class OpportunityCause
{
    public OpportunityCauseId OpportunityCauseId { get; set; }
    public string Name { get; set; } = string.Empty;

    public IEnumerable<VolunteerOpportunity> VolunteerOpportunities { get; set; } = new List<VolunteerOpportunity>();
}