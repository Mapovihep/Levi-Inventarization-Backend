using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inventarization.Migrations
{
    public partial class AddedSetupConnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "Setups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Setups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "Setups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Setups_DepartmentId",
                table: "Setups",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Setups_EmployeeId",
                table: "Setups",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Setups_RoomId",
                table: "Setups",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Setups_Departments_DepartmentId",
                table: "Setups",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Setups_Employees_EmployeeId",
                table: "Setups",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Setups_Rooms_RoomId",
                table: "Setups",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Setups_Departments_DepartmentId",
                table: "Setups");

            migrationBuilder.DropForeignKey(
                name: "FK_Setups_Employees_EmployeeId",
                table: "Setups");

            migrationBuilder.DropForeignKey(
                name: "FK_Setups_Rooms_RoomId",
                table: "Setups");

            migrationBuilder.DropIndex(
                name: "IX_Setups_DepartmentId",
                table: "Setups");

            migrationBuilder.DropIndex(
                name: "IX_Setups_EmployeeId",
                table: "Setups");

            migrationBuilder.DropIndex(
                name: "IX_Setups_RoomId",
                table: "Setups");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Setups");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Setups");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Setups");
        }
    }
}
