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
    
    public Task<bool> ExistsByIdNotAndTelephone(long id, string telephone)
    {
        return Query.AnyAsync(x => x.Id != id && x.Telephone.Equals(telephone));
    }

    public Task<bool> ExistsByIdNotAndCpf(long id, string cpf)
    {
        return Query.AnyAsync(x => x.Id != id && x.Cpf.Equals(cpf));
    }
}