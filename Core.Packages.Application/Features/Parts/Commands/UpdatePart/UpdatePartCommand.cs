using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePart
{
    public class UpdatePartCommand : IRequest<UpdatePartResponse>
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
        public decimal TaxRate { get; set; } = 20;
        public int MinimumStockLevel { get; set; } = 0;
        public bool IsLowStockAlertEnabled { get; set; } = true;
        public int? SupplierId { get; set; }
        public string Unit { get; set; } = "Adet";
        public int? WarrantyMonths { get; set; }
        public string? Notes { get; set; }
        public string? StockLocation { get; set; }

        public string[]? CompatibleVehicleBrands { get; set; }
        public string[]? CompatibleVehicleModels { get; set; }
        public int? CompatibleYearFrom { get; set; }
        public int? CompatibleYearTo { get; set; }
        public string[]? AdditionalOemCodes { get; set; }
    }
}

