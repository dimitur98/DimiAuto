using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DimiAuto.Data.Migrations
{
    public partial class AddSearchHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "UserImg",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SearchModels",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Make = table.Column<int>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    Condition = table.Column<int>(nullable: false),
                    Fuel = table.Column<int>(nullable: false),
                    GearBox = table.Column<int>(nullable: false),
                    YearFrom = table.Column<int>(nullable: true),
                    YearTo = table.Column<int>(nullable: true),
                    PriceFrom = table.Column<decimal>(nullable: true),
                    PriceTo = table.Column<decimal>(nullable: true),
                    TypeOfVeichle = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchModels", x => x.Id);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_SearchModels_IsDeleted",
                table: "SearchModels",
                column: "IsDeleted");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.DropTable(
                name: "SearchModels");

          

            migrationBuilder.DropColumn(
                name: "UserImg",
                table: "AspNetUsers");

            
        }
    }
}
