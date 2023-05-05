using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class OpportunityCauseRepository : BaseRepository<OpportunityCause>
{
    public OpportunityCauseRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }

    public List<OpportunityCause> FindByCodeIn(IEnumerable<string> codes)
    {
        return Query
            .Where(x => codes.Contains(x.Code))
            .ToList();
    }
    
    public async Task<List<OpportunityCause>> FindByIdIn(IEnumerable<long> ids)
    {
        return await Query
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }
}