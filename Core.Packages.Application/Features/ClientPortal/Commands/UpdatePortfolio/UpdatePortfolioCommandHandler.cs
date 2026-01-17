using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Commands.UpdatePortfolio
{
    public class UpdatePortfolioCommandHandler : IRequestHandler<UpdatePortfolioCommand, UpdatePortfolioResponse>
    {
        private readonly IServicePortfolioRepository _portfolioRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePortfolioCommandHandler(
            IServicePortfolioRepository portfolioRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _portfolioRepository = portfolioRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdatePortfolioResponse> Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var portfolio = await _portfolioRepository.GetByIdAsync(request.Id);
            if (portfolio == null || portfolio.ClientId != clientId)
            {
                throw new DomainException("PORTFOLIO_NOT_FOUND");
            }

            portfolio.Title = request.Title;
            portfolio.Description = request.Description;
            portfolio.Categories = request.Categories;
            portfolio.DisplayOrder = request.DisplayOrder;
            portfolio.IsPublished = request.IsPublished;

            // FeaturedPhotoIds'i JSON formatına çevir
            if (request.FeaturedPhotoIds != null && request.FeaturedPhotoIds.Any())
            {
                portfolio.FeaturedPhotoIds = JsonSerializer.Serialize(request.FeaturedPhotoIds);
            }
            else
            {
                portfolio.FeaturedPhotoIds = null;
            }

            // Eğer yayınlanıyorsa ve onaylanmışsa, yayın tarihini güncelle
            if (request.IsPublished && portfolio.CustomerApprovalStatus == CustomerApprovalStatus.Approved && !portfolio.PublishedDate.HasValue)
            {
                portfolio.PublishedDate = DateTime.UtcNow;
            }

            _portfolioRepository.Update(portfolio);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new UpdatePortfolioResponse
            {
                Id = portfolio.Id,
                Title = portfolio.Title,
                IsPublished = portfolio.IsPublished
            };
        }
    }
}
