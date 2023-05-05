using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class CandidateQualificationRepository : BaseRepository<CandidateQualification>
{
    public CandidateQualificationRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }

    public async Task<IEnumerable<CandidateQualification>> GetByIdIn(IEnumerable<long> ids)
    {
        return await Query
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

}