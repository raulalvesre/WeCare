namespace WeCare.Domain;

public class VolunteerOpportunity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly OpportunityDate { get; set; }
    public string Address { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string City { get; set; }
    public string Neighborhood { get; set; }
    public string State { get; set; }
    public string Cep { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }


    public long InstitutionId { get; set; }
    public Institution Institution { get; set; }
    
    public IEnumerable<Qualification> RequiredQualifications { get; set; }
    public IEnumerable<OpportunityCause> Causes { get; set; }
}
    
