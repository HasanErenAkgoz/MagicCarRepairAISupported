using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.StockAlerts.Commands.Resolve
{
    public class ResolveStockAlertCommand : IRequest<IResult>
    {
        public int StockAlertId { get; set; }
    }
}

