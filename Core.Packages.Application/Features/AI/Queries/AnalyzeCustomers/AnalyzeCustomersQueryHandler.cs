using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Domain.Exceptions;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.AnalyzeCustomers
{
    public class AnalyzeCustomersQueryHandler : IRequestHandler<AnalyzeCustomersQuery, AnalyzeCustomersResponse>
    {
        private readonly ICustomerAnalysisService _analysisService;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public AnalyzeCustomersQueryHandler(
            ICustomerAnalysisService analysisService,
            ITenantService tenantService,
            IMapper mapper)
        {
            _analysisService = analysisService;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<AnalyzeCustomersResponse> Handle(AnalyzeCustomersQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Request DTO oluştur
            var analysisRequest = new CustomerAnalysisRequestDto
            {
                CustomerId = request.CustomerId,
                AnalysisPeriodDays = request.AnalysisPeriodDays,
                IncludeDetailedAnalysis = request.IncludeDetailedAnalysis,
                IncludeRecommendations = request.IncludeRecommendations
            };

            // Analiz yap
            var analysisResponse = await _analysisService.AnalyzeCustomersAsync(analysisRequest, cancellationToken);

            // Response mapping
            var analyses = analysisResponse.Analyses.Select(a => new CustomerAnalysisDto
            {
                CustomerId = a.CustomerId,
                CustomerName = a.CustomerName,
                CustomerSegment = a.CustomerSegment,
                CustomerValue = a.CustomerValue,
                TotalWorkOrders = a.TotalWorkOrders,
                TotalRevenue = a.TotalRevenue,
                AverageWorkOrderAmount = a.AverageWorkOrderAmount,
                AverageRating = a.AverageRating,
                TotalVehicles = a.TotalVehicles,
                LastVisitDate = a.LastVisitDate,
                DaysSinceLastVisit = a.DaysSinceLastVisit,
                Trend = a.Trend,
                RiskFactors = a.RiskFactors,
                Recommendations = a.Recommendations,
                PaymentBehavior = a.PaymentBehavior,
                AveragePaymentDelayDays = a.AveragePaymentDelayDays,
                Explanation = a.Explanation,
                ConfidenceScore = a.ConfidenceScore
            }).ToList();

            return new AnalyzeCustomersResponse
            {
                Analyses = analyses,
                Summary = analysisResponse.Summary,
                AnalysisDate = analysisResponse.AnalysisDate
            };
        }
    }
}
