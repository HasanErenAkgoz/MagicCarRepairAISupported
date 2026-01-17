using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.StockAlerts.Queries.GetActive
{
    public class GetActiveStockAlertsQuery : IRequest<IDataResult<List<GetActiveStockAlertsResponse>>>
    {
        public StockAlertType? AlertType { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}

