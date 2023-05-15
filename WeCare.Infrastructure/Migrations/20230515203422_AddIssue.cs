using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WeCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIssue : Migration
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

            migrationBuilder.CreateTable(
                name: "issue_reports",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    resolution_notes = table.Column<string>(type: "text", nullable: true),
                    reported_user_id = table.Column<long>(type: "bigint", nullable: false),
                    reporter_id = table.Column<long>(type: "bigint", nullable: false),
                    resolver_id = table.Column<long>(type: "bigint", nullable: true),
                    opportunity_id = table.Column<long>(type: "bigint", nullable: false),
                    resolution_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_issue_reports", x => x.id);
                    table.ForeignKey(
                        name: "fk_issue_reports_users_reported_user_id",
                        column: x => x.reported_user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_issue_reports_users_reporter_id",
                        column: x => x.reporter_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_issue_reports_users_resolver_id",
                        column: x => x.resolver_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_issue_reports_volunteer_opportunity_opportunity_id",
                        column: x => x.opportunity_id,
                        principalSchema: "public",
                        principalTable: "volunteer_opportunities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "issue_messsages",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    issue_report_id = table.Column<long>(type: "bigint", nullable: false),
                    sender_id = table.Column<long>(type: "bigint", nullable: false),
                    content = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    issue_report_id1 = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_issue_messsages", x => x.id);
                    table.ForeignKey(
                        name: "fk_issue_messsages_issue_reports_issue_report_id",
                        column: x => x.issue_report_id,
                        principalSchema: "public",
                        principalTable: "issue_reports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_issue_messsages_issue_reports_issue_report_id1",
                        column: x => x.issue_report_id1,
                        principalSchema: "public",
                        principalTable: "issue_reports",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_issue_messsages_users_sender_id",
                        column: x => x.sender_id,
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

            migrationBuilder.CreateIndex(
                name: "ix_issue_messsages_issue_report_id",
                schema: "public",
                table: "issue_messsages",
                column: "issue_report_id");

            migrationBuilder.CreateIndex(
                name: "ix_issue_messsages_issue_report_id1",
                schema: "public",
                table: "issue_messsages",
                column: "issue_report_id1");

            migrationBuilder.CreateIndex(
                name: "ix_issue_messsages_sender_id",
                schema: "public",
                table: "issue_messsages",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "ix_issue_reports_opportunity_id",
                schema: "public",
                table: "issue_reports",
                column: "opportunity_id");

            migrationBuilder.CreateIndex(
                name: "ix_issue_reports_reported_user_id",
                schema: "public",
                table: "issue_reports",
                column: "reported_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_issue_reports_reporter_id",
                schema: "public",
                table: "issue_reports",
                column: "reporter_id");

            migrationBuilder.CreateIndex(
                name: "ix_issue_reports_resolver_id",
                schema: "public",
                table: "issue_reports",
                column: "resolver_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidate_opportunity_cause",
                schema: "public");

            migrationBuilder.DropTable(
                name: "issue_messsages",
                schema: "public");

            migrationBuilder.DropTable(
                name: "issue_reports",
                schema: "public");
        }
    }
}
