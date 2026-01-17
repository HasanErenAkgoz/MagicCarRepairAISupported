using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Payments.Commands.Initialize
{
    /// <summary>
    /// Ödeme başlatma komutu
    /// </summary>
    public class InitializePaymentCommand : IRequest<IDataResult<InitializePaymentResponse>>
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "TRY";
        public int? InvoiceId { get; set; }
        public int? WorkOrderId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string? CustomerIdentityNumber { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerCity { get; set; }
        public string? CustomerCountry { get; set; }
        public string? CustomerZipCode { get; set; }
        public string? Description { get; set; }
        public string CallbackUrl { get; set; }
        public int? InstallmentCount { get; set; }
    }
}

