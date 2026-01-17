using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Payment;
using MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Application.Features.Payments.Commands.Initialize
{
    /// <summary>
    /// Ödeme başlatma komut handler'ı
    /// </summary>
    public class InitializePaymentCommandHandler : IRequestHandler<InitializePaymentCommand, IDataResult<InitializePaymentResponse>>
    {
        private readonly IPaymentService _paymentService;
        private readonly ITenantService _tenantService;
        private readonly ILogger<InitializePaymentCommandHandler> _logger;

        public InitializePaymentCommandHandler(
            IPaymentService paymentService,
            ITenantService tenantService,
            ILogger<InitializePaymentCommandHandler> logger)
        {
            _paymentService = paymentService;
            _tenantService = tenantService;
            _logger = logger;
        }

        public async Task<IDataResult<InitializePaymentResponse>> Handle(InitializePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var paymentRequest = new PaymentInitRequest
                {
                    Amount = request.Amount,
                    Currency = request.Currency,
                    InvoiceId = request.InvoiceId,
                    WorkOrderId = request.WorkOrderId,
                    CustomerId = request.CustomerId,
                    CustomerName = request.CustomerName,
                    CustomerSurname = request.CustomerSurname,
                    CustomerEmail = request.CustomerEmail,
                    CustomerPhone = request.CustomerPhone,
                    CustomerIdentityNumber = request.CustomerIdentityNumber,
                    CustomerAddress = request.CustomerAddress,
                    CustomerCity = request.CustomerCity,
                    CustomerCountry = request.CustomerCountry,
                    CustomerZipCode = request.CustomerZipCode,
                    Description = request.Description,
                    CallbackUrl = request.CallbackUrl,
                    InstallmentCount = request.InstallmentCount
                };

                var response = await _paymentService.InitializePaymentAsync(paymentRequest, cancellationToken);

                if (!response.Success)
                {
                    return new ErrorDataResult<InitializePaymentResponse>(
                        response.ErrorMessage ?? $"Ödeme başlatılamadı. Hata Kodu: {response.ErrorCode}");
                }

                var result = new InitializePaymentResponse
                {
                    PaymentId = response.PaymentId,
                    GatewayPaymentId = response.GatewayPaymentId,
                    GatewayConversationId = response.GatewayConversationId,
                    HtmlContent = response.HtmlContent,
                    RedirectUrl = response.RedirectUrl,
                    Requires3DSecure = !string.IsNullOrEmpty(response.HtmlContent) || !string.IsNullOrEmpty(response.RedirectUrl)
                };

                return new SuccessDataResult<InitializePaymentResponse>(result, "Ödeme başarıyla başlatıldı");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödeme başlatma hatası");
                return new ErrorDataResult<InitializePaymentResponse>("Ödeme başlatılırken bir hata oluştu");
            }
        }
    }
}



