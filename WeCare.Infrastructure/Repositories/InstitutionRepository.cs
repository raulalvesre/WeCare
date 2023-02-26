using Microsoft.EntityFrameworkCore;
using WeCare.Domain;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class InstitutionRepository : BaseRepository<Institution>
{
    public InstitutionRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }

    public Task<bool> InstitutionExists(long id)
    {
        return Query
            .AnyAsync(x => x.Id == id);
    }
    
    public Task<Institution?> GetById(long id)
    {
        return Query
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsByIdNotAndCnpj(long id, string cnpj)
    {
        return await Query.AnyAsync(x => x.Id != id && x.Cnpj.Equals(cnpj));
    }
    
}