using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WeCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVolunteerOpportunityAndOpportunityCause : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "photo",
                schema: "public",
                table: "users",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");

            migrationBuilder.AlterColumn<string>(
                name: "bio",
                schema: "public",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "opportunity_causes",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
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
                    state = table.Column<int>(type: "integer", nullable: false),
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
                    causes_id = table.Column<int>(type: "integer", nullable: false),
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
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "POLITICS" },
                    { 2, "CITIZEN_PARTICIPATION" },
                    { 3, "FIGHT_AGAINST_HUNGER" },
                    { 4, "FIGHT_AGAINST_POVERTY" },
                    { 5, "CONSCIOUS_CONSUMPTION" },
                    { 6, "CHILDREN_AND_YOUTH" },
                    { 7, "CULTURE_SPORTS_AND_ART" },
                    { 8, "COMMUNITY_DEVELOPMENT" },
                    { 9, "EDUCATION" },
                    { 10, "RACIAL_EQUITY" },
                    { 11, "SPORTS" },
                    { 12, "ELDERLY" },
                    { 13, "YOUTH" },
                    { 14, "LGBTI" },
                    { 15, "ENVIRONMENT" },
                    { 16, "URBAN_MOBILITY" },
                    { 17, "WOMEN" },
                    { 18, "DISABLED_PEOPLE" },
                    { 19, "HOMELESS_POPULATION" },
                    { 20, "INDIGENOUS_PEOPLE" },
                    { 21, "ANIMAL_PROTECTION" },
                    { 22, "REFUGEES" },
                    { 23, "HEALTH" },
                    { 24, "SUSTAINABILITY" },
                    { 25, "PROFESSIONAL_TRAINING" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_opportunity_cause_volunteer_opportunity_volunteer_opportuni",
                schema: "public",
                table: "opportunity_cause_volunteer_opportunity",
                column: "volunteer_opportunities_id");

            migrationBuilder.CreateIndex(
                name: "ix_opportunity_causes_name",
                schema: "public",
                table: "opportunity_causes",
                column: "name");

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

            migrationBuilder.AlterColumn<byte[]>(
                name: "photo",
                schema: "public",
                table: "users",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "bio",
                schema: "public",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
