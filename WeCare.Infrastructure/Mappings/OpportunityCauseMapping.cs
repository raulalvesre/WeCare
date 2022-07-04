using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain;

namespace WeCare.Infrastructure.Mappings;

public class OpportunityCauseMapping : IEntityTypeConfiguration<OpportunityCause>
{
    public void Configure(EntityTypeBuilder<OpportunityCause> builder)
    {
        builder.ToTable("opportunities_causes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired();
    }
}