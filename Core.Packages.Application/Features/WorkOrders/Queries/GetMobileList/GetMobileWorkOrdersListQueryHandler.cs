using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Queries.GetMobileList
{
    public class GetMobileWorkOrdersListQueryHandler : IRequestHandler<GetMobileWorkOrdersListQuery, GetMobileWorkOrdersListResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;

        public GetMobileWorkOrdersListQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
        }

        public async Task<GetMobileWorkOrdersListResponse> Handle(GetMobileWorkOrdersListQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Query oluştur
            var query = _workOrderRepository.Query()
                .Include(wo => wo.Vehicle)
                .Include(wo => wo.Customer)
                .Include(wo => wo.AssignedEmployee)
                .Include(wo => wo.Items)
                .Where(wo => wo.ClientId == clientId);

            // Müşteri filtresi
            if (request.CustomerId.HasValue)
            {
                query = query.Where(wo => wo.CustomerId == request.CustomerId.Value);
            }

            // Status filtresi - string'den enum'a mapping
            if (!string.IsNullOrEmpty(request.Status))
            {
                var statusFilter = MapStatusStringToEnums(request.Status);
                if (statusFilter.Any())
                {
                    query = query.Where(wo => statusFilter.Contains(wo.Status));
                }
            }

            // Toplam sayı
            var totalCount = await query.CountAsync(cancellationToken);

            // Pagination
            var page = Math.Max(1, request.Page);
            var pageSize = Math.Max(1, Math.Min(100, request.PageSize)); // Max 100
            var skip = (page - 1) * pageSize;

            var workOrders = await query
                .OrderByDescending(wo => wo.EntryDate)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            // Response mapping
            var items = workOrders.Select(wo => new WorkOrderListItem
            {
                Id = wo.Id.ToString(),
                OrderNo = wo.WorkOrderNumber,
                CustomerName = wo.Customer?.FullName ?? "Bilinmiyor",
                VehiclePlate = wo.Vehicle?.LicensePlate ?? "",
                VehicleModel = wo.Vehicle != null 
                    ? $"{wo.Vehicle.Brand} {wo.Vehicle.Model} {wo.Vehicle.Year}"
                    : "",
                Service = GetServiceTitle(wo), // İlk item'ın description'ı veya "Genel Bakım"
                Status = MapStatusEnumToString(wo.Status),
                Date = wo.EntryDate.ToString("yyyy-MM-dd"), // ISO 8601
                Amount = wo.TotalAmount,
                TechnicianName = wo.AssignedEmployee?.FullName
            }).ToList();

            return new GetMobileWorkOrdersListResponse
            {
                Data = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

        /// <summary>
        /// String status'u enum listesine çevirir
        /// </summary>
        private List<WorkOrderStatus> MapStatusStringToEnums(string status)
        {
            return status.ToLower() switch
            {
                "pending" => new List<WorkOrderStatus>
                {
                    WorkOrderStatus.AppointmentScheduled,
                    WorkOrderStatus.VehicleEntered,
                    WorkOrderStatus.WaitingForParts,
                    WorkOrderStatus.ReadyForDelivery
                },
                "inprogress" => new List<WorkOrderStatus>
                {
                    WorkOrderStatus.InProgress,
                    WorkOrderStatus.InRepair,
                    WorkOrderStatus.DiagnosisCompleted,
                    WorkOrderStatus.QualityControl,
                    WorkOrderStatus.Washing
                },
                "completed" => new List<WorkOrderStatus> { WorkOrderStatus.Delivered },
                "cancelled" => new List<WorkOrderStatus> { WorkOrderStatus.Cancelled },
                _ => new List<WorkOrderStatus>()
            };
        }

        /// <summary>
        /// Enum status'u string'e çevirir
        /// </summary>
        private string MapStatusEnumToString(WorkOrderStatus status)
        {
            return status switch
            {
                WorkOrderStatus.AppointmentScheduled or
                WorkOrderStatus.VehicleEntered or
                WorkOrderStatus.WaitingForParts or
                WorkOrderStatus.ReadyForDelivery => "pending",
                
                WorkOrderStatus.InProgress or
                WorkOrderStatus.InRepair or
                WorkOrderStatus.DiagnosisCompleted or
                WorkOrderStatus.QualityControl or
                WorkOrderStatus.Washing => "inProgress",
                
                WorkOrderStatus.Delivered => "completed",
                
                WorkOrderStatus.Cancelled => "cancelled",
                
                _ => "pending"
            };
        }

        /// <summary>
        /// Servis başlığını alır (ilk item'ın description'ı veya varsayılan)
        /// </summary>
        private string GetServiceTitle(Domain.Entities.WorkOrder workOrder)
        {
            if (workOrder.Items != null && workOrder.Items.Any())
            {
                var firstItem = workOrder.Items.FirstOrDefault(i => !string.IsNullOrEmpty(i.Description));
                if (firstItem != null)
                {
                    return firstItem.Description;
                }
            }

            // Özel istekler veya müşteri şikayetleri varsa onları kullan
            if (!string.IsNullOrEmpty(workOrder.SpecialRequests))
            {
                return "Özel İstek";
            }

            if (!string.IsNullOrEmpty(workOrder.CustomerComplaints))
            {
                return "Müşteri Şikayeti";
            }

            return "Genel Bakım";
        }
    }
}
