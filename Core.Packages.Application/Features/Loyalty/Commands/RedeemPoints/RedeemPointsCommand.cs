using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Loyalty.Commands.RedeemPoints
{
    public class RedeemPointsCommand : IRequest<IDataResult<RedeemPointsResponse>>
    {
        public int CustomerId { get; set; }
        public int RewardId { get; set; }
        public int? ClientId { get; set; } // Opsiyonel — handler tenant'tan alır
    }

    public class RedeemPointsResponse
    {
        public int LoyaltyPointId { get; set; }
        public int RemainingPoints { get; set; }
        public string RewardName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
