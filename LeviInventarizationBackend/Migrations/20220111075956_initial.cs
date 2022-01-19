using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventarization.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryLots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryLots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryLots_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryLots_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLots_EmployeeId",
                table: "InventoryLots",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLots_RoomId",
                table: "InventoryLots",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryLots");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
