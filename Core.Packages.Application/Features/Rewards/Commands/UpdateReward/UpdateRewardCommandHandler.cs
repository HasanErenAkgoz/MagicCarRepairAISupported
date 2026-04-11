using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Rewards.Commands.UpdateReward
{
    public class UpdateRewardCommandHandler : IRequestHandler<UpdateRewardCommand, IDataResult<UpdateRewardResponse>>
    {
        private readonly IRewardRepository _rewardRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRewardCommandHandler(
            IRewardRepository rewardRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _rewardRepository = rewardRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<UpdateRewardResponse>> Handle(UpdateRewardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId();
                if (clientId == null)
                {
                    return new ErrorDataResult<UpdateRewardResponse>("Tenant bilgisi bulunamadı.");
                }

                var reward = await _rewardRepository.GetByIdAsync(request.Id, cancellationToken);
                if (reward == null)
                {
                    return new ErrorDataResult<UpdateRewardResponse>("Ödül bulunamadı.");
                }

                if (reward.ClientId != clientId.Value)
                {
                    return new ErrorDataResult<UpdateRewardResponse>("Bu ödüle erişim yetkiniz yok.");
                }

                // Sadece gönderilen alanları güncelle
                if (request.Name != null)
                {
                    if (string.IsNullOrWhiteSpace(request.Name))
                    {
                        return new ErrorDataResult<UpdateRewardResponse>("Ödül adı boş olamaz.");
                    }
                    reward.Name = request.Name;
                }

                if (request.Description != null)
                    reward.Description = request.Description;

                if (request.RequiredPoints.HasValue)
                {
                    if (request.RequiredPoints.Value <= 0)
                    {
                        return new ErrorDataResult<UpdateRewardResponse>("Gerekli puan sıfırdan büyük olmalıdır.");
                    }
                    reward.RequiredPoints = request.RequiredPoints.Value;
                }

                if (request.Type.HasValue)
                    reward.Type = request.Type.Value;

                if (request.DiscountPercentage.HasValue)
                    reward.DiscountPercentage = request.DiscountPercentage.Value;

                if (request.DiscountAmount.HasValue)
                    reward.DiscountAmount = request.DiscountAmount.Value;

                if (request.ImageUrl != null)
                    reward.ImageUrl = request.ImageUrl;

                if (request.IsActive.HasValue)
                    reward.IsActive = request.IsActive.Value;

                if (request.StockQuantity.HasValue)
                    reward.StockQuantity = request.StockQuantity.Value;

                if (request.ValidFrom.HasValue)
                    reward.ValidFrom = request.ValidFrom.Value;

                if (request.ValidTo.HasValue)
                    reward.ValidTo = request.ValidTo.Value;

                // Tarih tutarlılık kontrolü
                if (reward.ValidFrom.HasValue && reward.ValidTo.HasValue && reward.ValidFrom > reward.ValidTo)
                {
                    return new ErrorDataResult<UpdateRewardResponse>("Geçerlilik başlangıç tarihi bitiş tarihinden sonra olamaz.");
                }

                // İndirim tipi tutarlılık kontrolü
                if (reward.Type == RewardType.Discount && reward.DiscountPercentage == null && reward.DiscountAmount == null)
                {
                    return new ErrorDataResult<UpdateRewardResponse>("İndirim tipinde ödül için yüzde veya tutar belirtilmelidir.");
                }

                _rewardRepository.Update(reward);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessDataResult<UpdateRewardResponse>(
                    new UpdateRewardResponse
                    {
                        Id = reward.Id,
                        Name = reward.Name,
                        Message = $"'{reward.Name}' ödülü başarıyla güncellendi."
                    },
                    "Ödül başarıyla güncellendi."
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<UpdateRewardResponse>($"Ödül güncellenirken hata oluştu: {ex.Message}");
            }
        }
    }
}
