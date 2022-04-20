using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain;

namespace WeCare.Infrastructure.Mappings;

public class InstitutionLineOfWorkMapping : IEntityTypeConfiguration<InstitutionLineOfWork>
{
    
    public void Configure(EntityTypeBuilder<InstitutionLineOfWork> builder)
    {
        builder.ToTable("institution_line_of_work");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();
    }
    
}