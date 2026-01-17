using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.StockMovements.Commands.RecordStockMovement
{
    public class RecordStockMovementCommand : IRequest<RecordStockMovementResponse>
    {
        public int PartId { get; set; }
        public StockMovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public int? EmployeeId { get; set; } // İşlem yapan personel
        public string? Description { get; set; }
        public string? ReferenceNumber { get; set; } // Referans numarası (Fatura No, İş Emri No, vb.)
        public string? ReferenceType { get; set; } // Referans tipi (Invoice, WorkOrder, Purchase, vb.)
        public decimal? UnitPrice { get; set; } // Birim fiyat (giriş için)
    }
}

