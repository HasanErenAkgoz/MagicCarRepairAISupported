using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCustomerIdAndEmployeeIdFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Customers_CustomerId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CustomerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Users");

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
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(1497) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(1972) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2026) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2042) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2054) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2066) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2078) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2088) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 1, new DateTime(2025, 11, 29, 21, 34, 54, 640, DateTimeKind.Utc).AddTicks(2099) });

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
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3141));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3142));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3144));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 21, 34, 54, 626, DateTimeKind.Utc).AddTicks(3145));

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

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2025, 11, 29, 12, 42, 48, 535, DateTimeKind.Utc).AddTicks(783), new DateTime(2026, 11, 29, 12, 42, 48, 535, DateTimeKind.Utc).AddTicks(551), new DateTime(2025, 11, 29, 12, 42, 48, 535, DateTimeKind.Utc).AddTicks(289) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2025, 11, 29, 12, 42, 48, 535, DateTimeKind.Utc).AddTicks(1113), new DateTime(2026, 11, 29, 12, 42, 48, 535, DateTimeKind.Utc).AddTicks(1109), new DateTime(2025, 11, 29, 12, 42, 48, 535, DateTimeKind.Utc).AddTicks(1108) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(8660) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9243) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9305) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9320) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9332) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9344) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9358) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9369) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedBy", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9379) });

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1710));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1714));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1715));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1716));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1716));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1717));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1718));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1728));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1729));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1731));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1732));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1732));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1733));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1733));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1734));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1735));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1735));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1736));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1752));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1753));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1753));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1996));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1997));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1998));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1998));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1999));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2000));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2000));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2001));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2001));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2002));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2003));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2003));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2004));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2004));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2005));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2006));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2006));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2007));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2008));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2008));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2009));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2009));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2013));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2014));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2015));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2015));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2016));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2016));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2017));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2017));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2018));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2019));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2019));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2020));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2020));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2021));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2021));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2022));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2022));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2023));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2024));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2024));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2025));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2025));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2026));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(338) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(356) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(358) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(360) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(362) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(366) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(368) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(369) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(371) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(373) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(374) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "ClientId", "CreatedDate" },
                values: new object[] { 0, new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(376) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(377));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(379));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(380));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 537, DateTimeKind.Utc).AddTicks(382));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7192));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7206));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7207));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7207));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7218));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7221));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7221));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7222));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7223));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7224));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7225));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7225));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7226));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7226));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7227));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7227));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7312));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7314));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7315));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7315));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7315));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7316));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7317));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7317));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7319));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7319));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7324));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7325));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7325));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7326));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7326));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7328));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7330));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7331));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7331));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7332));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7332));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(7333));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9408));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9414));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9415));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9416));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9416));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9419));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9419));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9420));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9420));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9430));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9430));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9431));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9432));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9432));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9433));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9433));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9445));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9447));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9447));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9448));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9449));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9449));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9450));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9450));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9453));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9454));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9455));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 29, 12, 42, 48, 538, DateTimeKind.Utc).AddTicks(9455));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "22847332-8c7a-40fe-90e9-6d447c67dd03");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e4c60277-f3f7-4440-9b69-dc371fee6a36");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "3a2d3a74-6e6b-4868-97f2-68a0bf664740");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "37ad40bf-4e38-4943-a51f-31449a807889");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "f7a5b026-3b4d-43a9-90b5-20942edc2819");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "41e326df-9575-4ddb-a32c-88d354ac7094");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "6af5e92d-0701-442c-a7d5-99c3f453f885");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CustomerId",
                table: "Users",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeId",
                table: "Users",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Customers_CustomerId",
                table: "Users",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Employees_EmployeeId",
                table: "Users",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
