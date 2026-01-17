using MagicCarRepairAISupported.Application.Common.Messages;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetTimeline
{
    public class GetWorkOrderTimelineQueryHandler : IRequestHandler<GetWorkOrderTimelineQuery, List<GetWorkOrderTimelineResponse>>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;

        public GetWorkOrderTimelineQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
        }

        public async Task<List<GetWorkOrderTimelineResponse>> Handle(GetWorkOrderTimelineQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var workOrder = await _workOrderRepository.GetWithDetailsAsync(request.WorkOrderId, cancellationToken);
            if (workOrder == null)
            {
                throw new DomainException(Messages.NotFound, new { Entity = "WorkOrder", Id = request.WorkOrderId });
            }

            if (workOrder.ClientId != clientId)
            {
                throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId });
            }

            if (workOrder.Timeline == null || !workOrder.Timeline.Any())
            {
                return new List<GetWorkOrderTimelineResponse>();
            }

            return workOrder.Timeline
                .OrderBy(t => t.EventDate)
                .Select(t =>
                {
                    List<int>? photoIds = null;
                    if (!string.IsNullOrEmpty(t.PhotoIds))
                    {
                        try
                        {
                            photoIds = JsonSerializer.Deserialize<List<int>>(t.PhotoIds);
                        }
                        catch { }
                    }

                    return new GetWorkOrderTimelineResponse
                    {
                        Id = t.Id,
                        EventDate = t.EventDate,
                        OldStatus = t.OldStatus,
                        OldStatusName = t.OldStatus?.ToString(),
                        NewStatus = t.NewStatus,
                        NewStatusName = t.NewStatus?.ToString(),
                        StatusChangeText = t.OldStatus.HasValue && t.NewStatus.HasValue
                            ? $"{t.OldStatus} → {t.NewStatus}"
                            : t.Description,
                        EmployeeId = t.EmployeeId,
                        EmployeeName = t.Employee?.FullName,
                        Description = t.Description,
                        EventType = t.EventType,
                        PhotoIds = photoIds
                    };
                })
                .ToList();
        }
    }
}

