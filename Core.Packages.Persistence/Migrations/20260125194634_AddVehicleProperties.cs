using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Vehicles",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelVariant",
                table: "Vehicles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Trim",
                table: "Vehicles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vin",
                table: "Vehicles",
                type: "nvarchar(50)",
                maxLength: 50,
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
                columns: new[] { "CreatedDate", "ImageUrl", "ModelVariant", "Trim", "Vin" },
                values: new object[] { new DateTime(2025, 7, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8792), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800", "XLI", "Comfort", "JT2BF28K504123456" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ImageUrl", "ModelVariant", "Trim", "Vin" },
                values: new object[] { new DateTime(2025, 8, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8830), "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800", "Sedan", "EX", "19XFC2F59KE123456" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ImageUrl", "ModelVariant", "Trim", "Vin" },
                values: new object[] { new DateTime(2025, 8, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8834), "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800", "Hatchback", "Highline", "WVWZZZ1KZBW123456" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "ImageUrl", "ModelVariant", "Trim", "Vin" },
                values: new object[] { new DateTime(2025, 9, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8836), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800", "Hatchback", "Titanium", "WF0AXXWPW8H123456" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "ImageUrl", "ModelVariant", "Trim", "Vin" },
                values: new object[] { new DateTime(2025, 10, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8839), "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800", "Sedan", "Zen", "VF1RZ0H0Y12345678" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "ImageUrl", "ModelVariant", "Trim", "Vin" },
                values: new object[] { new DateTime(2025, 11, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8844), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800", "Sedan", "Premium", "KMHDN45D5KU123456" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "ImageUrl", "ModelVariant", "Trim", "Vin" },
                values: new object[] { new DateTime(2025, 12, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8846), "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800", "Sedan", "320i", "WBA3A5C59EK123456" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "ImageUrl", "ModelVariant", "Trim", "Vin" },
                values: new object[] { new DateTime(2026, 1, 10, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8859), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800", "Sedan", "C200", "WDDWF4KB5LR123456" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "ImageUrl", "ModelVariant", "Trim", "Vin" },
                values: new object[] { new DateTime(2026, 1, 18, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8863), "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800", "Sedan", "Premium", "WAUZZZ8K9KA123456" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "ImageUrl", "ModelVariant", "Trim", "Vin" },
                values: new object[] { new DateTime(2025, 9, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8866), "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?w=800", "Hatchback", "Elegance", "W0L0ZCF5812345678" });

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "ImageUrl", "ModelVariant", "Trim", "Vin" },
                values: new object[] { new DateTime(2025, 10, 25, 19, 46, 32, 527, DateTimeKind.Utc).AddTicks(8868), "https://images.unsplash.com/photo-1606664515524-ed2f786a0bd6?w=800", "Hatchback", "Allure", "VF3XXXXXXXXX123456" });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 12, 28, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2025, 12, 26, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2025, 12, 27, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2025, 12, 26, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2025, 12, 28, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 2, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2025, 12, 31, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 1, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2025, 12, 31, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 2, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 7, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 5, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 6, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 5, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 7, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 12, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 10, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 11, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 10, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 12, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 17, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 15, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 16, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 15, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 17, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 22, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 20, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 21, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 20, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 22, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 18, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 19, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 18, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 23, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 19, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 20, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 19, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 24, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 21, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 22, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 21, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 26, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 22, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 23, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 22, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 27, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 23, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 24, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 23, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 28, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 24, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 25, 7, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 24, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 29, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 25, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 25, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 30, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 26, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 26, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 31, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 17, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 18, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 17, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 20, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 25, 7, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 25, 9, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 25, 7, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 26, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 25, 13, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 25, 13, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949), new DateTime(2026, 1, 27, 19, 46, 32, 530, DateTimeKind.Utc).AddTicks(3949) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "ModelVariant",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Trim",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Vin",
                table: "Vehicles");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 25, 17, 14, 58, 853, DateTimeKind.Utc).AddTicks(1286), new DateTime(2027, 1, 25, 17, 14, 58, 853, DateTimeKind.Utc).AddTicks(703), new DateTime(2026, 1, 25, 17, 14, 58, 853, DateTimeKind.Utc).AddTicks(215) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 25, 17, 14, 58, 853, DateTimeKind.Utc).AddTicks(2028), new DateTime(2027, 1, 25, 17, 14, 58, 853, DateTimeKind.Utc).AddTicks(2022), new DateTime(2026, 1, 25, 17, 14, 58, 853, DateTimeKind.Utc).AddTicks(2021) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 25, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 25, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 25, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 877, DateTimeKind.Utc).AddTicks(4971));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 877, DateTimeKind.Utc).AddTicks(5811));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 877, DateTimeKind.Utc).AddTicks(5849));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 877, DateTimeKind.Utc).AddTicks(5871));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 877, DateTimeKind.Utc).AddTicks(5891));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 877, DateTimeKind.Utc).AddTicks(5909));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 877, DateTimeKind.Utc).AddTicks(5926));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 877, DateTimeKind.Utc).AddTicks(5943));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 877, DateTimeKind.Utc).AddTicks(5959));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(11));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(21));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(23));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(24));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(26));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(27));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(27));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(28));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(30));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(31));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(32));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(33));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(35));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(36));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(37));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(38));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(39));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(40));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(41));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(42));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(43));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(44));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(45));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(720));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(723));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(724));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(725));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(726));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(727));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(728));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(729));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(730));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(731));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(732));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(733));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(734));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(735));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(736));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(737));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(738));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(739));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(741));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(742));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(743));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(744));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(745));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(755));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(769));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(770));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(771));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(772));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(773));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(774));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(775));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(776));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(776));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(777));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(779));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(780));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(781));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(782));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(783));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(784));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(785));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(786));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(787));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(788));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(789));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(790));

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 10, 17, 14, 58, 894, DateTimeKind.Utc).AddTicks(853), new DateTime(2026, 1, 10, 17, 14, 58, 894, DateTimeKind.Utc).AddTicks(853) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 5, 17, 14, 58, 894, DateTimeKind.Utc).AddTicks(853), new DateTime(2026, 1, 5, 17, 14, 58, 894, DateTimeKind.Utc).AddTicks(853) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2025, 12, 31, 17, 14, 58, 894, DateTimeKind.Utc).AddTicks(853), new DateTime(2025, 12, 31, 17, 14, 58, 894, DateTimeKind.Utc).AddTicks(853) });

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 859, DateTimeKind.Utc).AddTicks(544));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 859, DateTimeKind.Utc).AddTicks(566));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 859, DateTimeKind.Utc).AddTicks(569));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 859, DateTimeKind.Utc).AddTicks(572));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 859, DateTimeKind.Utc).AddTicks(574));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 859, DateTimeKind.Utc).AddTicks(580));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 859, DateTimeKind.Utc).AddTicks(591));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 859, DateTimeKind.Utc).AddTicks(594));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 859, DateTimeKind.Utc).AddTicks(596));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 859, DateTimeKind.Utc).AddTicks(598));

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 10, 25, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918), new DateTime(2026, 1, 20, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 10, 25, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918), new DateTime(2026, 1, 22, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 11, 25, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918), new DateTime(2026, 1, 15, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 11, 25, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918), new DateTime(2026, 1, 23, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 11, 25, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918), new DateTime(2026, 1, 24, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 25, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918), new DateTime(2026, 1, 10, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 25, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918), new DateTime(2026, 1, 18, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 25, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918), new DateTime(2026, 1, 20, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 26, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918), new DateTime(2026, 1, 5, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 5, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918), new DateTime(2025, 12, 31, 17, 14, 58, 898, DateTimeKind.Utc).AddTicks(5918) });

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 17, 14, 58, 897, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 17, 14, 58, 897, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 17, 14, 58, 897, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 17, 14, 58, 897, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 17, 14, 58, 897, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 17, 14, 58, 897, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 17, 14, 58, 897, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 17, 14, 58, 897, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 26, 17, 14, 58, 897, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 5, 17, 14, 58, 897, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9798));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9819));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9820));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9822));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9823));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9828));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9829));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9831));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9832));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9834));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9836));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9837));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9838));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9840));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9841));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 855, DateTimeKind.Utc).AddTicks(9842));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9726));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9748));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9749));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9750));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9751));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9755));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9756));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9756));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9757));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9758));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9759));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9760));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9761));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9761));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9762));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9763));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9873));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9876));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9877));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9878));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9878));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9879));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9880));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9880));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9881));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9882));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9882));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9883));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9890));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9891));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9892));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9895));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9900));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9901));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9901));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9902));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9903));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 857, DateTimeKind.Utc).AddTicks(9915));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2327));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2334));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2336));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2336));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2337));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2340));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2341));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2342));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2343));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2344));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2345));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2346));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2347));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2347));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2348));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2349));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2365));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2368));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2368));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2369));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2370));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2370));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2371));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2377));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2378));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2379));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 17, 14, 58, 858, DateTimeKind.Utc).AddTicks(2380));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "ff43ba06-18f1-49eb-95c8-e647a62cc42b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c4d1ca42-282b-4184-9145-435bc9218eef");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e8aff8a5-3556-4168-89e0-087bb67e0dfb");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "15ebd1e1-856a-40cc-bde3-c5553d961d11");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "75b179d7-81a8-414d-8080-45a805501402");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "bb63da73-0a63-4d50-a11b-8defeacad5ca");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "edcb5ea2-3b64-4997-ae2b-84b7c71a9273");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 25, 17, 14, 58, 879, DateTimeKind.Utc).AddTicks(9702));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 25, 17, 14, 58, 879, DateTimeKind.Utc).AddTicks(9722));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 25, 17, 14, 58, 879, DateTimeKind.Utc).AddTicks(9724));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 25, 17, 14, 58, 879, DateTimeKind.Utc).AddTicks(9726));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 17, 14, 58, 879, DateTimeKind.Utc).AddTicks(9728));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 17, 14, 58, 879, DateTimeKind.Utc).AddTicks(9733));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 17, 14, 58, 879, DateTimeKind.Utc).AddTicks(9734));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 17, 14, 58, 879, DateTimeKind.Utc).AddTicks(9736));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 17, 14, 58, 879, DateTimeKind.Utc).AddTicks(9739));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 25, 17, 14, 58, 879, DateTimeKind.Utc).AddTicks(9751));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 17, 14, 58, 879, DateTimeKind.Utc).AddTicks(9753));

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 12, 28, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2025, 12, 26, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2025, 12, 27, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2025, 12, 26, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2025, 12, 28, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 2, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2025, 12, 31, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 1, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2025, 12, 31, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 2, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 7, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 5, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 6, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 5, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 7, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 12, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 10, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 11, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 10, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 12, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 17, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 15, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 16, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 15, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 17, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 22, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 20, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 21, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 20, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 22, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 18, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 19, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 18, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 23, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 19, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 20, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 19, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 24, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 21, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 22, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 21, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 26, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 22, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 23, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 22, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 27, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 23, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 24, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 23, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 28, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 24, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 25, 5, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 24, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 29, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 25, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 25, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 30, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 26, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 26, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 31, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 17, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 18, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 17, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 20, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 25, 5, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 25, 7, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 25, 5, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 26, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 25, 11, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 27, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });
        }
    }
}
