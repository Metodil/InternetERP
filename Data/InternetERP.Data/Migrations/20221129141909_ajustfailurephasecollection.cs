using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetERP.Data.Migrations
{
    public partial class ajustfailurephasecollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailurePhaseId",
                table: "Failures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailurePhaseId",
                table: "Failures");
        }
    }
}
