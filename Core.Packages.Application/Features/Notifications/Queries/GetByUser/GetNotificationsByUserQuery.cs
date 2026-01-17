using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Notifications.Queries.GetByUser
{
    public class GetNotificationsByUserQuery : IRequest<IDataResult<List<GetNotificationsByUserResponse>>>
    {
        public int? UserId { get; set; } // Null ise current user
        public bool UnreadOnly { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}

