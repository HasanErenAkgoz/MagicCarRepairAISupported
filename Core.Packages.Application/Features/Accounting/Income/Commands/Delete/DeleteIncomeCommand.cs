using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Delete
{
    public class DeleteIncomeCommand : IRequest<DeleteIncomeResponse>
    {
        public int Id { get; set; }
    }
}

