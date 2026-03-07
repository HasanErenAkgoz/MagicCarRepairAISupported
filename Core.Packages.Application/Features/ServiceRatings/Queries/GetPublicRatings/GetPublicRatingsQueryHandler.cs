using AutoMapper;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.ServiceRatings.Queries.GetPublicRatings
{
    public class GetPublicRatingsQueryHandler : IRequestHandler<GetPublicRatingsQuery, IDataResult<GetPublicRatingsResponse>>
    {
        private readonly IServiceRatingRepository _ratingRepository;
        private readonly IMapper _mapper;

        public GetPublicRatingsQueryHandler(IServiceRatingRepository ratingRepository, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public async Task<IDataResult<GetPublicRatingsResponse>> Handle(GetPublicRatingsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _ratingRepository.Query()
                    .Where(r => r.ClientId == request.ClientId && r.Status == RatingStatus.Approved);

                if (request.MinRating.HasValue)
                {
                    query = query.Where(r => r.Rating >= request.MinRating.Value);
                }

                var totalCount = await query.CountAsync(cancellationToken);

                var ratings = await query
                    .Include(r => r.Customer)
                    .OrderByDescending(r => r.CreatedDate)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);

                var ratingDtos = ratings.Select(r => new PublicRatingDto
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    ServiceQuality = r.ServiceQuality,
                    PriceValue = r.PriceValue,
                    OnTimeDelivery = r.OnTimeDelivery,
                    StaffBehavior = r.StaffBehavior,
                    Comment = r.Comment,
                    Photos = !string.IsNullOrEmpty(r.Photos) 
                        ? JsonSerializer.Deserialize<List<string>>(r.Photos) 
                        : new List<string>(),
                    CustomerName = $"{r.Customer?.FirstName} {r.Customer?.LastName}".Trim(),
                    CreatedDate = r.CreatedDate,
                    ServiceReply = r.ServiceReply,
                    ServiceReplyDate = r.ServiceReplyDate
                }).ToList();

                // Statistics
                var allRatings = await _ratingRepository.Query()
                    .Where(r => r.ClientId == request.ClientId && r.Status == RatingStatus.Approved)
                    .ToListAsync(cancellationToken);

                var statistics = new RatingStatistics
                {
                    TotalRatings = allRatings.Count,
                    FiveStarCount = allRatings.Count(r => r.Rating == 5),
                    FourStarCount = allRatings.Count(r => r.Rating == 4),
                    ThreeStarCount = allRatings.Count(r => r.Rating == 3),
                    TwoStarCount = allRatings.Count(r => r.Rating == 2),
                    OneStarCount = allRatings.Count(r => r.Rating == 1)
                };

                var averageRating = allRatings.Any() 
                    ? allRatings.Average(r => (decimal)r.Rating) 
                    : 0;

                var response = new GetPublicRatingsResponse
                {
                    Ratings = ratingDtos,
                    TotalCount = totalCount,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    AverageRating = averageRating,
                    Statistics = statistics
                };

                return new SuccessDataResult<GetPublicRatingsResponse>(response);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetPublicRatingsResponse>($"Değerlendirmeler getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
}
