using WeCare.Domain;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class VolunteerOpportunityRepository : BaseRepository<VolunteerOpportunity>
{
    public VolunteerOpportunityRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }
}