using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetTopCustomers
{
    public class GetTopCustomersQueryHandler : IRequestHandler<GetTopCustomersQuery, List<GetTopCustomersResponse>>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;

        public GetTopCustomersQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
        }

        public async Task<List<GetTopCustomersResponse>> Handle(GetTopCustomersQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;
            var startDate = request.StartDate ?? DateTime.UtcNow.AddMonths(-6);
            var endDate = request.EndDate ?? DateTime.UtcNow;

            var workOrders = await _workOrderRepository.Query()
                .Include(wo => wo.Customer)
                .Where(wo => wo.ClientId == clientId &&
                           wo.EntryDate >= startDate &&
                           wo.EntryDate <= endDate &&
                           wo.Status == Domain.Enums.WorkOrderStatus.Delivered)
                .ToListAsync(cancellationToken);

            var customerStats = workOrders
                .GroupBy(wo => wo.CustomerId)
                .Select(g => new GetTopCustomersResponse
                {
                    CustomerId = g.Key,
                    CustomerName = g.First().Customer?.FullName ?? "Bilinmeyen",
                    Email = g.First().Customer?.Email,
                    PhoneNumber = g.First().Customer?.PhoneNumber,
                    WorkOrderCount = g.Count(),
                    TotalSpent = g.Sum(wo => wo.TotalAmount),
                    LastVisitDate = g.Max(wo => wo.EntryDate)
                })
                .OrderByDescending(c => c.TotalSpent)
                .Take(request.Count)
                .ToList();

            return customerStats;
        }
    }
}

