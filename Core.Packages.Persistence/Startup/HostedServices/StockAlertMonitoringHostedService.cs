using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Stock;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Persistence.Startup.HostedServices
{
    /// <summary>
    /// Periyodik stok kontrolü yapan hosted service
    /// </summary>
    public class StockAlertMonitoringHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<StockAlertMonitoringHostedService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromHours(1); // Her 1 saatte bir kontrol

        public StockAlertMonitoringHostedService(
            IServiceProvider serviceProvider,
            ILogger<StockAlertMonitoringHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("StockAlertMonitoringHostedService started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckStocksAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in StockAlertMonitoringHostedService");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("StockAlertMonitoringHostedService stopped");
        }

        private async Task CheckStocksAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
            var tenantService = scope.ServiceProvider.GetRequiredService<ITenantService>();
            var stockAlertService = scope.ServiceProvider.GetRequiredService<IStockAlertService>();

            var clientIds = await context.Clients.AsNoTracking()
                .Select(c => c.Id)
                .ToListAsync(cancellationToken);

            if (clientIds.Count == 0)
            {
                _logger.LogDebug("No clients in database; skipping stock check.");
                return;
            }

            _logger.LogInformation("Starting stock check for {ClientCount} client(s)...", clientIds.Count);

            var allAlerts = new List<StockAlert>();
            foreach (var clientId in clientIds)
            {
                tenantService.SetCurrentClientId(clientId);
                allAlerts.AddRange(await stockAlertService.CheckAllStocksAsync(cancellationToken));
            }

            if (allAlerts.Count > 0)
            {
                _logger.LogWarning("Found {AlertCount} stock alerts", allAlerts.Count);
            }
            else
            {
                _logger.LogInformation("No stock alerts found");
            }
        }
    }
}

