using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePartStock
{
    public class UpdatePartStockCommand : IRequest<UpdatePartStockResponse>
    {
        public int PartId { get; set; }
        public int? Quantity { get; set; } // Null ise güncelleme yapılmaz
        public string? Location { get; set; } // Null ise güncelleme yapılmaz
        public int? EmployeeId { get; set; } // İşlem yapan personel
        public string? Description { get; set; } // Açıklama (StockMovement için)
    }
}

