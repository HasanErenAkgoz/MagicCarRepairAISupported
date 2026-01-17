using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Update
{
    public class UpdateIncomeCommand : IRequest<UpdateIncomeResponse>
    {
        public int Id { get; set; }
        public IncomeType? IncomeType { get; set; }
        public decimal? Amount { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string? Description { get; set; }
        public string? InvoiceNumber { get; set; }
    }
}

