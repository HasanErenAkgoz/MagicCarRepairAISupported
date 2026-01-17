using MagicCarRepairAISupported.Application.Common.Services.Stock;
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
            using (var scope = _serviceProvider.CreateScope())
            {
                var stockAlertService = scope.ServiceProvider.GetRequiredService<IStockAlertService>();

                _logger.LogInformation("Starting stock check...");

                var alerts = await stockAlertService.CheckAllStocksAsync(cancellationToken);

                if (alerts.Any())
                {
                    _logger.LogWarning($"Found {alerts.Count} stock alerts");
                }
                else
                {
                    _logger.LogInformation("No stock alerts found");
                }
            }
        }
    }
}

