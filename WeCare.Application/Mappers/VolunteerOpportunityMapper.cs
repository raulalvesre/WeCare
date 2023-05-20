using WeCare.Application.ViewModels;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Mappers;

public class VolunteerOpportunityMapper
{

    private readonly OpportunityCauseRepository _opportunityCauseRepository;
    private readonly QualificationRepository _qualificationRepository;
    private readonly InstitutionMapper _institutionMapper;

    public VolunteerOpportunityMapper(OpportunityCauseRepository opportunityCauseRepository,
        InstitutionMapper institutionMapper,
        QualificationRepository qualificationRepository)
    {
        _opportunityCauseRepository = opportunityCauseRepository;
        _institutionMapper = institutionMapper;
        _qualificationRepository = qualificationRepository;
    }

    public VolunteerOpportunityViewModel FromModel(VolunteerOpportunity opportunity)
    {
        return new VolunteerOpportunityViewModel()
        {
            Id = opportunity.Id,
            InstitutionId = opportunity.InstitutionId,
            Name = opportunity.Name,
            Description = opportunity.Description,
            OpportunityDate = opportunity.OpportunityDate,
            Photo = opportunity.Photo,
            Address = new AddressViewModel(opportunity),
            Causes = opportunity.Causes.Select(x => x.Name),
            DesirableQualifications = opportunity.DesirableQualifications.Select(x => x.Name),
            CreationDate = opportunity.CreationDate
        };
    }
    
    public OpportunityForRegistrationViewModel FromModelToCandidateRegistrationViewModel(VolunteerOpportunity opportunity)
    {
        return new OpportunityForRegistrationViewModel()
        {
            Id = opportunity.Id,
            Name = opportunity.Name,
            Description = opportunity.Description,
            OpportunityDate = opportunity.OpportunityDate,
            Photo = opportunity.Photo,
            Address = new SecretativeAddressViewModel(opportunity),
            Causes = opportunity.Causes.Select(x => x.Name),
            DesirableQualifications = opportunity.DesirableQualifications.Select(x => x.Name),
            Institution = _institutionMapper.FromModelToCandidateRegistrationViewModel(opportunity.Institution)
        };
    }
    
    public OpportunityForAcceptedRegistrationViewModel FromModelToOpportunityForAcceptedRegistrationViewModel(VolunteerOpportunity opportunity)
    {
        return new OpportunityForAcceptedRegistrationViewModel()
        {
            Id = opportunity.Id,
            Name = opportunity.Name,
            Description = opportunity.Description,
            OpportunityDate = opportunity.OpportunityDate,
            Photo = opportunity.Photo,
            Address = new AddressViewModel(opportunity),
            Causes = opportunity.Causes.Select(x => x.Name),
            DesirableQualifications = opportunity.DesirableQualifications.Select(x => x.Name),
            Institution = _institutionMapper.FromModelToCandidateRegistrationViewModel(opportunity.Institution)
        };
    }

    public VolunteerOpportunity ToModel(long institutionId, VolunteerOpportunityForm form)
    {
        var causes = _opportunityCauseRepository.FindByCodeIn(form.Causes);
        var qualifications = _qualificationRepository.FindByIdIn(form.DesirableQualificationsIds);

        using var memoryStream = new MemoryStream();
        form.Photo.CopyTo(memoryStream);
        
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
            DesirableQualifications = qualifications,
            InstitutionId = institutionId
        };
    }

    public void Merge(VolunteerOpportunity opportunity, VolunteerOpportunityForm form)
    {
        var causes = _opportunityCauseRepository.FindByCodeIn(form.Causes);
        var qualifications = _qualificationRepository.FindByIdIn(form.DesirableQualificationsIds);

        using var memoryStream = new MemoryStream();
        form.Photo.CopyTo(memoryStream);

        opportunity.Name = form.Name;
        opportunity.Description = form.Description;
        opportunity.OpportunityDate = form.OpportunityDate;
        opportunity.Photo = memoryStream.ToArray();
        opportunity.Street = form.Address.Street;
        opportunity.Number = form.Address.Number;
        opportunity.Complement = form.Address.Complement;
        opportunity.City = form.Address.City;
        opportunity.Neighborhood = form.Address.Neighborhood;
        opportunity.State = form.Address.State;
        opportunity.PostalCode = form.Address.PostalCode;
        opportunity.Causes = causes;
        opportunity.DesirableQualifications = qualifications;
    }
    
}