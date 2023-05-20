using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class QualificationRepository : BaseRepository<Qualification>
{
    public QualificationRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }

    public async Task<IEnumerable<Qualification>> FindByIdInAsync(IEnumerable<long> ids)
    {
        return await Query
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public IEnumerable<Qualification> FindByIdIn(IEnumerable<long> ids)
    {
        return Query
            .Where(x => ids.Contains(x.Id))
            .ToList();
    }
}