namespace WeCare.Domain;

public class Qualification
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<Candidate> Candidates { get; set; } = new List<Candidate>();
    public IEnumerable<VolunteerOpportunity> VolunteerOpportunities { get; set; } = new List<VolunteerOpportunity>();
}
