using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace qodeless.Infra.CrossCutting.Identity.Migrations
{
    public partial class _018_EntityModeling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Income_Account_AccountId",
                table: "Income");

            migrationBuilder.DropForeignKey(
                name: "FK_Income_IncomeType_IncomeTypeId",
                table: "Income");

            migrationBuilder.DropForeignKey(
                name: "FK_Income_Site_SiteId",
                table: "Income");

            migrationBuilder.DropIndex(
                name: "IX_Income_IncomeTypeId",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "FrequencyParcels",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "IncomeTypeId",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Income");

            migrationBuilder.DropColumn(
                name: "UserOperationId",
                table: "Income");

            migrationBuilder.AlterColumn<Guid>(
                name: "SiteId",
                table: "Income",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Income",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Income",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Income_Account_AccountId",
                table: "Income",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Income_Site_SiteId",
                table: "Income",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Income_Account_AccountId",
                table: "Income");

            migrationBuilder.DropForeignKey(
                name: "FK_Income_Site_SiteId",
                table: "Income");

            migrationBuilder.AlterColumn<Guid>(
                name: "SiteId",
                table: "Income",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Income",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Income",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Income",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "Income",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "FrequencyParcels",
                table: "Income",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "IncomeTypeId",
                table: "Income",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Income",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Income",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Income",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserOperationId",
                table: "Income",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Income_IncomeTypeId",
                table: "Income",
                column: "IncomeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Income_Account_AccountId",
                table: "Income",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Income_IncomeType_IncomeTypeId",
                table: "Income",
                column: "IncomeTypeId",
                principalTable: "IncomeType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Income_Site_SiteId",
                table: "Income",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
