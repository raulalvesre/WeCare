namespace WeCare.Domain;

public class Candidate
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; }
    public string Complement { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public State State { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
    
    public IEnumerable<Qualification> Qualifications { get; set; } = new List<Qualification>();
}