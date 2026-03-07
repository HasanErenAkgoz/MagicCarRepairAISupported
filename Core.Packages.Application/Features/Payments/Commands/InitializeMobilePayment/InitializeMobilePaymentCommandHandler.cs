using MagicCarRepairAISupported.Application.Common.Services.Payment;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Payments.Commands.InitializeMobilePayment
{
    public class InitializeMobilePaymentCommandHandler : IRequestHandler<InitializeMobilePaymentCommand, IDataResult<MobilePaymentInitResponse>>
    {
        private readonly IMobilePaymentService _mobilePaymentService;

        public InitializeMobilePaymentCommandHandler(IMobilePaymentService mobilePaymentService)
        {
            _mobilePaymentService = mobilePaymentService;
        }

        public async Task<IDataResult<MobilePaymentInitResponse>> Handle(InitializeMobilePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var paymentRequest = new MobilePaymentInitRequest
                {
                    InvoiceId = request.InvoiceId,
                    WorkOrderId = request.WorkOrderId,
                    CustomerId = request.CustomerId,
                    CustomerName = request.CustomerName,
                    CustomerSurname = request.CustomerSurname,
                    CustomerEmail = request.CustomerEmail,
                    CustomerPhone = request.CustomerPhone,
                    CustomerIdentityNumber = request.CustomerIdentityNumber,
                    CustomerAddress = request.CustomerAddress,
                    Amount = request.Amount,
                    Currency = request.Currency,
                    InstallmentCount = request.InstallmentCount,
                    Description = request.Description
                };

                var response = await _mobilePaymentService.InitializeMobilePaymentAsync(paymentRequest, cancellationToken);

                if (response.Success)
                {
                    return new SuccessDataResult<MobilePaymentInitResponse>(response, "Ödeme oturumu başlatıldı.");
                }
                else
                {
                    return new ErrorDataResult<MobilePaymentInitResponse>(response, response.ErrorMessage ?? "Ödeme oturumu başlatılamadı.");
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<MobilePaymentInitResponse>($"Ödeme başlatılırken hata oluştu: {ex.Message}");
            }
        }
    }
}
