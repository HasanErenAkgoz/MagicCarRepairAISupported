using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// Mock stok tahmin servisi (test ve geliştirme için)
    /// </summary>
    public class MockStockForecastService : IStockForecastService
    {
        public async Task<StockForecastResponseDto> ForecastStockAsync(
            StockForecastRequestDto request,
            CancellationToken cancellationToken = default)
        {
            await Task.Delay(300, cancellationToken); // Simulate processing

            return new StockForecastResponseDto
            {
                Summary = "Mock stok tahmini: Basit analiz tamamlandı.",
                AnalysisDate = DateTime.UtcNow
            };
        }
    }
}
