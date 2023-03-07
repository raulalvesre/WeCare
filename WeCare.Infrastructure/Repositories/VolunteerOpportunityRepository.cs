using Microsoft.EntityFrameworkCore;
using WeCare.Domain.Models;
using WeCare.Infrastructure.Repositories.Base;

namespace WeCare.Infrastructure.Repositories;

public class VolunteerOpportunityRepository : BaseRepository<VolunteerOpportunity>
{
    public VolunteerOpportunityRepository(WeCareDatabaseContext weCareDatabaseContext) : base(weCareDatabaseContext)
    {
    }

    public override Task Add(VolunteerOpportunity record)
    {
        foreach (var opportunityCause in record.Causes)
            WeCareDatabaseContext.Attach(opportunityCause);

        return base.Add(record);
    }

    public Task<VolunteerOpportunity?> GetByIdIncludingCauses(long id)
    {
        return Query
            .Include(x => x.Causes)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}