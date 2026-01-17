using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AutoOrders.Commands.Approve
{
    public class ApproveAutoOrderCommandHandler : IRequestHandler<ApproveAutoOrderCommand, IResult>
    {
        private readonly IAutoOrderService _autoOrderService;

        public ApproveAutoOrderCommandHandler(IAutoOrderService autoOrderService)
        {
            _autoOrderService = autoOrderService;
        }

        public async Task<IResult> Handle(ApproveAutoOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _autoOrderService.ApproveOrderAsync(
                    request.AutoOrderId,
                    request.UserId,
                    cancellationToken);

                if (result)
                {
                    return new SuccessResult("Auto order approved successfully");
                }
                return new ErrorResult("Failed to approve auto order");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}

