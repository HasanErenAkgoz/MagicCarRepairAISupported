using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Update
{
    public class UpdateExpenseCommand : IRequest<UpdateExpenseResponse>
    {
        public int Id { get; set; }
        public ExpenseType? ExpenseType { get; set; }
        public decimal? Amount { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? SupplierName { get; set; }
        public string? InvoiceNumber { get; set; }
        public string? Description { get; set; }
    }
}

