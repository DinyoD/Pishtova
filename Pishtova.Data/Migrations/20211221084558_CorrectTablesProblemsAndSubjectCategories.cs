using Microsoft.EntityFrameworkCore.Migrations;

namespace Pishtova.Data.Migrations
{
    public partial class CorrectTablesProblemsAndSubjectCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureId",
                table: "Problems",
                newName: "PictureUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "Problems",
                newName: "PictureId");
        }
    }
}
