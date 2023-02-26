using WeCare.Application.Exceptions;
using WeCare.Application.Mappers;
using WeCare.Application.SearchParams;
using WeCare.Application.ViewModels;
using WeCare.Domain;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class InstitutionService
{
    private readonly InstitutionRepository _institutionRepository;
    private readonly UserRepository _userRepository;
    private readonly UnitOfWork _unitOfWork;
    private readonly InstitutionMapper _mapper;

    public InstitutionService(InstitutionRepository institutionRepository,
        UserRepository userRepository,
        InstitutionMapper mapper,
        UnitOfWork unitOfWork)
    {
        _institutionRepository = institutionRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
     public async Task<InstitutionViewModel> GetById(long id)
    {
        var institution = await _institutionRepository.GetById(id);

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
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        await ValidateUniqueFields(form.Email, form.Cnpj, form.Telephone);

        var institution = _mapper.ToModel(form);

        await _institutionRepository.Add(institution);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(institution);
    }
    
    private async Task ValidateUniqueFields(string email, string cnpj, string telephone, long existingInstitutionId = 0)
    {
        var errorMessages = new List<string>();

        if (await _userRepository.ExistsByIdNotAndEmail(existingInstitutionId, email))
            errorMessages.Add("Email já cadastrado");
        
        if (await _institutionRepository.ExistsByIdNotAndCnpj(existingInstitutionId, cnpj))
            errorMessages.Add("CNPJ já cadastrado");
        
        if (await _userRepository.ExistsByIdNotAndTelephone(existingInstitutionId, telephone))
            errorMessages.Add("Telefone já cadastrado");

        if (errorMessages.Any())
            throw new UnprocessableEntityException(errorMessages);
    }
    
    public async Task<InstitutionViewModel> Update(long institutionId, InstitutionForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var institution = await _institutionRepository.GetById(institutionId);
        if (institution is null)
            throw new NotFoundException("Instituição não encontrada");
        
        await ValidateUniqueFields(form.Email, form.Cnpj, form.Telephone, institutionId);

        _mapper.Merge(institution, form);
        await _institutionRepository.Update(institution);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(institution);
    }

    public async Task<InstitutionViewModel> Save(InstitutionAdminForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);
        
        await ValidateUniqueFields(form.Email, form.Cnpj, form.Telephone);

        var institution = _mapper.ToModel(form);

        await _institutionRepository.Add(institution);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(institution);
    }

    public async Task<InstitutionViewModel> Update(long institutionId, InstitutionAdminForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var institution = await _institutionRepository.GetById(institutionId);
        if (institution is null)
            throw new NotFoundException("Instituição não encontrada");
        
        await ValidateUniqueFields(form.Email, form.Cnpj, form.Telephone, institutionId);

        _mapper.Merge(institution, form);
        await _institutionRepository.Update(institution);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(institution);
    }

    public async Task Delete(long institutionId)
    {
        var institution = await _institutionRepository.GetById(institutionId);
        if (institution is null)
            throw new NotFoundException("Instituição não encontrada");

        await _institutionRepository.Remove(institution);
        await _unitOfWork.SaveAsync();
    }

    public async Task<bool> IsCnpjAlreadyRegistered(string cnpj)
    {
        return await _institutionRepository.ExistsByIdNotAndCnpj(0, cnpj);
    }
}