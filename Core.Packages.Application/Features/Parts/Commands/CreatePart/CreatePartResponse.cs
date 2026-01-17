using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.CreatePart
{
    public class CreatePartResponse
    {
        public int Id { get; set; }
        public string PartCode { get; set; }
        public string Name { get; set; }
        public PartCategory Category { get; set; }
        public PartBrandType BrandType { get; set; }
        public decimal SalePrice { get; set; }
        public int? StockQuantity { get; set; }
    }
}

