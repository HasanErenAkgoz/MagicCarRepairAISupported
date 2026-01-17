using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Audit.Queries.ExportAuditLogs
{
    public class ExportAuditLogsQuery : IRequest<byte[]>
    {
        public int? UserId { get; set; }
        public string? EntityName { get; set; }
        public string? Action { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Format { get; set; } = "CSV"; // CSV (Excel için Infrastructure servisi kullanılabilir)
    }
}
