using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.ApprovePortfolio
{
    public class ApprovePortfolioCommand : IRequest<IResult>
    {
        public int PortfolioId { get; set; }
    }
}
