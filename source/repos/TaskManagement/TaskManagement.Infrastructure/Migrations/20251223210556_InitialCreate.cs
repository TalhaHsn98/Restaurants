using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "DueDate", "EndDate", "Name", "Priority", "StartDate", "Status" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Design REST endpoints", new DateTime(2025, 12, 26, 0, 0, 0, 0, DateTimeKind.Local), null, "Design API", 2, null, 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Business rules & validation", new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Local), null, "Implement Service Layer", 1, null, 0 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "xUnit + Moq", new DateTime(2025, 12, 30, 0, 0, 0, 0, DateTimeKind.Local), null, "Write Unit Tests", 2, null, 0 },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "EF Core + SQLite", new DateTime(2025, 12, 25, 0, 0, 0, 0, DateTimeKind.Local), null, "Setup SQLite", 0, null, 1 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "How to run & test", new DateTime(2025, 12, 24, 0, 0, 0, 0, DateTimeKind.Local), null, "Prepare README", 1, null, 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
