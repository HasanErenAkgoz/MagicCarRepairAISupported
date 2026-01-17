using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.Chat.Commands.MarkAsRead
{
    public class MarkMessagesAsReadCommandHandler : IRequestHandler<MarkMessagesAsReadCommand, MarkMessagesAsReadResponse>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MarkMessagesAsReadCommandHandler(
            IChatMessageRepository chatMessageRepository,
            ITenantService tenantService,
            IHttpContextAccessor httpContextAccessor)
        {
            _chatMessageRepository = chatMessageRepository;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<MarkMessagesAsReadResponse> Handle(MarkMessagesAsReadCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
            {
                throw new DomainException("USER_ID_REQUIRED");
            }

            await _chatMessageRepository.MarkMessagesAsReadAsync(userId, request.SenderId, request.WorkOrderId, cancellationToken);

            return new MarkMessagesAsReadResponse
            {
                Success = true
            };
        }
    }
}
