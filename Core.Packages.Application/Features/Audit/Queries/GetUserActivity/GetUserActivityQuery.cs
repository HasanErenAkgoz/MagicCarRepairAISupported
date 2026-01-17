using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Audit.Queries.GetUserActivity
{
    public class GetUserActivityQuery : IRequest<GetUserActivityResponse>
    {
        public int? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
