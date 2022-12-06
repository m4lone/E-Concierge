using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace qodeless.Infra.CrossCutting.Identity.Migrations
{
    public partial class _014_adjust_voucherTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VirtualBalance_Vouscher_VouscherId",
                table: "VirtualBalance");

            migrationBuilder.DropTable(
                name: "Vouscher");

            migrationBuilder.DropIndex(
                name: "IX_VirtualBalance_VouscherId",
                table: "VirtualBalance");

            migrationBuilder.DropColumn(
                name: "VouscherId",
                table: "VirtualBalance");

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserOperationId = table.Column<string>(type: "text", nullable: false),
                    QrCodeKey = table.Column<string>(type: "text", nullable: false),
                    QrCodeSecret = table.Column<string>(type: "text", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    SiteID = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Excluded = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voucher_Site_SiteID",
                        column: x => x.SiteID,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VirtualBalance_VoucherId",
                table: "VirtualBalance",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_QrCodeKey",
                table: "Voucher",
                column: "QrCodeKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_SiteID",
                table: "Voucher",
                column: "SiteID");

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualBalance_Voucher_VoucherId",
                table: "VirtualBalance",
                column: "VoucherId",
                principalTable: "Voucher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VirtualBalance_Voucher_VoucherId",
                table: "VirtualBalance");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropIndex(
                name: "IX_VirtualBalance_VoucherId",
                table: "VirtualBalance");

            migrationBuilder.AddColumn<Guid>(
                name: "VouscherId",
                table: "VirtualBalance",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vouscher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Excluded = table.Column<bool>(type: "boolean", nullable: false),
                    QrCodeKey = table.Column<string>(type: "text", nullable: false),
                    QrCodeSecret = table.Column<string>(type: "text", nullable: true),
                    SiteID = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UserOperationId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouscher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vouscher_Site_SiteID",
                        column: x => x.SiteID,
                        principalTable: "Site",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VirtualBalance_VouscherId",
                table: "VirtualBalance",
                column: "VouscherId");

            migrationBuilder.CreateIndex(
                name: "IX_Vouscher_SiteID",
                table: "Vouscher",
                column: "SiteID");

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualBalance_Vouscher_VouscherId",
                table: "VirtualBalance",
                column: "VouscherId",
                principalTable: "Vouscher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
