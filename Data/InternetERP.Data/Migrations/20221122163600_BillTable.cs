#nullable disable

namespace InternetERP.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class BillTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillId",
                table: "Sales",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusBillId",
                table: "Failures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StatusBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusBills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bills_StatusBills_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusBills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BillId",
                table: "Sales",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Failures_StatusBillId",
                table: "Failures",
                column: "StatusBillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_IsDeleted",
                table: "Bills",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_StatusId",
                table: "Bills",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserId",
                table: "Bills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusBills_IsDeleted",
                table: "StatusBills",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Failures_StatusBills_StatusBillId",
                table: "Failures",
                column: "StatusBillId",
                principalTable: "StatusBills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Bills_BillId",
                table: "Sales",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Failures_StatusBills_StatusBillId",
                table: "Failures");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Bills_BillId",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "StatusBills");

            migrationBuilder.DropIndex(
                name: "IX_Sales_BillId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Failures_StatusBillId",
                table: "Failures");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "StatusBillId",
                table: "Failures");
        }
    }
}
