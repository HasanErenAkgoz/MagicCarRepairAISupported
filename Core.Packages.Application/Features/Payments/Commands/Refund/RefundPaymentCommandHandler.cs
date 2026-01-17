using MagicCarRepairAISupported.Application.Common.Services.Payment;
using MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Application.Features.Payments.Commands.Refund
{
    /// <summary>
    /// Ödeme iade komut handler'ı
    /// </summary>
    public class RefundPaymentCommandHandler : IRequestHandler<RefundPaymentCommand, IResult>
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<RefundPaymentCommandHandler> _logger;

        public RefundPaymentCommandHandler(
            IPaymentService paymentService,
            ILogger<RefundPaymentCommandHandler> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        public async Task<IResult> Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var refundRequest = new RefundRequest
                {
                    PaymentId = request.PaymentId,
                    GatewayPaymentId = request.GatewayPaymentId,
                    RefundAmount = request.RefundAmount,
                    Description = request.Description
                };

                var response = await _paymentService.RefundPaymentAsync(refundRequest, cancellationToken);

                if (!response.Success)
                {
                    return new ErrorResult(
                        response.ErrorMessage ?? $"İade işlemi başarısız. Hata Kodu: {response.ErrorCode}");
                }

                return new SuccessResult("İade işlemi başarıyla tamamlandı");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödeme iade hatası");
                return new ErrorResult("İade işlemi sırasında bir hata oluştu");
            }
        }
    }
}



