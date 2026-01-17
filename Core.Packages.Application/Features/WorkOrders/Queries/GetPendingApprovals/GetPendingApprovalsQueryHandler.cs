using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetPendingApprovals
{
    public class GetPendingApprovalsQueryHandler : IRequestHandler<GetPendingApprovalsQuery, GetPendingApprovalsResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;

        public GetPendingApprovalsQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
        }

        public async Task<GetPendingApprovalsResponse> Handle(GetPendingApprovalsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            IQueryable<Domain.Entities.WorkOrder> query = _workOrderRepository.Query()
                .Where(w => w.ClientId == clientId &&
                           w.Status == Domain.Enums.WorkOrderStatus.ReadyForDelivery &&
                           w.CustomerApprovalStatus == Domain.Enums.CustomerApprovalStatus.Pending)
                .Include(w => w.Customer)
                .Include(w => w.Vehicle);

            if (request.CustomerId.HasValue)
            {
                query = query.Where(w => w.CustomerId == request.CustomerId.Value);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            if (request.Skip.HasValue)
                query = query.Skip(request.Skip.Value);

            if (request.Take.HasValue)
                query = query.Take(request.Take.Value);

            var workOrders = await query.OrderByDescending(w => w.CreatedDate).ToListAsync(cancellationToken);

            var workOrderDtos = workOrders.Select(w => new PendingApprovalDto
            {
                Id = w.Id,
                WorkOrderNumber = w.WorkOrderNumber,
                CustomerId = w.CustomerId,
                CustomerName = w.Customer?.FullName,
                VehicleId = w.VehicleId,
                VehicleLicensePlate = w.Vehicle?.LicensePlate,
                TotalAmount = w.TotalAmount,
                EstimatedDeliveryDate = w.EstimatedDeliveryDate,
                RequestDate = w.CreatedDate ?? DateTime.UtcNow
            }).ToList();

            return new GetPendingApprovalsResponse
            {
                WorkOrders = workOrderDtos,
                TotalCount = totalCount
            };
        }
    }
}
