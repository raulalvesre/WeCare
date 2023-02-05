using WeCare.Application.Exceptions;
using WeCare.Application.Mappers;
using WeCare.Application.SearchParams;
using WeCare.Application.ViewModels;
using WeCare.Domain;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class CandidateService
{
    private readonly CandidateRepository _candidateRepository;
    private readonly CandidateMapper _mapper;

    public CandidateService(CandidateRepository candidateRepository, CandidateMapper mapper)
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;
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
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        await ValidateUniqueFields(form);

        var candidate = _mapper.ToModel(form);

        await _candidateRepository.Save(candidate);

        return _mapper.FromModel(candidate);
    }

    private async Task ValidateUniqueFields(CandidateForm form)
    {
        await ValidateUniqueFields(0L, form);
    }

    private async Task ValidateUniqueFields(long existingCandidateId, CandidateForm form)
    {
        var errorMessages = new List<string>();

        var email = form.Email;
        var cpf = form.Cpf;
        var telephone = form.Telephone;

        if (await _candidateRepository.ExistsByIdNotAndEmail(existingCandidateId, email))
            errorMessages.Add("Email já cadastrado");
        
        if (await _candidateRepository.ExistsByIdNotAndCpf(existingCandidateId, cpf))
            errorMessages.Add("CPF já cadastrado");
        
        if (await _candidateRepository.ExistsByIdNotAndTelephone(existingCandidateId, telephone))
            errorMessages.Add("Telefone já cadastrado");

        if (errorMessages.Any())
            throw new UnprocessableEntityException(errorMessages);
    }
    
    public async Task<CandidateViewModel> Update(long candidateId, CandidateForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var candidate = await _candidateRepository.GetById(candidateId);
        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");
        
        await ValidateUniqueFields(candidateId, form);

        _mapper.Merge(candidate, form);
        await _candidateRepository.Update(candidate);

        return _mapper.FromModel(candidate);
    }

    public async Task<CandidateViewModel> Save(CandidateAdminForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);
        
        await ValidateUniqueFields(form);

        var candidate = _mapper.ToModel(form);

        await _candidateRepository.Save(candidate);

        return _mapper.FromModel(candidate);
    }
    
    private async Task ValidateUniqueFields(CandidateAdminForm form)
    {
        await ValidateUniqueFields(0L, form);
    }

    private async Task ValidateUniqueFields(long existingCandidateId, CandidateAdminForm form)
    {
        var errorMessages = new List<string>();

        var email = form.Email;
        var cpf = form.Cpf;
        var telephone = form.Telephone;

        if (await _candidateRepository.ExistsByIdNotAndEmail(existingCandidateId, email))
            errorMessages.Add("Email já cadastrado");
        
        if (await _candidateRepository.ExistsByIdNotAndCpf(existingCandidateId, cpf))
            errorMessages.Add("CPF já cadastrado");
        
        if (await _candidateRepository.ExistsByIdNotAndTelephone(existingCandidateId, telephone))
            errorMessages.Add("Telefone já cadastrado");

        if (errorMessages.Any())
            throw new UnprocessableEntityException(errorMessages);
    }
    
    
    public async Task<CandidateViewModel> Update(long candidateId, CandidateAdminForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var candidate = await _candidateRepository.GetById(candidateId);
        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");
        
        await ValidateUniqueFields(candidateId, form);

        _mapper.Merge(candidate, form);
        await _candidateRepository.Update(candidate);

        return _mapper.FromModel(candidate);
    }

    public async Task Remove(long candidateId)
    {
        var candidate = await _candidateRepository.GetById(candidateId);
        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");

        await _candidateRepository.Remove(candidate);
    }
    
}