using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.CustomerPortal.Commands.RejectPortfolio
{
    public class RejectPortfolioCommand : IRequest<IResult>
    {
        public int PortfolioId { get; set; }
        public string? RejectionReason { get; set; }
    }
}
