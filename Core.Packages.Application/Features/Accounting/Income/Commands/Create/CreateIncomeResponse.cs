using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Create
{
    public class CreateIncomeResponse
    {
        public int Id { get; set; }
        public int? WorkOrderId { get; set; }
        public string? WorkOrderNumber { get; set; }
        public IncomeType IncomeType { get; set; }
        public string IncomeTypeName { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Description { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

