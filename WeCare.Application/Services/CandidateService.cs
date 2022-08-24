using System.ComponentModel.DataAnnotations.Schema;
using WeCare.Application.Exceptions;
using WeCare.Application.SearchParams;
using WeCare.Application.ViewModels;
using WeCare.Domain;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class CandidateService
{
    private readonly CandidateRepository _candidateRepository;

    public CandidateService(CandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<CandidateViewModel> GetById(long id)
    {
        var candidate = await _candidateRepository.GetById(id);

        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");

        return new CandidateViewModel(candidate);
    }

    public async Task<Pagination<CandidateViewModel>> GetPage(CandidateSearchParams searchParams)
    {
        var modelPage = await _candidateRepository.Paginate(searchParams);

        return new Pagination<CandidateViewModel>(
            modelPage.PageNumber, 
            modelPage.PageSize,
            modelPage.TotalCount,
            modelPage.TotalPages,
            modelPage.Data.Select(x => new CandidateViewModel(x)));
    }

    public async Task<CandidateViewModel> Save(CandidateForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var candidate = new Candidate
        {
            Email = form.Email,
            Name = form.Name,
        };

        await _candidateRepository.Save(candidate);

        return new CandidateViewModel(candidate);
    }

    public async Task<CandidateViewModel> Save(CandidateAdminForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var candidate = form.toModel();

        await _candidateRepository.Save(candidate);

        return new CandidateViewModel(candidate);
    }

    public async Task<CandidateViewModel> Update(long candidateId, CandidateAdminForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var candidate = await _candidateRepository.GetById(candidateId);
        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");

        candidate = form.toModel();
        await _candidateRepository.Update(candidate);

        return new CandidateViewModel(candidate);
    }

    public async Task Remove(long candidateId)
    {
        var candidate = await _candidateRepository.GetById(candidateId);
        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");

        await _candidateRepository.Remove(candidate);
    }
    
}