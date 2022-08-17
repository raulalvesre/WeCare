using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WeCare.Domain;

namespace WeCare.Application.ViewModels;

public class VolunteerOpportunityResponse
{
    public long Id { get; set; }
    public long InstitutionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime OpportunityDate { get; set; }
    public string AddressStreet { get; set; }
    public string AddressNumber { get; set; }
    public string AddressComplement { get; set; }
    public string AddressCity { get; set; }
    public string AddressNeighborhood { get; set; } 
    public State AddressState { get; set; }
    public string AddressPostalCode { get; set; }
    public IEnumerable<Qualification> RequiredQualifications { get; set; } = new List<Qualification>();
    public IEnumerable<OpportunityCause> Causes { get; set; } = new List<OpportunityCause>();

    public VolunteerOpportunityResponse(VolunteerOpportunity model)
    {
        Id = model.Id;
        InstitutionId = model.InstitutionId;
        Name = model.Description;
        Description = model.Description;
        OpportunityDate = model.OpportunityDate;
        AddressStreet = model.Street;
        AddressNumber = model.Number;
        AddressComplement = model.Complement;
        AddressCity = model.City;
        AddressNeighborhood = model.Neighborhood;
        AddressState = model.State;
        AddressPostalCode = model.PostalCode;
        RequiredQualifications = model.RequiredQualifications;
        Causes = model.Causes;
    }
}