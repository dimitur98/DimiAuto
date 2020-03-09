using Microsoft.EntityFrameworkCore.Migrations;

namespace DimiAuto.Data.Migrations
{
    public partial class UpdateCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Extras");

            migrationBuilder.DropColumn(
                name: "Doors",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "Door",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Extras",
                table: "Cars",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Door",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Extras",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "Doors",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ABS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirCondiitioning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Airbag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alarm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllWheelDrive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlloyWheels = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Armored = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Autopilot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CentralLock = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Climatronic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DvdTV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ESP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElectricMirrors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElectricSeats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElectrinWindows = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HalogenHeadlights = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Immobilizer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Insurance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeylessGo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeatherInterior = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoreSeats = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MultiSteeringWheel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavigationSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnboardComputer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PanoramicRoof = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parktronic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PowerSteering = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refrigerated = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Retro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RightHandDrive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatHeating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceBook = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartStopSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stereo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sunroof = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taxi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TiptronicMultitronic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TractionControl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tuning = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Warranty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xenonlights = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_Extras_CarId",
                table: "Extras",
                column: "CarId");
        }
    }
}
