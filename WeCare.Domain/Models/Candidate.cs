namespace WeCare.Domain.Models;

public class Candidate : User
{
    public string Cpf { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    
    public IEnumerable<CandidateQualification> Qualifications { get; set; } = new List<CandidateQualification>();
    
}