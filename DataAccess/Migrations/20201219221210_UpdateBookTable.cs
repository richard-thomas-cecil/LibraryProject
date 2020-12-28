using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryProject.DataAccess.Migrations
{
    public partial class UpdateBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Books",
                newName: "AuthorFirstName");

            migrationBuilder.AddColumn<string>(
                name: "AuthorLastName",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorLastName",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "AuthorFirstName",
                table: "Books",
                newName: "Author");
        }
    }
}
