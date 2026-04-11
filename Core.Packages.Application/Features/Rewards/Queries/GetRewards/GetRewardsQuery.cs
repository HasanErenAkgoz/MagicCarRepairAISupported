using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Rewards.Queries.GetRewards
{
    public class GetRewardsQuery : IRequest<IDataResult<GetRewardsResponse>>
    {
        public bool? ActiveOnly { get; set; }
    }

    public class GetRewardsResponse
    {
        public List<RewardDto> Rewards { get; set; } = new();
        public int TotalCount { get; set; }
    }

    public class RewardDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int RequiredPoints { get; set; }
        public string TypeName { get; set; } = string.Empty;
        /// <summary>1=Discount 2=FreeService 3=Gift 4=Cashback — used by mobile app</summary>
        public int Type { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public int? StockQuantity { get; set; }
        public int UsedQuantity { get; set; }
        public int? RemainingStock { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
