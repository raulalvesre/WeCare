using Microsoft.EntityFrameworkCore;
using WeCare.Domain;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class ConfirmationTokenRepository : BaseRepository<ConfirmationToken>
{
    
    public ConfirmationTokenRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }
    
    public Task<ConfirmationToken?> GetByToken(string token)
    {
        return Query
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Token.Equals(token));
    }
    
}