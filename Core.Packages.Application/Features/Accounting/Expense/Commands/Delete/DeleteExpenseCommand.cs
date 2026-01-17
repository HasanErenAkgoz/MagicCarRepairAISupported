using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Delete
{
    public class DeleteExpenseCommand : IRequest<DeleteExpenseResponse>
    {
        public int Id { get; set; }
    }
}

