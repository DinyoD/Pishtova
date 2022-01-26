using Microsoft.EntityFrameworkCore.Migrations;

namespace Pishtova.Data.Migrations
{
    public partial class CorrectTableScores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Problems_ProblemId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_ProblemId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ProblemId",
                table: "Scores");

            migrationBuilder.AddColumn<int>(
                name: "SubjectCategoryId",
                table: "Scores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_SubjectCategoryId",
                table: "Scores",
                column: "SubjectCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_SubjectCategories_SubjectCategoryId",
                table: "Scores",
                column: "SubjectCategoryId",
                principalTable: "SubjectCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_SubjectCategories_SubjectCategoryId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_SubjectCategoryId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "SubjectCategoryId",
                table: "Scores");

            migrationBuilder.AddColumn<string>(
                name: "ProblemId",
                table: "Scores",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ProblemId",
                table: "Scores",
                column: "ProblemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Problems_ProblemId",
                table: "Scores",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
