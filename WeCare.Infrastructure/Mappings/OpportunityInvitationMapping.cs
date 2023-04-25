using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class OpportunityInvitationMapping : IEntityTypeConfiguration<OpportunityInvitation>
{
    public void Configure(EntityTypeBuilder<OpportunityInvitation> builder)
    {
        builder.ToTable("opportunity_invitation");

        builder.Property(x => x.Status)
            .HasConversion(
                v => v.ToString(),
                v => (InvitationStatus)Enum.Parse(typeof(InvitationStatus), v));

        builder.Property(x => x.InvitationMessage)
            .HasMaxLength(1024)
            .IsRequired(false);
        
        builder.Property(x => x.ResponseMessage)
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