using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Domain.Exceptions;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.ForecastStock
{
    public class ForecastStockQueryHandler : IRequestHandler<ForecastStockQuery, ForecastStockResponse>
    {
        private readonly IStockForecastService _forecastService;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public ForecastStockQueryHandler(
            IStockForecastService forecastService,
            ITenantService tenantService,
            IMapper mapper)
        {
            _forecastService = forecastService;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<ForecastStockResponse> Handle(ForecastStockQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Request DTO oluştur
            var forecastRequest = new StockForecastRequestDto
            {
                PartId = request.PartId,
                ForecastPeriodDays = request.ForecastPeriodDays,
                HistoricalDataDays = request.HistoricalDataDays,
                IncludeMinimumStockRecommendation = request.IncludeMinimumStockRecommendation,
                IncludeOrderRecommendation = request.IncludeOrderRecommendation
            };

            // Tahmin yap
            var forecastResponse = await _forecastService.ForecastStockAsync(forecastRequest, cancellationToken);

            // Response mapping
            var forecasts = forecastResponse.Forecasts.Select(f => new PartStockForecastDto
            {
                PartId = f.PartId,
                PartName = f.PartName,
                PartCode = f.PartCode,
                CurrentStock = f.CurrentStock,
                MinimumStockLevel = f.MinimumStockLevel,
                PredictedConsumption = f.PredictedConsumption,
                PredictedEndStock = f.PredictedEndStock,
                StockoutRisk = f.StockoutRisk,
                PredictedStockoutDate = f.PredictedStockoutDate,
                RecommendedMinimumStock = f.RecommendedMinimumStock,
                RecommendedOrderQuantity = f.RecommendedOrderQuantity,
                Trend = f.Trend,
                ConfidenceScore = f.ConfidenceScore,
                Explanation = f.Explanation,
                AverageDailyConsumption = f.AverageDailyConsumption
            }).ToList();

            return new ForecastStockResponse
            {
                Forecasts = forecasts,
                Summary = forecastResponse.Summary,
                AnalysisDate = forecastResponse.AnalysisDate
            };
        }
    }
}
