using Microsoft.EntityFrameworkCore;
using WeCare.Domain;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class InstitutionRepository : BaseRepository<Institution>
{
    public InstitutionRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }

    public Task<bool> InstitutionExists(long id)
    {
        return Query
            .AnyAsync(x => x.Id == id);
    }
}