using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;

namespace MagicCarRepairAISupported.Application.Common.Services.AI
{
    /// <summary>
    /// AI destekli stok tahmin servisi
    /// </summary>
    public interface IStockForecastService
    {
        /// <summary>
        /// Stok tüketimini tahmin eder
        /// </summary>
        Task<StockForecastResponseDto> ForecastStockAsync(
            StockForecastRequestDto request,
            CancellationToken cancellationToken = default);
    }
}
