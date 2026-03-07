using MagicCarRepairAISupported.Application.Common.Services.Payment;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Payments.Commands.InitializeMobilePayment
{
    public class InitializeMobilePaymentCommand : IRequest<IDataResult<MobilePaymentInitResponse>>
    {
        public int InvoiceId { get; set; }
        public int? WorkOrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerSurname { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string? CustomerIdentityNumber { get; set; }
        public string? CustomerAddress { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "TRY";
        public int? InstallmentCount { get; set; }
        public string? Description { get; set; }
    }
}
