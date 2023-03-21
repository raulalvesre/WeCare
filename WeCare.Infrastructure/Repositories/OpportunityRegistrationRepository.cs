using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class OpportunityRegistrationRepository : BaseRepository<OpportunityRegistration>
{
    public OpportunityRegistrationRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }
}