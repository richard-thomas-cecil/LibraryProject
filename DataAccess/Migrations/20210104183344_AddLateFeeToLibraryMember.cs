using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryProject.DataAccess.Migrations
{
    public partial class AddLateFeeToLibraryMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LateFees",
                table: "LibraryMembers",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LateFees",
                table: "LibraryMembers");
        }
    }
}
