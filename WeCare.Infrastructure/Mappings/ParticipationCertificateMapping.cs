using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain;

namespace WeCare.Infrastructure.Mappings;

public class ParticipationCertificateMapping : IEntityTypeConfiguration<ParticipationCertificate>
{
    public void Configure(EntityTypeBuilder<ParticipationCertificate> builder)
    {
        builder.ToTable("participations_certificates");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreationDate)
            .HasDefaultValueSql("now()")
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Candidate)
            .WithMany()
            .HasForeignKey(x => x.CandidateId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.VolunteerOpportunity)
            .WithMany()
            .HasForeignKey(x => x.VolunteerOpportunityId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}