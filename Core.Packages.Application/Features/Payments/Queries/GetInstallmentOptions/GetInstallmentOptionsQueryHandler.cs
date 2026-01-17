using MagicCarRepairAISupported.Application.Common.Services.Payment;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Application.Features.Payments.Queries.GetInstallmentOptions
{
    /// <summary>
    /// Taksit seçenekleri sorgu handler'ı
    /// </summary>
    public class GetInstallmentOptionsQueryHandler : IRequestHandler<GetInstallmentOptionsQuery, IDataResult<List<GetInstallmentOptionsResponse>>>
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<GetInstallmentOptionsQueryHandler> _logger;

        public GetInstallmentOptionsQueryHandler(
            IPaymentService paymentService,
            ILogger<GetInstallmentOptionsQueryHandler> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<IDataResult<List<GetInstallmentOptionsResponse>>> Handle(GetInstallmentOptionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var options = await _paymentService.GetInstallmentOptionsAsync(request.Amount, cancellationToken);

                var result = options.Select(o => new GetInstallmentOptionsResponse
                {
                    InstallmentCount = o.InstallmentCount,
                    MonthlyAmount = o.MonthlyAmount,
                    TotalAmount = o.TotalAmount,
                    InterestRate = o.InterestRate,
                    HasInterest = o.HasInterest
                }).ToList();

                return new SuccessDataResult<List<GetInstallmentOptionsResponse>>(result, "Taksit seçenekleri başarıyla getirildi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Taksit seçenekleri getirme hatası");
                return new ErrorDataResult<List<GetInstallmentOptionsResponse>>("Taksit seçenekleri getirilirken bir hata oluştu");
            }
        }
    }
}





