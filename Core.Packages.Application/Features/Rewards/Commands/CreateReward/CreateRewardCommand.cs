using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Rewards.Commands.CreateReward
{
    public class CreateRewardCommand : IRequest<IDataResult<CreateRewardResponse>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int RequiredPoints { get; set; }
        public RewardType Type { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string? ImageUrl { get; set; }
        public int? StockQuantity { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }

    public class CreateRewardResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
