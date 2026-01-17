using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Commands.PayTax
{
    public class PayTaxCommand : IRequest<PayTaxResponse>
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public PaymentMethod PaymentMethod { get; set; }
        public string? PaymentReferenceNumber { get; set; }
    }
}
