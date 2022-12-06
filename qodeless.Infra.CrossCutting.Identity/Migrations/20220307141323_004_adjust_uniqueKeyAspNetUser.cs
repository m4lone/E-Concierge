using Microsoft.EntityFrameworkCore.Migrations;

namespace qodeless.Infra.CrossCutting.Identity.Migrations
{
    public partial class _004_adjust_uniqueKeyAspNetUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Cpf_PixKey_PhoneNumber",
                table: "AspNetUsers",
                columns: new[] { "Cpf", "PixKey", "PhoneNumber" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Cpf_PixKey_PhoneNumber",
                table: "AspNetUsers");
        }
    }
}
