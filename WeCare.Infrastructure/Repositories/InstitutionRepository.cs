using Microsoft.EntityFrameworkCore;
using WeCare.Domain;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class InstitutionRepository : BaseRepository<Institution>
{
    private readonly WeCareDatabaseContext _databaseContext;

    public InstitutionRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
        this._databaseContext = weCareDatabaseContext;
    }

    public Task<Institution?> GetById(long id)
    {
        return _databaseContext
            .Institutions
            .AsNoTracking()
            .FirstOrDefaultAsync(institution => institution.Id == id);
    }

    public Task<bool> InstitutionExists(long id)
    {
        return Query
            .AnyAsync(x => x.Id == id);
    }
}