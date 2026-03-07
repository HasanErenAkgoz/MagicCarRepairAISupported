using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Loyalty.Queries.GetCustomerPoints
{
    public class GetCustomerPointsQueryHandler : IRequestHandler<GetCustomerPointsQuery, IDataResult<GetCustomerPointsResponse>>
    {
        private readonly ILoyaltyPointRepository _loyaltyPointRepository;

        public GetCustomerPointsQueryHandler(ILoyaltyPointRepository loyaltyPointRepository)
        {
            _loyaltyPointRepository = loyaltyPointRepository;
        }

        public async Task<IDataResult<GetCustomerPointsResponse>> Handle(GetCustomerPointsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalPoints = await _loyaltyPointRepository.GetTotalPointsByCustomerIdAsync(request.CustomerId, cancellationToken);
                var pointsHistory = await _loyaltyPointRepository.GetPointsHistoryByCustomerIdAsync(request.CustomerId, cancellationToken);

                // Süresi dolmamış puanlar
                var availablePoints = pointsHistory
                    .Where(p => p.Type == LoyaltyPointType.Earned && (p.ExpiryDate == null || p.ExpiryDate > DateTime.UtcNow))
                    .Sum(p => p.Points);

                // Yakında süresi dolacak puanlar (30 gün içinde)
                var expiringSoon = DateTime.UtcNow.AddDays(30);
                var expiringPoints = pointsHistory
                    .Where(p => p.Type == LoyaltyPointType.Earned && p.ExpiryDate.HasValue && p.ExpiryDate <= expiringSoon && p.ExpiryDate > DateTime.UtcNow)
                    .Sum(p => p.Points);

                var pointsHistoryDto = pointsHistory.Select(p => new LoyaltyPointDto
                {
                    Id = p.Id,
                    Points = p.Points,
                    Type = p.Type.ToString(),
                    Description = p.Description,
                    CreatedDate = p.CreatedDate ?? DateTime.UtcNow,
                    ExpiryDate = p.ExpiryDate,
                    WorkOrderId = p.WorkOrderId
                }).ToList();

                var response = new GetCustomerPointsResponse
                {
                    TotalPoints = totalPoints,
                    AvailablePoints = availablePoints,
                    ExpiringPoints = expiringPoints,
                    PointsHistory = pointsHistoryDto
                };

                return new SuccessDataResult<GetCustomerPointsResponse>(response);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetCustomerPointsResponse>($"Puan bilgileri getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
}
