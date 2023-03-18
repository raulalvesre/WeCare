using Microsoft.EntityFrameworkCore;
using WeCare.Domain;
using WeCare.Domain.Core;
using WeCare.Domain.Models;
using WeCare.Infrastructure;

namespace WeCare.Application.SearchParams;

public class CandidateSearchParams : PaginationFilterParamsBase<Candidate>
{
    public long? Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? City { get; set; }
    public State? State { get; set; }
    
    protected override void Filter()
    {
        if (Id.HasValue)
            And(x => x.Id == Id);
        
        if (!string.IsNullOrEmpty(Email))
            And(x => EF.Functions.Like(x.Email, $"%{Email}%"));
        
        if (!string.IsNullOrEmpty(Name))
            And(x => EF.Functions.Like(x.Name, $"%{Name}%"));
        
        if (!string.IsNullOrEmpty(City))
            And(x => EF.Functions.Like(x.City, $"%{City}%"));

        if (State.HasValue)
            And(x => x.State == State);
    }
}