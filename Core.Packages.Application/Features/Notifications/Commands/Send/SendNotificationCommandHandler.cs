using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Notifications.Commands.Send
{
    public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, IResult>
    {
        private readonly INotificationService _notificationService;

        public SendNotificationCommandHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<IResult> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var success = await _notificationService.SendNotificationAsync(
                    request.Type,
                    request.UserId,
                    request.Email,
                    request.PhoneNumber,
                    request.Title,
                    request.Content,
                    request.RelatedEntityType,
                    request.RelatedEntityId);

                if (success)
                {
                    return new SuccessResult("Notification sent successfully");
                }
                return new ErrorResult("Failed to send notification");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}

