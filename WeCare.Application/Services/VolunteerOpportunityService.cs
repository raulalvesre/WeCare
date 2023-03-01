using WeCare.Application.Exceptions;
using WeCare.Application.Mappers;
using WeCare.Application.ViewModels;
using WeCare.Domain.Models;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class VolunteerOpportunityService
{
    private readonly VolunteerOpportunityRepository _volunteerOpportunityRepository;
    private readonly InstitutionRepository _institutionRepository;
    private readonly VolunteerOpportunityMapper _mapper;
    private readonly UnitOfWork _unitOfWork;

    public VolunteerOpportunityService(VolunteerOpportunityRepository volunteerOpportunityRepository,
        InstitutionRepository institutionRepository,
        UnitOfWork unitOfWork, 
        VolunteerOpportunityMapper mapper)
    {
        _volunteerOpportunityRepository = volunteerOpportunityRepository;
        _institutionRepository = institutionRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<VolunteerOpportunityViewModel> Save(long institutionId, VolunteerOpportunityForm form)
    {
        var institution = _institutionRepository.GetByIdNoTracking(institutionId);
        if (institution is null)
            throw new NotFoundException("Instituição não encontrada");
        
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var opportunity = _mapper.ToModel(institutionId, form);
        await _volunteerOpportunityRepository.Add(opportunity);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(opportunity);
    }

}