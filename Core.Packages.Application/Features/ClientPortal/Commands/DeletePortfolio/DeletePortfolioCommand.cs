using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.DeletePortfolio
{
    public class DeletePortfolioCommand : IRequest<IResult>
    {
        public int Id { get; set; }
    }
}
