using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AutoOrders.Commands.Create
{
    public class CreateAutoOrderCommand : IRequest<IDataResult<CreateAutoOrderResponse>>
    {
        public int StockAlertId { get; set; }
        public int? SupplierId { get; set; }
    }
}

