using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DimiAuto.Data.Migrations
{
    public partial class AddTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "Cars");

            migrationBuilder.CreateTable(
                name: "Views",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CarId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Views", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Views_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Views_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Views_CarId",
                table: "Views",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Views_UserId",
                table: "Views",
                column: "UserId");

            //
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

            //
            migrationBuilder.DropTable(
               name: "Views");

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

        }
    }
}
