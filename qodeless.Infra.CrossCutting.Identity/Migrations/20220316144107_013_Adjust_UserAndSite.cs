using Microsoft.EntityFrameworkCore.Migrations;

namespace qodeless.Infra.CrossCutting.Identity.Migrations
{
    public partial class _013_Adjust_UserAndSite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAutoLogin",
                table: "AspNetUsers",
                newName: "RegisterCompleted");

            migrationBuilder.AddColumn<string>(
                name: "Neighborhood",
                table: "Site",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Neighborhood",
                table: "Site");

            migrationBuilder.RenameColumn(
                name: "RegisterCompleted",
                table: "AspNetUsers",
                newName: "IsAutoLogin");
        }
    }
}
