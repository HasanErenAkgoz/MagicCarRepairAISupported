using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Update
{
    public class UpdateExpenseResponse
    {
        public int Id { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public string ExpenseTypeName { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string PaymentMethodName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? SupplierName { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? Description { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

