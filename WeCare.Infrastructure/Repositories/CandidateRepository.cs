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
    
    
    public Task<bool> ExistsByIdNotAndEmail(long id, string email)
    {
        return Query.AnyAsync(x => x.Id != id && x.Email.Equals(email));
    }
    
    public Task<bool> ExistsByEmail(string email)
    {
        return Query.AnyAsync(x => x.Email.Equals(email));
    }
    
    public Task<bool> ExistsByIdNotAndTelephone(long id, string telephone)
    {
        return Query.AnyAsync(x => x.Id != id && x.Telephone.Equals(telephone));
    }
    
    public Task<bool> ExistsByTelephone(string telephone)
    {
        return Query.AnyAsync(x => x.Telephone.Equals(telephone));
    }

    public Task<bool> ExistsByIdNotAndCpf(long id, string cpf)
    {
        return Query.AnyAsync(x => x.Id != id && x.Cpf.Equals(cpf));
    }
    
    public Task<bool> ExistsByCpf(string cpf)
    {
        return Query.AnyAsync(x => x.Cpf.Equals(cpf));
    }

    public Task<Candidate?> GetByIdAsync(long id)
    {
        return Query.Include(x => x.Qualifications)
            .Include(x => x.CausesCandidateIsInterestedIn)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}