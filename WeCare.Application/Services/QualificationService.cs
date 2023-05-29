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

    public async Task<IEnumerable<QualificationViewModel>> GetAll()
    {
        var qualifications = await _qualificationRepository.Query.ToListAsync();
        return qualifications.Select(x => new QualificationViewModel(x));
    }

    public async Task<Pagination<QualificationViewModel>> GetPage(CandidateQualificationSearchParams searchParamses)
    {
        var qualifications = await _qualificationRepository.Paginate(searchParamses);
        return new Pagination<QualificationViewModel>(
            qualifications.PageNumber,
            qualifications.PageSize,
            qualifications.TotalCount,
            qualifications.TotalPages,
            qualifications.Data.Select(x => new QualificationViewModel(x))
        );
    }
}