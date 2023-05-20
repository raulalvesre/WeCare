using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class QualificationMapping : IEntityTypeConfiguration<Qualification>
{
    public void Configure(EntityTypeBuilder<Qualification> builder)
    {
        builder.ToTable("qualifications");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name);
        
        builder.HasData(
            new { Id = 1L, Name = "Agilidade" },
            new { Id = 2L, Name = "Artes/Trabalho manual" },
            new { Id = 3L, Name = "Computadores/Tecnologia" },
            new { Id = 4L, Name = "Comunicação" },
            new { Id = 5L, Name = "Cozinha" },
            new { Id = 6L, Name = "Dança/Música" },
            new { Id = 7L, Name = "Direito" },
            new { Id = 8L, Name = "Educação" },
            new { Id = 9L, Name = "Esportes" },
            new { Id = 10L, Name = "Gerenciamento" },
            new { Id = 11L, Name = "Idiomas" },
            new { Id = 12L, Name = "Organização" },
            new { Id = 13L, Name = "Saúde" },
            new { Id = 14L, Name = "Outros" }
        );

        builder.HasMany(x => x.Candidates)
            .WithMany(x => x.Qualifications);
    }
}