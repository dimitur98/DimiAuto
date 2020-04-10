using Microsoft.EntityFrameworkCore.Migrations;

namespace DimiAuto.Data.Migrations
{
    public partial class ChangeViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Views");

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Views",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Views");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Views",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
