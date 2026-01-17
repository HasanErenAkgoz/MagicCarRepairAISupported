using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Payments.Commands.Callback
{
    /// <summary>
    /// Ödeme callback komutu (3D Secure sonrası)
    /// </summary>
    public class HandlePaymentCallbackCommand : IRequest<IDataResult<HandlePaymentCallbackResponse>>
    {
        public string GatewayPaymentId { get; set; }
        public string? GatewayConversationId { get; set; }
        public string? Status { get; set; }
        public Dictionary<string, string>? AdditionalParameters { get; set; }
    }
}

