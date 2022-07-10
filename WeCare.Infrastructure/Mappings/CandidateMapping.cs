using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain;

namespace WeCare.Infrastructure.Mappings;

public class CandidateMapping : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable("candidates");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .IsRequired();

        builder.Property(x => x.Password)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired();
        
        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.Property(x => x.Cpf);
        
        builder.Property(x => x.Telephone)
            .IsRequired();
        
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
        
        builder.HasMany(x => x.Qualifications)
            .WithMany(x => x.Candidates)
            .UsingEntity(x => x.ToTable("candidates_qualification_link"));
    }
}