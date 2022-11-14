using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pishtova.Data.Migrations
{
    public partial class UpdateUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "SubscriberId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SubscriberId",
                table: "AspNetUsers",
                column: "SubscriberId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Subscribers_SubscriberId",
                table: "AspNetUsers",
                column: "SubscriberId",
                principalTable: "Subscribers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Subscribers_SubscriberId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SubscriberId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SubscriberId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
