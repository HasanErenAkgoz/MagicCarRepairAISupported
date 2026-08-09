using MediatR;

namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddParticipant;

public sealed class AddWorkOrderParticipantCommand : IRequest<WorkOrderParticipantResponse>
{
    public int WorkOrderId { get; set; }
    public int UserId { get; set; }
}
