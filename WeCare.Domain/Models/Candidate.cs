namespace WeCare.Domain;

public class Candidate
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public long AddressId { get; set; }
    public Address? Address { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public IEnumerable<Qualification> Qualifications { get; set; } = new List<Qualification>();
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}