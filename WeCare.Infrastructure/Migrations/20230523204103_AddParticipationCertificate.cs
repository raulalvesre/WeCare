using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WeCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipationCertificate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "participation_certificates",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    authenticity_code = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    registration_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_participation_certificates", x => x.id);
                    table.ForeignKey(
                        name: "fk_participation_certificates_opportunity_registrations_regist",
                        column: x => x.registration_id,
                        principalSchema: "public",
                        principalTable: "opportunity_registrations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "participation_certificate_qualification",
                schema: "public",
                columns: table => new
                {
                    displayed_qualifications_id = table.Column<long>(type: "bigint", nullable: false),
                    participation_certificates_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_participation_certificate_qualification", x => new { x.displayed_qualifications_id, x.participation_certificates_id });
                    table.ForeignKey(
                        name: "fk_participation_certificate_qualification_participation_certi",
                        column: x => x.participation_certificates_id,
                        principalSchema: "public",
                        principalTable: "participation_certificates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_participation_certificate_qualification_qualification_displ",
                        column: x => x.displayed_qualifications_id,
                        principalSchema: "public",
                        principalTable: "qualifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_participation_certificate_qualification_participation_certi",
                schema: "public",
                table: "participation_certificate_qualification",
                column: "participation_certificates_id");

            migrationBuilder.CreateIndex(
                name: "ix_participation_certificates_registration_id",
                schema: "public",
                table: "participation_certificates",
                column: "registration_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "participation_certificate_qualification",
                schema: "public");

            migrationBuilder.DropTable(
                name: "participation_certificates",
                schema: "public");
        }
    }
}
