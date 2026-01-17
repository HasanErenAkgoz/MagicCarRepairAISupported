using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.StockAlerts.Commands.Resolve
{
    public class ResolveStockAlertCommandHandler : IRequestHandler<ResolveStockAlertCommand, IResult>
    {
        private readonly IStockAlertService _stockAlertService;

        public ResolveStockAlertCommandHandler(IStockAlertService stockAlertService)
        {
            _stockAlertService = stockAlertService;
        }

        public async Task<IResult> Handle(ResolveStockAlertCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _stockAlertService.ResolveAlertAsync(request.StockAlertId, cancellationToken);
                if (result)
                {
                    return new SuccessResult("Stock alert resolved successfully");
                }
                return new ErrorResult("Failed to resolve stock alert");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}

