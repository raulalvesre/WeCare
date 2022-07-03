namespace WeCare.Domain;

public class Candidate : User
{
    public string Cpf { get; set; }
    public DateTime BirthDate { get; set; }
    
    public IEnumerable<Qualification> Qualifications { get; set; }
}