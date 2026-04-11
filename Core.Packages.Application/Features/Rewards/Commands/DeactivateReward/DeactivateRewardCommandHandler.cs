using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Rewards.Commands.DeactivateReward
{
    public class DeactivateRewardCommandHandler : IRequestHandler<DeactivateRewardCommand, IDataResult<DeactivateRewardResponse>>
    {
        private readonly IRewardRepository _rewardRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateRewardCommandHandler(
            IRewardRepository rewardRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _rewardRepository = rewardRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<DeactivateRewardResponse>> Handle(DeactivateRewardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId();
                if (clientId == null)
                {
                    return new ErrorDataResult<DeactivateRewardResponse>("Tenant bilgisi bulunamadı.");
                }

                var reward = await _rewardRepository.GetByIdAsync(request.Id, cancellationToken);
                if (reward == null)
                {
                    return new ErrorDataResult<DeactivateRewardResponse>("Ödül bulunamadı.");
                }

                if (reward.ClientId != clientId.Value)
                {
                    return new ErrorDataResult<DeactivateRewardResponse>("Bu ödüle erişim yetkiniz yok.");
                }

                if (!reward.IsActive)
                {
                    return new ErrorDataResult<DeactivateRewardResponse>("Bu ödül zaten pasif durumda.");
                }

                reward.IsActive = false;
                _rewardRepository.Update(reward);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessDataResult<DeactivateRewardResponse>(
                    new DeactivateRewardResponse
                    {
                        Id = reward.Id,
                        Message = $"'{reward.Name}' ödülü pasife alındı."
                    },
                    "Ödül başarıyla pasife alındı."
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<DeactivateRewardResponse>($"Ödül pasife alınırken hata oluştu: {ex.Message}");
            }
        }
    }
}
