using Microsoft.EntityFrameworkCore.Migrations;

namespace qodeless.Infra.CrossCutting.Identity.Migrations
{
    public partial class UpdateNameColumnInCardStonesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoneNumbers",
                table: "CardStones",
                newName: "StoneNumbersList");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoneNumbersList",
                table: "CardStones",
                newName: "StoneNumbers");
        }
    }
}
