using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class OpportunityCauseMapping : IEntityTypeConfiguration<OpportunityCause>
{
    public void Configure(EntityTypeBuilder<OpportunityCause> builder)
    {
        builder.ToTable("opportunity_causes");

        builder.Property(x => x.OpportunityCauseId)
            .HasConversion<int>();

        builder.HasData(
            Enum.GetValues(typeof(OpportunityCauseId))
                .Cast<OpportunityCauseId>()
                .Select(x => new OpportunityCause()
                {
                    OpportunityCauseId = x,
                    Name = x.ToString()
                })
        );
        
        builder.HasMany(x => x.VolunteerOpportunities)
            .WithMany(x => x.Causes);
    }
}