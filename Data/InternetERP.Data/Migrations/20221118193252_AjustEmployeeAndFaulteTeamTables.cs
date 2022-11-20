using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetERP.Data.Migrations
{
    public partial class AjustEmployeeAndFaulteTeamTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeFailureTeam");

            migrationBuilder.AddColumn<int>(
                name: "FailureTeamId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FailureTeamId",
                table: "Employees",
                column: "FailureTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_FailureTeams_FailureTeamId",
                table: "Employees",
                column: "FailureTeamId",
                principalTable: "FailureTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_FailureTeams_FailureTeamId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_FailureTeamId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FailureTeamId",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "EmployeeFailureTeam",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    FailuresTeamsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFailureTeam", x => new { x.EmployeesId, x.FailuresTeamsId });
                    table.ForeignKey(
                        name: "FK_EmployeeFailureTeam_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeFailureTeam_FailureTeams_FailuresTeamsId",
                        column: x => x.FailuresTeamsId,
                        principalTable: "FailureTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFailureTeam_FailuresTeamsId",
                table: "EmployeeFailureTeam",
                column: "FailuresTeamsId");
        }
    }
}
