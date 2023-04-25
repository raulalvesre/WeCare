using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WeCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOpportunityInvitation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "opportunity_invitation",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "text", nullable: false),
                    invitation_message = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    response_message = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    opportunity_id = table.Column<long>(type: "bigint", nullable: false),
                    candidate_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_opportunity_invitation", x => x.id);
                    table.ForeignKey(
                        name: "fk_opportunity_invitation_users_candidate_id",
                        column: x => x.candidate_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_opportunity_invitation_volunteer_opportunity_opportunity_id",
                        column: x => x.opportunity_id,
                        principalSchema: "public",
                        principalTable: "volunteer_opportunities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_opportunity_invitation_candidate_id",
                schema: "public",
                table: "opportunity_invitation",
                column: "candidate_id");

            migrationBuilder.CreateIndex(
                name: "ix_opportunity_invitation_opportunity_id",
                schema: "public",
                table: "opportunity_invitation",
                column: "opportunity_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "opportunity_invitation",
                schema: "public");
        }
    }
}
