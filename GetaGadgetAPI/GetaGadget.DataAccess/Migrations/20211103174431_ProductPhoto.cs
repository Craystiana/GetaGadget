using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GetaGadget.DataAccess.Migrations
{
    public partial class ProductPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Products");
        }
    }
}
