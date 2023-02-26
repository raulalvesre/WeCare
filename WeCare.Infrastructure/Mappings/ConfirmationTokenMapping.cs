using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class ConfirmationTokenMapping : IEntityTypeConfiguration<ConfirmationToken>
{
    public void Configure(EntityTypeBuilder<ConfirmationToken> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Token)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(x => x.CreationDate)
            .HasDefaultValueSql("NOW()")
            .ValueGeneratedOnAdd();
        
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}