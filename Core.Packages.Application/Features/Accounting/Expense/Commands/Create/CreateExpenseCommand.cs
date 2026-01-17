using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Create
{
    public class CreateExpenseCommand : IRequest<CreateExpenseResponse>
    {
        public ExpenseType ExpenseType { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string? SupplierName { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? Description { get; set; }
        public int? EmployeeId { get; set; }
        public int? PartPurchaseId { get; set; }
    }
}

