using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class OpportunityInvitationRepository : BaseRepository<OpportunityInvitation>
{
    public OpportunityInvitationRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }
}