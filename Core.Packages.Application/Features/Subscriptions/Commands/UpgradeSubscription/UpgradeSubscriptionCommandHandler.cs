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

namespace MagicCarRepairAISupported.Application.Features.Subscriptions.Commands.UpgradeSubscription
{
    public class UpgradeSubscriptionCommandHandler : IRequestHandler<UpgradeSubscriptionCommand, IDataResult<UpgradeSubscriptionResponse>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITenantService _tenantService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UpgradeSubscriptionCommandHandler> _logger;

        public UpgradeSubscriptionCommandHandler(
            ISubscriptionRepository subscriptionRepository,
            IClientRepository clientRepository,
            ITenantService tenantService,
            IConfiguration configuration,
            ILogger<UpgradeSubscriptionCommandHandler> logger)
        {
            _subscriptionRepository = subscriptionRepository;
            _clientRepository = clientRepository;
            _tenantService = tenantService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IDataResult<UpgradeSubscriptionResponse>> Handle(UpgradeSubscriptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Client kontrolü
                var client = await _clientRepository.GetByIdAsync(request.ClientId);
                if (client == null)
                {
                    return new ErrorDataResult<UpgradeSubscriptionResponse>("Client bulunamadı.");
                }

                // Mevcut aboneliği getir
                var currentSubscription = await _subscriptionRepository.GetActiveSubscriptionAsync(request.ClientId);
                if (currentSubscription == null)
                {
                    return new ErrorDataResult<UpgradeSubscriptionResponse>("Aktif abonelik bulunamadı.");
                }

                // Yeni plan mevcut plandan düşükse hata
                if (request.NewPlan < currentSubscription.Plan)
                {
                    return new ErrorDataResult<UpgradeSubscriptionResponse>("Plan düşürme işlemi için cancel ve yeni abonelik oluşturmanız gerekiyor.");
                }

                if (request.NewPlan == currentSubscription.Plan)
                {
                    return new ErrorDataResult<UpgradeSubscriptionResponse>("Zaten bu plandasınız.");
                }

                // Yeni plan konfigürasyonunu al
                var newPlanConfig = GetPlanConfiguration(request.NewPlan);
                if (newPlanConfig == null)
                {
                    return new ErrorDataResult<UpgradeSubscriptionResponse>("Geçersiz abonelik planı.");
                }

                // Fiyat hesapla (kalan günlere göre prorated)
                var amount = CalculateUpgradeAmount(currentSubscription, newPlanConfig, request.PaymentPeriod);

                // Mevcut aboneliği iptal et
                currentSubscription.Status = SubscriptionStatus.Cancelled;
                currentSubscription.CancelledDate = DateTime.UtcNow;
                currentSubscription.CancellationReason = $"Upgraded to {request.NewPlan}";
                _subscriptionRepository.Update(currentSubscription);

                // Yeni abonelik oluştur
                var startDate = DateTime.UtcNow;
                var endDate = request.PaymentPeriod == "Yearly"
                    ? startDate.AddYears(1)
                    : startDate.AddMonths(1);

                var newSubscription = new Subscription
                {
                    ClientId = request.ClientId,
                    Plan = request.NewPlan,
                    Status = SubscriptionStatus.Pending, // Ödeme yapılınca Active olacak
                    StartDate = startDate,
                    EndDate = endDate,
                    MonthlyPrice = newPlanConfig.MonthlyPrice,
                    YearlyPrice = newPlanConfig.YearlyPrice,
                    MaxWorkOrders = newPlanConfig.MaxWorkOrders,
                    MaxUsers = newPlanConfig.MaxUsers,
                    MaxAIAnalyses = newPlanConfig.MaxAIAnalyses,
                    Features = JsonSerializer.Serialize(newPlanConfig.Features),
                    AutoRenew = currentSubscription.AutoRenew
                };

                await _subscriptionRepository.AddAsync(newSubscription, cancellationToken);
                await _subscriptionRepository.SaveChangesAsync();

                _logger.LogInformation("Subscription upgraded for client {ClientId} from {OldPlan} to {NewPlan}", 
                    request.ClientId, currentSubscription.Plan, request.NewPlan);

                return new SuccessDataResult<UpgradeSubscriptionResponse>(new UpgradeSubscriptionResponse
                {
                    SubscriptionId = newSubscription.Id,
                    OldPlan = currentSubscription.Plan,
                    NewPlan = newSubscription.Plan,
                    Amount = amount,
                    StartDate = startDate,
                    EndDate = endDate,
                    Message = "Abonelik yükseltildi. Ödeme yapıldıktan sonra aktif olacak."
                }, "Abonelik başarıyla yükseltildi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error upgrading subscription for client {ClientId}", request.ClientId);
                return new ErrorDataResult<UpgradeSubscriptionResponse>("Abonelik yükseltilirken bir hata oluştu.");
            }
        }

        private decimal CalculateUpgradeAmount(Subscription currentSubscription, PlanConfiguration newPlanConfig, string paymentPeriod)
        {
            // Basit implementasyon: Tam fiyat (prorated hesaplama daha sonra eklenebilir)
            return paymentPeriod == "Yearly" && newPlanConfig.YearlyPrice.HasValue
                ? newPlanConfig.YearlyPrice.Value
                : newPlanConfig.MonthlyPrice;
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
                    SubscriptionPlan.Basic => new PlanConfiguration
                    {
                        MonthlyPrice = 299,
                        YearlyPrice = 2870,
                        MaxWorkOrders = 50,
                        MaxUsers = 2,
                        MaxAIAnalyses = 0,
                        Features = new List<string>()
                    },
                    SubscriptionPlan.Professional => new PlanConfiguration
                    {
                        MonthlyPrice = 799,
                        YearlyPrice = 7670,
                        MaxWorkOrders = -1,
                        MaxUsers = 5,
                        MaxAIAnalyses = 10,
                        Features = new List<string> { "AdvancedReports", "AIAnalysis" }
                    },
                    SubscriptionPlan.Enterprise => new PlanConfiguration
                    {
                        MonthlyPrice = 1999,
                        YearlyPrice = 19190,
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
