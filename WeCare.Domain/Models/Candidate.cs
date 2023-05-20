namespace WeCare.Domain.Models;

public class Candidate : User
{
    public string Cpf { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    
    public IEnumerable<Qualification> Qualifications { get; set; } = new List<Qualification>();
    public IEnumerable<OpportunityCause> CausesCandidateIsInterestedIn { get; set; } = new List<OpportunityCause>();
    
}