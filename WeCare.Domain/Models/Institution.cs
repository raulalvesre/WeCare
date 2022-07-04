namespace WeCare.Domain;

public class Institution
{
    public long Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public Address? Address { get; set; }
    public string Cnpj { get; set; } = string.Empty;
    public IEnumerable<VolunteerOpportunity> VolunteerOpportunities { get; set; } = new List<VolunteerOpportunity>();
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
}