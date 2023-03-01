using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class OpportunityCauseMapping : IEntityTypeConfiguration<OpportunityCause>
{
    public void Configure(EntityTypeBuilder<OpportunityCause> builder)
    {
        builder.ToTable("opportunity_causes");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name);

        builder.HasData(Enumeration.GetAll<OpportunityCause>());
        
        builder.HasMany(x => x.VolunteerOpportunities)
            .WithMany(x => x.Causes);
    }
}