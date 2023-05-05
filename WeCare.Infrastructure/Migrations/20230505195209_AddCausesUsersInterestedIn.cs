using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCausesUsersInterestedIn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "candidate_opportunity_cause",
                schema: "public",
                columns: table => new
                {
                    candidates_interested_in_id = table.Column<long>(type: "bigint", nullable: false),
                    causes_candidate_is_interested_in_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_candidate_opportunity_cause", x => new { x.candidates_interested_in_id, x.causes_candidate_is_interested_in_id });
                    table.ForeignKey(
                        name: "fk_candidate_opportunity_cause_opportunity_cause_causes_candid",
                        column: x => x.causes_candidate_is_interested_in_id,
                        principalSchema: "public",
                        principalTable: "opportunity_causes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_candidate_opportunity_cause_users_candidates_interested_in_",
                        column: x => x.candidates_interested_in_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_candidate_opportunity_cause_causes_candidate_is_interested_",
                schema: "public",
                table: "candidate_opportunity_cause",
                column: "causes_candidate_is_interested_in_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidate_opportunity_cause",
                schema: "public");
        }
    }
}
