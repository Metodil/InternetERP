#nullable disable

namespace InternetERP.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AjustBInternetAccountType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MontlyPrice",
                table: "InternetAccountTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MontlyPrice",
                table: "InternetAccountTypes");
        }
    }
}
