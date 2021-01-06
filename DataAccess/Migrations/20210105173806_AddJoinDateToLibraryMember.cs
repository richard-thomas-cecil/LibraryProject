using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryProject.DataAccess.Migrations
{
    public partial class AddJoinDateToLibraryMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "JoinDate",
                table: "LibraryMembers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "LibraryMembers");
        }
    }
}
