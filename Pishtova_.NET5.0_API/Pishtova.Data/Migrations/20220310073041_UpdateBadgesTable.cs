using Microsoft.EntityFrameworkCore.Migrations;

namespace Pishtova.Data.Migrations
{
    public partial class UpdateBadgesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "Badges",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Badges",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Badges");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Badges",
                newName: "PictureUrl");
        }
    }
}
