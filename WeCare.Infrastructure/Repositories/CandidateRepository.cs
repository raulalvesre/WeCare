using Microsoft.EntityFrameworkCore;
using WeCare.Domain;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class CandidateRepository : BaseRepository<Candidate>
{
    public CandidateRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }
    
    public Task<Candidate?> GetById(long id)
    {
        return Query
            .AsNoTracking()            
            .FirstOrDefaultAsync(candidate => candidate.Id == id);
    }
}