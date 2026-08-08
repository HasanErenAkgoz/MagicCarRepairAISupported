using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Audit.Queries.ExportAuditLogs
{
    public class ExportAuditLogsQueryHandler : IRequestHandler<ExportAuditLogsQuery, byte[]>
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly ITenantService _tenantService;

        public ExportAuditLogsQueryHandler(
            IAuditLogRepository auditLogRepository,
            ITenantService tenantService)
        {
            _auditLogRepository = auditLogRepository;
            _tenantService = tenantService;
        }

        public async Task<byte[]> Handle(ExportAuditLogsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var query = _auditLogRepository.Query()
                .Where(a => a.ClientId == clientId);

            // Filtreleme
            if (request.UserId.HasValue)
            {
                query = query.Where(a => a.UserId == request.UserId.Value);
            }

            if (!string.IsNullOrEmpty(request.EntityName))
            {
                query = query.Where(a => a.EntityName == request.EntityName);
            }

            if (!string.IsNullOrEmpty(request.Action))
            {
                query = query.Where(a => a.Action == request.Action);
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(a => a.CreatedDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(a => a.CreatedDate <= request.EndDate.Value);
            }

            var logs = await query
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync(cancellationToken);

            if (request.Format == "CSV")
            {
                return GenerateCsv(logs);
            }
            else
            {
                // Excel için Infrastructure'daki servisi kullanabiliriz veya CSV döndürebiliriz
                // Şimdilik CSV döndürelim, Excel için Infrastructure'a taşınabilir
                throw new NotSupportedException($"Format {request.Format} is not supported. Use CSV format.");
            }
        }

        private byte[] GenerateCsv(List<Domain.Entities.AuditLog> logs)
        {
            var csv = new System.Text.StringBuilder();
            csv.AppendLine("ID,Kullanıcı ID,Entity,Entity ID,İşlem,Tarih,IP Adresi,Başarılı,Açıklama");

            foreach (var log in logs)
            {
                csv.AppendLine($"{log.Id},{log.UserId},{log.EntityName},{log.EntityId},{log.Action}," +
                              $"{log.CreatedDate?.ToString("dd.MM.yyyy HH:mm:ss")},{log.IpAddress}," +
                              $"{(log.IsSuccess ? "Evet" : "Hayır")},\"{log.Description?.Replace("\"", "\"\"")}\"");
            }

            return System.Text.Encoding.UTF8.GetBytes(csv.ToString());
        }
    }
}
