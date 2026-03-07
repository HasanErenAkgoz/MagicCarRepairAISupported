using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Loyalty.Commands.EarnPoints
{
    public class EarnPointsCommand : IRequest<IDataResult<EarnPointsResponse>>
    {
        public int CustomerId { get; set; }
        public int Points { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? WorkOrderId { get; set; }
        public DateTime? ExpiryDate { get; set; } // Puan son kullanma tarihi
    }

    public class EarnPointsResponse
    {
        public int LoyaltyPointId { get; set; }
        public int TotalPoints { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
