using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Cache;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Services.Cache
{
    public class CacheInvalidationService : ICacheInvalidationService
    {
        private readonly IRedisCacheService _cacheService;
        private readonly ITenantService _tenantService;
        private readonly ILogger<CacheInvalidationService> _logger;

        public CacheInvalidationService(
            IRedisCacheService cacheService,
            ITenantService tenantService,
            ILogger<CacheInvalidationService> logger)
        {
            _cacheService = cacheService;
            _tenantService = tenantService;
            _logger = logger;
        }

        public async Task InvalidatePartCacheAsync(int? partId = null)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId();
                var patterns = new List<string>();

                if (clientId.HasValue)
                {
                    // Parts listesi cache'leri
                    patterns.Add($"parts:list:ClientId:{clientId.Value}:*");
                    
                    // Low stock parts cache
                    patterns.Add($"parts:low-stock:ClientId:{clientId.Value}:*");
                }

                if (partId.HasValue)
                {
                    // Belirli part cache'leri
                    patterns.Add($"parts:*:PartId:{partId.Value}:*");
                }

                foreach (var pattern in patterns)
                {
                    await _cacheService.RemoveByPatternAsync(pattern);
                    _logger.LogInformation("[CACHE] Invalidated pattern: {Pattern}", pattern);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CACHE] Error invalidating part cache for PartId: {PartId}", partId);
            }
        }

        public async Task InvalidateWorkOrderCacheAsync(int? workOrderId = null)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId();
                var patterns = new List<string>();

                if (clientId.HasValue)
                {
                    // WorkOrder listesi cache'leri
                    patterns.Add($"workorders:list:ClientId:{clientId.Value}:*");
                    
                    // Dashboard cache'leri (workorder istatistikleri etkilenir)
                    await InvalidateDashboardCacheAsync(clientId);
                }

                if (workOrderId.HasValue)
                {
                    // Belirli workorder cache'leri
                    patterns.Add($"workorders:*:WorkOrderId:{workOrderId.Value}:*");
                }

                foreach (var pattern in patterns)
                {
                    await _cacheService.RemoveByPatternAsync(pattern);
                    _logger.LogInformation("[CACHE] Invalidated pattern: {Pattern}", pattern);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CACHE] Error invalidating workorder cache for WorkOrderId: {WorkOrderId}", workOrderId);
            }
        }

        public async Task InvalidateDashboardCacheAsync(int? clientId = null)
        {
            try
            {
                var targetClientId = clientId ?? _tenantService.GetCurrentClientId();
                if (targetClientId.HasValue)
                {
                    var pattern = $"dashboard:*:ClientId:{targetClientId.Value}:*";
                    await _cacheService.RemoveByPatternAsync(pattern);
                    _logger.LogInformation("[CACHE] Invalidated dashboard cache for ClientId: {ClientId}", targetClientId.Value);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CACHE] Error invalidating dashboard cache for ClientId: {ClientId}", clientId);
            }
        }

        public async Task InvalidateReportCacheAsync(int? clientId = null)
        {
            try
            {
                var targetClientId = clientId ?? _tenantService.GetCurrentClientId();
                if (targetClientId.HasValue)
                {
                    var pattern = $"reports:*:ClientId:{targetClientId.Value}:*";
                    await _cacheService.RemoveByPatternAsync(pattern);
                    _logger.LogInformation("[CACHE] Invalidated report cache for ClientId: {ClientId}", targetClientId.Value);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CACHE] Error invalidating report cache for ClientId: {ClientId}", clientId);
            }
        }

        public async Task InvalidateSupplierCacheAsync(int? supplierId = null)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId();
                var patterns = new List<string>();

                if (clientId.HasValue)
                {
                    // Supplier listesi cache'leri
                    patterns.Add($"suppliers:list:ClientId:{clientId.Value}:*");
                }

                if (supplierId.HasValue)
                {
                    // Belirli supplier cache'leri
                    patterns.Add($"suppliers:*:SupplierId:{supplierId.Value}:*");
                }

                foreach (var pattern in patterns)
                {
                    await _cacheService.RemoveByPatternAsync(pattern);
                    _logger.LogInformation("[CACHE] Invalidated pattern: {Pattern}", pattern);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CACHE] Error invalidating supplier cache for SupplierId: {SupplierId}", supplierId);
            }
        }

        public async Task InvalidateCustomerCacheAsync(int? customerId = null)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId();
                var patterns = new List<string>();

                if (clientId.HasValue)
                {
                    // Customer listesi cache'leri
                    patterns.Add($"customers:list:ClientId:{clientId.Value}:*");
                    
                    // Dashboard cache'leri (customer istatistikleri etkilenir)
                    await InvalidateDashboardCacheAsync(clientId);
                }

                if (customerId.HasValue)
                {
                    // Belirli customer cache'leri
                    patterns.Add($"customers:*:CustomerId:{customerId.Value}:*");
                }

                foreach (var pattern in patterns)
                {
                    await _cacheService.RemoveByPatternAsync(pattern);
                    _logger.LogInformation("[CACHE] Invalidated pattern: {Pattern}", pattern);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CACHE] Error invalidating customer cache for CustomerId: {CustomerId}", customerId);
            }
        }

        public async Task InvalidateByPatternAsync(string pattern)
        {
            try
            {
                await _cacheService.RemoveByPatternAsync(pattern);
                _logger.LogInformation("[CACHE] Invalidated pattern: {Pattern}", pattern);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[CACHE] Error invalidating pattern: {Pattern}", pattern);
            }
        }
    }
}

