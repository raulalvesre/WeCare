using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WeCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOpportunityRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "opportunity_registrations",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "text", nullable: false),
                    opportunity_id = table.Column<long>(type: "bigint", nullable: false),
                    candidate_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_opportunity_registrations", x => x.id);
                    table.ForeignKey(
                        name: "fk_opportunity_registrations_users_candidate_id",
                        column: x => x.candidate_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_opportunity_registrations_volunteer_opportunity_opportunity",
                        column: x => x.opportunity_id,
                        principalSchema: "public",
                        principalTable: "volunteer_opportunities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "public",
                table: "opportunity_causes",
                keyColumn: "id",
                keyValue: 10L,
                column: "name",
                value: "Educação");

            migrationBuilder.CreateIndex(
                name: "ix_opportunity_registrations_candidate_id",
                schema: "public",
                table: "opportunity_registrations",
                column: "candidate_id");

            migrationBuilder.CreateIndex(
                name: "ix_opportunity_registrations_opportunity_id",
                schema: "public",
                table: "opportunity_registrations",
                column: "opportunity_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "opportunity_registrations",
                schema: "public");

            migrationBuilder.UpdateData(
                schema: "public",
                table: "opportunity_causes",
                keyColumn: "id",
                keyValue: 10L,
                column: "name",
                value: "education");
        }
    }
}
