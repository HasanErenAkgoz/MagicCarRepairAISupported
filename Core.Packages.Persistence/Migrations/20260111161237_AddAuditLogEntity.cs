using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditLogEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    EntityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChangedProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DurationMs = table.Column<long>(type: "bigint", nullable: true),
                    RequestPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RequestMethod = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 11, 16, 12, 36, 759, DateTimeKind.Utc).AddTicks(2647), new DateTime(2027, 1, 11, 16, 12, 36, 759, DateTimeKind.Utc).AddTicks(2422), new DateTime(2026, 1, 11, 16, 12, 36, 759, DateTimeKind.Utc).AddTicks(2154) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 11, 16, 12, 36, 759, DateTimeKind.Utc).AddTicks(2971), new DateTime(2027, 1, 11, 16, 12, 36, 759, DateTimeKind.Utc).AddTicks(2967), new DateTime(2026, 1, 11, 16, 12, 36, 759, DateTimeKind.Utc).AddTicks(2967) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 776, DateTimeKind.Utc).AddTicks(621));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 776, DateTimeKind.Utc).AddTicks(1135));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 776, DateTimeKind.Utc).AddTicks(1160));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 776, DateTimeKind.Utc).AddTicks(1175));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 776, DateTimeKind.Utc).AddTicks(1188));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 776, DateTimeKind.Utc).AddTicks(1200));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 776, DateTimeKind.Utc).AddTicks(1211));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 776, DateTimeKind.Utc).AddTicks(1222));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 776, DateTimeKind.Utc).AddTicks(1233));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7569));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7576));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7579));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7580));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7582));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7583));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7583));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7584));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7585));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7585));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7586));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7587));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7605));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7606));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7606));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7607));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7608));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7608));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7609));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7609));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7610));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7611));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7611));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7884));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7885));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7885));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7886));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7887));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7888));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7889));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7889));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7890));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7890));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7891));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7892));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7892));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7893));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7893));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7894));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7895));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7895));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7904));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7904));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7905));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7906));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7910));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7911));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7911));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7912));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7912));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7913));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7913));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7914));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7914));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7915));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7915));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7916));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7917));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7917));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7918));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7918));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7919));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7919));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7920));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7920));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7921));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7921));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 760, DateTimeKind.Utc).AddTicks(7922));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(7285));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(7307));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(7310));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(7312));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(7314));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(7317));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(7319));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(7320));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(7322));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(7324));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2870));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2882));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2883));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2885));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2886));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2889));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2890));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2891));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2892));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2893));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2902));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2903));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2904));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2905));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2906));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 761, DateTimeKind.Utc).AddTicks(2907));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9444));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9458));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9459));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9460));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9461));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9463));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9464));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9464));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9465));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9466));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9467));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9467));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9468));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9468));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9469));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9469));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9543));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9545));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9546));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9546));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9547));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9548));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9548));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9549));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9549));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9550));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9550));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9551));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9557));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9557));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9558));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9559));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9559));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9568));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9571));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9572));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9573));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9573));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9574));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 762, DateTimeKind.Utc).AddTicks(9574));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1590));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1595));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1596));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1597));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1597));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1600));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1601));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1601));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1602));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1603));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1604));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1604));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1605));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1605));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1606));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1615));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1617));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1617));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1618));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1619));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1619));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1624));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1625));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1625));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 12, 36, 763, DateTimeKind.Utc).AddTicks(1626));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "33092396-140f-4a93-8aee-bbf37caf7eff");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "fac20849-f045-4e1c-89e6-5ad6ca551aba");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5f5881ef-999b-40c9-93d2-38d3d7129509");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "a0291d65-7d4c-4ad9-b767-ee577754e3e3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "5c884209-3895-4f22-8b5f-b1715c957db4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "cf0352f8-e076-476f-aa54-188534f38fd4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "dd6d694f-ac5b-42c8-af31-7be5eb5bcf15");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_Action",
                table: "AuditLogs",
                column: "Action");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_ClientId",
                table: "AuditLogs",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_CreatedDate",
                table: "AuditLogs",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityId",
                table: "AuditLogs",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityName",
                table: "AuditLogs",
                column: "EntityName");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_EntityName_EntityId",
                table: "AuditLogs",
                columns: new[] { "EntityName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 11, 11, 56, 39, 544, DateTimeKind.Utc).AddTicks(595), new DateTime(2027, 1, 11, 11, 56, 39, 544, DateTimeKind.Utc).AddTicks(339), new DateTime(2026, 1, 11, 11, 56, 39, 544, DateTimeKind.Utc).AddTicks(8) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 11, 11, 56, 39, 544, DateTimeKind.Utc).AddTicks(935), new DateTime(2027, 1, 11, 11, 56, 39, 544, DateTimeKind.Utc).AddTicks(931), new DateTime(2026, 1, 11, 11, 56, 39, 544, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 560, DateTimeKind.Utc).AddTicks(1649));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 560, DateTimeKind.Utc).AddTicks(2161));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 560, DateTimeKind.Utc).AddTicks(2186));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 560, DateTimeKind.Utc).AddTicks(2201));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 560, DateTimeKind.Utc).AddTicks(2212));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 560, DateTimeKind.Utc).AddTicks(2224));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 560, DateTimeKind.Utc).AddTicks(2236));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 560, DateTimeKind.Utc).AddTicks(2247));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 560, DateTimeKind.Utc).AddTicks(2257));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1720));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1725));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1726));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1726));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1727));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1727));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1728));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1729));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1729));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1730));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1731));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1750));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1750));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1751));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1752));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1752));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1753));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1754));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1755));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1756));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(1756));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2112));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2113));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2114));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2115));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2116));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2116));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2117));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2117));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2118));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2118));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2119));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2119));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2120));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2120));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2121));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2121));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2122));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2123));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2123));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2124));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2124));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2125));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2130));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2130));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2131));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2132));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2132));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2133));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2133));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2134));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2135));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2135));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2136));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2136));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2144));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2145));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2145));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2146));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2147));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2148));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2148));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2149));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2149));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(2150));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 548, DateTimeKind.Utc).AddTicks(2972));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 548, DateTimeKind.Utc).AddTicks(2987));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 548, DateTimeKind.Utc).AddTicks(2990));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 548, DateTimeKind.Utc).AddTicks(2992));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 548, DateTimeKind.Utc).AddTicks(2993));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 548, DateTimeKind.Utc).AddTicks(2998));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 548, DateTimeKind.Utc).AddTicks(3000));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 548, DateTimeKind.Utc).AddTicks(3001));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 548, DateTimeKind.Utc).AddTicks(3003));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 548, DateTimeKind.Utc).AddTicks(3005));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7285));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7298));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7300));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7301));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7302));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7305));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7306));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7307));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7308));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7310));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7311));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7312));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7313));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7314));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7315));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 545, DateTimeKind.Utc).AddTicks(7316));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4501));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4515));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4516));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4517));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4517));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4520));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4521));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4523));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4523));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4524));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4524));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4525));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4525));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4526));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4609));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4611));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4612));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4613));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4613));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4614));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4615));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4615));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4616));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4616));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4617));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4617));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4622));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4623));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4624));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4624));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4625));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4626));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4628));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4629));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4630));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4630));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4631));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(4631));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6716));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6722));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6723));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6724));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6724));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6726));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6727));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6728));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6728));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6729));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6730));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6730));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6731));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6732));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6732));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6733));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6744));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6745));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6746));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6747));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6747));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6748));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6748));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6749));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6753));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6754));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6754));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 11, 56, 39, 547, DateTimeKind.Utc).AddTicks(6755));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "876e32b6-96a4-4ef6-8138-fa60320a9a8f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "d6db4c35-a8c0-4d3b-b843-58f912febbb6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b196b80d-2373-43b8-b0b3-2c218e373149");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "aa9dbd5c-3675-455e-969f-5792c5acaef3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "5d3801ab-e072-4c42-bb04-4e021d693d2c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "b2d2e4bb-bf9d-4292-ab0e-b1d7e7d0a999");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "2ed65344-0ae2-4e08-baa0-23ec02a92e8a");
        }
    }
}
