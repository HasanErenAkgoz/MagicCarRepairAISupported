using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.WorkOrders;
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
        private readonly IWorkOrderParticipantAuthorizationService _participantAuthorization;

        public GetWorkOrderMessagesQueryHandler(
            IChatMessageRepository chatMessageRepository,
            ITenantService tenantService,
            IWorkOrderParticipantAuthorizationService participantAuthorization)
        {
            _chatMessageRepository = chatMessageRepository;
            _tenantService = tenantService;
            _participantAuthorization = participantAuthorization;
        }

        public async Task<GetWorkOrderMessagesResponse> Handle(GetWorkOrderMessagesQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");
            await _participantAuthorization.EnsureCanAccessChatAsync(request.WorkOrderId, cancellationToken);

            var skip = Math.Max(request.Skip ?? 0, 0);
            var take = Math.Clamp(request.Take ?? 50, 1, 100);
            var messages = await _chatMessageRepository.Query()
                .Where(m => m.WorkOrderId == request.WorkOrderId && m.ClientId == clientId)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Include(m => m.Attachments)
                .OrderByDescending(m => m.SentDate)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);

            var totalCount = await _chatMessageRepository.Query()
                .CountAsync(m => m.WorkOrderId == request.WorkOrderId && m.ClientId == clientId, cancellationToken);

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
                SentDate = m.SentDate,
                Attachments = m.Attachments.Select(a => new GetConversation.ChatAttachmentDto
                {
                    Id = a.Id,
                    FileName = a.OriginalFileName,
                    ContentType = a.ContentType,
                    Length = a.Length,
                    DownloadUrl = $"/api/Chat/workorder/{request.WorkOrderId}/attachments/{a.Id}/download"
                }).ToList()
            }).ToList();

            return new GetWorkOrderMessagesResponse
            {
                Messages = messageDtos,
                TotalCount = totalCount
            };
        }
    }
}
