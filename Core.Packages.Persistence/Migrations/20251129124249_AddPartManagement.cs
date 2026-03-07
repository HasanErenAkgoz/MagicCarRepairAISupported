using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPartManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_Roles_Name",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_Name",
                table: "Permissions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ContactEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SubscriptionStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SubscriptionEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErrorCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Language = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityNo = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DateTimeOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false, defaultValue: "tr"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmploymentStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Specializations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BloodType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    EmergencyContact = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmergencyPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartSuppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TaxOffice = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PaymentTerms = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
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
                    table.PrimaryKey("PK_PartSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartSuppliers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Kilometers = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false),
                    BrandType = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OEMNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 20m),
                    MinimumStockLevel = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    IsLowStockAlertEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Adet"),
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
                    table.PrimaryKey("PK_Parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parts_PartSuppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "PartSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PartStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdatedByEmployeeId = table.Column<int>(type: "int", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartStocks_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartStocks_Employees_LastUpdatedByEmployeeId",
                        column: x => x.LastUpdatedByEmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PartStocks_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartId = table.Column<int>(type: "int", nullable: false),
                    MovementType = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MovementDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ReferenceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TargetLocationId = table.Column<int>(type: "int", nullable: true),
                    TargetLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockMovements_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_StockMovements_Parts_PartId",
                        column: x => x.PartId,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "Code", "ContactEmail", "ContactPhone", "CreatedBy", "CreatedDate", "Description", "IsActive", "ModifiedBy", "ModifiedDate", "Name", "Status", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[,]
                {
                    { 1, null, "DEMO001", "demo@example.com", "+90 555 123 4567", 0, new DateTime(2025, 11, 29, 12, 42, 48, 535, DateTimeKind.Utc).AddTicks(783), "Demo tenant for testing", true, null, null, "Demo Client", 1, new DateTime(2026, 11, 29, 12, 42, 48, 535, DateTimeKind.Utc).AddTicks(551), new DateTime(2025, 11, 29, 12, 42, 48, 535, DateTimeKind.Utc).AddTicks(289) },
                    { 2, null, "TEST001", "test@example.com", "+90 555 987 6543", 0, new DateTime(2025, 11, 29, 12, 42, 48, 535, DateTimeKind.Utc).AddTicks(1113), "Test tenant for development", true, null, null, "Test Client", 1, new DateTime(2026, 11, 29, 12, 42, 48, 535, DateTimeKind.Utc).AddTicks(1109), new DateTime(2025, 11, 29, 12, 42, 48, 535, DateTimeKind.Utc).AddTicks(1108) }
                });

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 1);

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

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 3);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ClientId", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IdentityNo", "Language", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "UserType" },
                values: new object[] { 1, 0, 1, Guid.NewGuid().ToString(), "system@magiccarrepair.com", true, "System", "00000000000", "tr", "User", false, null, "SYSTEM@MAGICCARREPAIR.COM", "SYSTEM", "AQAAAAIAAYagAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==", null, false, Guid.NewGuid().ToString(), false, "system", 3 });

            migrationBuilder.Sql("UPDATE Clients SET CreatedBy = 1 WHERE Id IN (1, 2)");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "RolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.InsertData(
                table: "ErrorMessages",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "ErrorCode", "Language", "Message", "ModifiedBy", "ModifiedDate", "Status" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1710), null, "ALREADY_EXISTS", "tr", "Zaten mevcut.", null, null, 1 },
                    { 2, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1714), null, "ADDED_SUCCESSFULLY", "tr", "Baþarýyla eklendi.", null, null, 1 },
                    { 3, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1715), null, "UPDATED_SUCCESSFULLY", "tr", "Baþarýyla güncellendi.", null, null, 1 },
                    { 4, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1716), null, "DELETED_SUCCESSFULLY", "tr", "Baþarýyla silindi.", null, null, 1 },
                    { 5, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1716), null, "NOT_FOUND", "tr", "Bulunamadý.", null, null, 1 },
                    { 6, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1717), null, "UNAUTHORIZED_ACCESS", "tr", "Yetkisiz eriþim.", null, null, 1 },
                    { 7, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1718), null, "INVALID_OPERATION", "tr", "Geçersiz iþlem.", null, null, 1 },
                    { 8, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1728), null, "SERVER_ERROR", "tr", "Sunucu hatasý oluþtu.", null, null, 1 },
                    { 9, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1729), null, "VALIDATION_NOT_EMPTY", "tr", "{PropertyName} alaný zorunludur.", null, null, 1 },
                    { 10, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1730), null, "VALIDATION_EMAIL_INVALID", "tr", "E-posta adresi geçerli deðil.", null, null, 1 },
                    { 11, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1730), null, "VALIDATION_PASSWORDS_DO_NOT_MATCH", "tr", "Þifreler eþleþmiyor.", null, null, 1 },
                    { 12, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1731), null, "VALIDATION_PASSWORD_LENGTH", "tr", "Þifre en az 6 karakter olmalýdýr.", null, null, 1 },
                    { 13, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1732), null, "VALIDATION_PASSWORD_UPPERCASE", "tr", "Þifre en az bir büyük harf içermelidir.", null, null, 1 },
                    { 14, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1732), null, "VALIDATION_PASSWORD_DIGIT", "tr", "Þifre en az bir rakam içermelidir.", null, null, 1 },
                    { 15, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1733), null, "VALIDATION_PASSWORD_SPECIAL_CHARACTER", "tr", "Þifre en az bir özel karakter içermelidir.", null, null, 1 },
                    { 16, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1733), null, "EMAIL_SENT_SUCCESSFULLY", "tr", "E-posta baþarýyla gönderildi!", null, null, 1 },
                    { 17, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1734), null, "EMAIL_SEND_FAILED", "tr", "E-posta gönderilemedi!", null, null, 1 },
                    { 18, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1735), null, "CLIENT_CODE_EXISTS", "tr", "{Code} koduna sahip müþteri zaten mevcut.", null, null, 1 },
                    { 19, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1735), null, "CLIENT_NOT_FOUND", "tr", "Müþteri bulunamadý.", null, null, 1 },
                    { 20, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1736), null, "VEHICLE_KM_LOWER_THAN_CURRENT", "tr", "{LicensePlate} plakalý araç için yeni kilometre ({NewKilometers}) mevcut kilometreden ({CurrentKilometers}) düþük olamaz.", null, null, 1 },
                    { 21, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1752), null, "VEHICLE_KM_NEGATIVE", "tr", "{LicensePlate} plakalý araç için kilometre negatif olamaz ({NewKilometers}).", null, null, 1 },
                    { 22, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1753), null, "EMPLOYEE_NO_EXISTS", "tr", "{EmployeeNo} numaralý personel zaten mevcut.", null, null, 1 },
                    { 23, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1753), null, "EMPLOYEE_NOT_FOUND", "tr", "Personel bulunamadý.", null, null, 1 },
                    { 24, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1996), null, "ALREADY_EXISTS", "en", "Already exists.", null, null, 1 },
                    { 25, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1997), null, "ADDED_SUCCESSFULLY", "en", "Added successfully.", null, null, 1 },
                    { 26, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1998), null, "UPDATED_SUCCESSFULLY", "en", "Updated successfully.", null, null, 1 },
                    { 27, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1998), null, "DELETED_SUCCESSFULLY", "en", "Deleted successfully.", null, null, 1 },
                    { 28, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(1999), null, "NOT_FOUND", "en", "Not found.", null, null, 1 },
                    { 29, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2000), null, "UNAUTHORIZED_ACCESS", "en", "Unauthorized access.", null, null, 1 },
                    { 30, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2000), null, "INVALID_OPERATION", "en", "Invalid operation.", null, null, 1 },
                    { 31, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2001), null, "SERVER_ERROR", "en", "A server error occurred.", null, null, 1 },
                    { 32, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2001), null, "VALIDATION_NOT_EMPTY", "en", "{PropertyName} is required.", null, null, 1 },
                    { 33, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2002), null, "VALIDATION_EMAIL_INVALID", "en", "Email address is not valid.", null, null, 1 },
                    { 34, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2003), null, "VALIDATION_PASSWORDS_DO_NOT_MATCH", "en", "Passwords do not match.", null, null, 1 },
                    { 35, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2003), null, "VALIDATION_PASSWORD_LENGTH", "en", "Password must be at least 6 characters.", null, null, 1 },
                    { 36, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2004), null, "VALIDATION_PASSWORD_UPPERCASE", "en", "Password must contain at least one uppercase letter.", null, null, 1 },
                    { 37, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2004), null, "VALIDATION_PASSWORD_DIGIT", "en", "Password must contain at least one digit.", null, null, 1 },
                    { 38, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2005), null, "VALIDATION_PASSWORD_SPECIAL_CHARACTER", "en", "Password must contain at least one special character.", null, null, 1 },
                    { 39, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2005), null, "EMAIL_SENT_SUCCESSFULLY", "en", "Email sent successfully!", null, null, 1 },
                    { 40, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2006), null, "EMAIL_SEND_FAILED", "en", "Failed to send email!", null, null, 1 },
                    { 41, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2006), null, "CLIENT_CODE_EXISTS", "en", "A client with code {Code} already exists.", null, null, 1 },
                    { 42, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2007), null, "CLIENT_NOT_FOUND", "en", "Client not found.", null, null, 1 },
                    { 43, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2008), null, "VEHICLE_KM_LOWER_THAN_CURRENT", "en", "New kilometers ({NewKilometers}) for vehicle {LicensePlate} cannot be lower than current kilometers ({CurrentKilometers}).", null, null, 1 },
                    { 44, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2008), null, "VEHICLE_KM_NEGATIVE", "en", "Kilometers cannot be negative ({NewKilometers}) for vehicle {LicensePlate}.", null, null, 1 },
                    { 45, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2009), null, "EMPLOYEE_NO_EXISTS", "en", "Employee with number {EmployeeNo} already exists.", null, null, 1 },
                    { 46, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2009), null, "EMPLOYEE_NOT_FOUND", "en", "Employee not found.", null, null, 1 },
                    { 47, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2013), null, "ALREADY_EXISTS", "ar", "????? ??????.", null, null, 1 },
                    { 48, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2014), null, "ADDED_SUCCESSFULLY", "ar", "??? ??????? ?????.", null, null, 1 },
                    { 49, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2015), null, "UPDATED_SUCCESSFULLY", "ar", "?? ??????? ?????.", null, null, 1 },
                    { 50, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2015), null, "DELETED_SUCCESSFULLY", "ar", "?? ????? ?????.", null, null, 1 },
                    { 51, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2016), null, "NOT_FOUND", "ar", "??? ?????.", null, null, 1 },
                    { 52, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2016), null, "UNAUTHORIZED_ACCESS", "ar", "???? ??? ???? ??.", null, null, 1 },
                    { 53, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2017), null, "INVALID_OPERATION", "ar", "????? ??? ?????.", null, null, 1 },
                    { 54, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2017), null, "SERVER_ERROR", "ar", "??? ??? ?? ??????.", null, null, 1 },
                    { 55, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2018), null, "VALIDATION_NOT_EMPTY", "ar", "{PropertyName} ?????.", null, null, 1 },
                    { 56, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2019), null, "VALIDATION_EMAIL_INVALID", "ar", "?????? ?????????? ??? ????.", null, null, 1 },
                    { 57, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2019), null, "VALIDATION_PASSWORDS_DO_NOT_MATCH", "ar", "????? ?????? ??? ???????.", null, null, 1 },
                    { 58, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2020), null, "VALIDATION_PASSWORD_LENGTH", "ar", "??? ?? ???? ???? ?????? 6 ???? ??? ?????.", null, null, 1 },
                    { 59, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2020), null, "VALIDATION_PASSWORD_UPPERCASE", "ar", "??? ?? ????? ???? ?????? ??? ??? ???? ???? ??? ?????.", null, null, 1 },
                    { 60, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2021), null, "VALIDATION_PASSWORD_DIGIT", "ar", "??? ?? ????? ???? ?????? ??? ??? ???? ??? ?????.", null, null, 1 },
                    { 61, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2021), null, "VALIDATION_PASSWORD_SPECIAL_CHARACTER", "ar", "??? ?? ????? ???? ?????? ??? ??? ??? ???? ??? ?????.", null, null, 1 },
                    { 62, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2022), null, "EMAIL_SENT_SUCCESSFULLY", "ar", "?? ????? ?????? ?????????? ?????!", null, null, 1 },
                    { 63, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2022), null, "EMAIL_SEND_FAILED", "ar", "??? ????? ?????? ??????????!", null, null, 1 },
                    { 64, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2023), null, "CLIENT_CODE_EXISTS", "ar", "?????? ???? {Code} ????? ??????.", null, null, 1 },
                    { 65, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2024), null, "CLIENT_NOT_FOUND", "ar", "?????? ??? ?????.", null, null, 1 },
                    { 66, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2024), null, "VEHICLE_KM_LOWER_THAN_CURRENT", "ar", "??????????? ??????? ({NewKilometers}) ??????? {LicensePlate} ?? ???? ?? ???? ??? ?? ??????????? ??????? ({CurrentKilometers}).", null, null, 1 },
                    { 67, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2025), null, "VEHICLE_KM_NEGATIVE", "ar", "?? ???? ?? ???? ??????????? ????? ({NewKilometers}) ??????? {LicensePlate}.", null, null, 1 },
                    { 68, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2025), null, "EMPLOYEE_NO_EXISTS", "ar", "?????? ???? {EmployeeNo} ????? ??????.", null, null, 1 },
                    { 69, 0, new DateTime(2025, 11, 29, 12, 42, 48, 536, DateTimeKind.Utc).AddTicks(2026), null, "EMPLOYEE_NOT_FOUND", "ar", "?????? ??? ?????.", null, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "BloodType", "ClientId", "CreatedBy", "CreatedDate", "Email", "EmergencyContact", "EmergencyPhone", "EmployeeNo", "EmploymentStatus", "FirstName", "HireDate", "LastName", "ModifiedBy", "ModifiedDate", "NationalId", "Notes", "Phone", "Position", "Salary", "Specializations", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, "Ýstanbul, Türkiye", "A+", 1, 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(8660), "ahmet.yilmaz@democlient.com", "Ayþe Yýlmaz", "+905329876543", "EMP001", 1, "Ahmet", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yýlmaz", null, null, "12345678901", null, "+905321234567", 1, 25000m, "[\"Genel Y\\u00F6netim\",\"Strateji\",\"\\u0130\\u015F Geli\\u015Ftirme\"]", 1, null },
                    { 2, "Ýstanbul, Türkiye", "0+", 1, 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9243), "mehmet.demir@democlient.com", "Fatma Demir", "+905339876543", "EMP002", 1, "Mehmet", new DateTime(2020, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Demir", null, null, "23456789012", null, "+905331234567", 2, 18000m, "[\"Servis Y\\u00F6netimi\",\"M\\u00FC\\u015Fteri \\u0130li\\u015Fkileri\"]", 1, null },
                    { 3, "Ýstanbul, Türkiye", "B+", 1, 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9305), "ali.kaya@democlient.com", "Zeynep Kaya", "+905349876543", "EMP003", 1, "Ali", new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kaya", null, null, "34567890123", null, "+905341234567", 4, 16000m, "[\"Motor Bak\\u0131m\",\"Elektrik Sistemleri\",\"Fren Sistemleri\",\"Ekip Y\\u00F6netimi\"]", 1, null },
                    { 4, "Ýstanbul, Türkiye", "A-", 1, 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9320), "mustafa.celik@democlient.com", "Emine Çelik", "+905359876543", "EMP004", 1, "Mustafa", new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Çelik", null, null, "45678901234", null, "+905351234567", 5, 13000m, "[\"Motor Bak\\u0131m\",\"\\u015Eanz\\u0131man\",\"Diferansiyel\"]", 1, null },
                    { 5, "Ýstanbul, Türkiye", "0-", 1, 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9332), "hasan.ozturk@democlient.com", "Ayþe Öztürk", "+905369876543", "EMP005", 1, "Hasan", new DateTime(2021, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Öztürk", null, null, "56789012345", null, "+905361234567", 5, 12000m, "[\"Fren Sistemleri\",\"S\\u00FCspansiyon\",\"Rot-Balans\"]", 1, null },
                    { 6, "Ýstanbul, Türkiye", "AB+", 1, 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9344), "emre.yildiz@democlient.com", "Seda Yýldýz", "+905379876543", "EMP006", 1, "Emre", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yýldýz", null, null, "67890123456", null, "+905371234567", 6, 14000m, "[\"Elektrik Sistemleri\",\"Elektronik\",\"Ak\\u00FC ve Jenerat\\u00F6r\"]", 1, null },
                    { 7, "Ýstanbul, Türkiye", "A+", 1, 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9358), "caner.arslan@democlient.com", "Elif Arslan", "+905389876543", "EMP007", 1, "Caner", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arslan", null, null, "78901234567", null, "+905381234567", 7, 13500m, "[\"Kaporta Tamir\",\"Kaynak\",\"Hasar Onar\\u0131m\\u0131\"]", 1, null },
                    { 8, "Ýstanbul, Türkiye", "0+", 1, 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9369), "burak.sahin@democlient.com", "Merve Þahin", "+905399876543", "EMP008", 1, "Burak", new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Þahin", null, null, "89012345678", null, "+905391234567", 8, 12500m, "[\"Boya\",\"Vernik\",\"Renk E\\u015Fleme\"]", 1, null },
                    { 9, "Ankara, Türkiye", "B+", 2, 0, new DateTime(2025, 11, 29, 12, 42, 48, 552, DateTimeKind.Utc).AddTicks(9379), "kemal.yalcin@testclient.com", "Sevgi Yalçýn", "+905409876543", "TST001", 1, "Kemal", new DateTime(2021, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yalçýn", null, null, "90123456789", null, "+905401234567", 2, 17000m, "[\"Servis Y\\u00F6netimi\",\"Kalite Kontrol\"]", 1, null }
                });


            migrationBuilder.CreateIndex(
                name: "IX_Users_ClientId",
                table: "Users",
                column: "ClientId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserType",
                table: "Users",
                column: "UserType");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ClientId",
                table: "Roles",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name_ClientId",
                table: "Roles",
                columns: new[] { "Name", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_ClientId",
                table: "RolePermissions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId_PermissionId_ClientId",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ClientId",
                table: "Permissions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name_ClientId",
                table: "Permissions",
                columns: new[] { "Name", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Code",
                table: "Clients",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ClientId",
                table: "Customers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_IdentityNo_ClientId",
                table: "Customers",
                columns: new[] { "IdentityNo", "ClientId" });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ClientId",
                table: "Employees",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeNo_ClientId",
                table: "Employees",
                columns: new[] { "EmployeeNo", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmploymentStatus",
                table: "Employees",
                column: "EmploymentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Position",
                table: "Employees",
                column: "Position");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorMessages_ErrorCode_Language",
                table: "ErrorMessages",
                columns: new[] { "ErrorCode", "Language" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_Barcode",
                table: "Parts",
                column: "Barcode");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_BrandType",
                table: "Parts",
                column: "BrandType");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_Category",
                table: "Parts",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_ClientId",
                table: "Parts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_PartCode_ClientId",
                table: "Parts",
                columns: new[] { "PartCode", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parts_SupplierId",
                table: "Parts",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PartStocks_ClientId",
                table: "PartStocks",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PartStocks_LastUpdatedByEmployeeId",
                table: "PartStocks",
                column: "LastUpdatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartStocks_PartId",
                table: "PartStocks",
                column: "PartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PartSuppliers_ClientId",
                table: "PartSuppliers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PartSuppliers_CompanyName_ClientId",
                table: "PartSuppliers",
                columns: new[] { "CompanyName", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ClientId",
                table: "StockMovements",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_EmployeeId",
                table: "StockMovements",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_MovementDate",
                table: "StockMovements",
                column: "MovementDate");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_MovementType",
                table: "StockMovements",
                column: "MovementType");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_PartId",
                table: "StockMovements",
                column: "PartId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ReferenceNumber",
                table: "StockMovements",
                column: "ReferenceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ClientId",
                table: "Vehicles",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CustomerId",
                table: "Vehicles",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LicensePlate_ClientId",
                table: "Vehicles",
                columns: new[] { "LicensePlate", "ClientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Clients_ClientId",
                table: "Permissions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Clients_ClientId",
                table: "Roles",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Clients_ClientId",
                table: "RolePermissions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Clients_ClientId",
                table: "Users",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Clients_ClientId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Clients_ClientId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Clients_ClientId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Clients_ClientId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Customers_CustomerId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Employees_EmployeeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ErrorMessages");

            migrationBuilder.DropTable(
                name: "PartStocks");

            migrationBuilder.DropTable(
                name: "StockMovements");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "PartSuppliers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Users_ClientId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CustomerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserType",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ClientId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_Name_ClientId",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_ClientId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_RolePermissions_RoleId_PermissionId_ClientId",
                table: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ClientId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_Name_ClientId",
                table: "Permissions");


            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "RolePermissions");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Permissions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RolePermissions",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Permissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
