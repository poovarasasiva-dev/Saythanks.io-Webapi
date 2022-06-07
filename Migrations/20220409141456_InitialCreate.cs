using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Saythanks.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:pgcrypto", ",,");

            migrationBuilder.CreateTable(
                name: "ar_internal_metadata",
                columns: table => new
                {
                    key = table.Column<string>(type: "character varying", nullable: false),
                    value = table.Column<string>(type: "character varying", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ar_internal_metadata_pkey", x => x.key);
                });

            migrationBuilder.CreateTable(
                name: "inboxes",
                columns: table => new
                {
                    auth_id = table.Column<string>(type: "text", nullable: false),
                    slug = table.Column<string>(type: "text", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    email_enabled = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    email = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("inboxes_pk", x => x.auth_id);
                });

            migrationBuilder.CreateTable(
                name: "schema_migrations",
                columns: table => new
                {
                    version = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("schema_migrations_pkey", x => x.version);
                });

            migrationBuilder.CreateTable(
                name: "notes",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "public.gen_random_uuid()"),
                    inboxes_auth_id = table.Column<string>(type: "text", nullable: false),
                    body = table.Column<string>(type: "text", nullable: false),
                    byline = table.Column<string>(type: "text", nullable: true),
                    archived = table.Column<bool>(type: "boolean", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("notes_pk", x => x.uuid);
                    table.ForeignKey(
                        name: "notes_inboxes",
                        column: x => x.inboxes_auth_id,
                        principalTable: "inboxes",
                        principalColumn: "auth_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_notes_inboxes_auth_id",
                table: "notes",
                column: "inboxes_auth_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ar_internal_metadata");

            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "schema_migrations");

            migrationBuilder.DropTable(
                name: "inboxes");
        }
    }
}
