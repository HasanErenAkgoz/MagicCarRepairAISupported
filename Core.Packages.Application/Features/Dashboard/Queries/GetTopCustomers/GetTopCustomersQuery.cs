using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetTopCustomers
{
    public class GetTopCustomersQuery : IRequest<List<GetTopCustomersResponse>>
    {
        public int Count { get; set; } = 10;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

