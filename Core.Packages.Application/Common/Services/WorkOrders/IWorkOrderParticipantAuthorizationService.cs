namespace MagicCarRepairAISupported.Application.Common.Services.WorkOrders;

public interface IWorkOrderParticipantAuthorizationService
{
    Task EnsureCanAccessChatAsync(int workOrderId, CancellationToken cancellationToken = default);
    Task EnsureUserCanAccessChatAsync(int workOrderId, int userId, CancellationToken cancellationToken = default);
    Task EnsureCanManageParticipantsAsync(int workOrderId, CancellationToken cancellationToken = default);
}
