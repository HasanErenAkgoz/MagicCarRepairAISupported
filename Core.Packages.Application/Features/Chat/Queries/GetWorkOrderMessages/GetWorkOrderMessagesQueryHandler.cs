using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Chat.Queries.GetWorkOrderMessages
{
    public class GetWorkOrderMessagesQueryHandler : IRequestHandler<GetWorkOrderMessagesQuery, GetWorkOrderMessagesResponse>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly ITenantService _tenantService;

        public GetWorkOrderMessagesQueryHandler(
            IChatMessageRepository chatMessageRepository,
            ITenantService tenantService)
        {
            _chatMessageRepository = chatMessageRepository;
            _tenantService = tenantService;
        }

        public async Task<GetWorkOrderMessagesResponse> Handle(GetWorkOrderMessagesQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            var messages = await _chatMessageRepository.GetMessagesByWorkOrderAsync(
                request.WorkOrderId,
                request.Skip,
                request.Take,
                cancellationToken);

            var totalCount = await _chatMessageRepository.Query()
                .CountAsync(m => m.WorkOrderId == request.WorkOrderId, cancellationToken);

            var messageDtos = messages.Select(m => new GetConversation.ChatMessageDto
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

            return new GetWorkOrderMessagesResponse
            {
                Messages = messageDtos,
                TotalCount = totalCount
            };
        }
    }
}
