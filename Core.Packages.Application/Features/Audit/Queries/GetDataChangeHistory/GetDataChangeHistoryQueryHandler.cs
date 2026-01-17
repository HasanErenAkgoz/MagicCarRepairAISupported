using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Audit.Queries.GetDataChangeHistory
{
    public class GetDataChangeHistoryQueryHandler : IRequestHandler<GetDataChangeHistoryQuery, GetDataChangeHistoryResponse>
    {
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly ITenantService _tenantService;

        public GetDataChangeHistoryQueryHandler(
            IAuditLogRepository auditLogRepository,
            ITenantService tenantService)
        {
            _auditLogRepository = auditLogRepository;
            _tenantService = tenantService;
        }

        public async Task<GetDataChangeHistoryResponse> Handle(GetDataChangeHistoryQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var logs = await _auditLogRepository.GetByEntityAsync(request.EntityName, request.EntityId, cancellationToken);

            // Sadece bu client'a ait logları filtrele
            logs = logs.Where(l => l.ClientId == clientId).ToList();

            var changes = logs.Select(log => new ChangeHistoryItem
            {
                Id = log.Id,
                Action = log.Action,
                OldValues = log.OldValues,
                NewValues = log.NewValues,
                ChangedProperties = log.ChangedProperties,
                UserId = log.UserId,
                Description = log.Description,
                CreatedDate = log.CreatedDate,
                IpAddress = log.IpAddress
            }).ToList();

            return new GetDataChangeHistoryResponse
            {
                EntityName = request.EntityName,
                EntityId = request.EntityId,
                Changes = changes,
                TotalCount = changes.Count
            };
        }
    }
}
