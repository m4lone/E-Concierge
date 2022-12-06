using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace qodeless.Infra.CrossCutting.Identity.Migrations
{
    public partial class UpdateVoucherIdInVirtualBalanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VirtualBalance_Voucher_VoucherId",
                table: "VirtualBalance");

            migrationBuilder.AlterColumn<Guid>(
                name: "VoucherId",
                table: "VirtualBalance",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualBalance_Voucher_VoucherId",
                table: "VirtualBalance",
                column: "VoucherId",
                principalTable: "Voucher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VirtualBalance_Voucher_VoucherId",
                table: "VirtualBalance");

            migrationBuilder.AlterColumn<Guid>(
                name: "VoucherId",
                table: "VirtualBalance",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualBalance_Voucher_VoucherId",
                table: "VirtualBalance",
                column: "VoucherId",
                principalTable: "Voucher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
