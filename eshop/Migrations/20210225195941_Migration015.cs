using Microsoft.EntityFrameworkCore.Migrations;

namespace eshop.Migrations
{
    public partial class Migration015 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStudent",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Sleva",
                table: "Users",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStudent",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Sleva",
                table: "Users");
        }
    }
}
