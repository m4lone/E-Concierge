using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using qodeless.domain.Enums;

namespace qodeless.Infra.CrossCutting.Identity.Migrations
{
    public partial class _015_addColumn_inSite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<EGameCurrency>>(
                name: "GameCurrencies",
                table: "Site",
                type: "integer[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameCurrencies",
                table: "Site");
        }
    }
}
