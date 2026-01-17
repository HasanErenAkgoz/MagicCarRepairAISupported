using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetWorkOrderStatusChart
{
    public class GetWorkOrderStatusChartQueryHandler : IRequestHandler<GetWorkOrderStatusChartQuery, List<GetWorkOrderStatusChartResponse>>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;

        public GetWorkOrderStatusChartQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
        }

        public async Task<List<GetWorkOrderStatusChartResponse>> Handle(GetWorkOrderStatusChartQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var workOrders = await _workOrderRepository.Query()
                .Where(wo => wo.ClientId == clientId)
                .GroupBy(wo => wo.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync(cancellationToken);

            var totalCount = workOrders.Sum(w => w.Count);

            var response = workOrders.Select(wo => new GetWorkOrderStatusChartResponse
            {
                StatusName = wo.Status.ToString(),
                Count = wo.Count,
                Percentage = totalCount > 0 ? (decimal)wo.Count / totalCount * 100 : 0
            }).ToList();

            return response;
        }
    }
}

