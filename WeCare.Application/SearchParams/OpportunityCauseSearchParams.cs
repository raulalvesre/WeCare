using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.SearchParams;

public class OpportunityCauseSearchParams : PaginationFilterParamsBase<OpportunityCause>
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    
    protected override void Filter()
    {
        if (!string.IsNullOrEmpty(Code))
            And(x => EF.Functions.Like(x.Code, $"%{Code}%"));
        
        if (!string.IsNullOrEmpty(Name))
            And(x => EF.Functions.Like(x.Name, $"%{Name}%"));
    }
}