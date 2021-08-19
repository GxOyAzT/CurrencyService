using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyService.Persistance.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    SourceCurrency = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TargetCurrency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => new { x.FromDate, x.Currency, x.SourceCurrency });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeRates");
        }
    }
}
