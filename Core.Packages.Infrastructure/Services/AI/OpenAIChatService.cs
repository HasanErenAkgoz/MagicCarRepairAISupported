using Azure;
using Azure.AI.OpenAI;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Infrastructure.Configurations.AI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// OpenAI destekli chat servisi
    /// </summary>
    public class OpenAIChatService : IAIChatService
    {
        private readonly ILogger<OpenAIChatService> _logger;
        private readonly AIOptions _aiOptions;
        private readonly OpenAIClient? _openAIClient;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ITenantService _tenantService;

        public OpenAIChatService(
            ILogger<OpenAIChatService> logger,
            IOptions<AIOptions> aiOptions,
            IWorkOrderRepository workOrderRepository,
            ICustomerRepository customerRepository,
            IAppointmentRepository appointmentRepository,
            ITenantService tenantService)
        {
            _logger = logger;
            _aiOptions = aiOptions.Value;
            _workOrderRepository = workOrderRepository;
            _customerRepository = customerRepository;
            _appointmentRepository = appointmentRepository;
            _tenantService = tenantService;

            // Initialize OpenAI client
            try
            {
                if (_aiOptions.Provider == "AzureOpenAI" && !string.IsNullOrEmpty(_aiOptions.AzureEndpoint))
                {
                    _openAIClient = new OpenAIClient(
                        new Uri(_aiOptions.AzureEndpoint!),
                        new AzureKeyCredential(_aiOptions.ApiKey ?? throw new InvalidOperationException("Azure OpenAI API Key is required")));
                }
                else if (_aiOptions.Provider == "OpenAI" && !string.IsNullOrEmpty(_aiOptions.ApiKey))
                {
                    _openAIClient = new OpenAIClient(_aiOptions.ApiKey);
                }
                else
                {
                    _logger.LogWarning("AI Provider not configured. Chat service will use mock responses.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize OpenAI client. Chat service will use mock responses.");
            }
        }

        public async Task<ChatResponseDto> GetChatResponseAsync(ChatRequestDto request, CancellationToken cancellationToken = default)
        {
            try
            {
                // Müşteri bilgilerini ve context'i hazırla
                var context = await BuildContextAsync(request, cancellationToken);

                // OpenAI'ye istek gönder
                if (_openAIClient != null)
                {
                    return await GetOpenAIResponseAsync(request, context, cancellationToken);
                }
                else
                {
                    // Mock response (AI yapılandırılmamışsa)
                    return GetMockResponse(request, context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting chat response");
                return new ChatResponseDto
                {
                    Response = request.Language == "tr" 
                        ? "Üzgünüm, şu anda size yardımcı olamıyorum. Lütfen daha sonra tekrar deneyin."
                        : "I'm sorry, I can't help you right now. Please try again later.",
                    ResponseType = "Error",
                    Timestamp = DateTime.UtcNow
                };
            }
        }

        private async Task<string> BuildContextAsync(ChatRequestDto request, CancellationToken cancellationToken)
        {
            var contextBuilder = new StringBuilder();

            contextBuilder.AppendLine("Sen bir otomobil tamir servisi müşteri destek asistanısın. Müşterilere yardımcı olacaksın.");
            contextBuilder.AppendLine();

            // Müşteri bilgileri
            if (request.CustomerId.HasValue)
            {
                var clientId = _tenantService.GetCurrentClientId();
                if (clientId.HasValue)
                {
                    var customer = await _customerRepository.GetByIdAsync(request.CustomerId.Value);
                    if (customer != null)
                    {
                        contextBuilder.AppendLine($"Müşteri Bilgileri:");
                        contextBuilder.AppendLine($"- İsim: {customer.FullName}");
                        contextBuilder.AppendLine($"- Telefon: {customer.PhoneNumber ?? "N/A"}");
                        contextBuilder.AppendLine();

                        // Müşterinin aktif iş emirleri
                        var allWorkOrders = await _workOrderRepository.GetByCustomerIdAsync(request.CustomerId.Value, cancellationToken);
                        var activeWorkOrders = allWorkOrders
                            .Where(wo => wo.Status != Domain.Enums.WorkOrderStatus.Delivered && 
                                       wo.Status != Domain.Enums.WorkOrderStatus.Cancelled)
                            .OrderByDescending(wo => wo.CreatedDate)
                            .Take(3)
                            .ToList();
                        
                        if (activeWorkOrders.Any())
                        {
                            contextBuilder.AppendLine("Aktif İş Emirleri:");
                            foreach (var wo in activeWorkOrders)
                            {
                                contextBuilder.AppendLine($"- İş Emri #{wo.WorkOrderNumber}: {wo.Status} (Araç: {wo.Vehicle?.LicensePlate})");
                            }
                            contextBuilder.AppendLine();
                        }

                        // Yaklaşan randevular
                        var allAppointments = await _appointmentRepository.GetByCustomerIdAsync(request.CustomerId.Value, cancellationToken);
                        var upcomingAppointments = allAppointments
                            .Where(apt => apt.AppointmentDate >= DateTime.UtcNow && 
                                        apt.Status != Domain.Enums.AppointmentStatus.Cancelled)
                            .OrderBy(apt => apt.AppointmentDate)
                            .Take(3)
                            .ToList();
                        
                        if (upcomingAppointments.Any())
                        {
                            contextBuilder.AppendLine("Yaklaşan Randevular:");
                            foreach (var apt in upcomingAppointments)
                            {
                                contextBuilder.AppendLine($"- {apt.AppointmentDate:dd.MM.yyyy HH:mm}");
                            }
                            contextBuilder.AppendLine();
                        }
                    }
                }
            }

            // Desteklenen işlemler
            contextBuilder.AppendLine("Yapabileceğin işlemler:");
            contextBuilder.AppendLine("- İş emri durumu sorgulama");
            contextBuilder.AppendLine("- Randevu alma/yeniden planlama");
            contextBuilder.AppendLine("- Genel sorulara cevap verme");
            contextBuilder.AppendLine("- Servis bilgileri paylaşma");
            contextBuilder.AppendLine();

            contextBuilder.AppendLine($"Dil: {request.Language}");

            return contextBuilder.ToString();
        }

        private async Task<ChatResponseDto> GetOpenAIResponseAsync(ChatRequestDto request, string context, CancellationToken cancellationToken)
        {
            var messages = new List<ChatRequestMessage>();

            // System message
            messages.Add(new ChatRequestSystemMessage(context));

            // Conversation history
            if (request.ConversationHistory != null && request.ConversationHistory.Any())
            {
                foreach (var msg in request.ConversationHistory)
                {
                    if (msg.Role == "user")
                        messages.Add(new ChatRequestUserMessage(msg.Content));
                    else if (msg.Role == "assistant")
                        messages.Add(new ChatRequestAssistantMessage(msg.Content));
                }
            }

            // Current user message
            messages.Add(new ChatRequestUserMessage(request.Message));

            var chatCompletionsOptions = new ChatCompletionsOptions(
                deploymentName: _aiOptions.AzureDeploymentName ?? _aiOptions.Model,
                messages);

            chatCompletionsOptions.Temperature = 0.7f;
            chatCompletionsOptions.MaxTokens = 500;

            var response = await _openAIClient!.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
            var chatResponse = response.Value.Choices[0].Message.Content;

            // Response type'ı belirle (mesaj içeriğine göre)
            var responseType = DetermineResponseType(request.Message, chatResponse);

            return new ChatResponseDto
            {
                Response = chatResponse,
                ResponseType = responseType,
                Timestamp = DateTime.UtcNow,
                SuggestedActions = GetSuggestedActions(responseType)
            };
        }

        private ChatResponseDto GetMockResponse(ChatRequestDto request, string context)
        {
            var message = request.Message.ToLower();

            string response;
            string responseType;

            if (message.Contains("iş emri") || message.Contains("work order") || message.Contains("durum"))
            {
                responseType = "WorkOrderStatus";
                response = request.Language == "tr"
                    ? "İş emri durumunu öğrenmek için lütfen iş emri numaranızı paylaşın."
                    : "To check your work order status, please provide your work order number.";
            }
            else if (message.Contains("randevu") || message.Contains("appointment") || message.Contains("tarih"))
            {
                responseType = "Appointment";
                response = request.Language == "tr"
                    ? "Randevu almak için lütfen tercih ettiğiniz tarih ve saat aralığını belirtin."
                    : "To schedule an appointment, please specify your preferred date and time.";
            }
            else
            {
                responseType = "General";
                response = request.Language == "tr"
                    ? "Size nasıl yardımcı olabilirim? İş emri durumu sorgulama, randevu alma gibi konularda yardımcı olabilirim."
                    : "How can I help you? I can assist you with work order status, appointments, and general questions.";
            }

            return new ChatResponseDto
            {
                Response = response,
                ResponseType = responseType,
                Timestamp = DateTime.UtcNow,
                SuggestedActions = GetSuggestedActions(responseType)
            };
        }

        private string DetermineResponseType(string userMessage, string aiResponse)
        {
            var message = userMessage.ToLower();
            if (message.Contains("iş emri") || message.Contains("work order") || message.Contains("durum"))
                return "WorkOrderStatus";
            if (message.Contains("randevu") || message.Contains("appointment"))
                return "Appointment";
            return "General";
        }

        private List<string> GetSuggestedActions(string responseType)
        {
            return responseType switch
            {
                "WorkOrderStatus" => new List<string> { "İş emri durumu sorgula", "İş emri detaylarını göster" },
                "Appointment" => new List<string> { "Randevu al", "Randevularımı görüntüle" },
                _ => new List<string> { "Yardım al", "İletişim bilgileri" }
            };
        }
    }
}
