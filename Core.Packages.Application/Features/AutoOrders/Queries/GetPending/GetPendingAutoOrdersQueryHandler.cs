using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AutoOrders.Queries.GetPending
{
    public class GetPendingAutoOrdersQueryHandler : IRequestHandler<GetPendingAutoOrdersQuery, IDataResult<List<GetPendingAutoOrdersResponse>>>
    {
        private readonly IAutoOrderService _autoOrderService;

        public GetPendingAutoOrdersQueryHandler(IAutoOrderService autoOrderService)
        {
            _autoOrderService = autoOrderService;
        }

        public async Task<IDataResult<List<GetPendingAutoOrdersResponse>>> Handle(GetPendingAutoOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _autoOrderService.GetPendingOrdersAsync(cancellationToken);

                var response = orders.Select(o => new GetPendingAutoOrdersResponse
                {
                    Id = o.Id,
                    OrderNumber = o.OrderNumber,
                    PartId = o.PartId,
                    PartCode = o.Part?.PartCode ?? string.Empty,
                    PartName = o.Part?.Name ?? string.Empty,
                    Quantity = o.Quantity,
                    UnitPrice = o.UnitPrice,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    StatusName = o.Status.ToString(),
                    SupplierId = o.PartSupplierId,
                    SupplierName = o.PartSupplier?.CompanyName,
                    ExpectedDeliveryDate = o.ExpectedDeliveryDate,
                    CreatedDate = o.CreatedDate ?? DateTime.UtcNow
                }).ToList();

                return new SuccessDataResult<List<GetPendingAutoOrdersResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<GetPendingAutoOrdersResponse>>(ex.Message);
            }
        }
    }
}

