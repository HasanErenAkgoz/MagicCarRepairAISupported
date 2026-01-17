using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Import;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.Import
{
    public class ImportPartsCommandHandler : IRequestHandler<ImportPartsCommand, ImportPartsResponse>
    {
        private readonly IImportService _importService;
        private readonly IPartRepository _partRepository;
        private readonly ITenantService _tenantService;

        public ImportPartsCommandHandler(
            IImportService importService,
            IPartRepository partRepository,
            ITenantService tenantService)
        {
            _importService = importService;
            _partRepository = partRepository;
            _tenantService = tenantService;
        }

        public async Task<ImportPartsResponse> Handle(ImportPartsCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Import data
            ImportResult<PartImportDto> importResult;
            if (request.Format.ToLower() == "excel")
            {
                importResult = await _importService.ImportFromExcelAsync<PartImportDto>(request.FileData, cancellationToken);
            }
            else if (request.Format.ToLower() == "csv")
            {
                importResult = await _importService.ImportFromCsvAsync<PartImportDto>(request.FileData, cancellationToken);
            }
            else
            {
                throw new NotSupportedException($"Format {request.Format} is not supported");
            }

            // Create parts from imported data
            foreach (var dto in importResult.SuccessItems)
            {
                // Check if part code already exists
                var existingPart = await _partRepository.GetByPartCodeAsync(dto.PartCode, cancellationToken);
                if (existingPart != null && existingPart.ClientId == clientId)
                {
                    // Update existing part
                    existingPart.Name = dto.Name;
                    existingPart.Description = dto.Description;
                    existingPart.Category = Enum.TryParse<PartCategory>(dto.Category, out var category) ? category : PartCategory.Other;
                    existingPart.BrandType = Enum.TryParse<PartBrandType>(dto.BrandType, out var brandType) ? brandType : PartBrandType.Original;
                    existingPart.Brand = dto.Brand;
                    existingPart.OEMNumber = dto.OEMNumber;
                    existingPart.Barcode = dto.Barcode;
                    existingPart.PurchasePrice = dto.PurchasePrice;
                    existingPart.SalePrice = dto.SalePrice;
                    existingPart.TaxRate = dto.TaxRate;
                    existingPart.Unit = dto.Unit ?? "Adet";
                    existingPart.MinimumStockLevel = dto.MinimumStockLevel;
                    existingPart.WarrantyMonths = dto.WarrantyMonths;
                    existingPart.Notes = dto.Notes;

                    _partRepository.Update(existingPart);
                }
                else
                {
                    // Create new part
                    var part = new Part
                    {
                        PartCode = dto.PartCode,
                        Name = dto.Name,
                        Description = dto.Description,
                        Category = Enum.TryParse<PartCategory>(dto.Category, out var category) ? category : PartCategory.Other,
                        BrandType = Enum.TryParse<PartBrandType>(dto.BrandType, out var brandType) ? brandType : PartBrandType.Original,
                        Brand = dto.Brand,
                        OEMNumber = dto.OEMNumber,
                        Barcode = dto.Barcode,
                        PurchasePrice = dto.PurchasePrice,
                        SalePrice = dto.SalePrice,
                        TaxRate = dto.TaxRate,
                        Unit = dto.Unit ?? "Adet",
                        MinimumStockLevel = dto.MinimumStockLevel,
                        WarrantyMonths = dto.WarrantyMonths,
                        Notes = dto.Notes,
                        ClientId = clientId
                    };

                    await _partRepository.AddAsync(part, cancellationToken);
                }
            }

            await _partRepository.SaveChangesAsync();

            return new ImportPartsResponse
            {
                TotalRows = importResult.TotalRows,
                SuccessCount = importResult.SuccessCount,
                ErrorCount = importResult.ErrorCount,
                Errors = importResult.Errors
            };
        }

        // DTO for import
        public class PartImportDto
        {
            public string PartCode { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public string Category { get; set; } = "Other";
            public string BrandType { get; set; } = "Original";
            public string? Brand { get; set; }
            public string? OEMNumber { get; set; }
            public string? Barcode { get; set; }
            public decimal PurchasePrice { get; set; }
            public decimal SalePrice { get; set; }
            public decimal TaxRate { get; set; } = 20;
            public string? Unit { get; set; }
            public int MinimumStockLevel { get; set; } = 0;
            public int? WarrantyMonths { get; set; }
            public string? Notes { get; set; }
        }
    }
}
