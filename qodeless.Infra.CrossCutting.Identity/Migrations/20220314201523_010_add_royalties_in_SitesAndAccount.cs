using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace qodeless.Infra.CrossCutting.Identity.Migrations
{
    public partial class _010_add_royalties_in_SitesAndAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellPhone",
                table: "Account");

            migrationBuilder.AddColumn<double>(
                name: "Royalties",
                table: "Site",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Royalties",
                table: "Account",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "SubAccountId",
                table: "Account",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_SubAccountId",
                table: "Account",
                column: "SubAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Account_SubAccountId",
                table: "Account",
                column: "SubAccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Account_SubAccountId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_SubAccountId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "Royalties",
                table: "Site");

            migrationBuilder.DropColumn(
                name: "Royalties",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "SubAccountId",
                table: "Account");

            migrationBuilder.AddColumn<string>(
                name: "CellPhone",
                table: "Account",
                type: "text",
                nullable: true);
        }
    }
}
