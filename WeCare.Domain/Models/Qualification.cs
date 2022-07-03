namespace WeCare.Domain;

public class Qualification
{
    public long Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<Candidate> Candidates { get; set; }
    public IEnumerable<VolunteerOpportunity> VolunteerOpportunities { get; set; }
}