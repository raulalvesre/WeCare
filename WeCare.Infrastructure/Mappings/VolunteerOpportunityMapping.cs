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
            .IsRequired();

        builder.Property(x => x.Description);
        
        builder.Property(x => x.Address)
            .IsRequired();

        builder.Property(x => x.OpportunityDate)
            .IsRequired();
        
        builder.Property(x => x.Address)
            .IsRequired();
        
        builder.Property(x => x.Number)
            .IsRequired();

        builder.Property(x => x.Complement);
        
        builder.Property(x => x.City)
            .IsRequired();
        
        builder.Property(x => x.City)
            .IsRequired();

        builder.Property(x => x.Neighborhood);
        
        builder.Property(x => x.State)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(x => x.Cep)
            .IsRequired();

        builder.Property(x => x.CreationDate)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.LastUpdateDate);

        builder.HasOne(x => x.Institution)
            .WithMany()
            .HasForeignKey(x => x.InstitutionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.RequiredQualifications)
            .WithMany(x => x.VolunteerOpportunities)
            .UsingEntity(x => x.ToTable("qualification_opportunity_link"));
        
        builder.HasMany(x => x.Causes)
            .WithMany(x => x.VolunteerOpportunities)
            .UsingEntity(x => x.ToTable("cause_opportunity_link"));
    }
}