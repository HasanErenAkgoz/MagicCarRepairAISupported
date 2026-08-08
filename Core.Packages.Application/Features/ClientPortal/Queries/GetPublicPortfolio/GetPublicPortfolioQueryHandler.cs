using MagicCarRepairAISupported.Application.Common.Services.FileUpload;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicPortfolio
{
    public class GetPublicPortfolioQueryHandler : IRequestHandler<GetPublicPortfolioQuery, IDataResult<List<GetPublicPortfolioResponse>>>
    {
        private readonly IServicePortfolioRepository _portfolioRepository;
        private readonly IEntityRepository<WorkOrderPhoto, int> _photoRepository;
        private readonly IFileStorageService _fileStorageService;

        public GetPublicPortfolioQueryHandler(
            IServicePortfolioRepository portfolioRepository,
            IEntityRepository<WorkOrderPhoto, int> photoRepository,
            IFileStorageService fileStorageService)
        {
            _portfolioRepository = portfolioRepository;
            _photoRepository = photoRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<IDataResult<List<GetPublicPortfolioResponse>>> Handle(GetPublicPortfolioQuery request, CancellationToken cancellationToken)
        {
            var portfolios = await _portfolioRepository.GetPublishedPortfoliosAsync(request.ClientId, cancellationToken);

            // Filter by category if provided
            if (!string.IsNullOrEmpty(request.Category))
            {
                portfolios = portfolios.Where(p => 
                    p.Categories != null && 
                    p.Categories.Contains(request.Category, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var responses = new List<GetPublicPortfolioResponse>();

            foreach (var portfolio in portfolios)
            {
                var featuredPhotoIds = new List<int>();
                if (!string.IsNullOrEmpty(portfolio.FeaturedPhotoIds))
                {
                    try
                    {
                        featuredPhotoIds = JsonSerializer.Deserialize<List<int>>(portfolio.FeaturedPhotoIds) ?? new List<int>();
                    }
                    catch
                    {
                        // Ignore JSON parse errors
                    }
                }

                var photoUrls = new List<string>();
                foreach (var photoId in featuredPhotoIds)
                {
                    var photo = await _photoRepository.GetByIdAsync(photoId);
                    if (photo != null && !string.IsNullOrEmpty(photo.FilePath))
                    {
                        // A portfolio is the explicit publication decision; never expose its storage path.
                        photoUrls.Add($"/api/public-media/clients/{request.ClientId}/portfolios/{portfolio.Id}/photos/{photoId}");
                    }
                }

                var response = new GetPublicPortfolioResponse
                {
                    Id = portfolio.Id,
                    Title = portfolio.Title,
                    Description = portfolio.Description,
                    Categories = portfolio.Categories,
                    FeaturedPhotoUrls = photoUrls,
                    PublishedDate = portfolio.PublishedDate,
                    ViewCount = portfolio.ViewCount,
                    LikeCount = portfolio.LikeCount,
                    VehicleBrand = portfolio.WorkOrder?.Vehicle?.Brand,
                    VehicleModel = portfolio.WorkOrder?.Vehicle?.Model,
                    VehicleYear = portfolio.WorkOrder?.Vehicle?.Year
                };

                responses.Add(response);
            }

            // Pagination
            var pageNumber = request.PageNumber ?? 1;
            var pageSize = request.PageSize ?? 20;
            var pagedResults = responses
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new SuccessDataResult<List<GetPublicPortfolioResponse>>(pagedResults);
        }
    }
}
