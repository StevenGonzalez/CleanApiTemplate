using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DotnetCleanApiTemplate.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreatedUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CreatedUtc", "Name" },
                values: new object[,]
                {
                    { new Guid("45ca059c-867b-4be9-b4e3-8209ec14f1ec"), new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Architecture notes" },
                    { new Guid("b3f93584-8602-42ba-b6d1-7a36f9b0e9e2"), new DateTime(2026, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Release checklist" },
                    { new Guid("e57e4bc8-8a7b-4a0e-bf8e-f56d7193b94b"), new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Roadmap draft" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
