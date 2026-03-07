using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddClientProfileAndPortfolioEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Employees",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoUrl",
                table: "Employees",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecializationsJson",
                table: "Employees",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutUs",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublicProfileEnabled",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Clients",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Services",
                table: "Clients",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialMediaLinks",
                table: "Clients",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteUrl",
                table: "Clients",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingHours",
                table: "Clients",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificateName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IssuingOrganization = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CertificateNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CertificateFileUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FacilityPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhotoPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityPhotos_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServicePortfolios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Categories = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FeaturedPhotoIds = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    CustomerApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerRejectionReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePortfolios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePortfolios_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServicePortfolios_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AboutUs", "CreatedDate", "IsPublicProfileEnabled", "LogoUrl", "Services", "SocialMediaLinks", "SubscriptionEndDate", "SubscriptionStartDate", "WebsiteUrl", "WorkingHours" },
                values: new object[] { null, new DateTime(2026, 1, 11, 19, 13, 50, 545, DateTimeKind.Utc).AddTicks(2609), false, null, null, null, new DateTime(2027, 1, 11, 19, 13, 50, 545, DateTimeKind.Utc).AddTicks(2376), new DateTime(2026, 1, 11, 19, 13, 50, 545, DateTimeKind.Utc).AddTicks(2120), null, null });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AboutUs", "CreatedDate", "IsPublicProfileEnabled", "LogoUrl", "Services", "SocialMediaLinks", "SubscriptionEndDate", "SubscriptionStartDate", "WebsiteUrl", "WorkingHours" },
                values: new object[] { null, new DateTime(2026, 1, 11, 19, 13, 50, 545, DateTimeKind.Utc).AddTicks(2930), false, null, null, null, new DateTime(2027, 1, 11, 19, 13, 50, 545, DateTimeKind.Utc).AddTicks(2926), new DateTime(2026, 1, 11, 19, 13, 50, 545, DateTimeKind.Utc).AddTicks(2926), null, null });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Biography", "CreatedDate", "DisplayOrder", "IsPublic", "ProfilePhotoUrl", "SpecializationsJson" },
                values: new object[] { null, new DateTime(2026, 1, 11, 19, 13, 50, 561, DateTimeKind.Utc).AddTicks(4052), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Biography", "CreatedDate", "DisplayOrder", "IsPublic", "ProfilePhotoUrl", "SpecializationsJson" },
                values: new object[] { null, new DateTime(2026, 1, 11, 19, 13, 50, 561, DateTimeKind.Utc).AddTicks(4560), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Biography", "CreatedDate", "DisplayOrder", "IsPublic", "ProfilePhotoUrl", "SpecializationsJson" },
                values: new object[] { null, new DateTime(2026, 1, 11, 19, 13, 50, 561, DateTimeKind.Utc).AddTicks(4586), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Biography", "CreatedDate", "DisplayOrder", "IsPublic", "ProfilePhotoUrl", "SpecializationsJson" },
                values: new object[] { null, new DateTime(2026, 1, 11, 19, 13, 50, 561, DateTimeKind.Utc).AddTicks(4601), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Biography", "CreatedDate", "DisplayOrder", "IsPublic", "ProfilePhotoUrl", "SpecializationsJson" },
                values: new object[] { null, new DateTime(2026, 1, 11, 19, 13, 50, 561, DateTimeKind.Utc).AddTicks(4613), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Biography", "CreatedDate", "DisplayOrder", "IsPublic", "ProfilePhotoUrl", "SpecializationsJson" },
                values: new object[] { null, new DateTime(2026, 1, 11, 19, 13, 50, 561, DateTimeKind.Utc).AddTicks(4626), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Biography", "CreatedDate", "DisplayOrder", "IsPublic", "ProfilePhotoUrl", "SpecializationsJson" },
                values: new object[] { null, new DateTime(2026, 1, 11, 19, 13, 50, 561, DateTimeKind.Utc).AddTicks(4639), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Biography", "CreatedDate", "DisplayOrder", "IsPublic", "ProfilePhotoUrl", "SpecializationsJson" },
                values: new object[] { null, new DateTime(2026, 1, 11, 19, 13, 50, 561, DateTimeKind.Utc).AddTicks(4650), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Biography", "CreatedDate", "DisplayOrder", "IsPublic", "ProfilePhotoUrl", "SpecializationsJson" },
                values: new object[] { null, new DateTime(2026, 1, 11, 19, 13, 50, 561, DateTimeKind.Utc).AddTicks(4661), 0, false, null, null });

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3419));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3424));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3424));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3425));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3426));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3426));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3427));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3428));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3428));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3429));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3430));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3430));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3431));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3431));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3432));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3432));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3433));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3434));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3434));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3435));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3435));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3436));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3436));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3679));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3680));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3681));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3681));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3682));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3682));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3683));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3683));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3684));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3684));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3685));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3685));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3686));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3687));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3687));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3688));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3688));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3689));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3689));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3690));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3690));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3691));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3691));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3703));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3704));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3705));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3705));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3706));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3706));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3707));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3707));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3708));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3708));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3709));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3709));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3710));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3710));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3711));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3711));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3712));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3713));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3713));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3714));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3714));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3715));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(3715));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 549, DateTimeKind.Utc).AddTicks(1319));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 549, DateTimeKind.Utc).AddTicks(1334));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 549, DateTimeKind.Utc).AddTicks(1336));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 549, DateTimeKind.Utc).AddTicks(1338));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 549, DateTimeKind.Utc).AddTicks(1340));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 549, DateTimeKind.Utc).AddTicks(1352));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 549, DateTimeKind.Utc).AddTicks(1354));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 549, DateTimeKind.Utc).AddTicks(1355));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 549, DateTimeKind.Utc).AddTicks(1357));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 549, DateTimeKind.Utc).AddTicks(1359));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8779));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8791));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8793));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8794));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8795));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8799));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8800));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8801));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8802));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8803));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8804));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8805));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8806));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8807));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8809));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 546, DateTimeKind.Utc).AddTicks(8810));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3438));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3451));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3452));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3453));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3454));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3456));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3457));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3458));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3458));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3459));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3460));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3460));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3461));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3462));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3462));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3463));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3545));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3547));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3547));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3548));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3549));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3549));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3550));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3550));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3551));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3551));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3552));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3552));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3556));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3557));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3557));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3558));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3558));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3559));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3562));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3563));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3563));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3564));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3571));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(3572));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5677));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5682));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5682));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5683));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5684));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5686));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5686));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5687));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5688));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5689));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5689));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5690));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5690));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5691));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5691));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5692));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5703));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5704));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5704));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5705));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5705));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5706));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5706));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5707));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5710));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5711));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5711));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 19, 13, 50, 548, DateTimeKind.Utc).AddTicks(5712));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "9fb729a9-72d7-4706-8481-8b382097e0d9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "679323dd-511e-487f-a24c-06615de92130");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "daa03f02-4417-48f0-aa25-d9aace64b4cd");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "570ea2bd-b969-4b0d-8bd7-d5ec9cdd6014");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "1ae7c737-a283-48d8-be3c-0dcb02af8840");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "1559913b-3760-4d67-bca9-e8dda72908b4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "da22adb7-f911-4188-9c02-68381b7d57d6");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_ClientId",
                table: "Certificates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_ClientId_IsPublic",
                table: "Certificates",
                columns: new[] { "ClientId", "IsPublic" });

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_ExpiryDate",
                table: "Certificates",
                column: "ExpiryDate");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityPhotos_ClientId",
                table: "FacilityPhotos",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityPhotos_ClientId_IsPublic_Category",
                table: "FacilityPhotos",
                columns: new[] { "ClientId", "IsPublic", "Category" });

            migrationBuilder.CreateIndex(
                name: "IX_ServicePortfolios_ClientId",
                table: "ServicePortfolios",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePortfolios_ClientId_IsPublished_CustomerApprovalStatus",
                table: "ServicePortfolios",
                columns: new[] { "ClientId", "IsPublished", "CustomerApprovalStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_ServicePortfolios_WorkOrderId",
                table: "ServicePortfolios",
                column: "WorkOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "FacilityPhotos");

            migrationBuilder.DropTable(
                name: "ServicePortfolios");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoUrl",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SpecializationsJson",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AboutUs",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsPublicProfileEnabled",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Services",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "SocialMediaLinks",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "WebsiteUrl",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "WorkingHours",
                table: "Clients");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 11, 18, 0, 40, 801, DateTimeKind.Utc).AddTicks(7375), new DateTime(2027, 1, 11, 18, 0, 40, 801, DateTimeKind.Utc).AddTicks(7143), new DateTime(2026, 1, 11, 18, 0, 40, 801, DateTimeKind.Utc).AddTicks(6882) });

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SubscriptionEndDate", "SubscriptionStartDate" },
                values: new object[] { new DateTime(2026, 1, 11, 18, 0, 40, 801, DateTimeKind.Utc).AddTicks(7705), new DateTime(2027, 1, 11, 18, 0, 40, 801, DateTimeKind.Utc).AddTicks(7701), new DateTime(2026, 1, 11, 18, 0, 40, 801, DateTimeKind.Utc).AddTicks(7700) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 818, DateTimeKind.Utc).AddTicks(4362));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 818, DateTimeKind.Utc).AddTicks(4861));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 818, DateTimeKind.Utc).AddTicks(4888));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 818, DateTimeKind.Utc).AddTicks(4903));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 818, DateTimeKind.Utc).AddTicks(4917));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 818, DateTimeKind.Utc).AddTicks(4932));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 818, DateTimeKind.Utc).AddTicks(4944));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 818, DateTimeKind.Utc).AddTicks(4956));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 818, DateTimeKind.Utc).AddTicks(4966));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8059));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8063));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8065));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8082));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8083));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8084));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8084));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8085));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8085));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8086));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8087));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8087));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8088));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8088));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8089));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8089));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8090));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8091));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8091));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8092));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8093));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8367));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8368));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8368));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8369));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8370));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8370));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8371));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8371));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8372));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8372));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8373));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8374));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8374));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8375));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8376));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8376));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 41,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8377));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 42,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8377));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 43,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8378));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 44,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8378));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 45,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8387));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 46,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8388));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 47,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8393));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 48,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8393));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 49,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8394));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 50,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8394));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 51,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8395));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 52,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8395));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 53,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8396));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 54,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8396));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 55,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8397));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 56,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8398));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 57,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8398));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 58,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8399));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 59,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8399));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 60,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8400));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 61,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8400));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 62,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8401));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 63,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8401));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 64,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8402));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 65,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8402));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 66,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8403));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 67,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8403));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 68,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8404));

            migrationBuilder.UpdateData(
                table: "ErrorMessages",
                keyColumn: "Id",
                keyValue: 69,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 802, DateTimeKind.Utc).AddTicks(8404));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 806, DateTimeKind.Utc).AddTicks(538));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 806, DateTimeKind.Utc).AddTicks(555));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 806, DateTimeKind.Utc).AddTicks(569));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 806, DateTimeKind.Utc).AddTicks(572));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 806, DateTimeKind.Utc).AddTicks(574));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 806, DateTimeKind.Utc).AddTicks(578));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 806, DateTimeKind.Utc).AddTicks(579));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 806, DateTimeKind.Utc).AddTicks(581));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 806, DateTimeKind.Utc).AddTicks(583));

            migrationBuilder.UpdateData(
                table: "InsuranceCompanies",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 806, DateTimeKind.Utc).AddTicks(585));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3564));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3575));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3577));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3578));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3579));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3582));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3583));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3584));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3586));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3587));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3588));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3598));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3599));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3600));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3601));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 803, DateTimeKind.Utc).AddTicks(3602));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1001));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1021));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1022));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1023));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1023));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1026));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1027));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1028));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1028));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1030));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1030));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1031));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1031));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1032));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1033));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 16,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1033));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 17,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1145));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 18,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1148));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 19,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1149));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 20,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1150));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 21,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1151));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 22,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1152));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 23,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1152));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 24,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1153));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 25,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1154));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 26,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1154));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 27,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1155));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 28,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1155));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 29,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1160));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 30,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1161));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 31,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1162));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 32,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1162));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 33,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1163));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 34,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1174));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 35,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1178));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 36,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1179));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 37,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1180));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 38,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1180));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 39,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1181));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 40,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(1182));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 100,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4046));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4055));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 102,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4056));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 103,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4057));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 104,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4058));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 105,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4061));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 106,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4062));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 107,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4063));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 108,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4064));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 109,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4065));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 110,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4066));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 111,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4067));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 112,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4067));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 113,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4068));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 114,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4069));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 115,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4069));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 116,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4087));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 117,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4090));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 118,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4091));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 119,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4091));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 120,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4092));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 121,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4092));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 122,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4093));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 123,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4094));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 124,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4099));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 125,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4100));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 126,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4101));

            migrationBuilder.UpdateData(
                table: "RolePermissions",
                keyColumn: "Id",
                keyValue: 127,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 11, 18, 0, 40, 805, DateTimeKind.Utc).AddTicks(4101));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2e77be51-7e5f-4ae1-a08c-976f16349c13");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b057133b-2e75-4c37-8460-f2178aa99681");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "78ea75ae-6d88-43df-bdf9-1656b293e2f6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "6a5c7bc4-8daf-4d07-bb58-a4fe577047de");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                column: "ConcurrencyStamp",
                value: "874c7506-4c2e-46df-948f-6ad38026daa4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6,
                column: "ConcurrencyStamp",
                value: "301094de-f779-4070-ba1e-488f2adc9673");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7,
                column: "ConcurrencyStamp",
                value: "7e93cc88-18a0-49d6-956f-29b46e4546fd");
        }
    }
}
