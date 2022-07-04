namespace WeCare.Domain;

public class OpportunityCause
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public IEnumerable<VolunteerOpportunity> VolunteerOpportunities { get; set; } = new List<VolunteerOpportunity>();
}