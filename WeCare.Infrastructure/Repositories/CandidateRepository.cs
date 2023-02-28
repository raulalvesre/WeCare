using Microsoft.EntityFrameworkCore;
using WeCare.Domain;
using WeCare.Domain.Models;
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
            .FirstOrDefaultAsync(candidate => candidate.Id == id);
    }

    public Task<bool> ExistsByIdNotAndCpf(long id, string cpf)
    {
        return Query.AnyAsync(x => x.Id != id && x.Cpf.Equals(cpf));
    }
    
}