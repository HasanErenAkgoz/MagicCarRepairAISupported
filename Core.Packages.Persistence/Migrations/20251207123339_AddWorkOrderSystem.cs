using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkOrderSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Priority = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    Kilometers = table.Column<long>(type: "bigint", nullable: true),
                    FuelLevel = table.Column<int>(type: "int", maxLength: 3, nullable: true),
                    CustomerComplaints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialRequests = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0m),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Notes = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    AssignedEmployeeId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Employees_AssignedEmployeeId",
                        column: x => x.AssignedEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    PartId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 1m),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 0m),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 20m),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    BrandType = table.Column<int>(type: "int", nullable: true),
                    WarrantyMonths = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderItems_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderItems_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WorkOrderItems_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderLabors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    OperationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurationHours = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
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
                    table.PrimaryKey("PK_WorkOrderLabors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderLabors_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderLabors_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderLabors_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderTimelines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusChange = table.Column<int>(type: "int", nullable: true),
                    OldStatus = table.Column<int>(type: "int", nullable: true),
                    NewStatus = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PhotoIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "StatusChange"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderTimelines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderTimelines_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderTimelines_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WorkOrderTimelines_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    TimelineId = table.Column<int>(type: "int", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UploadedFileId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PhotoType = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedByEmployeeId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderPhotos_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderPhotos_Employees_UploadedByEmployeeId",
                        column: x => x.UploadedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WorkOrderPhotos_UploadedFiles_UploadedFileId",
                        column: x => x.UploadedFileId,
                        principalTable: "UploadedFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WorkOrderPhotos_WorkOrderTimelines_TimelineId",
                        column: x => x.TimelineId,
                        principalTable: "WorkOrderTimelines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_WorkOrderPhotos_WorkOrders_WorkOrderId",
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
                values: new object[] { new DateTime(2025, 12, 7, 12, 33, 38, 577, DateTimeKind.Utc).AddTicks(8515), new DateTime(2026, 12, 7, 12, 33, 38, 577, DateTimeKind.Utc).AddTicks(8286), new DateTime(2025, 12, 7, 12, 33, 38, 577, DateTimeKind.Utc).AddTicks(8022) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2025, 12, 7, 12, 33, 38, 577, DateTimeKind.Utc).AddTicks(8835), new DateTime(2026, 12, 7, 12, 33, 38, 577, DateTimeKind.Utc).AddTicks(8832), new DateTime(2025, 12, 7, 12, 33, 38, 577, DateTimeKind.Utc).AddTicks(8832) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 594, DateTimeKind.Utc).AddTicks(981));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 594, DateTimeKind.Utc).AddTicks(1533));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 594, DateTimeKind.Utc).AddTicks(1562));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 594, DateTimeKind.Utc).AddTicks(1603));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 594, DateTimeKind.Utc).AddTicks(1616));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 594, DateTimeKind.Utc).AddTicks(1629));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 594, DateTimeKind.Utc).AddTicks(1641));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 594, DateTimeKind.Utc).AddTicks(1653));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 594, DateTimeKind.Utc).AddTicks(1663));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9387));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9391));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9393));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9393));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9394));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9395));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9395));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9396));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9396));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9397));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9398));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9398));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9399));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9399));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9417));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9418));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9419));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9419));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9420));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9421));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9430));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9431));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9431));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9789));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9790));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9791));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9791));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9792));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9792));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9793));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9794));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9794));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9795));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9795));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9796));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9796));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9797));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9797));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9798));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9798));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9799));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9800));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9800));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9801));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9801));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9802));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9807));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9808));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9808));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9809));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9809));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9810));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9810));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9811));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9811));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9812));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9813));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9813));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9814));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9814));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9815));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9815));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9816));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9816));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9817));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9818));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9818));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9819));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 578, DateTimeKind.Utc).AddTicks(9819));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5638));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5652));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5653));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5655));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5656));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5660));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5661));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5662));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5663));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5665));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5666));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5667));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5668));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5669));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5670));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 579, DateTimeKind.Utc).AddTicks(5671));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(422));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(434));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(435));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(436));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(437));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(439));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(440));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(440));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(441));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(442));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(443));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(443));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(444));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(444));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(445));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(445));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(541));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(543));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(544));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(545));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(545));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(546));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(547));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(547));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(548));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(548));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(548));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(549));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(553));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(554));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(554));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(555));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(555));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(556));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(559));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(560));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(560));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(561));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(562));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2804));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2810));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2811));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2811));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2812));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2815));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2815));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2816));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2816));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2817));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2818));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2818));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2819));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2820));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2821));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2833));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2835));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2835));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2836));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2845));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2845));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2846));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2846));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2850));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2851));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 7, 12, 33, 38, 581, DateTimeKind.Utc).AddTicks(2852));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c0d7f970-07d1-42d9-ab71-fbd09169f4c9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "47120a9d-b3fb-4be4-9e6c-e59371c1baea");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5a8705b7-2d0d-4d1f-99e8-39f51c8c8547");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "d281c18b-bb86-4272-8831-26414b7fc9bc");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "8f0a90fa-0604-43f9-ac11-cfe831e83561");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "ebc0f4be-a6e7-4b39-9947-fafa72e02d71");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "a4e92bd1-d3a6-47bb-9319-a760994fc817");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderItems_ClientId",
                table: "WorkOrderItems",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderItems_ItemType",
                table: "WorkOrderItems",
                column: "ItemType");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderItems_PartId",
                table: "WorkOrderItems",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderItems_WorkOrderId",
                table: "WorkOrderItems",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderLabors_ClientId",
                table: "WorkOrderLabors",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderLabors_EmployeeId",
                table: "WorkOrderLabors",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderLabors_WorkOrderId",
                table: "WorkOrderLabors",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderPhotos_ClientId",
                table: "WorkOrderPhotos",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderPhotos_PhotoType",
                table: "WorkOrderPhotos",
                column: "PhotoType");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderPhotos_TimelineId",
                table: "WorkOrderPhotos",
                column: "TimelineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderPhotos_UploadedByEmployeeId",
                table: "WorkOrderPhotos",
                column: "UploadedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderPhotos_UploadedFileId",
                table: "WorkOrderPhotos",
                column: "UploadedFileId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderPhotos_WorkOrderId",
                table: "WorkOrderPhotos",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_AssignedEmployeeId",
                table: "WorkOrders",
                column: "AssignedEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ClientId",
                table: "WorkOrders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_CustomerId",
                table: "WorkOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_EntryDate",
                table: "WorkOrders",
                column: "EntryDate");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_Priority",
                table: "WorkOrders",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_Status",
                table: "WorkOrders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_VehicleId",
                table: "WorkOrders",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkOrderNumber",
                table: "WorkOrders",
                column: "WorkOrderNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderTimelines_ClientId",
                table: "WorkOrderTimelines",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderTimelines_EmployeeId",
                table: "WorkOrderTimelines",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderTimelines_EventDate",
                table: "WorkOrderTimelines",
                column: "EventDate");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderTimelines_WorkOrderId",
                table: "WorkOrderTimelines",
                column: "WorkOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrderItems");

            migrationBuilder.DropTable(
                name: "WorkOrderLabors");

            migrationBuilder.DropTable(
                name: "WorkOrderPhotos");

            migrationBuilder.DropTable(
                name: "WorkOrderTimelines");

            migrationBuilder.DropTable(
                name: "WorkOrders");

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
        }
    }
}
