using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Audit.Queries.GetUserActivity
{
    public class GetUserActivityQueryHandler : IRequestHandler<GetUserActivityQuery, GetUserActivityResponse>
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly ITenantService _tenantService;

        public GetUserActivityQueryHandler(
            IAuditLogRepository auditLogRepository,
            ITenantService tenantService)
        {
            _auditLogRepository = auditLogRepository;
            _tenantService = tenantService;
        }

        public async Task<GetUserActivityResponse> Handle(GetUserActivityQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var query = _auditLogRepository.Query()
                .Where(a => a.ClientId == clientId);

            if (request.UserId.HasValue)
            {
                query = query.Where(a => a.UserId == request.UserId.Value);
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(a => a.CreatedDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(a => a.CreatedDate <= request.EndDate.Value);
            }

            var allLogs = await query.ToListAsync(cancellationToken);

            // Özet istatistikler
            var summary = new ActivitySummary
            {
                TotalActions = allLogs.Count,
                CreateCount = allLogs.Count(a => a.Action == "Create"),
                UpdateCount = allLogs.Count(a => a.Action == "Update"),
                DeleteCount = allLogs.Count(a => a.Action == "Delete"),
                ViewCount = allLogs.Count(a => a.Action == "View"),
                FailedActions = allLogs.Count(a => !a.IsSuccess),
                ActionsByEntity = allLogs
                    .GroupBy(a => a.EntityName)
                    .ToDictionary(g => g.Key, g => g.Count())
            };

            // Sayfalama
            var totalCount = allLogs.Count;
            var activities = allLogs
                .OrderByDescending(a => a.CreatedDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(log => new UserActivityItem
                {
                    Id = log.Id,
                    EntityName = log.EntityName,
                    EntityId = log.EntityId,
                    Action = log.Action,
                    Description = log.Description,
                    IpAddress = log.IpAddress,
                    RequestPath = log.RequestPath,
                    RequestMethod = log.RequestMethod,
                    IsSuccess = log.IsSuccess,
                    CreatedDate = log.CreatedDate
                })
                .ToList();

            return new GetUserActivityResponse
            {
                UserId = request.UserId,
                Activities = activities,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                Summary = summary
            };
        }
    }
}
