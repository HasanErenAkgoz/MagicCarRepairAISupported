using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Loyalty.Commands.RedeemPoints
{
    public class RedeemPointsCommandHandler : IRequestHandler<RedeemPointsCommand, IDataResult<RedeemPointsResponse>>
    {
        private readonly ILoyaltyPointRepository _loyaltyPointRepository;
        private readonly IRewardRepository _rewardRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public RedeemPointsCommandHandler(
            ILoyaltyPointRepository loyaltyPointRepository,
            IRewardRepository rewardRepository,
            ICustomerRepository customerRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _loyaltyPointRepository = loyaltyPointRepository;
            _rewardRepository = rewardRepository;
            _customerRepository = customerRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<RedeemPointsResponse>> Handle(RedeemPointsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = request.ClientId ?? _tenantService.GetCurrentClientId() ?? 0;
                if (clientId == 0)
                {
                    return new ErrorDataResult<RedeemPointsResponse>("Client bilgisi bulunamadı.");
                }

                // 1. Müşteri kontrolü
                var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
                if (customer == null)
                {
                    return new ErrorDataResult<RedeemPointsResponse>("Müşteri bulunamadı.");
                }

                // 2. Ödül kontrolü
                var reward = await _rewardRepository.GetByIdAsync(request.RewardId, cancellationToken);
                if (reward == null)
                {
                    return new ErrorDataResult<RedeemPointsResponse>("Ödül bulunamadı.");
                }

                if (!reward.IsActive)
                {
                    return new ErrorDataResult<RedeemPointsResponse>("Ödül aktif değil.");
                }

                if (reward.ClientId != clientId)
                {
                    return new ErrorDataResult<RedeemPointsResponse>("Ödül bu işletmeye ait değil.");
                }

                // 3. Stok kontrolü
                if (reward.StockQuantity.HasValue && reward.UsedQuantity >= reward.StockQuantity.Value)
                {
                    return new ErrorDataResult<RedeemPointsResponse>("Ödül stokta yok.");
                }

                // 4. Geçerlilik süresi kontrolü
                if (reward.ValidTo.HasValue && reward.ValidTo.Value < DateTime.UtcNow)
                {
                    return new ErrorDataResult<RedeemPointsResponse>("Ödülün geçerlilik süresi dolmuş.");
                }

                // 5. Müşterinin kullanılabilir puanlarını hesapla
                var pointsHistory = await _loyaltyPointRepository.GetPointsHistoryByCustomerIdAsync(request.CustomerId, cancellationToken);

                var availablePoints = pointsHistory
                    .Where(p => p.Type != LoyaltyPointType.Expired)
                    .Where(p => p.Type != LoyaltyPointType.Earned || p.ExpiryDate == null || p.ExpiryDate > DateTime.UtcNow)
                    .Sum(p => p.Points);

                if (availablePoints < reward.RequiredPoints)
                {
                    return new ErrorDataResult<RedeemPointsResponse>(
                        $"Yetersiz puan. Mevcut: {availablePoints}, Gerekli: {reward.RequiredPoints}");
                }

                // 6. Negatif puan kaydı oluştur (Redeemed)
                var loyaltyPoint = new LoyaltyPoint
                {
                    CustomerId = request.CustomerId,
                    Points = -reward.RequiredPoints,
                    Type = LoyaltyPointType.Redeemed,
                    Description = $"Ödül kullanıldı: {reward.Name}",
                    RewardId = reward.Id,
                    ClientId = clientId
                };

                await _loyaltyPointRepository.AddAsync(loyaltyPoint, cancellationToken);

                // 7. Ödül kullanım sayısını artır
                reward.UsedQuantity++;
                _rewardRepository.Update(reward);

                // 8. Kaydet
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // 9. Yeni bakiyeyi hesapla
                var newBalance = availablePoints - reward.RequiredPoints;

                return new SuccessDataResult<RedeemPointsResponse>(
                    new RedeemPointsResponse
                    {
                        LoyaltyPointId = loyaltyPoint.Id,
                        RemainingPoints = newBalance,
                        RewardName = reward.Name,
                        Message = $"'{reward.Name}' ödülü başarıyla kullanıldı. Kalan puan: {newBalance}"
                    },
                    "Ödül başarıyla kullanıldı."
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<RedeemPointsResponse>($"Ödül kullanılırken hata oluştu: {ex.Message}");
            }
        }
    }
}
