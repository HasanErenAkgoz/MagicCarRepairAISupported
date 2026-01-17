using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.Notifications.Queries.GetByUser
{
    public class GetNotificationsByUserQueryHandler : IRequestHandler<GetNotificationsByUserQuery, IDataResult<List<GetNotificationsByUserResponse>>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetNotificationsByUserQueryHandler(
            INotificationRepository notificationRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _notificationRepository = notificationRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult<List<GetNotificationsByUserResponse>>> Handle(GetNotificationsByUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                int? userId = request.UserId;

                // Eğer UserId verilmemişse, HttpContext'ten al
                if (!userId.HasValue && _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true)
                {
                    var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (int.TryParse(userIdClaim, out var parsedUserId))
                    {
                        userId = parsedUserId;
                    }
                }

                if (!userId.HasValue)
                {
                    return new ErrorDataResult<List<GetNotificationsByUserResponse>>("User ID is required");
                }

                List<Domain.Entities.Notification> notifications;

                if (request.UnreadOnly)
                {
                    notifications = await _notificationRepository.GetUnreadNotificationsByUserAsync(userId.Value, cancellationToken);
                }
                else
                {
                    notifications = await _notificationRepository.GetNotificationsByUserAsync(userId.Value, cancellationToken);
                }

                // Pagination
                var pagedNotifications = notifications
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var response = pagedNotifications.Select(n => new GetNotificationsByUserResponse
                {
                    Id = n.Id,
                    Type = n.Type,
                    TypeName = n.Type.ToString(),
                    Title = n.Title,
                    Content = n.Content,
                    Status = n.Status,
                    StatusName = n.Status.ToString(),
                    SentDate = n.SentDate,
                    ReadDate = n.ReadDate,
                    CreatedDate = n.CreatedDate,
                    RelatedEntityType = n.RelatedEntityType,
                    RelatedEntityId = n.RelatedEntityId
                }).ToList();

                return new SuccessDataResult<List<GetNotificationsByUserResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<GetNotificationsByUserResponse>>(ex.Message);
            }
        }
    }
}

