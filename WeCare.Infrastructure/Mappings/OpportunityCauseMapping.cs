using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeCare.Domain.Core;
using WeCare.Domain.Models;

namespace WeCare.Infrastructure.Mappings;

public class OpportunityCauseMapping : IEntityTypeConfiguration<OpportunityCause>
{
    public void Configure(EntityTypeBuilder<OpportunityCause> builder)
    {
        builder.ToTable("opportunity_causes");

        builder.HasKey(x => x.Id);
        
        builder.HasIndex(x => x.Code);

        builder.Property(x => x.Name);

        builder.Property(x => x.PrimaryColorCode)
            .IsRequired(false);
        
        builder.Property(x => x.SecondaryColorCode)
            .IsRequired(false);

        builder.HasData(
            new { Id = 1L, Code = "politics", Name = "Advocacy | Políticas Públicas" },
            new { Id = 2L, Code = "citizen-participation", Name = "Cidadania" },
            new { Id = 3L, Code = "fight-against-hunger", Name = "Combate à Fome" },
            new { Id = 4L, Code = "fight-against-poverty", Name = "Combate a Pobreza" },
            new { Id = 5L, Code = "conscious-consumption", Name = "Consumo Consciente" },
            new { Id = 6L, Code = "children-and-youth", Name = "Crianças" },
            new { Id = 7L, Code = "culture-and-art", Name = "Cultura e Arte" },
            new { Id = 8L, Code = "community-development", Name = "Desenvolvimento Comunitário" },
            new { Id = 9L, Code = "human-rights", Name = "Direitos humanos" },
            new { Id = 10L, Code = "education", Name = "education" },
            new { Id = 11L, Code = "racial-equity", Name = "Equidade Racial" },
            new { Id = 12L, Code = "sports", Name = "Esportes" },
            new { Id = 13L, Code = "elderly", Name = "Idosos" },
            new { Id = 14L, Code = "youth", Name = "Jovens" },
            new { Id = 15L, Code = "lgbti", Name = "LGBTI+" },
            new { Id = 16L, Code = "environment", Name = "Meio Ambiente" },
            new { Id = 17L, Code = "urban-mobility", Name = "Mobilidade Urbana" },
            new { Id = 18L, Code = "women", Name = "Mulheres" },
            new { Id = 19L, Code = "disabled-people", Name = "Pessoas com deficiência" },
            new { Id = 20L, Code = "homeless-population", Name = "População em Situação de Rua" },
            new { Id = 21L, Code = "indigenous-people", Name = "Povos Indígenas" },
            new { Id = 22L, Code = "animal-protection", Name = "Proteção Animal" },
            new { Id = 23L, Code = "refugees", Name = "Refugiados" },
            new { Id = 24L, Code = "health", Name = "Saúde" },
            new { Id = 25L, Code = "sustainability", Name = "Sustentabilidade" },
            new { Id = 26L, Code = "professional-training", Name = "Treinamento profissional" }
        );
        
        builder.HasMany(x => x.VolunteerOpportunities)
            .WithMany(x => x.Causes);
    }
}