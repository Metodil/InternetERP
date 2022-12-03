using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetERP.Data.Migrations
{
    public partial class AjustCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Phones_PhoneId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_InternetAccounts_Phones_PhoneId",
                table: "InternetAccounts");

            migrationBuilder.DropTable(
                name: "CustomerPhone");

            migrationBuilder.DropIndex(
                name: "IX_InternetAccounts_PhoneId",
                table: "InternetAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PhoneId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PhoneId",
                table: "InternetAccounts");

            migrationBuilder.DropColumn(
                name: "PhoneId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BayPrice",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Customers",
                newName: "Address");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Phones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Phones_CustomerId",
                table: "Phones",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Customers_CustomerId",
                table: "Phones",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Customers_CustomerId",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_CustomerId",
                table: "Phones");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Phones");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Customers",
                newName: "Street");

            migrationBuilder.AddColumn<int>(
                name: "PhoneId",
                table: "InternetAccounts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhoneId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BayPrice",
                table: "Customers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Customers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerPhone",
                columns: table => new
                {
                    CustomersId = table.Column<int>(type: "int", nullable: false),
                    PhonesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPhone", x => new { x.CustomersId, x.PhonesId });
                    table.ForeignKey(
                        name: "FK_CustomerPhone_Customers_CustomersId",
                        column: x => x.CustomersId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerPhone_Phones_PhonesId",
                        column: x => x.PhonesId,
                        principalTable: "Phones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternetAccounts_PhoneId",
                table: "InternetAccounts",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PhoneId",
                table: "Employees",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPhone_PhonesId",
                table: "CustomerPhone",
                column: "PhonesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Phones_PhoneId",
                table: "Employees",
                column: "PhoneId",
                principalTable: "Phones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InternetAccounts_Phones_PhoneId",
                table: "InternetAccounts",
                column: "PhoneId",
                principalTable: "Phones",
                principalColumn: "Id");
        }
    }
}
