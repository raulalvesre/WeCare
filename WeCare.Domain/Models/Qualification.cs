namespace WeCare.Domain.Models;

public class Qualification
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public IEnumerable<Candidate> Candidates { get; set; } = new List<Candidate>();
    public IEnumerable<VolunteerOpportunity> Opportunities { get; set; } = new List<VolunteerOpportunity>();
    public IEnumerable<ParticipationCertificate> ParticipationCertificates { get; set; } = new List<ParticipationCertificate>();
}