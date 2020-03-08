using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DimiAuto.Data.Migrations
{
    public partial class InitialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bulstad",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOfCompany",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameOfThePage",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TelephoneForCustomers",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    TypeOfAd = table.Column<int>(nullable: false),
                    Condition = table.Column<int>(nullable: false),
                    Make = table.Column<int>(nullable: false),
                    Model = table.Column<string>(nullable: true),
                    Modification = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Gearbox = table.Column<int>(nullable: false),
                    Fuel = table.Column<int>(nullable: false),
                    Horsepowers = table.Column<int>(nullable: false),
                    Cc = table.Column<int>(nullable: false),
                    YearOfProduction = table.Column<DateTime>(nullable: false),
                    Km = table.Column<int>(nullable: false),
                    Doors = table.Column<int>(nullable: false),
                    Color = table.Column<int>(nullable: false),
                    EuroStandart = table.Column<int>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    MoreInformation = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AirCondiitioning = table.Column<string>(nullable: true),
                    Climatronic = table.Column<string>(nullable: true),
                    LeatherInterior = table.Column<string>(nullable: true),
                    ElectrinWindows = table.Column<string>(nullable: true),
                    ElectricMirrors = table.Column<string>(nullable: true),
                    ElectricSeats = table.Column<string>(nullable: true),
                    SeatHeating = table.Column<string>(nullable: true),
                    Sunroof = table.Column<string>(nullable: true),
                    Stereo = table.Column<string>(nullable: true),
                    AlloyWheels = table.Column<string>(nullable: true),
                    DvdTV = table.Column<string>(nullable: true),
                    MultiSteeringWheel = table.Column<string>(nullable: true),
                    AllWheelDrive = table.Column<string>(nullable: true),
                    ABS = table.Column<string>(nullable: true),
                    ESP = table.Column<string>(nullable: true),
                    Airbag = table.Column<string>(nullable: true),
                    Xenonlights = table.Column<string>(nullable: true),
                    HalogenHeadlights = table.Column<string>(nullable: true),
                    TractionControl = table.Column<string>(nullable: true),
                    Parktronic = table.Column<string>(nullable: true),
                    Alarm = table.Column<string>(nullable: true),
                    Immobilizer = table.Column<string>(nullable: true),
                    CentralLock = table.Column<string>(nullable: true),
                    Insurance = table.Column<string>(nullable: true),
                    Armored = table.Column<string>(nullable: true),
                    StartStopSystem = table.Column<string>(nullable: true),
                    KeylessGo = table.Column<string>(nullable: true),
                    TiptronicMultitronic = table.Column<string>(nullable: true),
                    Autopilot = table.Column<string>(nullable: true),
                    PowerSteering = table.Column<string>(nullable: true),
                    OnboardComputer = table.Column<string>(nullable: true),
                    ServiceBook = table.Column<string>(nullable: true),
                    Warranty = table.Column<string>(nullable: true),
                    NavigationSystem = table.Column<string>(nullable: true),
                    RightHandDrive = table.Column<string>(nullable: true),
                    Tuning = table.Column<string>(nullable: true),
                    PanoramicRoof = table.Column<string>(nullable: true),
                    Taxi = table.Column<string>(nullable: true),
                    Retro = table.Column<string>(nullable: true),
                    Tow = table.Column<string>(nullable: true),
                    MoreSeats = table.Column<string>(nullable: true),
                    Refrigerated = table.Column<string>(nullable: true),
                    CarId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Extras_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_IsDeleted",
                table: "Cars",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_UserId",
                table: "Cars",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Extras_CarId",
                table: "Extras",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Extras");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Bulstad",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NameOfCompany",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NameOfThePage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TelephoneForCustomers",
                table: "AspNetUsers");
        }
    }
}
