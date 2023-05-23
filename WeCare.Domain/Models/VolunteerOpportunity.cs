using WeCare.Domain.Core;

namespace WeCare.Domain.Models;

public class VolunteerOpportunity
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime OpportunityDate { get; set; } //TODO change name to only Date
    public byte[] Photo { get; set; }
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public State State { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime? LastUpdateDate { get; set; }
    public bool Enabled { get; set; }

    public long InstitutionId { get; set; }
    public Institution? Institution { get; set; }
    public IEnumerable<OpportunityCause> Causes { get; set; } = new List<OpportunityCause>();
    public IEnumerable<OpportunityRegistration> Registrations { get; set; } = new List<OpportunityRegistration>();
    public IEnumerable<Qualification> DesirableQualifications { get; set; } = new List<Qualification>();

    public bool HasAlreadyHappened() => OpportunityDate <= DateTime.Now;
}

