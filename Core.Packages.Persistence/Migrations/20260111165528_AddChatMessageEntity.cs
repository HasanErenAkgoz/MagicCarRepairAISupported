using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddChatMessageEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    WorkOrderId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FileSize = table.Column<long>(type: "bigint", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 11, 16, 55, 27, 264, DateTimeKind.Utc).AddTicks(6625), new DateTime(2027, 1, 11, 16, 55, 27, 264, DateTimeKind.Utc).AddTicks(6396), new DateTime(2026, 1, 11, 16, 55, 27, 264, DateTimeKind.Utc).AddTicks(6138) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 11, 16, 55, 27, 264, DateTimeKind.Utc).AddTicks(6944), new DateTime(2027, 1, 11, 16, 55, 27, 264, DateTimeKind.Utc).AddTicks(6939), new DateTime(2026, 1, 11, 16, 55, 27, 264, DateTimeKind.Utc).AddTicks(6939) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 280, DateTimeKind.Utc).AddTicks(2671));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 280, DateTimeKind.Utc).AddTicks(3150));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 280, DateTimeKind.Utc).AddTicks(3175));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 280, DateTimeKind.Utc).AddTicks(3189));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 280, DateTimeKind.Utc).AddTicks(3201));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 280, DateTimeKind.Utc).AddTicks(3213));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 280, DateTimeKind.Utc).AddTicks(3226));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 280, DateTimeKind.Utc).AddTicks(3237));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 280, DateTimeKind.Utc).AddTicks(3248));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7308));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7313));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7314));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7315));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7333));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7334));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7335));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7335));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7336));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7336));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7337));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7338));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7338));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7339));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7339));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7340));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7340));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7341));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7342));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7342));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7343));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7343));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7344));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7599));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7600));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7601));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7601));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7602));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7602));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7603));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7603));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7604));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7605));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7605));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7606));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7606));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7620));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7621));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7621));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7622));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7622));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7623));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7623));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7624));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7624));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7625));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7629));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7630));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7630));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7631));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7632));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7632));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7633));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7633));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7634));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7635));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7635));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7636));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7637));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7637));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7638));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7638));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7639));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7639));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7640));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7640));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7641));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7641));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 265, DateTimeKind.Utc).AddTicks(7642));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 268, DateTimeKind.Utc).AddTicks(4977));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 268, DateTimeKind.Utc).AddTicks(4994));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 268, DateTimeKind.Utc).AddTicks(4996));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 268, DateTimeKind.Utc).AddTicks(4999));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 268, DateTimeKind.Utc).AddTicks(5000));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 268, DateTimeKind.Utc).AddTicks(5004));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 268, DateTimeKind.Utc).AddTicks(5006));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 268, DateTimeKind.Utc).AddTicks(5008));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 268, DateTimeKind.Utc).AddTicks(5009));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 268, DateTimeKind.Utc).AddTicks(5012));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2645));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2659));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2660));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2661));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2662));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2665));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2675));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2676));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2677));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2679));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2680));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2681));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2682));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2684));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2685));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 266, DateTimeKind.Utc).AddTicks(2686));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7163));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7177));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7178));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7179));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7180));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7182));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7183));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7183));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7184));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7185));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7185));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7186));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7186));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7187));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7187));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7188));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7265));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7267));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7268));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7268));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7269));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7269));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7270));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7270));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7271));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7272));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7272));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7273));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7277));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7277));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7286));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7287));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7287));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7289));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7292));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7293));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7293));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7293));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7294));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(7294));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9320));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9325));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9326));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9327));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9327));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9330));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9331));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9331));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9332));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9333));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9334));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9334));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9335));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9335));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9336));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9346));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9348));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9348));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9349));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9349));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9350));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9350));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9351));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9354));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9355));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9356));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 16, 55, 27, 267, DateTimeKind.Utc).AddTicks(9356));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "abb4733c-3039-42dc-9b05-00110a7d26b5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "c2fedb0e-574c-4d99-933e-98614af09c4d");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "86b0ca07-c48a-4b0a-a6cb-a6c52da74c23");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "f6b4ed8a-5941-4f8a-b781-aaa992673721");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "2e2e0dd2-9660-4149-b0bb-bea7688008cf");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "441b3238-2885-420e-84ec-804db2a5834c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "55c6b8a8-da33-48e3-89bb-ba2f1e47c5eb");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ClientId",
                table: "ChatMessages",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_CustomerId",
                table: "ChatMessages",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_ReceiverId",
                table: "ChatMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId",
                table: "ChatMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SenderId_ReceiverId_SentDate",
                table: "ChatMessages",
                columns: new[] { "SenderId", "ReceiverId", "SentDate" });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_SentDate",
                table: "ChatMessages",
                column: "SentDate");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_WorkOrderId",
                table: "ChatMessages",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_WorkOrderId_SentDate",
                table: "ChatMessages",
                columns: new[] { "WorkOrderId", "SentDate" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");

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
        }
    }
}
