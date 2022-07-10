using FluentValidation.Results;
using WeCare.Application.Validators;
using WeCare.Domain;

namespace WeCare.Application.ViewModels;

public class VolunteerOpportunityForm
{
    public long? Id { get; set; }
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
    
    public Task<ValidationResult> ValidateAsync()
    {
        return new VolunteerOpportunityFormValidator().ValidateAsync(this);
    }
    
    public VolunteerOpportunity ToModel(long institutionId)
    {
        return new VolunteerOpportunity
        {
            Name = Name,
            InstitutionId = institutionId,
            Description = Description,
            OpportunityDate = OpportunityDate,
            Street = AddressStreet,
            Number = AddressNumber,
            Complement = AddressComplement,
            City = AddressCity,
            Neighborhood = AddressNeighborhood,
            State = AddressState,
            PostalCode = AddressPostalCode,
            RequiredQualifications = RequiredQualifications,
            Causes = Causes
        };
    }
}