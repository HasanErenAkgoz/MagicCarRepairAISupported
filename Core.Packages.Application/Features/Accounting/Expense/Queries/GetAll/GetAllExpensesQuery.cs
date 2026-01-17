using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Expense.Queries.GetAll
{
    public class GetAllExpensesQuery : IRequest<List<GetAllExpensesResponse>>
    {
        public ExpenseType? ExpenseType { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}

