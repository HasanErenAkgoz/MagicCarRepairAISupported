using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations;

public partial class AddAiDiagnosisMediaAssets : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "MediaAssets",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false).Annotation("Npgsql:ValueGenerationStrategy", Npgsql.EntityFrameworkCore.PostgreSQL.Metadata.NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                ClientId = table.Column<int>(type: "integer", nullable: false), OwnerUserId = table.Column<int>(type: "integer", nullable: false),
                Purpose = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false), StorageKey = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                ContentType = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false), Length = table.Column<long>(type: "bigint", nullable: false), ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true), ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true), CreatedBy = table.Column<int>(type: "integer", nullable: false), ModifiedBy = table.Column<int>(type: "integer", nullable: true), Status = table.Column<int>(type: "integer", nullable: false)
            }, constraints: table => table.PrimaryKey("PK_MediaAssets", x => x.Id));
        migrationBuilder.CreateIndex(name: "IX_MediaAssets_ClientId_OwnerUserId_Purpose_ExpiresAt", table: "MediaAssets", columns: new[] { "ClientId", "OwnerUserId", "Purpose", "ExpiresAt" });
    }
    protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(name: "MediaAssets");
}
