using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class CandidateMapping : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.Property(x => x.BirthDate)
            .IsRequired();

        builder.Property(x => x.Cpf);

        builder.HasMany(x => x.Qualifications)
            .WithMany(x => x.Candidates);
    }
}