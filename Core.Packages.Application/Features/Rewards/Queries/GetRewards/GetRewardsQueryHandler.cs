using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Rewards.Queries.GetRewards
{
    public class GetRewardsQueryHandler : IRequestHandler<GetRewardsQuery, IDataResult<GetRewardsResponse>>
    {
        private readonly IRewardRepository _rewardRepository;
        private readonly ITenantService _tenantService;

        public GetRewardsQueryHandler(
            IRewardRepository rewardRepository,
            ITenantService tenantService)
        {
            _rewardRepository = rewardRepository;
            _tenantService = tenantService;
        }

        public async Task<IDataResult<GetRewardsResponse>> Handle(GetRewardsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId();
                if (clientId == null)
                {
                    return new ErrorDataResult<GetRewardsResponse>("Tenant bilgisi bulunamadı.");
                }

                List<Reward> rewards;

                if (request.ActiveOnly == true)
                {
                    // Aktif ödülleri getir (tarih + stok kontrolü dahil)
                    var activeRewards = await _rewardRepository.GetActiveRewardsAsync(cancellationToken);
                    rewards = activeRewards.Where(r => r.ClientId == clientId.Value).ToList();
                }
                else
                {
                    // Tüm ödülleri getir
                    var allRewards = await _rewardRepository.GetListAsync(cancellationToken, r => r.ClientId == clientId.Value);
                    rewards = allRewards.ToList();
                }

                var rewardDtos = rewards.Select(r => new RewardDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    RequiredPoints = r.RequiredPoints,
                    TypeName = r.Type.ToString(),
                    Type = (int)r.Type,
                    DiscountPercentage = r.DiscountPercentage,
                    DiscountAmount = r.DiscountAmount,
                    ImageUrl = r.ImageUrl,
                    IsActive = r.IsActive,
                    StockQuantity = r.StockQuantity,
                    UsedQuantity = r.UsedQuantity,
                    RemainingStock = r.StockQuantity.HasValue ? r.StockQuantity.Value - r.UsedQuantity : null,
                    ValidFrom = r.ValidFrom,
                    ValidTo = r.ValidTo
                }).ToList();

                return new SuccessDataResult<GetRewardsResponse>(
                    new GetRewardsResponse
                    {
                        Rewards = rewardDtos,
                        TotalCount = rewardDtos.Count
                    }
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetRewardsResponse>($"Ödüller getirilirken hata oluştu: {ex.Message}");
            }
        }
    }
}
