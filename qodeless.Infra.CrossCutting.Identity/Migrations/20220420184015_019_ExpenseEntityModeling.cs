using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace qodeless.Infra.CrossCutting.Identity.Migrations
{
    public partial class _019_ExpenseEntityModeling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Site_SiteId",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "FrequencyParcels",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "TransactionDate",
                table: "Expense");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Expense",
                newName: "Type");

            migrationBuilder.AlterColumn<Guid>(
                name: "SiteId",
                table: "Expense",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Expense",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Expense",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_AccountId",
                table: "Expense",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Account_AccountId",
                table: "Expense",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Site_SiteId",
                table: "Expense",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Account_AccountId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Site_SiteId",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_Expense_AccountId",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Expense");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Expense",
                newName: "Status");

            migrationBuilder.AlterColumn<Guid>(
                name: "SiteId",
                table: "Expense",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Expense",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "Expense",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "FrequencyParcels",
                table: "Expense",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Expense",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionDate",
                table: "Expense",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Site_SiteId",
                table: "Expense",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
