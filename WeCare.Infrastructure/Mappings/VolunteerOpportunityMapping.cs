using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain;

namespace WeCare.Infrastructure.Mappings;

public class VolunteerOpportunityMapping : IEntityTypeConfiguration<VolunteerOpportunity>
{
    
    public void Configure(EntityTypeBuilder<VolunteerOpportunity> builder)
    {
        builder.ToTable("volunteer_opportunity");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Address)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.OpportunityDate)
            .IsRequired();
        
        builder.Property(x => x.CreationDate)
            .HasDefaultValue("GETDATE()")
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.LastUpdatedDate);

        builder.HasOne(x => x.Institution)
            .WithMany()
            .HasForeignKey(x => x.InstitutionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}