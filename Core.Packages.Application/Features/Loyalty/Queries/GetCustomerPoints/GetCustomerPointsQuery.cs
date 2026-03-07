using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Loyalty.Queries.GetCustomerPoints
{
    public class GetCustomerPointsQuery : IRequest<IDataResult<GetCustomerPointsResponse>>
    {
        public int CustomerId { get; set; }
    }

    public class GetCustomerPointsResponse
    {
        public int TotalPoints { get; set; }
        public int AvailablePoints { get; set; } // Süresi dolmamış puanlar
        public int ExpiringPoints { get; set; } // Yakında süresi dolacak puanlar
        public List<LoyaltyPointDto> PointsHistory { get; set; } = new();
    }

    public class LoyaltyPointDto
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? WorkOrderId { get; set; }
    }
}
