using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.Chat.Queries.GetConversation
{
    public class GetConversationQueryHandler : IRequestHandler<GetConversationQuery, GetConversationResponse>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetConversationQueryHandler(
            IChatMessageRepository chatMessageRepository,
            ITenantService tenantService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _chatMessageRepository = chatMessageRepository;
            _tenantService = tenantService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetConversationResponse> Handle(GetConversationQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out var currentUserId))
            {
                throw new DomainException("USER_ID_REQUIRED");
            }

            var messages = await _chatMessageRepository.GetConversationAsync(
                currentUserId,
                request.OtherUserId,
                request.Skip,
                request.Take,
                cancellationToken);

            var totalCount = await _chatMessageRepository.Query()
                .CountAsync(m => (m.SenderId == currentUserId && m.ReceiverId == request.OtherUserId) ||
                               (m.SenderId == request.OtherUserId && m.ReceiverId == currentUserId),
                    cancellationToken);

            var messageDtos = messages.Select(m => new ChatMessageDto
            {
                Id = m.Id,
                SenderId = m.SenderId,
                SenderName = m.Sender?.UserName ?? "Unknown",
                ReceiverId = m.ReceiverId,
                ReceiverName = m.Receiver?.UserName ?? "Unknown",
                Message = m.Message,
                MessageType = m.MessageType,
                WorkOrderId = m.WorkOrderId,
                CustomerId = m.CustomerId,
                FilePath = m.FilePath,
                FileName = m.FileName,
                FileSize = m.FileSize,
                IsRead = m.IsRead,
                ReadDate = m.ReadDate,
                SentDate = m.SentDate
            }).ToList();

            return new GetConversationResponse
            {
                Messages = messageDtos,
                TotalCount = totalCount
            };
        }
    }
}
