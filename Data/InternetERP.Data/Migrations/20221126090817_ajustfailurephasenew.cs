using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetERP.Data.Migrations
{
    public partial class ajustfailurephasenew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailureTeamId",
                table: "FailurePhases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusFailureId",
                table: "FailurePhases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FailurePhases_FailureTeamId",
                table: "FailurePhases",
                column: "FailureTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_FailurePhases_StatusFailureId",
                table: "FailurePhases",
                column: "StatusFailureId");

            migrationBuilder.AddForeignKey(
                name: "FK_FailurePhases_FailureTeams_FailureTeamId",
                table: "FailurePhases",
                column: "FailureTeamId",
                principalTable: "FailureTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FailurePhases_StatusFailures_StatusFailureId",
                table: "FailurePhases",
                column: "StatusFailureId",
                principalTable: "StatusFailures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FailurePhases_FailureTeams_FailureTeamId",
                table: "FailurePhases");

            migrationBuilder.DropForeignKey(
                name: "FK_FailurePhases_StatusFailures_StatusFailureId",
                table: "FailurePhases");

            migrationBuilder.DropIndex(
                name: "IX_FailurePhases_FailureTeamId",
                table: "FailurePhases");

            migrationBuilder.DropIndex(
                name: "IX_FailurePhases_StatusFailureId",
                table: "FailurePhases");

            migrationBuilder.DropColumn(
                name: "FailureTeamId",
                table: "FailurePhases");

            migrationBuilder.DropColumn(
                name: "StatusFailureId",
                table: "FailurePhases");
        }
    }
}
