using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain;

namespace WeCare.Infrastructure.Mappings;

public class CandidateMapping : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.Property(x => x.Cpf);
        
        builder.HasMany(x => x.Qualifications)
            .WithMany(x => x.Candidates)
            .UsingEntity(x => x.ToTable("candidates_qualification_link"));
    }
}