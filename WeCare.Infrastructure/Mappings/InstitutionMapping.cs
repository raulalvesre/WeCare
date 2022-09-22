using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain;

namespace WeCare.Infrastructure.Mappings;

public class InstitutionMapping : IEntityTypeConfiguration<Institution>
{
    public void Configure(EntityTypeBuilder<Institution> builder)
    {
        builder.Property(x => x.Cnpj);
        
        builder.HasMany(x => x.VolunteerOpportunities)
            .WithOne(x => x.Institution)
            .HasForeignKey(x => x.InstitutionId);
    }
}