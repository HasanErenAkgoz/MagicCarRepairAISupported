using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Invoices.Commands.SendByEmail
{
    public class SendInvoiceByEmailCommand : IRequest<SendInvoiceByEmailResponse>
    {
        public int InvoiceId { get; set; }
        public string ToEmail { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
    }
}






