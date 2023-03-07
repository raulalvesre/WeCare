using WeCare.Application.Exceptions;
using WeCare.Application.Mappers;
using WeCare.Application.SearchParams;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;
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
    
    public async Task<VolunteerOpportunityViewModel> GetById(long id)
    {
        var opportunity = await _volunteerOpportunityRepository.GetByIdIncludingCauses(id);
        if (opportunity is null)
            throw new NotFoundException("Oportunidade não encontrada");

        return _mapper.FromModel(opportunity);
    }
    
    public async Task<Pagination<VolunteerOpportunityViewModel>> GetPage(VolunteerOpportunitySearchParam searchParams)
    {
        var opportunitiesPage = await _volunteerOpportunityRepository.Paginate(searchParams);
        return new Pagination<VolunteerOpportunityViewModel>(
            opportunitiesPage.PageNumber, 
            opportunitiesPage.PageSize,
            opportunitiesPage.TotalCount,
            opportunitiesPage.TotalPages,
            opportunitiesPage.Data.Select(x => _mapper.FromModel(x)));
    }

    public async Task<VolunteerOpportunityViewModel> Save(long institutionId, VolunteerOpportunityForm form)
    {
        var institution = await _institutionRepository.GetByIdNoTracking(institutionId);
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


    public async Task<VolunteerOpportunityViewModel> Update(long institutionId, long opportunityId, VolunteerOpportunityForm form)
    {
        var institution = await _institutionRepository.GetByIdNoTracking(institutionId);
        if (institution is null)
            throw new NotFoundException("Instituição não encontrada");
        
        var opportunity = await _volunteerOpportunityRepository.GetByIdIncludingCauses(opportunityId);
        if (opportunity is null)
            throw new NotFoundException("Oportunidade não encontrada");
        
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        _mapper.Merge(opportunity, form);
        await _volunteerOpportunityRepository.Update(opportunity);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(opportunity);
    }

    public async Task Delete(long institutionId, long opportunityId)
    {
        var institution = await _institutionRepository.GetByIdNoTracking(institutionId);
        if (institution is null)
            throw new NotFoundException("Instituição não encontrada");
        
        var opportunity = await _volunteerOpportunityRepository.GetByIdIncludingCauses(opportunityId);
        if (opportunity is null)
            throw new NotFoundException("Oportunidade não encontrada");

        await _volunteerOpportunityRepository.Remove(opportunity);
        await _unitOfWork.SaveAsync();
    }
}