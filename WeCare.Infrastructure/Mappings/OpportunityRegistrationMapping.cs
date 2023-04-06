using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class OpportunityRegistrationMapping : IEntityTypeConfiguration<OpportunityRegistration>
{
    public void Configure(EntityTypeBuilder<OpportunityRegistration> builder)
    {
        builder.ToTable("opportunity_registrations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Status)
            .HasConversion(
                v => v.ToString(),
                v => (OpportunityStatus)Enum.Parse(typeof(OpportunityStatus), v));

        builder.Property(x => x.FeedbackMessage)
            .HasMaxLength(1024)
            .IsRequired(false);
        
        builder.HasOne(x => x.Opportunity)
            .WithMany()
            .HasForeignKey(x => x.OpportunityId);
       
        builder.HasOne(x => x.Candidate)
            .WithMany()
            .HasForeignKey(x => x.CandidateId);
    }
}