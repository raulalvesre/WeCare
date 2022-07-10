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

    public async Task<Candidate?> GetById(long id)
    {
        var candidate = await _candidateRepository.GetById(id);
        
        if (candidate is null)
            throw new NotFoundException("Candidato não encontrado");

        return candidate;
    }

    public Task<Pagination<Candidate>> GetPage(CandidateSearchParams searchParams)
    {
        return _candidateRepository.Paginate(searchParams);
    }

    public async Task<Candidate?> Save(CandidateForm form)
    {
        var validationResult = await form.ValidateAsync();
        if (!validationResult.IsValid)
            throw new BadRequestException(validationResult.Errors);

        var candidate = new Candidate
        {
            Name = form.Name,
            Email = form.Email
        };

        await _candidateRepository.Save(candidate);

        return candidate;
    }
}