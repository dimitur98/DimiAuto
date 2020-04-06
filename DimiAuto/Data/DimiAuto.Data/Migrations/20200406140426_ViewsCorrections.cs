using Microsoft.EntityFrameworkCore.Migrations;

namespace DimiAuto.Data.Migrations
{
    public partial class ViewsCorrections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Views_AspNetUsers_UserId",
                table: "Views");

            migrationBuilder.DropIndex(
                name: "IX_Views_UserId",
                table: "Views");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Views",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Views",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Views_UserId",
                table: "Views",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Views_AspNetUsers_UserId",
                table: "Views",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
