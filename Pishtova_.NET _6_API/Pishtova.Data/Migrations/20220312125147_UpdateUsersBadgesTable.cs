using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pishtova.Data.Migrations
{
    public partial class UpdateUsersBadgesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersBadges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BadgeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersBadges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersBadges_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersBadges_Badges_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersBadges_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersBadges_BadgeId",
                table: "UsersBadges",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersBadges_IsDeleted",
                table: "UsersBadges",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UsersBadges_TestId",
                table: "UsersBadges",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersBadges_UserId",
                table: "UsersBadges",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersBadges");
        }
    }
}
