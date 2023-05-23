using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class ParticipationCertificateMapping : IEntityTypeConfiguration<ParticipationCertificate
>
{
    public void Configure(EntityTypeBuilder<ParticipationCertificate> builder)
    {
        builder.ToTable("participation_certificates");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.AuthenticityCode);

        builder.Property(x => x.CreationDate)
            .HasDefaultValueSql("now()")
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Registration)
            .WithMany()
            .HasForeignKey(x => x.RegistrationId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.DisplayedQualifications)
            .WithMany(x => x.ParticipationCertificates);
    }
}