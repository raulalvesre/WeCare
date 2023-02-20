using WeCare.Application.Exceptions;
using WeCare.Application.ViewModels;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class VolunteerOpportunityService
{
    private readonly VolunteerOpportunityRepository _volunteerOpportunityRepository;
    private readonly InstitutionRepository _institutionRepository;
    private readonly UnitOfWork _unitOfWork;

    public VolunteerOpportunityService(VolunteerOpportunityRepository volunteerOpportunityRepository,
        InstitutionRepository institutionRepository,
        UnitOfWork unitOfWork)
    {
        _volunteerOpportunityRepository = volunteerOpportunityRepository;
        _institutionRepository = institutionRepository;
        _unitOfWork = unitOfWork;
    }

    //TODO resolver como q vai receber causas e requerimentos
    public async Task<VolunteerOpportunityResponse> Save(long institutionId, VolunteerOpportunityForm newOpportunity)
    {
        var validationResult = await newOpportunity.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var opportunityModel = newOpportunity.ToModel(institutionId);
        await _volunteerOpportunityRepository.Add(opportunityModel);
        await _unitOfWork.SaveAsync();

        return new VolunteerOpportunityResponse(opportunityModel);
    }

}