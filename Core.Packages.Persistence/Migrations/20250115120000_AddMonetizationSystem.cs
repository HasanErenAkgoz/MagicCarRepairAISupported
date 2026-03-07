using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMonetizationSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add SubscriptionPlan and SubscriptionStatus enums (stored as int)
            // Add CommissionStatus enum (stored as int)
            // Add LimitType enum (stored as int)

            // Create Subscriptions table
            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Plan = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MonthlyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YearlyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaxWorkOrders = table.Column<int>(type: "int", nullable: false, defaultValue: 10),
                    MaxUsers = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MaxAIAnalyses = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutoRenew = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CancelledDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CancellationReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    LastPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // Create SubscriptionPayments table
            migrationBuilder.CreateTable(
                name: "SubscriptionPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubscriptionId = table.Column<int>(type: "int", nullable: false),
                    PaymentNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentGateway = table.Column<int>(type: "int", nullable: true),
                    GatewayPaymentId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GatewayConversationId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PaymentPeriod = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Monthly"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GatewayResponseMessage = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GatewayResponseCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPayments_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionPayments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // Add CommissionAmount and CommissionRate to Payments table
            migrationBuilder.AddColumn<decimal>(
                name: "CommissionAmount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionRate",
                table: "Payments",
                type: "decimal(5,2)",
                nullable: true);

            // Create Commissions table
            migrationBuilder.CreateTable(
                name: "Commissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    CommissionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CommissionRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CommissionStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefundDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefundReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commissions_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Commissions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // Create UsageTracking table
            migrationBuilder.CreateTable(
                name: "UsageTracking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    LimitType = table.Column<int>(type: "int", nullable: false),
                    CurrentPeriod = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UsageCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LimitAmount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ResetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageTracking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsageTracking_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            // Create indexes for Subscriptions
            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ClientId",
                table: "Subscriptions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ClientId_Status",
                table: "Subscriptions",
                columns: new[] { "ClientId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_EndDate",
                table: "Subscriptions",
                column: "EndDate");

            // Create indexes for SubscriptionPayments
            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_SubscriptionId",
                table: "SubscriptionPayments",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_PaymentNumber",
                table: "SubscriptionPayments",
                column: "PaymentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_ClientId",
                table: "SubscriptionPayments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_PaymentDate",
                table: "SubscriptionPayments",
                column: "PaymentDate");

            // Create indexes for Commissions
            migrationBuilder.CreateIndex(
                name: "IX_Commissions_PaymentId",
                table: "Commissions",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_ClientId",
                table: "Commissions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_Status",
                table: "Commissions",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_PaymentDate",
                table: "Commissions",
                column: "PaymentDate");

            // Create indexes for UsageTracking
            migrationBuilder.CreateIndex(
                name: "IX_UsageTracking_ClientId",
                table: "UsageTracking",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_UsageTracking_ClientId_LimitType_CurrentPeriod",
                table: "UsageTracking",
                columns: new[] { "ClientId", "LimitType", "CurrentPeriod" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop indexes
            migrationBuilder.DropIndex(
                name: "IX_UsageTracking_ClientId_LimitType_CurrentPeriod",
                table: "UsageTracking");

            migrationBuilder.DropIndex(
                name: "IX_UsageTracking_ClientId",
                table: "UsageTracking");

            migrationBuilder.DropIndex(
                name: "IX_Commissions_PaymentDate",
                table: "Commissions");

            migrationBuilder.DropIndex(
                name: "IX_Commissions_Status",
                table: "Commissions");

            migrationBuilder.DropIndex(
                name: "IX_Commissions_ClientId",
                table: "Commissions");

            migrationBuilder.DropIndex(
                name: "IX_Commissions_PaymentId",
                table: "Commissions");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPayments_PaymentDate",
                table: "SubscriptionPayments");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPayments_ClientId",
                table: "SubscriptionPayments");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPayments_PaymentNumber",
                table: "SubscriptionPayments");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPayments_SubscriptionId",
                table: "SubscriptionPayments");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_EndDate",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ClientId_Status",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ClientId",
                table: "Subscriptions");

            // Drop tables
            migrationBuilder.DropTable(
                name: "UsageTracking");

            migrationBuilder.DropTable(
                name: "Commissions");

            migrationBuilder.DropTable(
                name: "SubscriptionPayments");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            // Remove columns from Payments
            migrationBuilder.DropColumn(
                name: "CommissionAmount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "CommissionRate",
                table: "Payments");
        }
    }
}
