using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class CandidateService
{

    private readonly CandidateRepository _candidateRepository;

    public CandidateService(CandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

}