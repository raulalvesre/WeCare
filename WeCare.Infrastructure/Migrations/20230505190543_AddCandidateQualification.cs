using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidateQualification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "candidate_qualifications",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_candidate_qualifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "candidate_candidate_qualification",
                schema: "public",
                columns: table => new
                {
                    candidates_id = table.Column<long>(type: "bigint", nullable: false),
                    qualifications_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_candidate_candidate_qualification", x => new { x.candidates_id, x.qualifications_id });
                    table.ForeignKey(
                        name: "fk_candidate_candidate_qualification_candidate_qualification_q",
                        column: x => x.qualifications_id,
                        principalSchema: "public",
                        principalTable: "candidate_qualifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_candidate_candidate_qualification_users_candidates_id",
                        column: x => x.candidates_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "candidate_qualifications",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1L, "Agilidade" },
                    { 2L, "Artes/Trabalho manual" },
                    { 3L, "Computadores/Tecnologia" },
                    { 4L, "Comunicação" },
                    { 5L, "Cozinha" },
                    { 6L, "Dança/Música" },
                    { 7L, "Direito" },
                    { 8L, "Educação" },
                    { 9L, "Esportes" },
                    { 10L, "Gerenciamento" },
                    { 11L, "Idiomas" },
                    { 12L, "Organização" },
                    { 13L, "Saúde" },
                    { 14L, "Outros" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_candidate_candidate_qualification_qualifications_id",
                schema: "public",
                table: "candidate_candidate_qualification",
                column: "qualifications_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidate_candidate_qualification",
                schema: "public");

            migrationBuilder.DropTable(
                name: "candidate_qualifications",
                schema: "public");
        }
    }
}
