using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Subscription;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, IDataResult<CreateSubscriptionResponse>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITenantService _tenantService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CreateSubscriptionCommandHandler> _logger;

        public CreateSubscriptionCommandHandler(
            ISubscriptionRepository subscriptionRepository,
            IClientRepository clientRepository,
            ITenantService tenantService,
            IConfiguration configuration,
            ILogger<CreateSubscriptionCommandHandler> logger)
        {
            _subscriptionRepository = subscriptionRepository;
            _clientRepository = clientRepository;
            _tenantService = tenantService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IDataResult<CreateSubscriptionResponse>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Client kontrolü
                var client = await _clientRepository.GetByIdAsync(request.ClientId);
                if (client == null)
                {
                    return new ErrorDataResult<CreateSubscriptionResponse>("Client bulunamadı.");
                }

                // Mevcut aktif aboneliği kontrol et
                var existingSubscription = await _subscriptionRepository.GetActiveSubscriptionAsync(request.ClientId);
                if (existingSubscription != null && existingSubscription.IsActive())
                {
                    return new ErrorDataResult<CreateSubscriptionResponse>("Zaten aktif bir aboneliğiniz var. Önce mevcut aboneliği iptal edin veya yükseltin.");
                }

                // Plan konfigürasyonunu al
                var planConfig = GetPlanConfiguration(request.Plan);
                if (planConfig == null)
                {
                    return new ErrorDataResult<CreateSubscriptionResponse>("Geçersiz abonelik planı.");
                }

                // Fiyat hesapla
                var amount = request.PaymentPeriod == "Yearly" && planConfig.YearlyPrice.HasValue
                    ? planConfig.YearlyPrice.Value
                    : planConfig.MonthlyPrice;

                // Bitiş tarihi hesapla
                var startDate = DateTime.UtcNow;
                var endDate = request.PaymentPeriod == "Yearly"
                    ? startDate.AddYears(1)
                    : startDate.AddMonths(1);

                // Subscription oluştur
                var subscription = new Subscription
                {
                    ClientId = request.ClientId,
                    Plan = request.Plan,
                    Status = SubscriptionStatus.Pending, // Ödeme yapılınca Active olacak
                    StartDate = startDate,
                    EndDate = endDate,
                    MonthlyPrice = planConfig.MonthlyPrice,
                    YearlyPrice = planConfig.YearlyPrice,
                    MaxWorkOrders = planConfig.MaxWorkOrders,
                    MaxUsers = planConfig.MaxUsers,
                    MaxAIAnalyses = planConfig.MaxAIAnalyses,
                    Features = JsonSerializer.Serialize(planConfig.Features),
                    AutoRenew = request.AutoRenew
                };

                await _subscriptionRepository.AddAsync(subscription, cancellationToken);
                await _subscriptionRepository.SaveChangesAsync();

                _logger.LogInformation("Subscription created for client {ClientId}, plan {Plan}", request.ClientId, request.Plan);

                return new SuccessDataResult<CreateSubscriptionResponse>(new CreateSubscriptionResponse
                {
                    SubscriptionId = subscription.Id,
                    Plan = subscription.Plan,
                    Amount = amount,
                    StartDate = startDate,
                    EndDate = endDate,
                    Message = "Abonelik oluşturuldu. Ödeme yapıldıktan sonra aktif olacak."
                }, "Abonelik başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating subscription for client {ClientId}", request.ClientId);
                return new ErrorDataResult<CreateSubscriptionResponse>("Abonelik oluşturulurken bir hata oluştu.");
            }
        }

        private PlanConfiguration? GetPlanConfiguration(SubscriptionPlan plan)
        {
            var planName = plan switch
            {
                SubscriptionPlan.Free => "Free",
                SubscriptionPlan.Basic => "Basic",
                SubscriptionPlan.Professional => "Professional",
                SubscriptionPlan.Enterprise => "Enterprise",
                _ => null
            };

            if (planName == null)
            {
                return null;
            }

            var configSection = _configuration.GetSection($"Subscription:Plans:{planName}");
            if (!configSection.Exists())
            {
                // Varsayılan değerler
                return plan switch
                {
                    SubscriptionPlan.Free => new PlanConfiguration
                    {
                        MonthlyPrice = 0,
                        YearlyPrice = 0,
                        MaxWorkOrders = 10,
                        MaxUsers = 1,
                        MaxAIAnalyses = 0,
                        Features = new List<string>()
                    },
                    SubscriptionPlan.Basic => new PlanConfiguration
                    {
                        MonthlyPrice = 299,
                        YearlyPrice = 2870, // %20 indirim
                        MaxWorkOrders = 50,
                        MaxUsers = 2,
                        MaxAIAnalyses = 0,
                        Features = new List<string>()
                    },
                    SubscriptionPlan.Professional => new PlanConfiguration
                    {
                        MonthlyPrice = 799,
                        YearlyPrice = 7670, // %20 indirim
                        MaxWorkOrders = -1,
                        MaxUsers = 5,
                        MaxAIAnalyses = 10,
                        Features = new List<string> { "AdvancedReports", "AIAnalysis" }
                    },
                    SubscriptionPlan.Enterprise => new PlanConfiguration
                    {
                        MonthlyPrice = 1999,
                        YearlyPrice = 19190, // %20 indirim
                        MaxWorkOrders = -1,
                        MaxUsers = -1,
                        MaxAIAnalyses = -1,
                        Features = new List<string> { "AdvancedReports", "APIAccess", "WhiteLabel", "PrioritySupport", "AIAnalysis" }
                    },
                    _ => null
                };
            }

            return new PlanConfiguration
            {
                MonthlyPrice = configSection.GetValue<decimal>("MonthlyPrice", 0),
                YearlyPrice = configSection.GetValue<decimal?>("YearlyPrice"),
                MaxWorkOrders = configSection.GetValue<int>("MaxWorkOrders", 10),
                MaxUsers = configSection.GetValue<int>("MaxUsers", 1),
                MaxAIAnalyses = configSection.GetValue<int>("MaxAIAnalyses", 0),
                Features = configSection.GetSection("Features").Get<List<string>>() ?? new List<string>()
            };
        }

        private class PlanConfiguration
        {
            public decimal MonthlyPrice { get; set; }
            public decimal? YearlyPrice { get; set; }
            public int MaxWorkOrders { get; set; }
            public int MaxUsers { get; set; }
            public int MaxAIAnalyses { get; set; }
            public List<string> Features { get; set; } = new List<string>();
        }
    }
}
