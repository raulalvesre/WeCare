using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class IssueRepository : BaseRepository<IssueReport>
{
    public IssueRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }

    public async Task<IssueReport?> GetById(long id)
    {
        return await Query
            .Include(x => x.ReportedUser)
            .Include(x => x.Reporter)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}