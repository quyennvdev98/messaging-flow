using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketFlow.Services.Tickets.Core.Data.Migrations.Tickets
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tickets");

            migrationBuilder.CreateTable(
                name: "Agents",
                schema: "tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    JobPosition = table.Column<int>(type: "integer", nullable: false),
                    AvatarUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketScheduledActions",
                schema: "tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TranslatedText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketScheduledActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    LanguageCode = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: true),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    TranslatedDescription = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    InternalNotes = table.Column<string>(type: "text", nullable: true),
                    AssignedTo = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    DeadlineUtc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Resolution = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Agents_AssignedTo",
                        column: x => x.AssignedTo,
                        principalSchema: "tickets",
                        principalTable: "Agents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedTo",
                schema: "tickets",
                table: "Tickets",
                column: "AssignedTo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "tickets");

            migrationBuilder.DropTable(
                name: "TicketScheduledActions",
                schema: "tickets");

            migrationBuilder.DropTable(
                name: "Agents",
                schema: "tickets");
        }
    }
}
