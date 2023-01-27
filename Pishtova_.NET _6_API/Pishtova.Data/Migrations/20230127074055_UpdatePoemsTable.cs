using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pishtova.Data.Migrations
{
    public partial class UpdatePoemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextLink",
                table: "Poems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextLink",
                table: "Poems",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
