using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.GetPartById
{
    public class GetPartByIdResponse
    {
        public int Id { get; set; }
        public string PartCode { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public PartCategory Category { get; set; }
        public PartBrandType BrandType { get; set; }
        public string? Brand { get; set; }
        public string? OEMNumber { get; set; }
        public string? Barcode { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TaxRate { get; set; }
        public int MinimumStockLevel { get; set; }
        public bool IsLowStockAlertEnabled { get; set; }
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string Unit { get; set; }
        public int? WarrantyMonths { get; set; }
        public string? Notes { get; set; }
        public int? StockQuantity { get; set; }
        public string? StockLocation { get; set; }
        public bool IsLowStock { get; set; }
        public List<PartPhotoItem> Photos { get; set; } = new();

        public string[]? CompatibleVehicleBrands { get; set; }
        public string[]? CompatibleVehicleModels { get; set; }
        public int? CompatibleYearFrom { get; set; }
        public int? CompatibleYearTo { get; set; }
        public string[]? AdditionalOemCodes { get; set; }
    }

    public class PartPhotoItem
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime UploadDate { get; set; }
    }
}

