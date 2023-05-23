using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class ParticipationCertificateRepository : BaseRepository<ParticipationCertificate>
{
    public ParticipationCertificateRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }
    
    public async ValueTask<IEnumerable<ParticipationCertificate>> GetByIdIn(IEnumerable<long> ids)
    {
        return await Query.Where(x => ids.Contains(x.Id)).ToListAsync();
    }
}