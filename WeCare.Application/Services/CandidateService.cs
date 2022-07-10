using WeCare.Application.SearchParams;
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

    public Task<Candidate?> GetById(long id) => _candidateRepository.GetById(id);

    public Task<Pagination<Candidate>> GetPage(CandidateSearchParams searchParams)
    {
        return _candidateRepository.Paginate(searchParams);
    }
}