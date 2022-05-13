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

    public Candidate GetById(int id)
    {
        Candidate? candidate = _candidateRepository.Query
            .FirstOrDefault(x => x.Id == id);

        if (candidate == null)
            throw new Exception("Candidate not found!");

        return candidate;
    }
}