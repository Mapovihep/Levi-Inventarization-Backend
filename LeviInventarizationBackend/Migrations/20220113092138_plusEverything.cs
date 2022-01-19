using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventarization.Migrations
{
    public partial class plusEverything : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "InventoryLots",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "InventoryLots",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Defects",
                table: "InventoryLots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "InventoryLots",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Defects = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryLots_DepartmentId",
                table: "InventoryLots",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryLots_Departments_DepartmentId",
                table: "InventoryLots",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryLots_Departments_DepartmentId",
                table: "InventoryLots");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Setups");

            migrationBuilder.DropIndex(
                name: "IX_InventoryLots_DepartmentId",
                table: "InventoryLots");

            migrationBuilder.DropColumn(
                name: "Defects",
                table: "InventoryLots");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "InventoryLots");

            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedBy",
                table: "InventoryLots",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "InventoryLots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
