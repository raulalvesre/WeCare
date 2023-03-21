using Microsoft.EntityFrameworkCore;
using WeCare.Application.Mappers;
using WeCare.Application.SearchParams;
using WeCare.Application.ViewModels;
using WeCare.Domain.Core;
using WeCare.Infrastructure.Repositories;

namespace WeCare.Application.Services;

public class OpportunityCauseService
{
    private readonly OpportunityCauseRepository _opportunityCauseRepository;
    private readonly OpportunityCauseMapper _mapper;

    public OpportunityCauseService(OpportunityCauseRepository opportunityCauseRepository, OpportunityCauseMapper mapper)
    {
        _opportunityCauseRepository = opportunityCauseRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OpportunityCauseViewModel>> GetAll()
    {
        var causes = await _opportunityCauseRepository.Query.ToListAsync();
        return causes.Select(x => _mapper.FromModel(x));
    }
    
    public async Task<Pagination<OpportunityCauseViewModel>> GetPage(OpportunityCauseSearchParams searchParamses)
    {
        var causesPage = await _opportunityCauseRepository.Paginate(searchParamses);
        return new Pagination<OpportunityCauseViewModel>(
            causesPage.PageNumber, 
            causesPage.PageSize,
            causesPage.TotalCount,
            causesPage.TotalPages,
            causesPage.Data.Select(x => _mapper.FromModel(x)));
    }

}