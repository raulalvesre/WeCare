using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class OpportunityInvitationRepository : BaseRepository<OpportunityInvitation>
{
    public OpportunityInvitationRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }

    public Task<OpportunityInvitation?> GetByIdAsync(long id)
    {
        return Query.Include(x => x.Candidate)
            .Include(x => x.Opportunity)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}