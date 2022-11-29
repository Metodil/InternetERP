#nullable disable

namespace InternetERP.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AjustStatusBillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Failures_StatusBills_StatusBillId",
                table: "Failures");

            migrationBuilder.DropIndex(
                name: "IX_Failures_StatusBillId",
                table: "Failures");

            migrationBuilder.DropColumn(
                name: "StatusBillId",
                table: "Failures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusBillId",
                table: "Failures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Failures_StatusBillId",
                table: "Failures",
                column: "StatusBillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Failures_StatusBills_StatusBillId",
                table: "Failures",
                column: "StatusBillId",
                principalTable: "StatusBills",
                principalColumn: "Id");
        }
    }
}
