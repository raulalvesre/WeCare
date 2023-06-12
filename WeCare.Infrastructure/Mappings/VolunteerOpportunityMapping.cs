using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class VolunteerOpportunityMapping : IEntityTypeConfiguration<VolunteerOpportunity>
{
    public void Configure(EntityTypeBuilder<VolunteerOpportunity> builder)
    {
        builder.ToTable("volunteer_opportunities");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired();
        
        builder.Property(x => x.OpportunityDate)
            .IsRequired();

        builder.Property(x => x.Photo)
            .IsRequired();
        
         builder.Property(x => x.LastUpdateDate);

         builder.Property(x => x.Street)
             .IsRequired();
        
         builder.Property(x => x.Number)
             .IsRequired();

         builder.Property(x => x.Complement);
        
         builder.Property(x => x.City)
             .IsRequired();
        
         builder.Property(x => x.Neighborhood);
        
         builder.Property(x => x.State)
             .HasConversion(
                 x => x.ToString(),
                 x => (State)Enum.Parse(typeof(State), x))
             .IsRequired();
        
         builder.Property(x => x.PostalCode)
             .IsRequired();
        
        builder.Property(x => x.CreationDate)
            .HasDefaultValueSql("now()")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.LastUpdateDate);

        builder.Property(x => x.Enabled)
            .HasDefaultValue(true)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Institution)
            .WithMany(x => x.VolunteerOpportunities)
            .HasForeignKey(x => x.InstitutionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Causes)
            .WithMany(x => x.VolunteerOpportunities);

        builder.HasMany(x => x.Registrations)
            .WithOne(x => x.Opportunity)
            .HasForeignKey(x => x.OpportunityId);
        
        builder.HasMany(x => x.Invitations)
            .WithOne(x => x.Opportunity)
            .HasForeignKey(x => x.OpportunityId);
        
        builder.HasMany(x => x.DesirableQualifications)
            .WithMany(x => x.Opportunities);
    }
}