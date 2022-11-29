#nullable disable

namespace InternetERP.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AjustSaleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternetPayments");

            migrationBuilder.RenameColumn(
                name: "InernetPaymentId",
                table: "Sales",
                newName: "InternetAccountId");

            migrationBuilder.AddColumn<string>(
                name: "InernetAccountId",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_InternetAccountId",
                table: "Sales",
                column: "InternetAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_InternetAccounts_InternetAccountId",
                table: "Sales",
                column: "InternetAccountId",
                principalTable: "InternetAccounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_InternetAccounts_InternetAccountId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_InternetAccountId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "InernetAccountId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "InternetAccountId",
                table: "Sales",
                newName: "InernetPaymentId");

            migrationBuilder.CreateTable(
                name: "InternetPayments",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    SaleId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternetPayments", x => new { x.AccountId, x.SaleId });
                    table.ForeignKey(
                        name: "FK_InternetPayments_InternetAccounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "InternetAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InternetPayments_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternetPayments_IsDeleted",
                table: "InternetPayments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_InternetPayments_SaleId",
                table: "InternetPayments",
                column: "SaleId",
                unique: true);
        }
    }
}
