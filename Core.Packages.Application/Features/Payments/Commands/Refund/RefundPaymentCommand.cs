using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Payments.Commands.Refund
{
    /// <summary>
    /// Ödeme iade komutu
    /// </summary>
    public class RefundPaymentCommand : IRequest<IResult>
    {
        public int PaymentId { get; set; }
        public string GatewayPaymentId { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Description { get; set; }
    }
}

