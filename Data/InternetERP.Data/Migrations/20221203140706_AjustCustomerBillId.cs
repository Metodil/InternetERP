using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetERP.Data.Migrations
{
    public partial class AjustCustomerBillId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BillId",
                table: "Invoices",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Bills_BillId",
                table: "Invoices",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Bills_BillId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_BillId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Invoices");
        }
    }
}
