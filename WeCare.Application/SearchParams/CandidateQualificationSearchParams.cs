using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.SearchParams;

public class CandidateQualificationSearchParams : PaginationFilterParamsBase<CandidateQualification>
{
    public string Name { get; set; }
    
    protected override void Filter()
    {
        if (string.IsNullOrEmpty(Name))
            And(x => EF.Functions.Like(x.Name, $"%{Name}%"));
    }
}