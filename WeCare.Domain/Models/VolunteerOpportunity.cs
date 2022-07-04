namespace WeCare.Domain;

public class VolunteerOpportunity
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime OpportunityDate { get; set; }
    public Address? Address { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdateDate { get; set; }

    public long InstitutionId { get; set; }
    public Institution? Institution { get; set; }

    public IEnumerable<Qualification> RequiredQualifications { get; set; } = new List<Qualification>();
    public IEnumerable<OpportunityCause> Causes { get; set; } = new List<OpportunityCause>();
}

