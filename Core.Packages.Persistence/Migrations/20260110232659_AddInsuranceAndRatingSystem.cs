using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInsuranceAndRatingSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsuranceCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CompanyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ApiEndpoint = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ApiKey = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SupportedInsuranceTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceCompanies_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: true),
                    WorkOrderId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentGateway = table.Column<int>(type: "int", nullable: true),
                    GatewayPaymentId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GatewayConversationId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    InstallmentCount = table.Column<int>(type: "int", nullable: true),
                    CardLastFourDigits = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    CardHolderName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GatewayResponseMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    GatewayResponseCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsRefunded = table.Column<bool>(type: "bit", nullable: false),
                    RefundDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RefundDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    GatewayRefundId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    ServiceQuality = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    PriceValue = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    OnTimeDelivery = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    StaffBehavior = table.Column<int>(type: "int", maxLength: 1, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Photos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ServiceReply = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ServiceReplyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRatings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRatings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRatings_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsurancePolicies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    InsuranceCompanyId = table.Column<int>(type: "int", nullable: false),
                    InsuranceType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PremiumAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoverageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DeductiblePercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0m),
                    DeductibleAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    PolicyFileId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsurancePolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_InsuranceCompanies_InsuranceCompanyId",
                        column: x => x.InsuranceCompanyId,
                        principalTable: "InsuranceCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_UploadedFiles_PolicyFileId",
                        column: x => x.PolicyFileId,
                        principalTable: "UploadedFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WorkOrderId = table.Column<int>(type: "int", nullable: true),
                    InsurancePolicyId = table.Column<int>(type: "int", nullable: false),
                    DamageDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DamageDescription = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DamageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ApprovedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DeductibleAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PayableAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RejectionReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Photos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InsuranceClaims_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuranceClaims_InsurancePolicies_InsurancePolicyId",
                        column: x => x.InsurancePolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InsuranceClaims_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 10, 23, 26, 58, 656, DateTimeKind.Utc).AddTicks(3052), new DateTime(2027, 1, 10, 23, 26, 58, 656, DateTimeKind.Utc).AddTicks(2733), new DateTime(2026, 1, 10, 23, 26, 58, 656, DateTimeKind.Utc).AddTicks(2348) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 10, 23, 26, 58, 656, DateTimeKind.Utc).AddTicks(3399), new DateTime(2027, 1, 10, 23, 26, 58, 656, DateTimeKind.Utc).AddTicks(3395), new DateTime(2026, 1, 10, 23, 26, 58, 656, DateTimeKind.Utc).AddTicks(3394) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 673, DateTimeKind.Utc).AddTicks(6580));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 673, DateTimeKind.Utc).AddTicks(7108));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 673, DateTimeKind.Utc).AddTicks(7134));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 673, DateTimeKind.Utc).AddTicks(7177));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 673, DateTimeKind.Utc).AddTicks(7189));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 673, DateTimeKind.Utc).AddTicks(7202));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 673, DateTimeKind.Utc).AddTicks(7213));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 673, DateTimeKind.Utc).AddTicks(7226));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 673, DateTimeKind.Utc).AddTicks(7236));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5607));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5611));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5612));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5613));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5614));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5615));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5615));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5616));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5617));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5617));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5618));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5619));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5619));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5620));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5620));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5621));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5621));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5622));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5624));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5624));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5625));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5852));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5853));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5854));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5855));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5873));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5873));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5874));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5874));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5875));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5875));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5876));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5876));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5886));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5887));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5888));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5888));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5889));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5889));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5890));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5890));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5891));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5891));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5896));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5896));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5897));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5897));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5898));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5898));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5899));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5900));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5900));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5901));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5901));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5902));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5902));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5903));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5904));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5904));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5905));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5905));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5906));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5906));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5907));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5907));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 657, DateTimeKind.Utc).AddTicks(5908));

            migrationBuilder.InsertData(
                table: "InsuranceCompanies",
                columns: new[] { "Id", "Address", "ApiEndpoint", "ApiKey", "ClientId", "CompanyCode", "CompanyName", "ContactPerson", "CreatedBy", "CreatedDate", "Email", "IsActive", "ModifiedBy", "ModifiedDate", "Phone", "Status", "SupportedInsuranceTypes" },
                values: new object[,]
                {
                    { 1, "Ýstanbul, Türkiye", null, null, 1, "ALLIANZ", "Allianz Sigorta", "Genel Müdürlük", 0, new DateTime(2026, 1, 10, 23, 26, 58, 660, DateTimeKind.Utc).AddTicks(4629), "info@allianz.com.tr", true, null, null, "+90 850 222 0 100", 1, "[\"Comprehensive\", \"TrafficInsurance\", \"ThirdParty\"]" },
                    { 2, "Ýstanbul, Türkiye", null, null, 1, "ANADOLU", "Anadolu Sigorta", "Genel Müdürlük", 0, new DateTime(2026, 1, 10, 23, 26, 58, 660, DateTimeKind.Utc).AddTicks(4643), "info@anadolusigorta.com.tr", true, null, null, "+90 850 222 0 200", 1, "[\"Comprehensive\", \"TrafficInsurance\", \"ThirdParty\"]" },
                    { 3, "Ýstanbul, Türkiye", null, null, 1, "AXA", "Axa Sigorta", "Genel Müdürlük", 0, new DateTime(2026, 1, 10, 23, 26, 58, 660, DateTimeKind.Utc).AddTicks(4646), "info@axa-sigorta.com.tr", true, null, null, "+90 850 222 0 300", 1, "[\"Comprehensive\", \"TrafficInsurance\"]" },
                    { 4, "Ýstanbul, Türkiye", null, null, 1, "GROUPAMA", "Groupama Sigorta", "Genel Müdürlük", 0, new DateTime(2026, 1, 10, 23, 26, 58, 660, DateTimeKind.Utc).AddTicks(4648), "info@groupama.com.tr", true, null, null, "+90 850 222 0 400", 1, "[\"Comprehensive\", \"TrafficInsurance\", \"ThirdParty\"]" },
                    { 5, "Ýstanbul, Türkiye", null, null, 1, "HDI", "HDI Sigorta", "Genel Müdürlük", 0, new DateTime(2026, 1, 10, 23, 26, 58, 660, DateTimeKind.Utc).AddTicks(4650), "info@hdi-sigorta.com.tr", true, null, null, "+90 850 222 0 500", 1, "[\"Comprehensive\", \"TrafficInsurance\"]" },
                    { 6, "Ýstanbul, Türkiye", null, null, 1, "MAPFRE", "Mapfre Sigorta", "Genel Müdürlük", 0, new DateTime(2026, 1, 10, 23, 26, 58, 660, DateTimeKind.Utc).AddTicks(4653), "info@mapfre.com.tr", true, null, null, "+90 850 222 0 600", 1, "[\"Comprehensive\", \"TrafficInsurance\", \"ThirdParty\"]" },
                    { 7, "Ankara, Türkiye", null, null, 1, "NEOVA", "Neova Sigorta", "Genel Müdürlük", 0, new DateTime(2026, 1, 10, 23, 26, 58, 660, DateTimeKind.Utc).AddTicks(4655), "info@neova.com.tr", true, null, null, "+90 850 222 0 700", 1, "[\"Comprehensive\", \"TrafficInsurance\"]" },
                    { 8, "Ýstanbul, Türkiye", null, null, 1, "RAY", "Ray Sigorta", "Genel Müdürlük", 0, new DateTime(2026, 1, 10, 23, 26, 58, 660, DateTimeKind.Utc).AddTicks(4657), "info@ray.com.tr", true, null, null, "+90 850 222 0 800", 1, "[\"Comprehensive\", \"TrafficInsurance\", \"ThirdParty\"]" },
                    { 9, "Ýstanbul, Türkiye", null, null, 1, "UNICO", "Unico Sigorta", "Genel Müdürlük", 0, new DateTime(2026, 1, 10, 23, 26, 58, 660, DateTimeKind.Utc).AddTicks(4659), "info@unico.com.tr", true, null, null, "+90 850 222 0 900", 1, "[\"Comprehensive\", \"TrafficInsurance\"]" },
                    { 10, "Ankara, Türkiye", null, null, 1, "ZIRAAT", "Ziraat Sigorta", "Genel Müdürlük", 0, new DateTime(2026, 1, 10, 23, 26, 58, 660, DateTimeKind.Utc).AddTicks(4661), "info@ziraat.com.tr", true, null, null, "+90 850 222 0 000", 1, "[\"Comprehensive\", \"TrafficInsurance\", \"ThirdParty\"]" }
                });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(952));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(964));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(966));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(968));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(969));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(972));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(981));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(982));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(983));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(985));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(986));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(987));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(988));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(989));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(991));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 658, DateTimeKind.Utc).AddTicks(992));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6430));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6446));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6447));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6448));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6448));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6451));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6452));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6452));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6453));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6454));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6455));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6455));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6456));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6457));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6457));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6458));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6536));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6538));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6539));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6539));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6540));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6541));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6541));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6542));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6542));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6543));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6544));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6544));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6549));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6558));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6559));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6559));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6560));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6561));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6564));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6565));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6566));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6567));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6567));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(6568));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8730));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8735));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8736));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8737));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8737));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8739));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8741));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8742));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8742));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8743));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8743));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8744));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8744));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8745));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8769));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8771));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8771));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8772));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8773));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8773));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8774));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8775));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8780));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8781));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8781));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 10, 23, 26, 58, 659, DateTimeKind.Utc).AddTicks(8782));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "96cf5f93-e474-4e6c-8158-192008fe5b54");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "afa871f3-ba6f-47e8-969f-6c0faef826fd");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "e6259144-55cb-432f-b475-5176e51d3363");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "7978a7d0-d470-4a00-93df-52bf4ed35117");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "72356cdd-5ec5-41b1-ba7a-98dd974b140f");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "c1d0f97f-319c-4e0c-9350-149c3233a2f2");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "ee71dbbc-1601-4fbc-b9b6-3186cf9ce035");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ClientId_AssignedEmployeeId_Status",
                table: "WorkOrders",
                columns: new[] { "ClientId", "AssignedEmployeeId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ClientId_CustomerId_Status",
                table: "WorkOrders",
                columns: new[] { "ClientId", "CustomerId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ClientId_EntryDate_Status",
                table: "WorkOrders",
                columns: new[] { "ClientId", "EntryDate", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ClientId_Priority_Status",
                table: "WorkOrders",
                columns: new[] { "ClientId", "Priority", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ClientId_Status",
                table: "WorkOrders",
                columns: new[] { "ClientId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_PaymentStatus",
                table: "WorkOrders",
                column: "PaymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_PartStocks_ClientId_Quantity",
                table: "PartStocks",
                columns: new[] { "ClientId", "Quantity" });

            migrationBuilder.CreateIndex(
                name: "IX_PartStocks_ClientId_Status_Quantity",
                table: "PartStocks",
                columns: new[] { "ClientId", "Status", "Quantity" });

            migrationBuilder.CreateIndex(
                name: "IX_PartStocks_Quantity",
                table: "PartStocks",
                column: "Quantity");

            migrationBuilder.CreateIndex(
                name: "IX_PartStocks_Status",
                table: "PartStocks",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ClientId_BrandType_Status",
                table: "Parts",
                columns: new[] { "ClientId", "BrandType", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ClientId_Category_Status",
                table: "Parts",
                columns: new[] { "ClientId", "Category", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ClientId_IsLowStockAlertEnabled_Status",
                table: "Parts",
                columns: new[] { "ClientId", "IsLowStockAlertEnabled", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ClientId_Name",
                table: "Parts",
                columns: new[] { "ClientId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_Status",
                table: "Parts",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientId_CustomerId_InvoiceDate",
                table: "Invoices",
                columns: new[] { "ClientId", "CustomerId", "InvoiceDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientId_InvoiceDate",
                table: "Invoices",
                columns: new[] { "ClientId", "InvoiceDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientId_Status_DueDate",
                table: "Invoices",
                columns: new[] { "ClientId", "Status", "DueDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Status",
                table: "Invoices",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_ClaimNumber",
                table: "InsuranceClaims",
                column: "ClaimNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_ClientId_ClaimNumber",
                table: "InsuranceClaims",
                columns: new[] { "ClientId", "ClaimNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_DamageDate",
                table: "InsuranceClaims",
                column: "DamageDate");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_InsurancePolicyId",
                table: "InsuranceClaims",
                column: "InsurancePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_Status",
                table: "InsuranceClaims",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceClaims_WorkOrderId",
                table: "InsuranceClaims",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCompanies_ClientId_CompanyCode",
                table: "InsuranceCompanies",
                columns: new[] { "ClientId", "CompanyCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCompanies_CompanyCode",
                table: "InsuranceCompanies",
                column: "CompanyCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_ClientId_PolicyNumber",
                table: "InsurancePolicies",
                columns: new[] { "ClientId", "PolicyNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_CustomerId",
                table: "InsurancePolicies",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_EndDate",
                table: "InsurancePolicies",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_InsuranceCompanyId",
                table: "InsurancePolicies",
                column: "InsuranceCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_PolicyFileId",
                table: "InsurancePolicies",
                column: "PolicyFileId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_PolicyNumber",
                table: "InsurancePolicies",
                column: "PolicyNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_Status_EndDate",
                table: "InsurancePolicies",
                columns: new[] { "Status", "EndDate" });

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_VehicleId",
                table: "InsurancePolicies",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ClientId",
                table: "Payments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerId",
                table: "Payments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_GatewayConversationId",
                table: "Payments",
                column: "GatewayConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_GatewayPaymentId",
                table: "Payments",
                column: "GatewayPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentDate",
                table: "Payments",
                column: "PaymentDate");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentNumber",
                table: "Payments",
                column: "PaymentNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentNumber_ClientId",
                table: "Payments",
                columns: new[] { "PaymentNumber", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_WorkOrderId",
                table: "Payments",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRatings_ClientId_Status",
                table: "ServiceRatings",
                columns: new[] { "ClientId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRatings_CreatedDate",
                table: "ServiceRatings",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRatings_CustomerId",
                table: "ServiceRatings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRatings_WorkOrderId",
                table: "ServiceRatings",
                column: "WorkOrderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsuranceClaims");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ServiceRatings");

            migrationBuilder.DropTable(
                name: "InsurancePolicies");

            migrationBuilder.DropTable(
                name: "InsuranceCompanies");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_ClientId_AssignedEmployeeId_Status",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_ClientId_CustomerId_Status",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_ClientId_EntryDate_Status",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_ClientId_Priority_Status",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_ClientId_Status",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_PaymentStatus",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_PartStocks_ClientId_Quantity",
                table: "PartStocks");

            migrationBuilder.DropIndex(
                name: "IX_PartStocks_ClientId_Status_Quantity",
                table: "PartStocks");

            migrationBuilder.DropIndex(
                name: "IX_PartStocks_Quantity",
                table: "PartStocks");

            migrationBuilder.DropIndex(
                name: "IX_PartStocks_Status",
                table: "PartStocks");

            migrationBuilder.DropIndex(
                name: "IX_Parts_ClientId_BrandType_Status",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_ClientId_Category_Status",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_ClientId_IsLowStockAlertEnabled_Status",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_ClientId_Name",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Parts_Status",
                table: "Parts");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ClientId_CustomerId_InvoiceDate",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ClientId_InvoiceDate",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ClientId_Status_DueDate",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_Status",
                table: "Invoices");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2025, 12, 21, 9, 29, 4, 29, DateTimeKind.Utc).AddTicks(8371), new DateTime(2026, 12, 21, 9, 29, 4, 29, DateTimeKind.Utc).AddTicks(8140), new DateTime(2025, 12, 21, 9, 29, 4, 29, DateTimeKind.Utc).AddTicks(7867) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2025, 12, 21, 9, 29, 4, 29, DateTimeKind.Utc).AddTicks(8702), new DateTime(2026, 12, 21, 9, 29, 4, 29, DateTimeKind.Utc).AddTicks(8698), new DateTime(2025, 12, 21, 9, 29, 4, 29, DateTimeKind.Utc).AddTicks(8697) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 46, DateTimeKind.Utc).AddTicks(489));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 46, DateTimeKind.Utc).AddTicks(983));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 46, DateTimeKind.Utc).AddTicks(1046));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 46, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 46, DateTimeKind.Utc).AddTicks(1081));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 46, DateTimeKind.Utc).AddTicks(1096));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 46, DateTimeKind.Utc).AddTicks(1109));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 46, DateTimeKind.Utc).AddTicks(1121));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 46, DateTimeKind.Utc).AddTicks(1134));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9494));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9500));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9501));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9502));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9503));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9503));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9504));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9523));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9524));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9524));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9525));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9526));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9526));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9527));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9528));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9529));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9529));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9530));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9530));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9531));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9532));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9532));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9533));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9882));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9884));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9885));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9885));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9886));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9887));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9887));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9888));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9888));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9889));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9890));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9890));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9891));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9891));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9892));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9892));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9894));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9894));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9895));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9895));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9896));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9896));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9902));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9903));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9903));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9904));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9904));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9905));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9905));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9906));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9906));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9907));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9907));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9908));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9908));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9909));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9910));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9910));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9911));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9911));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9912));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9912));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9913));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9913));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 30, DateTimeKind.Utc).AddTicks(9914));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5082));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5098));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5100));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5101));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5102));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5106));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5107));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5108));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5109));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5111));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5112));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5113));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5114));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5115));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5116));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 31, DateTimeKind.Utc).AddTicks(5118));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2058));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2079));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2080));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2081));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2082));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2086));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2087));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2088));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2088));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2090));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2090));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2091));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2092));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2092));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2093));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2094));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2192));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2195));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2210));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2211));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2212));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2212));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2213));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2214));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2215));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2215));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2216));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2217));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2229));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2230));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2230));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2231));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2232));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2234));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2245));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2246));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2247));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2248));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2249));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(2250));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5563));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5573));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5574));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5575));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5576));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5580));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5581));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5582));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5583));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5584));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5585));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5586));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5587));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5588));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5589));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5589));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5610));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5612));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5613));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5614));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5615));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5616));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5617));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5617));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5635));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5636));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 21, 9, 29, 4, 33, DateTimeKind.Utc).AddTicks(5637));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "d52564d4-5adc-47bc-a6b6-4d75ed717af9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "23eff13a-ea93-46e6-9d36-1a7679fc26a6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6570d4b3-ab72-431f-a59f-bbf508d5bb39");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "22032332-50aa-49cb-92b4-f78219037b24");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "f4fba55f-611a-4bb5-951a-39c4ef9d1fe3");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "6717634a-9d0c-4421-8a7d-e55bbfb8bfc6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "8df3eecc-f760-4d82-a512-5ae8fd030c76");
        }
    }
}
