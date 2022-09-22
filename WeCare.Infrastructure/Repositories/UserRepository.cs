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
}