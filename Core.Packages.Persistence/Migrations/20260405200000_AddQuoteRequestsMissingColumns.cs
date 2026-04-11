using MagicCarRepairAISupported.Persistence.Context;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicCarRepairAISupported.Persistence.Migrations
{
    /// <summary>
    /// Eski veritabanlarında QuoteRequests tablosu modelden geri kalmış olabiliyor (Description, PhotoPaths, AI tahmin alanları).
    /// Sütunlar yoksa ekler; zaten varsa atlar.
    /// </summary>
    [DbContext(typeof(BaseDbContext))]
    [Migration("20260405200000_AddQuoteRequestsMissingColumns")]
    public class AddQuoteRequestsMissingColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Ayrı Sql çağrıları: aynı batch içinde yeni eklenen sütun UPDATE'te kullanılamaz (compile-time metadata).
            migrationBuilder.Sql(@"
IF OBJECT_ID(N'[dbo].[QuoteRequests]', N'U') IS NULL RETURN;

IF COL_LENGTH('dbo.QuoteRequests', 'Description') IS NULL
    ALTER TABLE [dbo].[QuoteRequests] ADD [Description] nvarchar(2000) NULL;

IF COL_LENGTH('dbo.QuoteRequests', 'EstimatedCost') IS NULL
    ALTER TABLE [dbo].[QuoteRequests] ADD [EstimatedCost] decimal(18,2) NULL;

IF COL_LENGTH('dbo.QuoteRequests', 'EstimatedDescription') IS NULL
    ALTER TABLE [dbo].[QuoteRequests] ADD [EstimatedDescription] nvarchar(max) NULL;

IF COL_LENGTH('dbo.QuoteRequests', 'PhotoPaths') IS NULL
BEGIN
    ALTER TABLE [dbo].[QuoteRequests] ADD [PhotoPaths] nvarchar(max) NOT NULL
        CONSTRAINT [DF_QuoteRequests_PhotoPaths] DEFAULT N'[]';
END
");

            // sp_executesql + C# kaçışı: IF içindeki UPDATE batch derlemesinde yine Description aranır; metni runtime'da derlenir.
            const string copyProblemToDescription = @"
UPDATE [dbo].[QuoteRequests]
SET [Description] = [ProblemDescription]
WHERE ([Description] IS NULL OR LTRIM(RTRIM([Description])) = N'')
  AND [ProblemDescription] IS NOT NULL;
";
            var escapedCopy = copyProblemToDescription.Trim().Replace("'", "''");
            migrationBuilder.Sql($@"
IF OBJECT_ID(N'[dbo].[QuoteRequests]', N'U') IS NULL RETURN;

IF COL_LENGTH('dbo.QuoteRequests', 'ProblemDescription') IS NOT NULL
  AND COL_LENGTH('dbo.QuoteRequests', 'Description') IS NOT NULL
    EXEC sp_executesql N'{escapedCopy}';
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF COL_LENGTH('dbo.QuoteRequests', 'PhotoPaths') IS NOT NULL
BEGIN
    ALTER TABLE [dbo].[QuoteRequests] DROP CONSTRAINT IF EXISTS [DF_QuoteRequests_PhotoPaths];
    ALTER TABLE [dbo].[QuoteRequests] DROP COLUMN [PhotoPaths];
END
IF COL_LENGTH('dbo.QuoteRequests', 'EstimatedDescription') IS NOT NULL
    ALTER TABLE [dbo].[QuoteRequests] DROP COLUMN [EstimatedDescription];
IF COL_LENGTH('dbo.QuoteRequests', 'EstimatedCost') IS NOT NULL
    ALTER TABLE [dbo].[QuoteRequests] DROP COLUMN [EstimatedCost];
IF COL_LENGTH('dbo.QuoteRequests', 'Description') IS NOT NULL
    ALTER TABLE [dbo].[QuoteRequests] DROP COLUMN [Description];
");
        }
    }
}
