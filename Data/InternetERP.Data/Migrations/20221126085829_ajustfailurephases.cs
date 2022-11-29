using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetERP.Data.Migrations
{
    public partial class ajustfailurephases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FailurePhases",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FailurePhases_UserId",
                table: "FailurePhases",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FailurePhases_AspNetUsers_UserId",
                table: "FailurePhases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FailurePhases_AspNetUsers_UserId",
                table: "FailurePhases");

            migrationBuilder.DropIndex(
                name: "IX_FailurePhases_UserId",
                table: "FailurePhases");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FailurePhases");
        }
    }
}
