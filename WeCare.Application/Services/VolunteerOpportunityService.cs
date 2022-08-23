using WeCare.Application.Exceptions;
using WeCare.Application.ViewModels;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class VolunteerOpportunityService
{
    private readonly VolunteerOpportunityRepository _volunteerOpportunityRepository;
    private readonly InstitutionRepository _institutionRepository;

    public VolunteerOpportunityService(VolunteerOpportunityRepository volunteerOpportunityRepository, InstitutionRepository institutionRepository)
    {
        _volunteerOpportunityRepository = volunteerOpportunityRepository;
        _institutionRepository = institutionRepository;
    }

    //TODO resolver como q vai receber causas e requerimentos
    public async Task<VolunteerOpportunityResponse> Save(long institutionId, VolunteerOpportunityForm newOpportunity)
    {
        var validationResult = await newOpportunity.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var opportunityModel = newOpportunity.ToModel(institutionId);
        await _volunteerOpportunityRepository.Save(opportunityModel);

        return new VolunteerOpportunityResponse(opportunityModel);
    }

}