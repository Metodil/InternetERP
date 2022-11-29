#nullable disable

namespace InternetERP.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AjustFailureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateUserId",
                table: "Failures",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Failures_CreateUserId",
                table: "Failures",
                column: "CreateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Failures_AspNetUsers_CreateUserId",
                table: "Failures",
                column: "CreateUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Failures_AspNetUsers_CreateUserId",
                table: "Failures");

            migrationBuilder.DropIndex(
                name: "IX_Failures_CreateUserId",
                table: "Failures");

            migrationBuilder.DropColumn(
                name: "CreateUserId",
                table: "Failures");
        }
    }
}
