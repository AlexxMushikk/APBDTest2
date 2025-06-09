using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Test2C.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.RaceId);
                });

            migrationBuilder.CreateTable(
                name: "Racer",
                columns: table => new
                {
                    RacerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racer", x => x.RacerId);
                });

            migrationBuilder.CreateTable(
                name: "Track",
                columns: table => new
                {
                    TrackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LengthInKm = table.Column<double>(type: "float(5)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track", x => x.TrackId);
                });

            migrationBuilder.CreateTable(
                name: "Track_Race",
                columns: table => new
                {
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    BestTimeInSeconds = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track_Race", x => new { x.RaceId, x.TrackId });
                    table.ForeignKey(
                        name: "FK_Track_Race_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "RaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Track_Race_Track_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Track",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participation",
                columns: table => new
                {
                    ParticipationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RacerId = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    TrackId = table.Column<int>(type: "int", nullable: false),
                    Laps = table.Column<int>(type: "int", nullable: false),
                    FinishTimeInSeconds = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participation", x => x.ParticipationId);
                    table.ForeignKey(
                        name: "FK_Participation_Racer_RacerId",
                        column: x => x.RacerId,
                        principalTable: "Racer",
                        principalColumn: "RacerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Participation_Track_Race_RaceId_TrackId",
                        columns: x => new { x.RaceId, x.TrackId },
                        principalTable: "Track_Race",
                        principalColumns: new[] { "RaceId", "TrackId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Race",
                columns: new[] { "RaceId", "Date", "Location", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Silverstone, UK", "British Grand Prix" },
                    { 2, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monte Carlo, Monaco", "Monaco Grand Prix" }
                });

            migrationBuilder.InsertData(
                table: "Racer",
                columns: new[] { "RacerId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Lewis", "Hamilton" },
                    { 2, "Raamon", "Make" }
                });

            migrationBuilder.InsertData(
                table: "Track",
                columns: new[] { "TrackId", "LengthInKm", "Name" },
                values: new object[,]
                {
                    { 1, 5.8899999999999997, "Silverstone Circuit" },
                    { 2, 3.3399999999999999, "Monaco Circuit" }
                });

            migrationBuilder.InsertData(
                table: "Track_Race",
                columns: new[] { "RaceId", "TrackId", "BestTimeInSeconds" },
                values: new object[,]
                {
                    { 1, 1, 5460 },
                    { 2, 2, 6300 }
                });

            migrationBuilder.InsertData(
                table: "Participation",
                columns: new[] { "ParticipationId", "FinishTimeInSeconds", "Laps", "Position", "RaceId", "RacerId", "TrackId" },
                values: new object[,]
                {
                    { 1, 5460, 52, 1, 1, 1, 1 },
                    { 2, 6300, 78, 2, 2, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participation_RaceId_TrackId",
                table: "Participation",
                columns: new[] { "RaceId", "TrackId" });

            migrationBuilder.CreateIndex(
                name: "IX_Participation_RacerId",
                table: "Participation",
                column: "RacerId");

            migrationBuilder.CreateIndex(
                name: "IX_Track_Race_TrackId",
                table: "Track_Race",
                column: "TrackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participation");

            migrationBuilder.DropTable(
                name: "Racer");

            migrationBuilder.DropTable(
                name: "Track_Race");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "Track");
        }
    }
}
