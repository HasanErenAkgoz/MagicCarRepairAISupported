using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TitleTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentTemplate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedEntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTemplates_Clients_ClientId",
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
                values: new object[] { new DateTime(2025, 12, 7, 18, 4, 58, 106, DateTimeKind.Utc).AddTicks(7654), new DateTime(2026, 12, 7, 18, 4, 58, 106, DateTimeKind.Utc).AddTicks(7402), new DateTime(2025, 12, 7, 18, 4, 58, 106, DateTimeKind.Utc).AddTicks(7066) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2025, 12, 7, 18, 4, 58, 106, DateTimeKind.Utc).AddTicks(8003), new DateTime(2026, 12, 7, 18, 4, 58, 106, DateTimeKind.Utc).AddTicks(7996), new DateTime(2025, 12, 7, 18, 4, 58, 106, DateTimeKind.Utc).AddTicks(7996) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 122, DateTimeKind.Utc).AddTicks(9247));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 122, DateTimeKind.Utc).AddTicks(9750));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 122, DateTimeKind.Utc).AddTicks(9776));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 122, DateTimeKind.Utc).AddTicks(9818));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 122, DateTimeKind.Utc).AddTicks(9831));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 122, DateTimeKind.Utc).AddTicks(9846));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 122, DateTimeKind.Utc).AddTicks(9858));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 122, DateTimeKind.Utc).AddTicks(9869));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 122, DateTimeKind.Utc).AddTicks(9880));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8641));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8646));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8648));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8649));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8651));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8651));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8652));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8653));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8671));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8672));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8673));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8673));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8674));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8675));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8676));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8677));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8680));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8681));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8681));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8682));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8682));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8683));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(8684));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9024));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9025));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9025));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9026));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9027));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9028));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9028));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9029));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9029));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9030));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9030));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9031));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9031));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9032));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9032));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9033));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9033));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9034));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9035));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9035));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9036));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9036));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9037));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9050));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9050));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9051));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9051));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9052));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9052));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9053));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9053));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9054));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9055));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9055));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9056));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9056));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9057));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9057));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9058));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9058));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9059));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9059));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9060));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9060));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9061));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 107, DateTimeKind.Utc).AddTicks(9061));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4127));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4140));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4142));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4143));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4144));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4147));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4148));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4149));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4150));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4152));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4153));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4154));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4155));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4164));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4165));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 108, DateTimeKind.Utc).AddTicks(4166));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8966));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8979));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8980));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8981));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8981));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8983));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8984));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8985));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8985));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8986));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8987));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8988));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8988));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8989));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8989));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(8990));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9069));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9071));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9071));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9072));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9072));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9073));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9074));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9074));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9075));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9075));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9076));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9077));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9081));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9082));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9083));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9083));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9083));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9085));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9096));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9097));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9097));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9098));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9098));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 109, DateTimeKind.Utc).AddTicks(9099));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1179));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1184));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1185));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1186));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1186));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1189));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1189));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1190));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1190));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1191));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1192));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1192));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1193));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1194));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1194));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1195));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1206));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1207));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1208));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1208));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1209));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1209));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1210));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1210));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1214));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1215));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1216));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 18, 4, 58, 110, DateTimeKind.Utc).AddTicks(1216));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c4a94c94-9cac-4ee1-b610-e45e7285cb21");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a37baf07-0003-4b69-8e65-4d81c0239aae");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "ad48b4c9-43a3-48f1-aae5-f612e48c879a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "2821c68a-de32-4c94-9ac3-a9aef30e87a7");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "51fe4f05-7da5-4fb1-a3a7-6fd0e57f7ab9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "d9b8454e-a38a-4c9c-8295-46e9a43c4f1a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "5717d14e-0b88-4b18-b1e9-e1fafcbb4802");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_ClientId",
                table: "NotificationTemplates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_Name",
                table: "NotificationTemplates",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_Name_ClientId",
                table: "NotificationTemplates",
                columns: new[] { "Name", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTemplates_RelatedEntityType",
                table: "NotificationTemplates",
                column: "RelatedEntityType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationTemplates");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2025, 12, 7, 17, 42, 45, 366, DateTimeKind.Utc).AddTicks(8677), new DateTime(2026, 12, 7, 17, 42, 45, 366, DateTimeKind.Utc).AddTicks(8439), new DateTime(2025, 12, 7, 17, 42, 45, 366, DateTimeKind.Utc).AddTicks(8164) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2025, 12, 7, 17, 42, 45, 366, DateTimeKind.Utc).AddTicks(9058), new DateTime(2026, 12, 7, 17, 42, 45, 366, DateTimeKind.Utc).AddTicks(9054), new DateTime(2025, 12, 7, 17, 42, 45, 366, DateTimeKind.Utc).AddTicks(9054) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 382, DateTimeKind.Utc).AddTicks(5127));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 382, DateTimeKind.Utc).AddTicks(5629));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 382, DateTimeKind.Utc).AddTicks(5681));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 382, DateTimeKind.Utc).AddTicks(5697));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 382, DateTimeKind.Utc).AddTicks(5710));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 382, DateTimeKind.Utc).AddTicks(5722));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 382, DateTimeKind.Utc).AddTicks(5735));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 382, DateTimeKind.Utc).AddTicks(5746));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 382, DateTimeKind.Utc).AddTicks(5756));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(173));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(178));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(179));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(180));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(180));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(181));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(182));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(200));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(210));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(211));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(211));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(212));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(212));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(213));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(214));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(214));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(215));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(215));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(216));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(216));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(217));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(217));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(218));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(548));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(549));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(550));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(551));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(551));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(552));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(553));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(553));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(554));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(554));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(555));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(555));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(556));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(556));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(557));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(557));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(558));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(558));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(559));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(560));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(560));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(567));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(567));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(568));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(568));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(569));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(569));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(570));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(571));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(571));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(572));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(572));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(573));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(573));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(574));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(575));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(575));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(576));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(576));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(577));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(577));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(578));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(578));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(579));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5729));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5742));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5744));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5745));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5746));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5750));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5751));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5752));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5753));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5754));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5756));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5757));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5758));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5759));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5760));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 368, DateTimeKind.Utc).AddTicks(5761));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(571));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(586));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(586));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(587));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(588));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(590));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(600));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(601));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(601));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(603));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(603));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(604));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(604));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(605));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(605));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(606));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(696));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(698));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(699));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(699));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(700));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(701));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(701));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(702));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(702));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(703));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(703));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(704));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(709));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(710));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(710));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(711));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(711));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(713));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(715));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(716));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(717));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(717));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(718));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(718));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2800));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2806));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2807));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2807));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2808));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2811));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2811));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2812));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2812));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2813));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2823));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2823));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2824));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2824));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2835));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2836));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2837));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2838));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2838));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2838));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2839));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2840));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2843));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2844));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2844));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 17, 42, 45, 370, DateTimeKind.Utc).AddTicks(2845));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2efc7f6c-b6de-4b7b-9416-b1e7933f29fa");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "89fac90b-e4ac-4628-bbbc-a6e594dafdd6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "43aaaf66-a870-43db-8e00-4883290519bf");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "54132813-0ca6-47d7-b4ed-360222ac82e9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "1e5c7df7-17bd-4aa2-8b98-acae5c98ceef");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "9378f63e-64f6-4047-8f56-8339636e7074");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "f998984a-b748-46c0-ba1c-3a6fb9c260ce");
        }
    }
}
