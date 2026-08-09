using System.Security.Claims;
using MagicCarRepairAISupported.Application.Common.Services.Audit;
using MagicCarRepairAISupported.Application.Common.Services.WorkOrders;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddParticipant;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RemoveParticipant;
public sealed class RemoveWorkOrderParticipantCommandHandler : IRequestHandler<RemoveWorkOrderParticipantCommand, WorkOrderParticipantResponse>
{
    private readonly IWorkOrderParticipantAuthorizationService _authorization; private readonly IEntityRepository<WorkOrderParticipant, int> _participants; private readonly IAuditLogService _audit; private readonly IHttpContextAccessor _http;
    public RemoveWorkOrderParticipantCommandHandler(IWorkOrderParticipantAuthorizationService authorization, IEntityRepository<WorkOrderParticipant, int> participants, IAuditLogService audit, IHttpContextAccessor http) => (_authorization, _participants, _audit, _http) = (authorization, participants, audit, http);
    public async Task<WorkOrderParticipantResponse> Handle(RemoveWorkOrderParticipantCommand request, CancellationToken cancellationToken)
    {
        await _authorization.EnsureCanManageParticipantsAsync(request.WorkOrderId, cancellationToken);
        var participant = await _participants.Query().SingleOrDefaultAsync(x => x.WorkOrderId == request.WorkOrderId && x.UserId == request.UserId && x.RemovedAt == null, cancellationToken) ?? throw new DomainException("WORK_ORDER_PARTICIPANT_NOT_FOUND");
        if (!int.TryParse(_http.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out var actorId)) throw new DomainException("USER_ID_REQUIRED");
        participant.RemovedAt = DateTime.UtcNow; participant.RemovedByUserId = actorId; _participants.Update(participant);
        await _audit.LogAsync("WorkOrderParticipant", participant.Id, "ParticipantRevoked", changedProperties: "RemovedAt", description: "Work-order chat participant access revoked.", cancellationToken: cancellationToken);
        return new WorkOrderParticipantResponse { ParticipantId = participant.Id, UserId = participant.UserId };
    }
}
