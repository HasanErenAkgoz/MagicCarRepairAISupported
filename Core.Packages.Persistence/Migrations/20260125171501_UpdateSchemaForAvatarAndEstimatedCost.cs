using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchemaForAvatarAndEstimatedCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedCost",
                table: "WorkOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

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
                columns: new[] { "Avatar", "CreatedDate" },
                values: new object[] { "https://i.pravatar.cc/150?u=1", new DateTime(2025, 7, 25, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Avatar", "CreatedDate" },
                values: new object[] { "https://i.pravatar.cc/150?u=2", new DateTime(2025, 8, 25, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Avatar", "CreatedDate" },
                values: new object[] { "https://i.pravatar.cc/150?u=3", new DateTime(2025, 9, 25, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Avatar", "CreatedDate" },
                values: new object[] { null, new DateTime(2025, 10, 25, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Avatar", "CreatedDate" },
                values: new object[] { null, new DateTime(2025, 11, 25, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Avatar", "CreatedDate" },
                values: new object[] { null, new DateTime(2025, 12, 25, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Avatar", "CreatedDate" },
                values: new object[] { null, new DateTime(2026, 1, 10, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Avatar", "CreatedDate" },
                values: new object[] { null, new DateTime(2026, 1, 18, 17, 14, 58, 878, DateTimeKind.Utc).AddTicks(4954) });

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
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 12, 28, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2025, 12, 26, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2025, 12, 27, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2025, 12, 26, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), 3000m, new DateTime(2025, 12, 28, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 2, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2025, 12, 31, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 1, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2025, 12, 31, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), 2200m, new DateTime(2026, 1, 2, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 7, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 5, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 6, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 5, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 7, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 12, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 10, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 11, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 10, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 12, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 17, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 15, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 16, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 15, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 17, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 22, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 20, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 21, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 20, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 22, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 18, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 19, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 18, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 23, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 19, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 20, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 19, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 24, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 21, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 22, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 21, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 26, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 22, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 23, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 22, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 27, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 23, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 24, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 23, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 28, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 24, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 25, 5, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 24, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 29, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 25, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 25, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), 1200m, new DateTime(2026, 1, 30, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 26, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 26, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 31, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 17, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 18, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 17, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 20, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 25, 5, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 25, 7, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 25, 5, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 26, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedCost", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 25, 11, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), new DateTime(2026, 1, 25, 11, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714), null, new DateTime(2026, 1, 27, 17, 14, 58, 882, DateTimeKind.Utc).AddTicks(9714) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedCost",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 25, 16, 9, 10, 698, DateTimeKind.Utc).AddTicks(982), new DateTime(2027, 1, 25, 16, 9, 10, 698, DateTimeKind.Utc).AddTicks(750), new DateTime(2026, 1, 25, 16, 9, 10, 698, DateTimeKind.Utc).AddTicks(487) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 25, 16, 9, 10, 698, DateTimeKind.Utc).AddTicks(1307), new DateTime(2027, 1, 25, 16, 9, 10, 698, DateTimeKind.Utc).AddTicks(1303), new DateTime(2026, 1, 25, 16, 9, 10, 698, DateTimeKind.Utc).AddTicks(1303) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(8432));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(2130));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(2758));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(2812));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(2830));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(2845));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(2860));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(2874));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(2888));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 715, DateTimeKind.Utc).AddTicks(2901));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3407));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3411));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3412));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3412));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3413));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3414));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3414));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3415));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3415));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3433));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3434));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3434));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3435));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3435));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3436));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3436));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3437));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3437));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3438));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3439));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3439));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3440));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3440));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3765));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3766));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3766));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3767));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3767));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3768));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3768));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3769));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3770));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3779));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3781));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3782));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3782));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3783));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3783));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3784));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3784));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3785));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3785));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3786));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3791));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3792));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3792));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3793));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3793));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3794));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3795));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3795));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3796));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3796));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3797));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3797));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3798));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3798));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3799));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3799));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3800));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3801));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3801));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3802));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3802));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3803));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(3803));

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 10, 16, 9, 10, 725, DateTimeKind.Utc).AddTicks(4207), new DateTime(2026, 1, 10, 16, 9, 10, 725, DateTimeKind.Utc).AddTicks(4207) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2026, 1, 5, 16, 9, 10, 725, DateTimeKind.Utc).AddTicks(4207), new DateTime(2026, 1, 5, 16, 9, 10, 725, DateTimeKind.Utc).AddTicks(4207) });

            migrationBuilder.UpdateData(
                table: "Incomes",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "TransactionDate" },
                values: new object[] { new DateTime(2025, 12, 31, 16, 9, 10, 725, DateTimeKind.Utc).AddTicks(4207), new DateTime(2025, 12, 31, 16, 9, 10, 725, DateTimeKind.Utc).AddTicks(4207) });

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 702, DateTimeKind.Utc).AddTicks(5583));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 702, DateTimeKind.Utc).AddTicks(5597));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 702, DateTimeKind.Utc).AddTicks(5600));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 702, DateTimeKind.Utc).AddTicks(5602));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 702, DateTimeKind.Utc).AddTicks(5603));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 702, DateTimeKind.Utc).AddTicks(5607));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 702, DateTimeKind.Utc).AddTicks(5609));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 702, DateTimeKind.Utc).AddTicks(5611));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 702, DateTimeKind.Utc).AddTicks(5612));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 702, DateTimeKind.Utc).AddTicks(5614));

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 10, 25, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944), new DateTime(2026, 1, 20, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 10, 25, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944), new DateTime(2026, 1, 22, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 11, 25, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944), new DateTime(2026, 1, 15, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 11, 25, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944), new DateTime(2026, 1, 23, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 11, 25, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944), new DateTime(2026, 1, 24, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 25, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944), new DateTime(2026, 1, 10, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 25, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944), new DateTime(2026, 1, 18, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 25, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944), new DateTime(2026, 1, 20, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2025, 12, 26, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944), new DateTime(2026, 1, 5, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944) });

            migrationBuilder.UpdateData(
                table: "PartStocks",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "LastUpdatedDate" },
                values: new object[] { new DateTime(2026, 1, 5, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944), new DateTime(2025, 12, 31, 16, 9, 10, 728, DateTimeKind.Utc).AddTicks(944) });

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 16, 9, 10, 727, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 16, 9, 10, 727, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 16, 9, 10, 727, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 16, 9, 10, 727, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 16, 9, 10, 727, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 16, 9, 10, 727, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 16, 9, 10, 727, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 16, 9, 10, 727, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 26, 16, 9, 10, 727, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "Parts",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 5, 16, 9, 10, 727, DateTimeKind.Utc).AddTicks(2372));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8849));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8850));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8852));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8853));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8863));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8864));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8865));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8866));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8868));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8869));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8870));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8871));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8872));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8873));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 699, DateTimeKind.Utc).AddTicks(8874));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7436));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7450));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7451));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7452));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7453));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7455));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7456));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7457));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7457));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7458));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7459));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7459));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7460));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7461));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7461));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7462));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7545));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7547));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7548));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7548));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7549));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7550));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7550));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7551));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7551));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7552));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7552));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7553));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7565));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7566));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7566));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7567));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7567));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7569));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7572));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7572));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7573));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7573));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7574));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(7574));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9657));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9662));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9663));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9663));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9664));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9667));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9667));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9668));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9668));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9669));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9670));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9670));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9671));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9671));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9672));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9672));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9683));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9685));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9685));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9686));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9686));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9687));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9687));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9688));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9692));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9692));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9693));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 25, 16, 9, 10, 701, DateTimeKind.Utc).AddTicks(9693));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "76a0ff86-e3d2-4587-bc5f-c2b45c8b8dae");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "09ef8da7-04eb-4ed2-a4ba-350513dc5f4a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e172610c-7d62-4ab3-9e99-b27a9de96271");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "8349afc3-f1c8-48b1-83df-bdd4f9c5bd75");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "02b56529-e63c-482b-b45f-53224389b530");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "f4cf3b11-16cf-484a-8d34-2ce47dc4138b");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "85651618-edaf-4ddb-a5b6-8f8060ee8fee");

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 7, 25, 16, 9, 10, 716, DateTimeKind.Utc).AddTicks(6807));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 25, 16, 9, 10, 716, DateTimeKind.Utc).AddTicks(6823));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 25, 16, 9, 10, 716, DateTimeKind.Utc).AddTicks(6825));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 25, 16, 9, 10, 716, DateTimeKind.Utc).AddTicks(6827));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 16, 9, 10, 716, DateTimeKind.Utc).AddTicks(6829));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 25, 16, 9, 10, 716, DateTimeKind.Utc).AddTicks(6833));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 25, 16, 9, 10, 716, DateTimeKind.Utc).AddTicks(6835));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 16, 9, 10, 716, DateTimeKind.Utc).AddTicks(6836));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 18, 16, 9, 10, 716, DateTimeKind.Utc).AddTicks(6839));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 9, 25, 16, 9, 10, 716, DateTimeKind.Utc).AddTicks(6849));

            migrationBuilder.UpdateData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 16, 9, 10, 716, DateTimeKind.Utc).AddTicks(6851));

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2025, 12, 28, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2025, 12, 26, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2025, 12, 27, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2025, 12, 26, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2025, 12, 28, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 2, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2025, 12, 31, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 1, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2025, 12, 31, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 2, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 7, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 5, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 6, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 5, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 7, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 12, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 10, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 11, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 10, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 12, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 15, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 16, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 15, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 17, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ActualDeliveryDate", "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 22, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 20, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 21, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 20, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 22, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 18, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 19, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 18, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 23, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 19, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 20, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 19, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 24, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 21, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 22, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 21, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 26, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 22, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 23, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 22, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 27, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 23, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 24, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 23, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 28, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 24, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 25, 4, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 24, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 29, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 25, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 25, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 30, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 26, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 26, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 31, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 17, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 18, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 17, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 20, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedDate", "CustomerApprovalDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 25, 4, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 25, 6, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 25, 4, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 26, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });

            migrationBuilder.UpdateData(
                table: "WorkOrders",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedDate", "EntryDate", "EstimatedDeliveryDate" },
                values: new object[] { new DateTime(2026, 1, 25, 10, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 25, 10, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409), new DateTime(2026, 1, 27, 16, 9, 10, 718, DateTimeKind.Utc).AddTicks(6409) });
        }
    }
}
