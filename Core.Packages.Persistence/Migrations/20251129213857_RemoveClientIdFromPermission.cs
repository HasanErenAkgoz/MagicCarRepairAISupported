using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveClientIdFromPermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Clients_ClientId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ClientId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_Name_ClientId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Permissions");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2025, 11, 29, 21, 38, 56, 973, DateTimeKind.Utc).AddTicks(4238), new DateTime(2026, 11, 29, 21, 38, 56, 973, DateTimeKind.Utc).AddTicks(4011), new DateTime(2025, 11, 29, 21, 38, 56, 973, DateTimeKind.Utc).AddTicks(3754) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2025, 11, 29, 21, 38, 56, 973, DateTimeKind.Utc).AddTicks(4561), new DateTime(2026, 11, 29, 21, 38, 56, 973, DateTimeKind.Utc).AddTicks(4558), new DateTime(2025, 11, 29, 21, 38, 56, 973, DateTimeKind.Utc).AddTicks(4557) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 988, DateTimeKind.Utc).AddTicks(6674));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 988, DateTimeKind.Utc).AddTicks(7177));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 988, DateTimeKind.Utc).AddTicks(7205));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 988, DateTimeKind.Utc).AddTicks(7218));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 988, DateTimeKind.Utc).AddTicks(7230));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 988, DateTimeKind.Utc).AddTicks(7242));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 988, DateTimeKind.Utc).AddTicks(7253));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 988, DateTimeKind.Utc).AddTicks(7264));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 988, DateTimeKind.Utc).AddTicks(7274));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5027));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5031));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5032));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5033));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5033));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5034));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5035));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5045));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5046));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5063));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5063));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5064));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5064));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5065));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5066));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5066));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5067));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5067));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5068));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5069));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5069));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5070));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5450));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5451));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5452));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5452));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5453));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5454));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5454));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5455));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5455));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5456));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5456));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5457));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5457));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5458));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5459));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5459));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5460));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5460));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5461));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5461));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5462));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5462));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5463));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5469));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5470));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5471));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5471));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5472));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5472));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5473));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5473));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5474));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5474));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5475));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5475));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5476));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5476));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5477));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5477));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5478));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5479));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5479));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5480));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5480));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5481));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 974, DateTimeKind.Utc).AddTicks(5481));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(392));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(404));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(406));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(407));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(408));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(412));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(413));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(414));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(415));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(417));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(418));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(419));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(420));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(421));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(422));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 975, DateTimeKind.Utc).AddTicks(423));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5092));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5107));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5108));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5108));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5109));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5121));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5122));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5122));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5123));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5124));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5125));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5125));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5126));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5126));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5127));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5127));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5200));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5203));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5204));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5204));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5205));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5206));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5206));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5207));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5208));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5209));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5212));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5213));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5214));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5214));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5215));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5216));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5219));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5220));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5220));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5221));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5221));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(5222));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7301));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7307));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7308));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7309));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7309));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7312));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7312));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7313));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7313));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7314));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7315));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7323));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7324));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7325));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7325));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7326));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7337));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7338));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7339));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7339));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7340));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7340));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7341));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7341));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7344));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7345));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7345));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 38, 56, 976, DateTimeKind.Utc).AddTicks(7346));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c4f0149b-4126-4cee-83e7-7671a274e1c6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "92ea863b-79af-4932-9afb-4df9fe222d3a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3ffd6250-78b5-4937-8f76-e46ed4bc20cd");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "e1c1cab3-8382-4211-bf1e-4f879999ce62");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "0ae69a59-492f-43b0-b80a-c0e0b3fa4e29");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "cb575d76-4820-45f2-b625-1152f8da235c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "527c2ddb-5c8f-478a-9e68-2fc7355cead8");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_Name",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2025, 11, 29, 21, 34, 54, 624, DateTimeKind.Utc).AddTicks(6529), new DateTime(2026, 11, 29, 21, 34, 54, 624, DateTimeKind.Utc).AddTicks(6295), new DateTime(2025, 11, 29, 21, 34, 54, 624, DateTimeKind.Utc).AddTicks(6036) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2025, 11, 29, 21, 34, 54, 624, DateTimeKind.Utc).AddTicks(6961), new DateTime(2026, 11, 29, 21, 34, 54, 624, DateTimeKind.Utc).AddTicks(6956), new DateTime(2025, 11, 29, 21, 34, 54, 624, DateTimeKind.Utc).AddTicks(6955) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(1497));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(1972));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2026));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2042));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2054));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2066));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2078));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2088));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2099));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7270));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7275));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7276));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7276));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7277));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7278));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7278));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7279));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7280));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7280));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7291));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7292));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7292));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7293));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7293));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7294));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7294));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7295));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7295));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7296));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7297));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7314));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7315));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7665));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7666));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7667));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7667));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7668));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7668));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7669));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7669));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7670));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7670));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7671));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7672));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7672));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7673));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7673));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7674));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7675));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7675));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7676));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7676));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7677));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7677));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7678));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7683));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7684));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7685));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7685));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7686));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7686));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7687));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7687));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7688));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7688));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7689));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7689));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7690));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7690));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7691));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7691));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7692));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7693));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7693));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7694));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7694));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7695));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 625, DateTimeKind.Utc).AddTicks(7695));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3111) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3125) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3126) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3128) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3129) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3133) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3134) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3135) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3136) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3138) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3139) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3140) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3141) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3142) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3144) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3145) });

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9023));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9039));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9040));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9041));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9042));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9057));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9058));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9058));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9059));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9061));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9062));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9062));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9063));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9064));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9064));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9065));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9142));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9145));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9146));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9147));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9148));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9149));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9149));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9150));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9151));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9152));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9154));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9160));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9161));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9162));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9162));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9163));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9165));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9168));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9169));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9170));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9170));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9171));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 627, DateTimeKind.Utc).AddTicks(9172));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1386));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1392));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1392));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1393));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1394));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1396));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1397));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1397));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1398));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1399));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1399));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1408));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1409));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1410));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1410));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1411));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1421));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1423));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1423));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1424));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1424));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1425));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1425));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1426));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1429));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1430));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1431));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 628, DateTimeKind.Utc).AddTicks(1431));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d6be6998-4ed8-424d-bf3d-37a5414c831b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "dc149174-09bf-45d2-a4af-21d29b33a0a6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5165fd38-9a80-42da-a362-707d094fb6db");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "86472d74-6bb1-4057-b39b-f5e7033a948a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "f492a7ce-d847-405f-be71-c9d050d51e21");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "d3426b94-a92d-4d89-a45b-e476a0512649");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "c96acca6-7887-44e2-9777-75338d1c1a0c");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ClientId",
                table: "Permissions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name_ClientId",
                table: "Permissions",
                columns: new[] { "Name", "ClientId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Clients_ClientId",
                table: "Permissions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
