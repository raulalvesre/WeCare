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
    
    public Task<bool> ExistsByIdNotAndEmail(long id, string email)
    {
        return Query.AnyAsync(x => x.Id != id && x.Email.Equals(email));
    }
    
    public Task<bool> ExistsByIdNotAndTelephone(long id, string telephone)
    {
        return Query.AnyAsync(x => x.Id != id && x.Telephone.Equals(telephone));
    }
    
    public Task<Institution?> GetByIdNoTracking(long id)
    {
        return Query
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsByIdNotAndCnpj(long id, string cnpj)
    {
        return await Query.AnyAsync(x => x.Id != id && x.Cnpj.Equals(cnpj));
    }
    
}