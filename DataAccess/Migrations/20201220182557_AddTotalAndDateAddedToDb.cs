using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryProject.DataAccess.Migrations
{
    public partial class AddTotalAndDateAddedToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CheckedOut",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckedOut",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Books");
        }
    }
}
