namespace WeCare.Domain;

public class Institution : User
{
    public string Cnpj { get; set; } = string.Empty;

    public IEnumerable<VolunteerOpportunity> VolunteerOpportunities { get; set; } = new List<VolunteerOpportunity>();
}