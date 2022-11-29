#nullable disable

namespace InternetERP.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AjustBillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AspNetUsers_UserId",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Bills",
                newName: "SelectedUserId");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Bills",
                newName: "UserFullName");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_UserId",
                table: "Bills",
                newName: "IX_Bills_SelectedUserId");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SaleUserId",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserAddress",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CustomerId",
                table: "Bills",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AspNetUsers_SelectedUserId",
                table: "Bills",
                column: "SelectedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Customers_CustomerId",
                table: "Bills",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AspNetUsers_SelectedUserId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Customers_CustomerId",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_CustomerId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "SaleUserId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "UserAddress",
                table: "Bills");

            migrationBuilder.RenameColumn(
                name: "UserFullName",
                table: "Bills",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "SelectedUserId",
                table: "Bills",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_SelectedUserId",
                table: "Bills",
                newName: "IX_Bills_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AspNetUsers_UserId",
                table: "Bills",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
