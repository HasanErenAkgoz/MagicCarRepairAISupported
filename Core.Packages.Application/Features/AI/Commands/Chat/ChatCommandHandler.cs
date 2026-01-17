using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MagicCarRepairAISupported.Application.Features.AI.Commands.Chat
{
    public class ChatCommandHandler : IRequestHandler<ChatCommand, ChatResponse>
    {
        private readonly IAIChatService _chatService;
        private readonly ITenantService _tenantService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public ChatCommandHandler(
            IAIChatService chatService,
            ITenantService tenantService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _chatService = chatService;
            _tenantService = tenantService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ChatResponse> Handle(ChatCommand request, CancellationToken cancellationToken)
        {
            // Customer ID'yi HttpContext'ten al (eğer belirtilmemişse)
            if (!request.CustomerId.HasValue)
            {
                var customerIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("CustomerId")?.Value;
                if (!string.IsNullOrEmpty(customerIdClaim) && int.TryParse(customerIdClaim, out var parsedCustomerId))
                {
                    request.CustomerId = parsedCustomerId;
                }
            }

            // Chat request DTO oluştur
            var chatRequest = new ChatRequestDto
            {
                Message = request.Message,
                CustomerId = request.CustomerId,
                ConversationHistory = request.ConversationHistory,
                Language = request.Language
            };

            // Chat servisinden yanıt al
            var chatResponse = await _chatService.GetChatResponseAsync(chatRequest, cancellationToken);

            // Response mapping
            var response = new ChatResponse
            {
                Response = chatResponse.Response,
                ResponseType = chatResponse.ResponseType,
                RelatedEntityId = chatResponse.RelatedEntityId,
                Timestamp = chatResponse.Timestamp,
                AdditionalData = chatResponse.AdditionalData,
                SuggestedActions = chatResponse.SuggestedActions
            };

            return response;
        }
    }
}
