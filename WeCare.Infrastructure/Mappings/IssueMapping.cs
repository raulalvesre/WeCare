using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class IssueMapping :  IEntityTypeConfiguration<IssueReport>
{
    public void Configure(EntityTypeBuilder<IssueReport> builder)
    {
        builder.ToTable("issue_reports");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Status)
            .HasConversion(
                v => v.ToString(),
                v => (IssueStatus)Enum.Parse(typeof(IssueStatus), v));

        builder.Property(x => x.Title);
        
        builder.Property(x => x.Description);

        builder.Property(x => x.ResolutionNotes)
            .IsRequired(false);
        
        builder.Property(x => x.ResolutionDate)
            .IsRequired(false);

        builder.Property(x => x.CreationDate)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Reporter)
            .WithMany()
            .HasForeignKey(x => x.ReporterId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.ReportedUser)
            .WithMany()
            .HasForeignKey(x => x.ReportedUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Resolver)
            .WithMany()
            .HasForeignKey(x => x.ResolverId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Opportunity)
            .WithMany()
            .HasForeignKey(x => x.OpportunityId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Messages)
            .WithOne()
            .HasForeignKey(x => x.IssueReportId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}