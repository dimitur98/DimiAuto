using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DimiAuto.Data.Migrations
{
    public partial class AddTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Views",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Views",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Views_IsDeleted",
                table: "Views",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Views_IsDeleted",
                table: "Views");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Views");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Views");
        }
    }
}
