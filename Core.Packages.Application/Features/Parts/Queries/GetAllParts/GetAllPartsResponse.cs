using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.GetAllParts
{
    public class GetAllPartsResponse
    {
        public List<PartDto> Parts { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }

    public class PartDto
    {
        public int Id { get; set; }
        public string PartCode { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public PartCategory Category { get; set; }
        public PartBrandType BrandType { get; set; }
        public string? Brand { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int? StockQuantity { get; set; }
        public int MinimumStockLevel { get; set; }
        public bool IsLowStock { get; set; }
        public string? SupplierName { get; set; }
    }
}

