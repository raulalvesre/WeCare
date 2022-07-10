using WeCare.Domain;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class VolunteerOpportunityRepository : BaseRepository<VolunteerOpportunity>
{
    public VolunteerOpportunityRepository(DatabaseContext databaseContext) : base(databaseContext)
    {
    }
}