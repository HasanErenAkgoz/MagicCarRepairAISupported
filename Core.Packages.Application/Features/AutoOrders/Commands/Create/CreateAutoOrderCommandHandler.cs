using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AutoOrders.Commands.Create
{
    public class CreateAutoOrderCommandHandler : IRequestHandler<CreateAutoOrderCommand, IDataResult<CreateAutoOrderResponse>>
    {
        private readonly IAutoOrderService _autoOrderService;
        private readonly IMapper _mapper;

        public CreateAutoOrderCommandHandler(
            IAutoOrderService autoOrderService,
            IMapper mapper)
        {
            _autoOrderService = autoOrderService;
            _mapper = mapper;
        }

        public async Task<IDataResult<CreateAutoOrderResponse>> Handle(CreateAutoOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var autoOrder = await _autoOrderService.CreateAutoOrderFromAlertAsync(
                    request.StockAlertId,
                    request.SupplierId,
                    cancellationToken);

                if (autoOrder == null)
                {
                    return new ErrorDataResult<CreateAutoOrderResponse>("Failed to create auto order");
                }

                var response = new CreateAutoOrderResponse
                {
                    Id = autoOrder.Id,
                    OrderNumber = autoOrder.OrderNumber,
                    PartId = autoOrder.PartId,
                    PartName = autoOrder.Part?.Name ?? string.Empty,
                    Quantity = autoOrder.Quantity,
                    UnitPrice = autoOrder.UnitPrice,
                    TotalAmount = autoOrder.TotalAmount,
                    SupplierId = autoOrder.PartSupplierId,
                    SupplierName = autoOrder.PartSupplier?.CompanyName
                };

                return new SuccessDataResult<CreateAutoOrderResponse>(response, "Auto order created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CreateAutoOrderResponse>(ex.Message);
            }
        }
    }
}

