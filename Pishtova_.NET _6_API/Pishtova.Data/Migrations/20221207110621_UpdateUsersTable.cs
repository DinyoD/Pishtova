using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pishtova.Data.Migrations
{
    public partial class UpdateUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Subsriptions_SubsriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SubsriptionId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SubsriptionId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "SubsriptionId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SubsriptionId",
                table: "AspNetUsers",
                column: "SubsriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Subsriptions_SubsriptionId",
                table: "AspNetUsers",
                column: "SubsriptionId",
                principalTable: "Subsriptions",
                principalColumn: "Id");
        }
    }
}
