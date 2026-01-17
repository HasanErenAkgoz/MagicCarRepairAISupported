using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Expense.Queries.GetById
{
    public class GetExpenseByIdQuery : IRequest<GetExpenseByIdResponse>
    {
        public int Id { get; set; }
    }
}

