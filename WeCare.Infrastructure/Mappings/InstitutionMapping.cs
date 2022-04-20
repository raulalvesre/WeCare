using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain;

namespace WeCare.Infrastructure.Mappings;

public class InstitutionMapping : IEntityTypeConfiguration<Institution>
{
    
    public void Configure(EntityTypeBuilder<Institution> builder)
    {
        builder.ToTable("institution");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Email)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.Cnpj)
            .HasMaxLength(18)
            .IsRequired();

        builder.Property(x => x.Telephone)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(x => x.Address)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(x => x.CreationDate)
            .HasDefaultValue("GETDATE()")
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.LastUpdatedDate);

        builder.HasOne(x => x.LineOfWork)
            .WithMany()
            .HasForeignKey(x => x.LineOfWorkId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}