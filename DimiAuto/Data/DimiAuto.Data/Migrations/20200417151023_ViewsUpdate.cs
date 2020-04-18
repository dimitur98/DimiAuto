using Microsoft.EntityFrameworkCore.Migrations;

namespace DimiAuto.Data.Migrations
{
    public partial class ViewsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Views",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CarId",
                table: "Views",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<string>(
                name: "User",
                table: "Views",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CarId",
                table: "Views",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

           
        }
    }
}
