using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Export;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.Export
{
    public class ExportPartsQueryHandler : IRequestHandler<ExportPartsQuery, byte[]>
    {
        private readonly IPartRepository _partRepository;
        private readonly ITenantService _tenantService;
        private readonly IExportService _exportService;

        public ExportPartsQueryHandler(
            IPartRepository partRepository,
            ITenantService tenantService,
            IExportService exportService)
        {
            _partRepository = partRepository;
            _tenantService = tenantService;
            _exportService = exportService;
        }

        public async Task<byte[]> Handle(ExportPartsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var query = _partRepository.Query()
                .Include(p => p.Stock)
                .Include(p => p.Supplier)
                .Where(p => p.ClientId == clientId);

            var parts = await query
                .OrderBy(p => p.Name)
                .ToListAsync(cancellationToken);

            // DTO'ya dönüştür
            var exportData = parts.Select(p => new
            {
                PartCode = p.PartCode,
                Name = p.Name,
                Description = p.Description ?? "N/A",
                Category = p.Category.ToString(),
                BrandType = p.BrandType.ToString(),
                Brand = p.Brand ?? "N/A",
                OEMNumber = p.OEMNumber ?? "N/A",
                Barcode = p.Barcode ?? "N/A",
                PurchasePrice = p.PurchasePrice.ToString("N2"),
                SalePrice = p.SalePrice.ToString("N2"),
                TaxRate = p.TaxRate.ToString("N2"),
                Unit = p.Unit,
                MinimumStockLevel = p.MinimumStockLevel.ToString(),
                WarrantyMonths = p.WarrantyMonths?.ToString() ?? "N/A",
                SupplierName = p.Supplier?.CompanyName ?? "N/A",
                StockQuantity = request.IncludeStock ? (p.Stock?.Quantity.ToString() ?? "0") : "N/A",
                StockLocation = request.IncludeStock ? (p.Stock?.Location ?? "N/A") : "N/A",
                Notes = p.Notes ?? "N/A"
            }).ToList();

            // Format'a göre export et
            return request.Format.ToLower() switch
            {
                "excel" => await _exportService.ExportToExcelAsync(exportData, "Parts", cancellationToken),
                "csv" => await _exportService.ExportToCsvAsync(exportData, cancellationToken),
                _ => throw new NotSupportedException($"Format {request.Format} is not supported")
            };
        }
    }
}
