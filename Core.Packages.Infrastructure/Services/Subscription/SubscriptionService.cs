using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Subscription;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using SubscriptionEntity = MagicCarRepairAISupported.Domain.Entities.Subscription;
using AppLimitType = MagicCarRepairAISupported.Application.Common.Services.Subscription.LimitType;

namespace MagicCarRepairAISupported.Infrastructure.Services.Subscription
{
    /// <summary>
    /// Abonelik servisi implementasyonu
    /// </summary>
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly ITenantService _tenantService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SubscriptionService> _logger;

        public SubscriptionService(
            ISubscriptionRepository subscriptionRepository,
            ITenantService tenantService,
            IConfiguration configuration,
            ILogger<SubscriptionService> logger)
        {
            _subscriptionRepository = subscriptionRepository;
            _tenantService = tenantService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<SubscriptionEntity?> GetCurrentSubscriptionAsync(int clientId)
        {
            try
            {
                var subscription = await _subscriptionRepository.GetActiveSubscriptionAsync(clientId);
                
                // Eğer aktif abonelik yoksa, Free plan varsayılan abonelik oluştur
                if (subscription == null)
                {
                    subscription = await CreateDefaultFreeSubscriptionAsync(clientId);
                }

                return subscription;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting current subscription for client {ClientId}", clientId);
                return null;
            }
        }

        public async Task<bool> CheckFeatureAccessAsync(int clientId, string featureName)
        {
            try
            {
                var subscription = await GetCurrentSubscriptionAsync(clientId);
                if (subscription == null || !subscription.IsActive())
                {
                    return false;
                }

                // Features JSON'dan parse et
                if (string.IsNullOrEmpty(subscription.Features))
                {
                    return false;
                }

                var features = JsonSerializer.Deserialize<List<string>>(subscription.Features) ?? new List<string>();
                return features.Contains(featureName, StringComparer.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking feature access for client {ClientId}, feature {Feature}", clientId, featureName);
                return false;
            }
        }

        public async Task<bool> CheckLimitAsync(int clientId, AppLimitType limitType, int currentUsage)
        {
            try
            {
                var subscription = await GetCurrentSubscriptionAsync(clientId);
                if (subscription == null || !subscription.IsActive())
                {
                    return false;
                }

                var limit = GetLimitForType(subscription, limitType);
                
                // -1 = sınırsız
                if (limit == -1)
                {
                    return true;
                }

                return currentUsage < limit;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking limit for client {ClientId}, limitType {LimitType}", clientId, limitType);
                return false;
            }
        }

        public async Task<int> GetRemainingLimitAsync(int clientId, AppLimitType limitType)
        {
            try
            {
                var subscription = await GetCurrentSubscriptionAsync(clientId);
                if (subscription == null || !subscription.IsActive())
                {
                    return 0;
                }

                var limit = GetLimitForType(subscription, limitType);
                
                // -1 = sınırsız
                if (limit == -1)
                {
                    return int.MaxValue;
                }

                // TODO: Mevcut kullanımı hesapla (UsageTracking'den)
                // Şimdilik sadece limit'i döndür
                return limit;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting remaining limit for client {ClientId}, limitType {LimitType}", clientId, limitType);
                return 0;
            }
        }

        public async Task<SubscriptionLimits> GetPlanLimitsAsync(int clientId)
        {
            try
            {
                var subscription = await GetCurrentSubscriptionAsync(clientId);
                if (subscription == null || !subscription.IsActive())
                {
                    // Free plan varsayılan limitleri
                    return new SubscriptionLimits
                    {
                        MaxWorkOrders = 10,
                        MaxUsers = 1,
                        MaxAIAnalyses = 0,
                        Features = new List<string>()
                    };
                }

                var features = new List<string>();
                if (!string.IsNullOrEmpty(subscription.Features))
                {
                    features = JsonSerializer.Deserialize<List<string>>(subscription.Features) ?? new List<string>();
                }

                return new SubscriptionLimits
                {
                    MaxWorkOrders = subscription.MaxWorkOrders,
                    MaxUsers = subscription.MaxUsers,
                    MaxAIAnalyses = subscription.MaxAIAnalyses,
                    Features = features
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting plan limits for client {ClientId}", clientId);
                return new SubscriptionLimits();
            }
        }

        public async Task<bool> IsSubscriptionActiveAsync(int clientId)
        {
            try
            {
                var subscription = await GetCurrentSubscriptionAsync(clientId);
                return subscription != null && subscription.IsActive();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking subscription active status for client {ClientId}", clientId);
                return false;
            }
        }

        private int GetLimitForType(SubscriptionEntity subscription, AppLimitType limitType)
        {
            return limitType switch
            {
                AppLimitType.WorkOrder => subscription.MaxWorkOrders,
                AppLimitType.User => subscription.MaxUsers,
                AppLimitType.AIAnalysis => subscription.MaxAIAnalyses,
                _ => 0
            };
        }

        private async Task<SubscriptionEntity> CreateDefaultFreeSubscriptionAsync(int clientId)
        {
            var planConfig = _configuration.GetSection("Subscription:Plans:Free");
            var monthlyPrice = planConfig.GetValue<decimal>("MonthlyPrice", 0);
            var maxWorkOrders = planConfig.GetValue<int>("MaxWorkOrders", 10);
            var maxUsers = planConfig.GetValue<int>("MaxUsers", 1);
            var maxAIAnalyses = planConfig.GetValue<int>("MaxAIAnalyses", 0);

            var subscription = new SubscriptionEntity
            {
                ClientId = clientId,
                Plan = SubscriptionPlan.Free,
                Status = SubscriptionStatus.Active,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddYears(100), // Free plan için uzun süre
                MonthlyPrice = monthlyPrice,
                MaxWorkOrders = maxWorkOrders,
                MaxUsers = maxUsers,
                MaxAIAnalyses = maxAIAnalyses,
                Features = JsonSerializer.Serialize(new List<string>()),
                AutoRenew = false
            };

            await _subscriptionRepository.AddAsync(subscription, CancellationToken.None);
            await _subscriptionRepository.SaveChangesAsync();

            _logger.LogInformation("Created default Free subscription for client {ClientId}", clientId);
            return subscription;
        }
    }
}
