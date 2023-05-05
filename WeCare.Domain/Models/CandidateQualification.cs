namespace WeCare.Domain.Models;

public class CandidateQualification
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public IEnumerable<Candidate> Candidates { get; set; } = new List<Candidate>();
}