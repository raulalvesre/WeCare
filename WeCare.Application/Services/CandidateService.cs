using WeCare.Application.Exceptions;
using WeCare.Application.Mappers;
using WeCare.Application.SearchParams;
using WeCare.Application.Validators;
using WeCare.Application.ViewModels;
using WeCare.Domain;
using WeCare.Infrastructure;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class CandidateService
{
    private readonly CandidateRepository _candidateRepository;
    private readonly UserRepository _userRepository;
    private readonly UnitOfWork _unitOfWork;
    private readonly CandidateMapper _mapper;
    private readonly CandidateAdminFormValidator _candidateAdminFormValidator;
    private readonly CandidateFormValidator _candidateFormValidator;

    public CandidateService(CandidateRepository candidateRepository,
        CandidateMapper mapper,
        UserRepository userRepository,
        UnitOfWork unitOfWork, 
        CandidateAdminFormValidator candidateAdminFormValidator,
        CandidateFormValidator candidateFormValidator)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _candidateAdminFormValidator = candidateAdminFormValidator;
        _candidateFormValidator = candidateFormValidator;
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

        await ValidateUniqueFields(form.Email, form.Cpf, form.Telephone);

        var candidate = _mapper.ToModel(form);

        await _candidateRepository.Add(candidate);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(candidate);
    }
    
    private async Task ValidateUniqueFields(string email, string cpf, string telephone, long existingCandidateId = 0)
    {
        var errorMessages = new List<string>();

        if (await _userRepository.ExistsByIdNotAndEmail(existingCandidateId, email))
            errorMessages.Add("Email já cadastrado");
        
        if (await _candidateRepository.ExistsByIdNotAndCpf(existingCandidateId, cpf))
            errorMessages.Add("CPF já cadastrado");
        
        if (await _userRepository.ExistsByIdNotAndTelephone(existingCandidateId, telephone))
            errorMessages.Add("Telefone já cadastrado");

        if (errorMessages.Any())
            throw new UnprocessableEntityException(errorMessages);
    }
    
    public async Task<CandidateViewModel> Update(long candidateId, CandidateForm form)
    {
        var validationResult = await _candidateFormValidator.ValidateAsync(form);;
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var candidate = await _candidateRepository.GetById(candidateId);
        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");
        
        await ValidateUniqueFields(form.Email, form.Cpf, form.Telephone, candidateId);

        _mapper.Merge(candidate, form);
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

        await _candidateRepository.Add(candidate);
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
        
        await ValidateUniqueFields(form.Email, form.Cpf, form.Telephone, candidateId);

        _mapper.Merge(candidate, form);
        await _candidateRepository.Update(candidate);
        await _unitOfWork.SaveAsync();

        return _mapper.FromModel(candidate);
    }

    public async Task Delete(long candidateId)
    {
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
    
}