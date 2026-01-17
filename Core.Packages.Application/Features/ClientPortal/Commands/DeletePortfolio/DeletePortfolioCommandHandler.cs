using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.DeletePortfolio
{
    public class DeletePortfolioCommandHandler : IRequestHandler<DeletePortfolioCommand, IResult>
    {
        private readonly IServicePortfolioRepository _portfolioRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePortfolioCommandHandler(
            IServicePortfolioRepository portfolioRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _portfolioRepository = portfolioRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var portfolio = await _portfolioRepository.GetByIdAsync(request.Id);
            if (portfolio == null || portfolio.ClientId != clientId)
            {
                return new ErrorResult("PORTFOLIO_NOT_FOUND");
            }

            _portfolioRepository.Delete(portfolio);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new SuccessResult("Portfolio deleted successfully");
        }
    }
}
