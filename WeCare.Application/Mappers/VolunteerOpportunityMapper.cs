using WeCare.Application.ViewModels;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Mappers;

public class VolunteerOpportunityMapper
{
    public VolunteerOpportunityViewModel FromModel(VolunteerOpportunity opportunity)
    {
        return new VolunteerOpportunityViewModel()
        {
            Id = opportunity.Id,
            InstitutionId = opportunity.InstitutionId,
            Name = opportunity.Name,
            Description = opportunity.Description,
            OpportunityDate = opportunity.OpportunityDate,
            Photo = Convert.ToBase64String(opportunity.Photo),
            Address = new AddressViewModel(opportunity),
            Causes = opportunity.Causes.Select(x => x.Name),
            CreationDate = opportunity.CreationDate
        };
    }
    
    public VolunteerOpportunity ToModel(long institutionId, VolunteerOpportunityForm form)
    {
        var causes = form.Causes
            .Select(x => Enumeration.FromDisplayName<OpportunityCause>(x))
            .ToList();
        
        using var memoryStream = new MemoryStream();
        form.Photo.Photo.CopyTo(memoryStream);
        
        return new VolunteerOpportunity()
        {
            Name = form.Name,
            Description = form.Description,
            OpportunityDate = form.OpportunityDate,
            Photo = memoryStream.ToArray(),
            Street = form.Address.Street,
            Number =  form.Address.Number,
            Complement =  form.Address.Complement,
            City =  form.Address.City,
            Neighborhood =  form.Address.Neighborhood,
            State =  form.Address.State,
            PostalCode =  form.Address.PostalCode,
            Causes = causes,
            InstitutionId = institutionId
        };
    }
}