using MediatR;
using MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddParticipant;
namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.RemoveParticipant;
public sealed class RemoveWorkOrderParticipantCommand : IRequest<WorkOrderParticipantResponse> { public int WorkOrderId { get; set; } public int UserId { get; set; } }
