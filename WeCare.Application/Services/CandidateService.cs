using Microsoft.EntityFrameworkCore;
using WeCare.Application.Exceptions;
using WeCare.Application.Interfaces;
using WeCare.Application.Mappers;
using WeCare.Application.SearchParams;
using WeCare.Application.Validators;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class CandidateService
{
    private readonly CandidateRepository _candidateRepository;
    private readonly CandidateQualificationRepository _candidateQualificationRepository;
    private readonly OpportunityCauseRepository _opportunityCauseRepository;
    private readonly UserRepository _userRepository;
    private readonly UnitOfWork _unitOfWork;
    private readonly CandidateMapper _mapper;
    private readonly CandidateAdminFormValidator _candidateAdminFormValidator;
    private readonly CandidateFormValidator _candidateFormValidator;
    private readonly ICurrentUser _currentUser;

    public CandidateService(CandidateRepository candidateRepository,
        CandidateMapper mapper,
        UserRepository userRepository,
        UnitOfWork unitOfWork, 
        CandidateAdminFormValidator candidateAdminFormValidator,
        CandidateFormValidator candidateFormValidator, ICurrentUser currentUser, 
        CandidateQualificationRepository candidateQualificationRepository, 
        OpportunityCauseRepository opportunityCauseRepository)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _candidateAdminFormValidator = candidateAdminFormValidator;
        _candidateFormValidator = candidateFormValidator;
        _currentUser = currentUser;
        _candidateQualificationRepository = candidateQualificationRepository;
        _opportunityCauseRepository = opportunityCauseRepository;
    }

    public async Task<CandidateViewModel> GetById(long id)
    {
        var candidate = await _candidateRepository.GetById(id);

        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");

        return _mapper.FromModel(candidate);
    }

    public async Task<Pagination<CandidateViewModel>> GetPage(CandidateSearchParams searchParams)
    {
        var modelPage = await _candidateRepository.Paginate(searchParams);

        return new Pagination<CandidateViewModel>(
            modelPage.PageNumber, 
            modelPage.PageSize,
            modelPage.TotalCount,
            modelPage.TotalPages,
            modelPage.Data.Select(x => _mapper.FromModel(x)));
    }

    public async Task<CandidateViewModel> Save(CandidateForm form)
    {
        var validationResult = await _candidateFormValidator.ValidateAsync(form);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var qualifications = await _candidateQualificationRepository.FindByIdIn(form.QualificationsIds);
        var interestedInCauses = await _opportunityCauseRepository.FindByIdIn(form.InterestedInCausesIds);

        var candidate = _mapper.ToModel(form, qualifications, interestedInCauses);

        await _candidateRepository.Save(candidate);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(candidate);
    }
    
    public async Task<CandidateViewModel> Update(long candidateId, CandidateForm form)
    {
        if (candidateId != _currentUser.GetUserId())
            throw new ForbiddenException("Vocẽ não ter permissão");
        
        form.Id = candidateId;
        var validationResult = await _candidateFormValidator.ValidateAsync(form);;
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);
        
        var candidate = await _candidateRepository.GetById(candidateId);
        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");
        
        var qualifications = await _candidateQualificationRepository.FindByIdIn(form.QualificationsIds);
        var interestedInCauses = await _opportunityCauseRepository.FindByIdIn(form.InterestedInCausesIds);

        _mapper.Merge(candidate, form, qualifications, interestedInCauses);
        await _candidateRepository.Update(candidate);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(candidate);
    }

    public async Task<CandidateViewModel> Save(CandidateAdminForm form)
    {
        var validationResult = await _candidateAdminFormValidator.ValidateAsync(form);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);
        
        var candidate = _mapper.ToModel(form);

        await _candidateRepository.Save(candidate);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(candidate);
    }

    public async Task<CandidateViewModel> Update(long candidateId, CandidateAdminForm form)
    {
        form.Id = candidateId;
        var validationResult = await _candidateAdminFormValidator.ValidateAsync(form);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var candidate = await _candidateRepository.GetById(candidateId);
        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");

        _mapper.Merge(candidate, form);
        await _candidateRepository.Update(candidate);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(candidate);
    }

    public async Task Delete(long candidateId)
    {
        if (!_currentUser.IsInRole("ADMIN") && candidateId != _currentUser.GetUserId())
            throw new ForbiddenException("Vocẽ não ter permissão");
        
        var candidate = await _candidateRepository.GetById(candidateId);
        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");

        await _candidateRepository.Remove(candidate);
        await _unitOfWork.SaveAsync();
    }

    public async Task<bool> IsCpfAlreadyRegistered(string cpf)
    {
        return await _candidateRepository.ExistsByIdNotAndCpf(0, cpf);
    }

    public async Task AddPhoto(long candidateId, ImageUploadForm form)
    {
        if (!_currentUser.IsInRole("ADMIN") && candidateId != _currentUser.GetUserId())
            throw new ForbiddenException("Vocẽ não ter permissão");
        
        var candidate = await _candidateRepository.GetById(candidateId);
        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");

        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        using var memoryStream = new MemoryStream();
        await form.Photo.CopyToAsync(memoryStream);
        
        candidate.Photo = memoryStream.ToArray();
        await _unitOfWork.SaveAsync();
    }

    public async Task<UserCompleteViewModel> GetByEmailAndEnabled(string email)
    {
        var candidate = await _candidateRepository.Query
            .FirstOrDefaultAsync(x => x.Email.Equals(email) && x.Enabled);

        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");

        return new UserCompleteViewModel(candidate);
    }
    
}