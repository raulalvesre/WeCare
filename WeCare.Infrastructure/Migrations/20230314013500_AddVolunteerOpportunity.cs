using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVolunteerOpportunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "opportunity_causes",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    primary_color_code = table.Column<string>(type: "text", nullable: true),
                    secondary_color_code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_opportunity_causes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "volunteer_opportunities",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    opportunity_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    photo = table.Column<byte[]>(type: "bytea", nullable: false),
                    street = table.Column<string>(type: "text", nullable: false),
                    number = table.Column<string>(type: "text", nullable: false),
                    complement = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    neighborhood = table.Column<string>(type: "text", nullable: false),
                    state = table.Column<string>(type: "text", nullable: false),
                    postal_code = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    last_update_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    institution_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteer_opportunities", x => x.id);
                    table.ForeignKey(
                        name: "fk_volunteer_opportunities_users_institution_id",
                        column: x => x.institution_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "opportunity_cause_volunteer_opportunity",
                schema: "public",
                columns: table => new
                {
                    causes_id = table.Column<long>(type: "bigint", nullable: false),
                    volunteer_opportunities_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_opportunity_cause_volunteer_opportunity", x => new { x.causes_id, x.volunteer_opportunities_id });
                    table.ForeignKey(
                        name: "fk_opportunity_cause_volunteer_opportunity_opportunity_cause_c",
                        column: x => x.causes_id,
                        principalSchema: "public",
                        principalTable: "opportunity_causes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_opportunity_cause_volunteer_opportunity_volunteer_opportuni",
                        column: x => x.volunteer_opportunities_id,
                        principalSchema: "public",
                        principalTable: "volunteer_opportunities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "opportunity_causes",
                columns: new[] { "id", "code", "name", "primary_color_code", "secondary_color_code" },
                values: new object[,]
                {
                    { 1L, "politics", "Advocacy | Políticas Públicas", null, null },
                    { 2L, "citizen-participation", "Cidadania", null, null },
                    { 3L, "fight-against-hunger", "Combate à Fome", null, null },
                    { 4L, "fight-against-poverty", "Combate a Pobreza", null, null },
                    { 5L, "conscious-consumption", "Consumo Consciente", null, null },
                    { 6L, "children-and-youth", "Crianças", null, null },
                    { 7L, "culture-and-art", "Cultura e Arte", null, null },
                    { 8L, "community-development", "Desenvolvimento Comunitário", null, null },
                    { 9L, "human-rights", "Direitos humanos", null, null },
                    { 10L, "education", "Educação", null, null },
                    { 11L, "racial-equity", "Equidade Racial", null, null },
                    { 12L, "sports", "Esportes", null, null },
                    { 13L, "elderly", "Idosos", null, null },
                    { 14L, "youth", "Jovens", null, null },
                    { 15L, "lgbti", "LGBTI+", null, null },
                    { 16L, "environment", "Meio Ambiente", null, null },
                    { 17L, "urban-mobility", "Mobilidade Urbana", null, null },
                    { 18L, "women", "Mulheres", null, null },
                    { 19L, "disabled-people", "Pessoas com deficiência", null, null },
                    { 20L, "homeless-population", "População em Situação de Rua", null, null },
                    { 21L, "indigenous-people", "Povos Indígenas", null, null },
                    { 22L, "animal-protection", "Proteção Animal", null, null },
                    { 23L, "refugees", "Refugiados", null, null },
                    { 24L, "health", "Saúde", null, null },
                    { 25L, "sustainability", "Sustentabilidade", null, null },
                    { 26L, "professional-training", "Treinamento profissional", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_opportunity_cause_volunteer_opportunity_volunteer_opportuni",
                schema: "public",
                table: "opportunity_cause_volunteer_opportunity",
                column: "volunteer_opportunities_id");

            migrationBuilder.CreateIndex(
                name: "ix_opportunity_causes_code",
                schema: "public",
                table: "opportunity_causes",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_volunteer_opportunities_institution_id",
                schema: "public",
                table: "volunteer_opportunities",
                column: "institution_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "opportunity_cause_volunteer_opportunity",
                schema: "public");

            migrationBuilder.DropTable(
                name: "opportunity_causes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "volunteer_opportunities",
                schema: "public");
        }
    }
}
