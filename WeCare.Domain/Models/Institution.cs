namespace WeCare.Domain;

public class Institution : User
{
    public string Cnpj { get; set; }
    public IEnumerable<VolunteerOpportunity> VolunteerOpportunities { get; set; }

}