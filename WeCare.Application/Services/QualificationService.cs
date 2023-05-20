using Microsoft.EntityFrameworkCore;
using WeCare.Application.Mappers;
using WeCare.Application.SearchParams;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class QualificationService
{
    private readonly QualificationRepository _qualificationRepository;

    public QualificationService(QualificationRepository qualificationRepository)
    {
        _qualificationRepository = qualificationRepository;
    }

    public async Task<IEnumerable<Qualification>> GetAll()
    {
        return await _qualificationRepository.Query.ToListAsync();
    }
    
    public async Task<Pagination<Qualification>> GetPage(CandidateQualificationSearchParams searchParamses)
    {
        return await _qualificationRepository.Paginate(searchParamses);
    }
}