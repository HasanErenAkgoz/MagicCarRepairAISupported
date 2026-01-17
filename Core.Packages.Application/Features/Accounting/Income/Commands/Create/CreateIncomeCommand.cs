using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Create
{
    public class CreateIncomeCommand : IRequest<CreateIncomeResponse>
    {
        public int? WorkOrderId { get; set; }
        public IncomeType IncomeType { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
        public string? InvoiceNumber { get; set; }
        public int? CustomerId { get; set; }
    }
}

