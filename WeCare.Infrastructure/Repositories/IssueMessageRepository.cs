using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class IssueMessageRepository : BaseRepository<IssueMessage>
{
    public IssueMessageRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }

    public  Task<IssueMessage?> GetByIdAsync(long id)
    {
        return Query
            .Include(x => x.Sender)
            .Include(x => x.IssueReport)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}