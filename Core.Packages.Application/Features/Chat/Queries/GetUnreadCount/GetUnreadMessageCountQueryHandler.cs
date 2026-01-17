using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.Chat.Queries.GetUnreadCount
{
    public class GetUnreadMessageCountQueryHandler : IRequestHandler<GetUnreadMessageCountQuery, GetUnreadMessageCountResponse>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUnreadMessageCountQueryHandler(
            IChatMessageRepository chatMessageRepository,
            ITenantService tenantService,
            IHttpContextAccessor httpContextAccessor)
        {
            _chatMessageRepository = chatMessageRepository;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetUnreadMessageCountResponse> Handle(GetUnreadMessageCountQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                throw new DomainException("USER_ID_REQUIRED");
            }

            var unreadCount = await _chatMessageRepository.GetUnreadMessageCountAsync(userId, cancellationToken);

            return new GetUnreadMessageCountResponse
            {
                UnreadCount = unreadCount
            };
        }
    }
}
