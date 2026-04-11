using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetFleetStatus
{
    public class GetFleetStatusQueryHandler : IRequestHandler<GetFleetStatusQuery, IDataResult<GetFleetStatusResponse>>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetFleetStatusQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            UserManager<UserEntity> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult<GetFleetStatusResponse>> Handle(GetFleetStatusQuery request, CancellationToken cancellationToken)
        {
            // Authorization kontrolü - Tüm roller erişebilir ama kontrol edilmeli
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userIdInt))
            {
                return new ErrorDataResult<GetFleetStatusResponse>("Unauthorized access");
            }

            var user = await _userManager.FindByIdAsync(userIdInt.ToString());
            if (user == null)
            {
                return new ErrorDataResult<GetFleetStatusResponse>("User not found");
            }

            int? clientId = null;

            // SystemAdmin tüm verileri görebilir (clientId = null), Manager ve Employee sadece kendi client'ını
            if (user.UserType != UserType.SystemAdmin)
            {
                clientId = _tenantService.GetCurrentClientId();
                if (clientId == null)
                {
                    return new ErrorDataResult<GetFleetStatusResponse>("ClientId is required");
                }
            }

            var workOrdersQuery = _workOrderRepository.Query()
                .AsNoTracking()
                .Where(wo => EF.Property<Domain.Enums.Status>(wo, "Status") != Domain.Enums.Status.Deleted); // Silinmemiş kayıtlar (BaseEntity'den gelen Status)

            // ClientId filtreleme
            if (clientId.HasValue)
            {
                workOrdersQuery = workOrdersQuery.Where(wo => wo.ClientId == clientId.Value);
            }

            // Repairing: InProgress, InRepair, DiagnosisCompleted, QualityControl, Washing
            var repairing = await workOrdersQuery
                .Where(wo => wo.Status == WorkOrderStatus.InProgress ||
                           wo.Status == WorkOrderStatus.InRepair ||
                           wo.Status == WorkOrderStatus.DiagnosisCompleted ||
                           wo.Status == WorkOrderStatus.QualityControl ||
                           wo.Status == WorkOrderStatus.Washing)
                .CountAsync(cancellationToken);

            // Completed: Delivered
            var completed = await workOrdersQuery
                .Where(wo => wo.Status == WorkOrderStatus.Delivered)
                .CountAsync(cancellationToken);

            // Waiting: VehicleEntered, AppointmentScheduled, WaitingForParts, ReadyForDelivery
            var waiting = await workOrdersQuery
                .Where(wo => wo.Status == WorkOrderStatus.VehicleEntered ||
                           wo.Status == WorkOrderStatus.AppointmentScheduled ||
                           wo.Status == WorkOrderStatus.WaitingForParts ||
                           wo.Status == WorkOrderStatus.ReadyForDelivery)
                .CountAsync(cancellationToken);

            // Total (excluding Cancelled)
            var total = await workOrdersQuery
                .Where(wo => wo.Status != WorkOrderStatus.Cancelled)
                .CountAsync(cancellationToken);

            // Efficiency: (Completed / Total) * 100, 1 ondalık basamak
            decimal efficiency = 0;
            if (total > 0)
            {
                efficiency = Math.Round((decimal)completed / total * 100, 1);
            }

            var response = new GetFleetStatusResponse
            {
                Repairing = repairing,
                Completed = completed,
                Waiting = waiting,
                Efficiency = efficiency
            };

            return new SuccessDataResult<GetFleetStatusResponse>(response);
        }
    }
}
