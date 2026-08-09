using System.Security.Claims;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Audit;
using MagicCarRepairAISupported.Application.Common.Services.WorkOrders;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddParticipant;

public sealed class AddWorkOrderParticipantCommandHandler : IRequestHandler<AddWorkOrderParticipantCommand, WorkOrderParticipantResponse>
{
    private readonly IWorkOrderParticipantAuthorizationService _authorization;
    private readonly IEntityRepository<WorkOrderParticipant, int> _participants;
    private readonly IEmployeeRepository _employees;
    private readonly IUserRepository _users;
    private readonly ITenantService _tenant;
    private readonly IAuditLogService _audit;
    private readonly IHttpContextAccessor _http;
    public AddWorkOrderParticipantCommandHandler(IWorkOrderParticipantAuthorizationService authorization, IEntityRepository<WorkOrderParticipant, int> participants, IEmployeeRepository employees, IUserRepository users, ITenantService tenant, IAuditLogService audit, IHttpContextAccessor http)
        => (_authorization, _participants, _employees, _users, _tenant, _audit, _http) = (authorization, participants, employees, users, tenant, audit, http);

    public async Task<WorkOrderParticipantResponse> Handle(AddWorkOrderParticipantCommand request, CancellationToken cancellationToken)
    {
        await _authorization.EnsureCanManageParticipantsAsync(request.WorkOrderId, cancellationToken);
        var clientId = _tenant.GetRequiredClientId();
        var actorId = ParseActor();
        var user = await _users.Query().AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.UserId && x.ClientId == clientId, cancellationToken)
            ?? throw new DomainException("WORK_ORDER_PARTICIPANT_USER_NOT_FOUND");
        var isActiveStaff = await _employees.Query().AsNoTracking().AnyAsync(x => x.UserId == user.Id && x.ClientId == clientId && x.EmploymentStatus == EmploymentStatus.Active, cancellationToken);
        if (!isActiveStaff || user.UserType != UserType.Employee)
            throw new DomainException("WORK_ORDER_PARTICIPANT_MUST_BE_ACTIVE_STAFF");
        var participant = await _participants.Query().SingleOrDefaultAsync(x => x.WorkOrderId == request.WorkOrderId && x.UserId == request.UserId, cancellationToken);
        if (participant is not null && participant.RemovedAt is null)
            throw new DomainException("WORK_ORDER_PARTICIPANT_ALREADY_EXISTS");
        if (participant is not null)
        {
            participant.RemovedAt = null;
            participant.RemovedByUserId = null;
            participant.AddedByUserId = actorId;
            _participants.Update(participant);
        }
        else
        {
            participant = await _participants.AddAsync(new WorkOrderParticipant { WorkOrderId = request.WorkOrderId, UserId = request.UserId, Role = WorkOrderParticipantRole.ServiceAdvisor, AddedByUserId = actorId, ClientId = clientId }, cancellationToken);
        }
        await _audit.LogAsync("WorkOrderParticipant", participant.Id, "ParticipantGranted", changedProperties: "Role", description: "Work-order chat participant access granted.", cancellationToken: cancellationToken);
        return new WorkOrderParticipantResponse { ParticipantId = participant.Id, UserId = participant.UserId };
    }
    private int ParseActor() => int.TryParse(_http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : throw new DomainException("USER_ID_REQUIRED");
}
