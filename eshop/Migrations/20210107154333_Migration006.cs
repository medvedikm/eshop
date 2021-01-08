using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eshop.Migrations
{
    public partial class Migration006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeCreated",
                table: "Product",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeCreated",
                table: "Order",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeCreated",
                table: "Carousel",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeCreated",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DateTimeCreated",
                table: "Order");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTimeCreated",
                table: "Carousel",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
