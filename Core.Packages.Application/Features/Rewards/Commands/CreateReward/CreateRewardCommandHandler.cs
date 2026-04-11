using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Rewards.Commands.CreateReward
{
    public class CreateRewardCommandHandler : IRequestHandler<CreateRewardCommand, IDataResult<CreateRewardResponse>>
    {
        private readonly IRewardRepository _rewardRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRewardCommandHandler(
            IRewardRepository rewardRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _rewardRepository = rewardRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<CreateRewardResponse>> Handle(CreateRewardCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId();
                if (clientId == null)
                {
                    return new ErrorDataResult<CreateRewardResponse>("Tenant bilgisi bulunamadı.");
                }

                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return new ErrorDataResult<CreateRewardResponse>("Ödül adı boş olamaz.");
                }

                if (request.RequiredPoints <= 0)
                {
                    return new ErrorDataResult<CreateRewardResponse>("Gerekli puan sıfırdan büyük olmalıdır.");
                }

                if (request.Type == RewardType.Discount && request.DiscountPercentage == null && request.DiscountAmount == null)
                {
                    return new ErrorDataResult<CreateRewardResponse>("İndirim tipinde ödül için yüzde veya tutar belirtilmelidir.");
                }

                if (request.ValidFrom.HasValue && request.ValidTo.HasValue && request.ValidFrom > request.ValidTo)
                {
                    return new ErrorDataResult<CreateRewardResponse>("Geçerlilik başlangıç tarihi bitiş tarihinden sonra olamaz.");
                }

                var reward = new Reward
                {
                    Name = request.Name,
                    Description = request.Description,
                    RequiredPoints = request.RequiredPoints,
                    Type = request.Type,
                    DiscountPercentage = request.DiscountPercentage,
                    DiscountAmount = request.DiscountAmount,
                    ImageUrl = request.ImageUrl,
                    StockQuantity = request.StockQuantity,
                    ValidFrom = request.ValidFrom,
                    ValidTo = request.ValidTo,
                    IsActive = true,
                    UsedQuantity = 0,
                    ClientId = clientId.Value
                };

                await _rewardRepository.AddAsync(reward, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessDataResult<CreateRewardResponse>(
                    new CreateRewardResponse
                    {
                        Id = reward.Id,
                        Name = reward.Name,
                        Message = $"'{reward.Name}' ödülü başarıyla oluşturuldu."
                    },
                    "Ödül başarıyla oluşturuldu."
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CreateRewardResponse>($"Ödül oluşturulurken hata oluştu: {ex.Message}");
            }
        }
    }
}
