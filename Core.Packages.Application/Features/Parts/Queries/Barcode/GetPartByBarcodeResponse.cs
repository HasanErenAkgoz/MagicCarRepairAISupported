using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.Barcode
{
    public class GetPartByBarcodeResponse
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
        public int? StockQuantity { get; set; }
        public string? StockLocation { get; set; }
        public string Unit { get; set; }
    }
}
