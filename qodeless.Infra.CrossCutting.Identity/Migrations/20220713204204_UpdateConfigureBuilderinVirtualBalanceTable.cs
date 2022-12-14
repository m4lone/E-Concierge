using Microsoft.EntityFrameworkCore.Migrations;

namespace qodeless.Infra.CrossCutting.Identity.Migrations
{
    public partial class UpdateConfigureBuilderinVirtualBalanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserPlayId",
                table: "VirtualBalance",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserPlayId",
                table: "VirtualBalance",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
