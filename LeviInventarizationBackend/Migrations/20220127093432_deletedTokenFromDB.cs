using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeviInventarizationBackend.Migrations
{
    public partial class deletedTokenFromDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "token",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
