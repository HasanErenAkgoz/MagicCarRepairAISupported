using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Income.Queries.GetById
{
    public class GetIncomeByIdQuery : IRequest<GetIncomeByIdResponse>
    {
        public int Id { get; set; }
    }
}

