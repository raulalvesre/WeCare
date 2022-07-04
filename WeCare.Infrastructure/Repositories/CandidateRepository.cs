using Microsoft.EntityFrameworkCore;
using WeCare.Domain;

namespace WeCare.Infrastructure.Repositories;

public class CandidateRepository
{
    private readonly DatabaseContext _databaseContext;

    public CandidateRepository(DatabaseContext database)
    {
        this._databaseContext = database;
    }

    public Task<Candidate?> GetById(long id)
    {
        return _databaseContext.Candidates
            .Include(candidate => candidate.Address)
            .AsNoTracking()            
            .FirstOrDefaultAsync(candidate => candidate.Id == id);
    }
}