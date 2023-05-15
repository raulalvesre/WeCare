using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class IssueMessageMapping : IEntityTypeConfiguration<IssueMessage>
{
    public void Configure(EntityTypeBuilder<IssueMessage> builder)
    {
        builder.ToTable("issue_messsages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Content)
            .HasMaxLength(1024);
        
        builder.Property(x => x.Timestamp)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Sender)
            .WithMany()
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.IssueReport)
            .WithMany()
            .HasForeignKey(x => x.IssueReportId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}