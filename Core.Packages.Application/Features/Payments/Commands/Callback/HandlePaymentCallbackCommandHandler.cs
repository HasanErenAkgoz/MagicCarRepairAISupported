using MagicCarRepairAISupported.Application.Common.Services.Payment;
using MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Application.Features.Payments.Commands.Callback
{
    /// <summary>
    /// Ödeme callback komut handler'ı
    /// </summary>
    public class HandlePaymentCallbackCommandHandler : IRequestHandler<HandlePaymentCallbackCommand, IDataResult<HandlePaymentCallbackResponse>>
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<HandlePaymentCallbackCommandHandler> _logger;

        public HandlePaymentCallbackCommandHandler(
            IPaymentService paymentService,
            ILogger<HandlePaymentCallbackCommandHandler> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<IDataResult<HandlePaymentCallbackResponse>> Handle(HandlePaymentCallbackCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var callbackRequest = new PaymentCallbackRequest
                {
                    GatewayPaymentId = request.GatewayPaymentId,
                    GatewayConversationId = request.GatewayConversationId,
                    Status = request.Status,
                    AdditionalParameters = request.AdditionalParameters
                };

                var response = await _paymentService.HandlePaymentCallbackAsync(callbackRequest, cancellationToken);

                if (!response.Success)
                {
                    return new ErrorDataResult<HandlePaymentCallbackResponse>(
                        response.ErrorMessage ?? $"Ödeme callback işlenemedi. Hata Kodu: {response.ErrorCode}");
                }

                var result = new HandlePaymentCallbackResponse
                {
                    PaymentId = response.PaymentId,
                    PaymentStatus = response.PaymentStatus,
                    PaidAmount = response.PaidAmount,
                    CardLastFourDigits = response.CardLastFourDigits,
                    BankName = response.BankName,
                    InstallmentCount = response.InstallmentCount
                };

                return new SuccessDataResult<HandlePaymentCallbackResponse>(result, "Ödeme callback başarıyla işlendi");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödeme callback işleme hatası");
                return new ErrorDataResult<HandlePaymentCallbackResponse>("Ödeme callback işlenirken bir hata oluştu");
            }
        }
    }
}



