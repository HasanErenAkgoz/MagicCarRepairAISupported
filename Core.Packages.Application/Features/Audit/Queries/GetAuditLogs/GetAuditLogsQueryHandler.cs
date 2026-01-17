using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Audit.Queries.GetAuditLogs
{
    public class GetAuditLogsQueryHandler : IRequestHandler<GetAuditLogsQuery, GetAuditLogsResponse>
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly ITenantService _tenantService;

        public GetAuditLogsQueryHandler(
            IAuditLogRepository auditLogRepository,
            ITenantService tenantService)
        {
            _auditLogRepository = auditLogRepository;
            _tenantService = tenantService;
        }

        public async Task<GetAuditLogsResponse> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

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

            if (request.EntityId.HasValue)
            {
                query = query.Where(a => a.EntityId == request.EntityId.Value);
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

            // Toplam sayı
            var totalCount = await query.CountAsync(cancellationToken);

            // Sayfalama
            var logs = await query
                .OrderByDescending(a => a.CreatedDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var items = logs.Select(log => new AuditLogItem
            {
                Id = log.Id,
                UserId = log.UserId,
                EntityName = log.EntityName,
                EntityId = log.EntityId,
                Action = log.Action,
                OldValues = log.OldValues,
                NewValues = log.NewValues,
                ChangedProperties = log.ChangedProperties,
                IpAddress = log.IpAddress,
                UserAgent = log.UserAgent,
                Description = log.Description,
                IsSuccess = log.IsSuccess,
                ErrorMessage = log.ErrorMessage,
                DurationMs = log.DurationMs,
                RequestPath = log.RequestPath,
                RequestMethod = log.RequestMethod,
                CreatedDate = log.CreatedDate
            }).ToList();

            return new GetAuditLogsResponse
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
