#nullable disable

namespace InternetERP.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AjustEmployeeTableFailureIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_FailureTeams_FailureTeamId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "FailureTeamId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_FailureTeams_FailureTeamId",
                table: "Employees",
                column: "FailureTeamId",
                principalTable: "FailureTeams",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_FailureTeams_FailureTeamId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "FailureTeamId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_FailureTeams_FailureTeamId",
                table: "Employees",
                column: "FailureTeamId",
                principalTable: "FailureTeams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
