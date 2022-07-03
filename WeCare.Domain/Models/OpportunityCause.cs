namespace WeCare.Domain;

public class OpportunityCause
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public IEnumerable<VolunteerOpportunity> VolunteerOpportunities { get; set; }
}