using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDesirableQualificationsToOpportunity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_issue_reports_volunteer_opportunity_opportunity_id",
                schema: "public",
                table: "issue_reports");

            migrationBuilder.DropForeignKey(
                name: "fk_opportunity_invitation_volunteer_opportunity_opportunity_id",
                schema: "public",
                table: "opportunity_invitation");

            migrationBuilder.DropForeignKey(
                name: "fk_opportunity_registrations_volunteer_opportunity_opportunity",
                schema: "public",
                table: "opportunity_registrations");

            migrationBuilder.DropTable(
                name: "candidate_candidate_qualification",
                schema: "public");

            migrationBuilder.DropTable(
                name: "candidate_qualifications",
                schema: "public");

            migrationBuilder.AddColumn<bool>(
                name: "enabled",
                schema: "public",
                table: "volunteer_opportunities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "qualifications",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_qualifications", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "candidate_qualification",
                schema: "public",
                columns: table => new
                {
                    candidates_id = table.Column<long>(type: "bigint", nullable: false),
                    qualifications_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_candidate_qualification", x => new { x.candidates_id, x.qualifications_id });
                    table.ForeignKey(
                        name: "fk_candidate_qualification_qualification_qualifications_id",
                        column: x => x.qualifications_id,
                        principalSchema: "public",
                        principalTable: "qualifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_candidate_qualification_users_candidates_id",
                        column: x => x.candidates_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qualification_volunteer_opportunity",
                schema: "public",
                columns: table => new
                {
                    desirable_qualifications_id = table.Column<long>(type: "bigint", nullable: false),
                    opportunities_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_qualification_volunteer_opportunity", x => new { x.desirable_qualifications_id, x.opportunities_id });
                    table.ForeignKey(
                        name: "fk_qualification_volunteer_opportunity_qualification_desirable",
                        column: x => x.desirable_qualifications_id,
                        principalSchema: "public",
                        principalTable: "qualifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_qualification_volunteer_opportunity_volunteer_opportunities",
                        column: x => x.opportunities_id,
                        principalSchema: "public",
                        principalTable: "volunteer_opportunities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "qualifications",
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
                name: "ix_candidate_qualification_qualifications_id",
                schema: "public",
                table: "candidate_qualification",
                column: "qualifications_id");

            migrationBuilder.CreateIndex(
                name: "ix_qualification_volunteer_opportunity_opportunities_id",
                schema: "public",
                table: "qualification_volunteer_opportunity",
                column: "opportunities_id");

            migrationBuilder.AddForeignKey(
                name: "fk_issue_reports_volunteer_opportunities_opportunity_id",
                schema: "public",
                table: "issue_reports",
                column: "opportunity_id",
                principalSchema: "public",
                principalTable: "volunteer_opportunities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_opportunity_invitation_volunteer_opportunities_opportunity_",
                schema: "public",
                table: "opportunity_invitation",
                column: "opportunity_id",
                principalSchema: "public",
                principalTable: "volunteer_opportunities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_opportunity_registrations_volunteer_opportunities_opportuni",
                schema: "public",
                table: "opportunity_registrations",
                column: "opportunity_id",
                principalSchema: "public",
                principalTable: "volunteer_opportunities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_issue_reports_volunteer_opportunities_opportunity_id",
                schema: "public",
                table: "issue_reports");

            migrationBuilder.DropForeignKey(
                name: "fk_opportunity_invitation_volunteer_opportunities_opportunity_",
                schema: "public",
                table: "opportunity_invitation");

            migrationBuilder.DropForeignKey(
                name: "fk_opportunity_registrations_volunteer_opportunities_opportuni",
                schema: "public",
                table: "opportunity_registrations");

            migrationBuilder.DropTable(
                name: "candidate_qualification",
                schema: "public");

            migrationBuilder.DropTable(
                name: "qualification_volunteer_opportunity",
                schema: "public");

            migrationBuilder.DropTable(
                name: "qualifications",
                schema: "public");

            migrationBuilder.DropColumn(
                name: "enabled",
                schema: "public",
                table: "volunteer_opportunities");

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

            migrationBuilder.AddForeignKey(
                name: "fk_issue_reports_volunteer_opportunity_opportunity_id",
                schema: "public",
                table: "issue_reports",
                column: "opportunity_id",
                principalSchema: "public",
                principalTable: "volunteer_opportunities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_opportunity_invitation_volunteer_opportunity_opportunity_id",
                schema: "public",
                table: "opportunity_invitation",
                column: "opportunity_id",
                principalSchema: "public",
                principalTable: "volunteer_opportunities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_opportunity_registrations_volunteer_opportunity_opportunity",
                schema: "public",
                table: "opportunity_registrations",
                column: "opportunity_id",
                principalSchema: "public",
                principalTable: "volunteer_opportunities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
