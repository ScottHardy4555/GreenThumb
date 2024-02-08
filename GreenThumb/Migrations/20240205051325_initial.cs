using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenThumb.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyLogs",
                columns: table => new
                {
                    DailyLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyLogs", x => x.DailyLogId);
                });

            migrationBuilder.CreateTable(
                name: "StatusReports",
                columns: table => new
                {
                    StatusReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyLogId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: true),
                    Humidity = table.Column<float>(type: "real", nullable: true),
                    PartsPerMillion = table.Column<float>(type: "real", nullable: true),
                    ElectricalConductivity = table.Column<float>(type: "real", nullable: true),
                    PH = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusReports", x => x.StatusReportId);
                });

            migrationBuilder.InsertData(
                table: "DailyLogs",
                columns: new[] { "DailyLogId", "Date" },
                values: new object[] { 1, new DateOnly(2024, 1, 31) });

            migrationBuilder.InsertData(
                table: "StatusReports",
                columns: new[] { "StatusReportId", "DailyLogId", "ElectricalConductivity", "Humidity", "PH", "PartsPerMillion", "Temperature", "Time" },
                values: new object[,]
                {
                    { 1, 1, 128f, 60f, 7.2f, 10f, 29f, new TimeOnly(0, 0, 0) },
                    { 2, 1, 128f, 62f, 7.1f, 10f, 22.5f, new TimeOnly(1, 0, 0) },
                    { 3, 1, 128f, 64.5f, 7.2f, 10f, 20.1f, new TimeOnly(2, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyLogs");

            migrationBuilder.DropTable(
                name: "StatusReports");
        }
    }
}
