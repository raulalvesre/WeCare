using Microsoft.EntityFrameworkCore;
using WeCare.Application.Mappers;
using WeCare.Application.SearchParams;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class CandidateQualificationService
{
    private readonly CandidateQualificationRepository _candidateQualificationRepository;

    public CandidateQualificationService(CandidateQualificationRepository candidateQualificationRepository)
    {
        _candidateQualificationRepository = candidateQualificationRepository;
    }

    public async Task<IEnumerable<CandidateQualification>> GetAll()
    {
        return await _candidateQualificationRepository.Query.ToListAsync();
    }
    
    public async Task<Pagination<CandidateQualification>> GetPage(CandidateQualificationSearchParams searchParamses)
    {
        return await _candidateQualificationRepository.Paginate(searchParamses);
    }
}