using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Audit.Queries.GetAuditLogs
{
    public class GetAuditLogsQuery : IRequest<GetAuditLogsResponse>
    {
        public int? UserId { get; set; }
        public string? EntityName { get; set; }
        public int? EntityId { get; set; }
        public string? Action { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
