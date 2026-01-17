using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Notifications.Commands.MarkAsRead
{
    public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, IResult>
    {
        private readonly INotificationRepository _notificationRepository;

        public MarkNotificationAsReadCommandHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IResult> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var notification = await _notificationRepository.GetByIdAsync(request.NotificationId);
                if (notification == null)
                {
                    return new ErrorResult("Notification not found");
                }

                notification.MarkAsRead();
                _notificationRepository.Update(notification);

                return new SuccessResult("Notification marked as read");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}

