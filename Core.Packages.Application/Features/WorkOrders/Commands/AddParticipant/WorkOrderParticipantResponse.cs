namespace MagicCarRepairAISupported.Application.Features.WorkOrders.Commands.AddParticipant;

public sealed class WorkOrderParticipantResponse
{
    public int ParticipantId { get; init; }
    public int UserId { get; init; }
    public string Role { get; init; } = "ServiceAdvisor";
}
