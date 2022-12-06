using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace qodeless.Infra.CrossCutting.Identity.Migrations
{
    public partial class Exchange_Correction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exchange_Site_SiteId",
                table: "Exchange");

            migrationBuilder.DropIndex(
                name: "IX_Exchange_SiteId",
                table: "Exchange");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "Exchange");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SiteId",
                table: "Exchange",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Exchange_SiteId",
                table: "Exchange",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exchange_Site_SiteId",
                table: "Exchange",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
