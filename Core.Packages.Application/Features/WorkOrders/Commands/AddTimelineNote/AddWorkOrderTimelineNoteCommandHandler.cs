using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddTimelineNote
{
    public class AddWorkOrderTimelineNoteCommandHandler : IRequestHandler<AddWorkOrderTimelineNoteCommand, AddWorkOrderTimelineNoteResponse>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IEmployeeRepository _employeeRepository;

        public AddWorkOrderTimelineNoteCommandHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<UserEntity> userManager,
            IEmployeeRepository employeeRepository)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _employeeRepository = employeeRepository;
        }

        public async Task<AddWorkOrderTimelineNoteResponse> Handle(AddWorkOrderTimelineNoteCommand request, CancellationToken cancellationToken)
        {
            if (!int.TryParse(request.WorkOrderId, out var workOrderId))
            {
                throw new DomainException("INVALID_WORK_ORDER_ID", new { Id = request.WorkOrderId });
            }

            var clientId = _tenantService.GetRequiredClientId();

            // WorkOrder kontrolü
            var workOrder = await _workOrderRepository.Query()
                .FirstOrDefaultAsync(wo => wo.Id == workOrderId && wo.ClientId == clientId, cancellationToken);

            if (workOrder == null)
            {
                throw new DomainException("WORKORDER_NOT_FOUND", new { Id = request.WorkOrderId });
            }

            // Validation
            if (string.IsNullOrWhiteSpace(request.Note))
            {
                throw new DomainException("NOTE_REQUIRED", new { Message = "Not boş olamaz." });
            }

            // Timeline kaydı oluştur
            var userId = GetCurrentUserId();
            var createdByName = await GetCreatedByNameAsync(userId);

            // Notu ekleyen kullanıcının çalışan kaydını bul (manager/owner için null olabilir)
            int? noteEmployeeId = null;
            if (userId > 0)
            {
                var noteEmployee = await _employeeRepository.Query()
                    .FirstOrDefaultAsync(e => e.UserId == userId, cancellationToken);
                noteEmployeeId = noteEmployee?.Id;
            }

            var timeline = new WorkOrderTimeline
            {
                WorkOrderId = workOrder.Id,
                EventDate = DateTime.UtcNow,
                EventType = "note",
                Description = request.Note,
                EmployeeId = noteEmployeeId,
                ClientId = clientId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = userId,
                Status = Domain.Enums.Status.Active
            };

            await _workOrderRepository.AddTimelineAsync(timeline, cancellationToken);

            return new AddWorkOrderTimelineNoteResponse
            {
                Data = new TimelineNoteDto
                {
                    Id = $"t{timeline.Id}",
                    Status = "note",
                    Note = timeline.Description,
                    CreatedAt = timeline.EventDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    CreatedBy = createdByName
                }
            };
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            return 0; // System user
        }

        private async Task<string> GetCreatedByNameAsync(int userId)
        {
            if (userId == 0)
            {
                return "Sistem";
            }

            // Önce Employee'den dene
            var employee = await _employeeRepository.Query()
                .FirstOrDefaultAsync(e => e.UserId == userId);
            if (employee != null)
            {
                return employee.FullName;
            }

            // Sonra User'dan dene
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var fullName = $"{user.FirstName} {user.LastName}".Trim();
                return !string.IsNullOrEmpty(fullName) ? fullName : (user.UserName ?? "Sistem");
            }

            return "Sistem";
        }
    }
}
