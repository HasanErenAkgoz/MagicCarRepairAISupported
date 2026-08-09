using System.Security.Claims;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.WorkOrders;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Persistence.Services;

/// <summary>Database-backed authorization; no participant state is cached.</summary>
public sealed class WorkOrderParticipantAuthorizationService : IWorkOrderParticipantAuthorizationService
{
    private readonly BaseDbContext _db;
    private readonly ITenantService _tenant;
    private readonly IHttpContextAccessor _http;

    public WorkOrderParticipantAuthorizationService(BaseDbContext db, ITenantService tenant, IHttpContextAccessor http)
        => (_db, _tenant, _http) = (db, tenant, http);

    public async Task EnsureCanAccessChatAsync(int workOrderId, CancellationToken cancellationToken = default)
    {
        var (userId, clientId) = CurrentIdentity();
        await EnsureUserCanAccessChatAsync(workOrderId, userId, cancellationToken);
    }

    public async Task EnsureUserCanAccessChatAsync(int workOrderId, int userId, CancellationToken cancellationToken = default)
    {
        var clientId = _tenant.GetRequiredClientId();
        var workOrder = await _db.WorkOrders.AsNoTracking()
            .Include(x => x.Customer).Include(x => x.AssignedEmployee)
            .SingleOrDefaultAsync(x => x.Id == workOrderId && x.ClientId == clientId, cancellationToken)
            ?? throw new DomainException("WORK_ORDER_NOT_FOUND");

        var accountIsUsable = await _db.Users.AsNoTracking().AnyAsync(x =>
            x.Id == userId && x.ClientId == clientId && (!x.LockoutEnabled || x.LockoutEnd == null || x.LockoutEnd <= DateTimeOffset.UtcNow), cancellationToken);
        if (!accountIsUsable)
            throw new DomainException("WORK_ORDER_CHAT_ACCESS_DENIED");

        var isCustomer = workOrder.Customer?.UserId == userId;
        var isAssignedActiveEmployee = workOrder.AssignedEmployee?.UserId == userId &&
            workOrder.AssignedEmployee.EmploymentStatus == EmploymentStatus.Active;
        var isActiveAdvisor = await _db.WorkOrderParticipants.AsNoTracking().AnyAsync(x =>
            x.WorkOrderId == workOrderId && x.UserId == userId && x.ClientId == clientId && x.RemovedAt == null &&
            _db.Employees.Any(employee =>
                employee.UserId == userId &&
                employee.ClientId == clientId &&
                employee.EmploymentStatus == EmploymentStatus.Active),
            cancellationToken);

        if (!isCustomer && !isAssignedActiveEmployee && !isActiveAdvisor)
            throw new DomainException("WORK_ORDER_CHAT_ACCESS_DENIED");
    }

    public async Task EnsureCanManageParticipantsAsync(int workOrderId, CancellationToken cancellationToken = default)
    {
        var (_, clientId) = CurrentIdentity();
        if (_tenant.GetCurrentUserType() != UserType.Manager)
            throw new DomainException("WORK_ORDER_PARTICIPANT_MANAGEMENT_DENIED");

        var status = await _db.WorkOrders.AsNoTracking()
            .Where(x => x.Id == workOrderId && x.ClientId == clientId)
            .Select(x => (WorkOrderStatus?)x.Status)
            .SingleOrDefaultAsync(cancellationToken);
        if (status is null)
            throw new DomainException("WORK_ORDER_NOT_FOUND");
        if (status is WorkOrderStatus.Delivered or WorkOrderStatus.Cancelled)
            throw new DomainException("WORK_ORDER_PARTICIPANT_MANAGEMENT_CLOSED");
    }

    private (int UserId, int ClientId) CurrentIdentity()
    {
        var claim = _http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(claim, out var userId)) throw new DomainException("USER_ID_REQUIRED");
        return (userId, _tenant.GetRequiredClientId());
    }
}
