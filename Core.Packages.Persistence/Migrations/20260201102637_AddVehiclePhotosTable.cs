using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVehiclePhotosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Vehicles");

            migrationBuilder.CreateTable(
                name: "VehiclePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadedFileId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PhotoType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedByEmployeeId = table.Column<int>(type: "int", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiclePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehiclePhotos_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehiclePhotos_Employees_UploadedByEmployeeId",
                        column: x => x.UploadedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_VehiclePhotos_UploadedFiles_UploadedFileId",
                        column: x => x.UploadedFileId,
                        principalTable: "UploadedFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_VehiclePhotos_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 26, 35, 723, DateTimeKind.Utc).AddTicks(1287), new DateTime(2027, 2, 1, 10, 26, 35, 723, DateTimeKind.Utc).AddTicks(1061), new DateTime(2026, 2, 1, 10, 26, 35, 723, DateTimeKind.Utc).AddTicks(795) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 26, 35, 723, DateTimeKind.Utc).AddTicks(1711), new DateTime(2027, 2, 1, 10, 26, 35, 723, DateTimeKind.Utc).AddTicks(1707), new DateTime(2026, 2, 1, 10, 26, 35, 723, DateTimeKind.Utc).AddTicks(1706) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(9253));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(9253));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(9253));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(9253));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(9253));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(9253));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(9253));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(9253));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(2718));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(3289));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(3315));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(3331));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(3344));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(3356));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(3368));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(3379));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 740, DateTimeKind.Utc).AddTicks(3390));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2438));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2442));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2443));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2444));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2445));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2445));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2446));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2446));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2447));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2447));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2448));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2449));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2449));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2450));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2451));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2451));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2452));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2452));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2453));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2453));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2454));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2454));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2455));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2820));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2821));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2821));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2822));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2822));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2823));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2824));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2824));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2825));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2826));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2826));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2827));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2827));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2828));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2828));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2830));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2830));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2831));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2832));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2837));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2837));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2838));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2838));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2839));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2839));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2840));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2840));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2841));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2841));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2842));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2842));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2843));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2843));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2844));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2845));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2845));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2846));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2846));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2847));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2847));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2855));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(2856));

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 2, 1, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 1, 9, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 1, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 31, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 31, 11, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 31, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 31, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 26, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 27, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 30, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 31, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 1, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 2, 1, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 1, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 2, 1, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 2, 1, 16, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 31, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 31, 11, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 31, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 31, 15, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 26, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 26, 9, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 27, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 27, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 28, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 28, 11, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 29, 13, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 29, 13, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 30, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 30, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 31, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 31, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 17, 10, 26, 35, 750, DateTimeKind.Utc).AddTicks(8758), new DateTime(2026, 1, 17, 10, 26, 35, 750, DateTimeKind.Utc).AddTicks(8758) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 12, 10, 26, 35, 750, DateTimeKind.Utc).AddTicks(8758), new DateTime(2026, 1, 12, 10, 26, 35, 750, DateTimeKind.Utc).AddTicks(8758) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 7, 10, 26, 35, 750, DateTimeKind.Utc).AddTicks(8758), new DateTime(2026, 1, 7, 10, 26, 35, 750, DateTimeKind.Utc).AddTicks(8758) });

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 727, DateTimeKind.Utc).AddTicks(1625));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 727, DateTimeKind.Utc).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 727, DateTimeKind.Utc).AddTicks(1642));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 727, DateTimeKind.Utc).AddTicks(1644));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 727, DateTimeKind.Utc).AddTicks(1646));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 727, DateTimeKind.Utc).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 727, DateTimeKind.Utc).AddTicks(1652));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 727, DateTimeKind.Utc).AddTicks(1654));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 727, DateTimeKind.Utc).AddTicks(1655));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 727, DateTimeKind.Utc).AddTicks(1657));

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 11, 1, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 1, 27, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 11, 1, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 1, 29, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 1, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 1, 22, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 1, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 1, 30, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 1, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 1, 31, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 1, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 1, 17, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 1, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 1, 25, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 1, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 1, 27, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 2, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 1, 12, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 12, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390), new DateTime(2026, 1, 7, 10, 26, 35, 753, DateTimeKind.Utc).AddTicks(6390) });

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 1, 10, 26, 35, 752, DateTimeKind.Utc).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 1, 10, 26, 35, 752, DateTimeKind.Utc).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 1, 10, 26, 35, 752, DateTimeKind.Utc).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 1, 10, 26, 35, 752, DateTimeKind.Utc).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 1, 10, 26, 35, 752, DateTimeKind.Utc).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 10, 26, 35, 752, DateTimeKind.Utc).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 10, 26, 35, 752, DateTimeKind.Utc).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 10, 26, 35, 752, DateTimeKind.Utc).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 2, 10, 26, 35, 752, DateTimeKind.Utc).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 12, 10, 26, 35, 752, DateTimeKind.Utc).AddTicks(8152));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8038));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8050));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8052));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8053));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8054));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8058));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8059));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8060));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8062));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8063));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8065));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8066));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8067));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 724, DateTimeKind.Utc).AddTicks(8068));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3431));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3446));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3447));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3448));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3448));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3451));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3452));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3452));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3453));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3454));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3455));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3455));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3456));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3456));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3457));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3457));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3546));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3548));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3548));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3549));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3550));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3550));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3551));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3552));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3552));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3553));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3553));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3554));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3559));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3560));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3560));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3561));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3561));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3562));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3565));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3566));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3566));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3567));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3567));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(3568));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5666));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5671));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5672));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5672));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5673));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5684));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5685));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5686));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5686));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5687));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5688));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5688));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5689));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5689));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5690));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5690));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5701));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5703));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5703));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5704));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5704));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5705));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5705));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5706));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5710));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5711));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5711));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2026, 2, 1, 10, 26, 35, 726, DateTimeKind.Utc).AddTicks(5712));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "6a909a37-d0c9-4dce-b9bc-8134bfbade42");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "7a89cac8-dbf5-4403-b4cc-b7e380fb49b6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "46d1ca03-8b05-44f9-9ec5-d493df30a90c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "f00a81cf-b7ce-4c01-8db6-6fcc6f876c4a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "ffbd0959-e02e-4f13-8d7a-7565a55fa442");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "563ec330-7fb5-4ee9-aa36-23d093e65eed");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "a91cc050-4048-4a90-b9eb-7581fcdd26eb");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 1, 10, 26, 35, 741, DateTimeKind.Utc).AddTicks(9391));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 1, 10, 26, 35, 741, DateTimeKind.Utc).AddTicks(9411));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 1, 10, 26, 35, 741, DateTimeKind.Utc).AddTicks(9414));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 1, 10, 26, 35, 741, DateTimeKind.Utc).AddTicks(9416));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 1, 10, 26, 35, 741, DateTimeKind.Utc).AddTicks(9418));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 1, 10, 26, 35, 741, DateTimeKind.Utc).AddTicks(9423));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 10, 26, 35, 741, DateTimeKind.Utc).AddTicks(9436));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 17, 10, 26, 35, 741, DateTimeKind.Utc).AddTicks(9438));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 10, 26, 35, 741, DateTimeKind.Utc).AddTicks(9441));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 1, 10, 26, 35, 741, DateTimeKind.Utc).AddTicks(9444));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 1, 10, 26, 35, 741, DateTimeKind.Utc).AddTicks(9446));

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 4, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 2, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 3, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 2, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 4, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260102-0001" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 9, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 7, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 8, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 7, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 9, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260107-0002" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 14, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 12, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 13, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 12, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 14, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260112-0003" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 19, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 17, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 18, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 17, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 19, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260117-0004" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 24, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 22, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 23, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 22, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 24, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260122-0005" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 29, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 27, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 28, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 27, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 29, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260127-0006" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 25, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 26, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 25, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 30, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260125-0007" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 26, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 27, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 26, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 31, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260126-0008" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 28, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 29, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 28, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 2, 2, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260128-0009" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 29, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 30, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 29, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 2, 3, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260129-0010" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 30, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 31, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 30, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 2, 4, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260130-0011" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 31, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 31, 22, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 31, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 2, 5, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260131-0012" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 2, 1, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 2, 6, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260201-0013" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 2, 2, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 2, 2, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 2, 7, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260202-0014" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 24, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 25, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 24, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 27, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260124-0015" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 31, 22, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 2, 1, 0, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 1, 31, 22, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 2, 2, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260131-0016" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 2, 1, 4, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 2, 1, 4, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), new DateTime(2026, 2, 3, 10, 26, 35, 744, DateTimeKind.Utc).AddTicks(2136), "WO-20260201-0017" });

            migrationBuilder.CreateIndex(
                name: "IX_VehiclePhotos_ClientId",
                table: "VehiclePhotos",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclePhotos_UploadedByEmployeeId",
                table: "VehiclePhotos",
                column: "UploadedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclePhotos_UploadedFileId",
                table: "VehiclePhotos",
                column: "UploadedFileId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiclePhotos_VehicleId",
                table: "VehiclePhotos",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehiclePhotos");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Vehicles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 25, 19, 46, 32, 506, DateTimeKind.Utc).AddTicks(2100), new DateTime(2027, 1, 25, 19, 46, 32, 506, DateTimeKind.Utc).AddTicks(1513), new DateTime(2026, 1, 25, 19, 46, 32, 506, DateTimeKind.Utc).AddTicks(1234) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 25, 19, 46, 32, 506, DateTimeKind.Utc).AddTicks(2718), new DateTime(2027, 1, 25, 19, 46, 32, 506, DateTimeKind.Utc).AddTicks(2708), new DateTime(2026, 1, 25, 19, 46, 32, 506, DateTimeKind.Utc).AddTicks(2708) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(7755));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(381));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(1046));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(1075));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(1089));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(1103));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(1116));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(1128));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(1140));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 526, DateTimeKind.Utc).AddTicks(1151));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4640));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4646));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4647));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4648));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4648));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4649));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4650));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4650));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4651));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4651));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4652));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4662));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4663));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4664));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4664));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4665));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4666));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4666));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4667));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4667));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4668));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4668));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5051));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5052));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5053));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5053));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5054));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5054));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5055));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5056));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5056));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5057));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5057));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5058));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5059));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5059));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5060));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5060));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5061));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5061));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5062));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5063));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5063));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5064));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5064));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5071));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5071));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5072));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5072));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5073));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5074));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5074));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5075));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5075));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5076));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5077));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5077));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5078));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5078));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5079));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5080));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5080));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5081));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5081));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5082));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5082));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5083));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 507, DateTimeKind.Utc).AddTicks(5084));

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 25, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 25, 9, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 25, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 25, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 24, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 24, 11, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 24, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 24, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 21, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 22, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 24, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 25, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 25, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 25, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 25, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 25, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 25, 16, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 24, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 24, 11, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 24, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 24, 15, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 19, 9, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 19, 9, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 20, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 20, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 21, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 21, 11, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 22, 13, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 22, 13, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 23, 14, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 23, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 24, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 1, 24, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 10, 19, 46, 32, 538, DateTimeKind.Utc).AddTicks(2018), new DateTime(2026, 1, 10, 19, 46, 32, 538, DateTimeKind.Utc).AddTicks(2018) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 5, 19, 46, 32, 538, DateTimeKind.Utc).AddTicks(2018), new DateTime(2026, 1, 5, 19, 46, 32, 538, DateTimeKind.Utc).AddTicks(2018) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2025, 12, 31, 19, 46, 32, 538, DateTimeKind.Utc).AddTicks(2018), new DateTime(2025, 12, 31, 19, 46, 32, 538, DateTimeKind.Utc).AddTicks(2018) });

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(9807));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(9826));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(9829));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(9831));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(9833));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(9838));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(9840));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(9842));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(9843));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(9846));

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 10, 25, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081), new DateTime(2026, 1, 20, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 10, 25, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081), new DateTime(2026, 1, 22, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 11, 25, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081), new DateTime(2026, 1, 15, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 11, 25, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081), new DateTime(2026, 1, 23, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 11, 25, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081), new DateTime(2026, 1, 24, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 25, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081), new DateTime(2026, 1, 10, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 25, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081), new DateTime(2026, 1, 18, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 25, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081), new DateTime(2026, 1, 20, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 26, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081), new DateTime(2026, 1, 5, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 5, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081), new DateTime(2025, 12, 31, 19, 46, 32, 541, DateTimeKind.Utc).AddTicks(6081) });

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 19, 46, 32, 540, DateTimeKind.Utc).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 19, 46, 32, 540, DateTimeKind.Utc).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 19, 46, 32, 540, DateTimeKind.Utc).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 19, 46, 32, 540, DateTimeKind.Utc).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 19, 46, 32, 540, DateTimeKind.Utc).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 19, 46, 32, 540, DateTimeKind.Utc).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 19, 46, 32, 540, DateTimeKind.Utc).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 19, 46, 32, 540, DateTimeKind.Utc).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 26, 19, 46, 32, 540, DateTimeKind.Utc).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 5, 19, 46, 32, 540, DateTimeKind.Utc).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(915));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(932));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(933));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(935));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(936));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(940));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(941));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(943));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(944));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(945));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(946));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(948));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(949));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(950));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(951));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 508, DateTimeKind.Utc).AddTicks(952));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9130));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9151));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9154));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9158));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9159));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9160));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9174));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9177));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9178));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9178));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9179));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9180));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9180));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9181));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9373));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9377));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9378));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9379));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9379));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9380));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9381));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9382));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9382));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9383));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9384));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9384));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9392));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9393));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9393));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9394));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9395));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9397));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9401));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9402));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9403));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9404));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9405));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 509, DateTimeKind.Utc).AddTicks(9406));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1974));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1981));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1983));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1983));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1984));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1989));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1989));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1990));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1991));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1993));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1993));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1994));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1995));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(1995));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2006));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2007));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2024));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2027));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2028));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2029));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2030));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2031));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2032));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2033));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2038));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2040));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2040));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 19, 46, 32, 510, DateTimeKind.Utc).AddTicks(2041));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c05d2950-1928-4ac4-b27e-29b786e62764");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "21ed5f49-3d3e-4305-8399-8f80fbbc25bf");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ab509ab5-ddb8-4022-a35f-cf9529eab37f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "eb1e185a-f00a-4e0a-9c72-3262e63f3130");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "5ee09dd7-dde6-459a-ae1e-7a317bd7f151");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "4cb89c24-6e08-4d49-be5d-002059eca6cc");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "1f9a0f59-8b89-4f50-99ac-7f9b3f8453e3");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2025, 7, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8792), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8830), "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2025, 8, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8834), "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2025, 9, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8836), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8839), "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2025, 11, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8844), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2025, 12, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8846), "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2026, 1, 10, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8859), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2026, 1, 18, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8863), "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2025, 9, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8866), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "ImageUrl" },
                values: new object[] { new DateTime(2025, 10, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8868), "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2025, 12, 28, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2025, 12, 26, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2025, 12, 27, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2025, 12, 26, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2025, 12, 28, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20251226-0001" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 2, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2025, 12, 31, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 1, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2025, 12, 31, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 2, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20251231-0002" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 7, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 5, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 6, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 5, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 7, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260105-0003" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 12, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 10, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 11, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 10, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 12, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260110-0004" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 17, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 15, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 16, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 15, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 17, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260115-0005" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 22, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 20, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 21, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 20, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 22, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260120-0006" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 18, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 19, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 18, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 23, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260118-0007" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 19, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 20, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 19, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 24, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260119-0008" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 21, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 22, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 21, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 26, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260121-0009" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 22, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 23, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 22, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 27, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260122-0010" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 24, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 23, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 28, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260123-0011" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 24, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 25, 7, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 24, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 29, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260124-0012" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 25, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 25, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 30, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260125-0013" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 26, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 26, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 31, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260126-0014" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 17, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 18, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 17, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 20, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260117-0015" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 25, 7, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 25, 9, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 25, 7, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 26, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260125-0016" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate", "WorkOrderNumber" },
                values: new object[] { new DateTime(2026, 1, 25, 13, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 25, 13, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 27, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), "WO-20260125-0017" });
        }
    }
}
