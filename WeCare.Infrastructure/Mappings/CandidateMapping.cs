using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain;

namespace WeCare.Infrastructure.Mappings;

public class CandidateMapping : IEntityTypeConfiguration<Candidate>
{
    
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable("candidate");
        
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

        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.Property(x => x.Cpf)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(x => x.Telephone)
            .HasMaxLength(11)
            .IsRequired();

        builder.Property(x => x.Address)
            .HasMaxLength(500)
            .IsRequired();
        
        builder.Property(x => x.CreationDate) 
            .HasDefaultValue("GETDATE()") //TODO ver se esse é o metodo em postgres msm
            .ValueGeneratedOnAdd();        
        
        builder.Property(x => x.LastUpdatedDate);
    }
    
}