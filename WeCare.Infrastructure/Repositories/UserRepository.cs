using Microsoft.EntityFrameworkCore;
using WeCare.Domain;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>
{
    public UserRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }
    
    public Task<User?> GetById(long id)
    {
        return Query
            .AsNoTracking()            
            .FirstOrDefaultAsync(user => user.Id == id);
    }
    
    public Task<User?> GetByEmail(string email)
    {
        return Query
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Equals(email));
    }
    
    public Task<bool> ExistsByIdNotAndEmail(long id, string email)
    {
        return Query.AnyAsync(x => x.Id != id && x.Email.Equals(email));
    }
    
    public Task<bool> ExistsByIdNotAndTelephone(long id, string telephone)
    {
        return Query.AnyAsync(x => x.Id != id && x.Telephone.Equals(telephone));
    }
    
}