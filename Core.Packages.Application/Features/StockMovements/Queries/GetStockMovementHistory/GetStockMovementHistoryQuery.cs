using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.StockMovements.Queries.GetStockMovementHistory
{
    public class GetStockMovementHistoryQuery : IRequest<GetStockMovementHistoryResponse>
    {
        public int? PartId { get; set; } // PartId'ye göre filtreleme (opsiyonel)
        public StockMovementType? MovementType { get; set; } // Hareket tipine göre filtreleme
        public DateTime? StartDate { get; set; } // Başlangıç tarihi
        public DateTime? EndDate { get; set; } // Bitiş tarihi
        public int? EmployeeId { get; set; } // Personel ID'ye göre filtreleme
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}

