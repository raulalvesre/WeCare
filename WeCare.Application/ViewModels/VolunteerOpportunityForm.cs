// using FluentValidation.Results;
// using WeCare.Application.Validators;
// using WeCare.Domain;
//
// namespace WeCare.Application.ViewModels;
//
// public class VolunteerOpportunityForm
// {
//     public long? Id { get; set; }
//     public string Name { get; set; }
//     public string Description { get; set; }
//     public DateTime OpportunityDate { get; set; }
//     public AddressViewModel Address { get; set; }
//     // public IEnumerable<Qualification> RequiredQualifications { get; set; } = new List<Qualification>();
//     // public IEnumerable<OpportunityCause> Causes { get; set; } = new List<OpportunityCause>();
//     
//     public Task<ValidationResult> ValidateAsync()
//     {
//         return new VolunteerOpportunityFormValidator().ValidateAsync(this);
//     }
//     
//     public VolunteerOpportunity ToModel(long institutionId)
//     {
//         return new VolunteerOpportunity
//         {
//             Name = Name,
//             InstitutionId = institutionId,
//             Description = Description,
//             OpportunityDate = OpportunityDate,
//             Street = Address.Street,
//             Number = Address.Number,
//             Complement = Address.Complement,
//             City = Address.City,
//             Neighborhood = Address.Neighborhood,
//             State = Address.State,
//             PostalCode = Address.PostalCode,
//             RequiredQualifications = new List<Qualification>(),
//             Causes = new List<OpportunityCause>()
//         };
//     }
// }