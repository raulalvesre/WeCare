using Microsoft.EntityFrameworkCore;
using WeCare.Application.Exceptions;
using WeCare.Application.Mappers;
using WeCare.Application.SearchParams;
using WeCare.Application.Validators;
using WeCare.Application.ViewModels;
using WeCare.Domain;
using WeCare.Domain.Core;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class InstitutionService
{
    private readonly InstitutionRepository _institutionRepository;
    private readonly UserRepository _userRepository;
    private readonly UnitOfWork _unitOfWork;
    private readonly InstitutionMapper _mapper;
    private readonly InstitutionAdminFormValidator _institutionAdminFormValidator;
    private readonly InstitutionFormValidator _institutionFormValidator;

    public InstitutionService(InstitutionRepository institutionRepository,
        UserRepository userRepository,
        InstitutionMapper mapper,
        UnitOfWork unitOfWork, 
        InstitutionAdminFormValidator institutionAdminFormValidator, 
        InstitutionFormValidator institutionFormValidator)
    {
        _institutionRepository = institutionRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _institutionAdminFormValidator = institutionAdminFormValidator;
        _institutionFormValidator = institutionFormValidator;
    }
    
     public async Task<InstitutionViewModel> GetById(long id)
    {
        var institution = await _institutionRepository.GetByIdAsync(id);

        if (institution is null)
            throw new NotFoundException("Instituição não encontrada");

        return _mapper.FromModel(institution);
    }

    public async Task<Pagination<InstitutionViewModel>> GetPage(InstitutionSearchParams searchParams)
    {
        var modelPage = await _institutionRepository.Paginate(searchParams);

        return new Pagination<InstitutionViewModel>(
            modelPage.PageNumber, 
            modelPage.PageSize,
            modelPage.TotalCount,
            modelPage.TotalPages,
            modelPage.Data.Select(x => _mapper.FromModel(x)));
    }

    public async Task<InstitutionViewModel> Save(InstitutionForm form)
    {
        var validationResult = await _institutionFormValidator.ValidateAsync(form);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var institution = _mapper.ToModel(form);

        await _institutionRepository.Save(institution);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(institution);
    }
    
    public async Task<InstitutionViewModel> Update(long institutionId, InstitutionForm form)
    {
        form.Id = institutionId;
        var validationResult = await _institutionFormValidator.ValidateAsync(form);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var institution = await _institutionRepository.GetByIdAsync(institutionId);
        if (institution is null)
            throw new NotFoundException("Instituição não encontrada");
        
        _mapper.Merge(institution, form);
        await _institutionRepository.Update(institution);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(institution);
    }

    public async Task<InstitutionViewModel> Save(InstitutionAdminForm form)
    {
        var validationResult = await _institutionAdminFormValidator.ValidateAsync(form);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);
        
        var institution = _mapper.ToModel(form);

        await _institutionRepository.Save(institution);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(institution);
    }

    public async Task<InstitutionViewModel> Update(long institutionId, InstitutionAdminForm form)
    {
        form.Id = institutionId;
        var validationResult = await _institutionAdminFormValidator.ValidateAsync(form);
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var institution = await _institutionRepository.GetByIdAsync(institutionId);
        if (institution is null)
            throw new NotFoundException("Instituição não encontrada");

        _mapper.Merge(institution, form);
        await _institutionRepository.Update(institution);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(institution);
    }

    public async Task Delete(long institutionId)
    {
        var institution = await _institutionRepository.GetByIdAsync(institutionId);
        if (institution is null)
            throw new NotFoundException("Instituição não encontrada");

        await _institutionRepository.Remove(institution);
        await _unitOfWork.SaveAsync();
    }

    public async Task<bool> IsCnpjAlreadyRegistered(string cnpj)
    {
        return await _institutionRepository.ExistsByIdNotAndCnpj(0, cnpj);
    }
    
    public async Task AddPhoto(long candidateId, ImageUploadForm form)
    {
        var institution = await _institutionRepository.GetByIdAsync(candidateId);
        if (institution is null)
            throw new NotFoundException("Instituição não encontrada");

        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        using var memoryStream = new MemoryStream();
        await form.Photo.CopyToAsync(memoryStream);
        
        institution.Photo = memoryStream.ToArray();
        await _unitOfWork.SaveAsync();
    }
    
    public async Task<UserCompleteViewModel> GetByEmailAndEnabled(string email)
    {
        var institution = await _institutionRepository.Query
            .FirstOrDefaultAsync(x => x.Email.Equals(email) && x.Enabled);

        if (institution is null)
            throw new NotFoundException("Instituição não encontrada");

        return new UserCompleteViewModel(institution);
    }
}