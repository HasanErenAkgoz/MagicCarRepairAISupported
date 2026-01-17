using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Domain.Exceptions;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.AnalyzeEmployeePerformance
{
    public class AnalyzeEmployeePerformanceQueryHandler : IRequestHandler<AnalyzeEmployeePerformanceQuery, AnalyzeEmployeePerformanceResponse>
    {
        private readonly IEmployeePerformanceAnalysisService _analysisService;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public AnalyzeEmployeePerformanceQueryHandler(
            IEmployeePerformanceAnalysisService analysisService,
            ITenantService tenantService,
            IMapper mapper)
        {
            _analysisService = analysisService;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<AnalyzeEmployeePerformanceResponse> Handle(AnalyzeEmployeePerformanceQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Request DTO oluştur
            var analysisRequest = new EmployeePerformanceAnalysisRequestDto
            {
                EmployeeId = request.EmployeeId,
                AnalysisPeriodDays = request.AnalysisPeriodDays,
                IncludeDetailedAnalysis = request.IncludeDetailedAnalysis,
                IncludeRecommendations = request.IncludeRecommendations
            };

            // Analiz yap
            var analysisResponse = await _analysisService.AnalyzeEmployeePerformanceAsync(analysisRequest, cancellationToken);

            // Response mapping
            var analyses = analysisResponse.Analyses.Select(a => new EmployeePerformanceAnalysisDto
            {
                EmployeeId = a.EmployeeId,
                EmployeeName = a.EmployeeName,
                Position = a.Position,
                PerformanceLevel = a.PerformanceLevel,
                PerformanceScore = a.PerformanceScore,
                TotalAssignedWorkOrders = a.TotalAssignedWorkOrders,
                CompletedWorkOrders = a.CompletedWorkOrders,
                TotalWorkHours = a.TotalWorkHours,
                AverageCompletionDays = a.AverageCompletionDays,
                TotalRevenueContribution = a.TotalRevenueContribution,
                AverageCustomerSatisfaction = a.AverageCustomerSatisfaction,
                OnTimeDeliveryRate = a.OnTimeDeliveryRate,
                EfficiencyScore = a.EfficiencyScore,
                Trend = a.Trend,
                Strengths = a.Strengths,
                ImprovementAreas = a.ImprovementAreas,
                Recommendations = a.Recommendations,
                Explanation = a.Explanation,
                ConfidenceScore = a.ConfidenceScore
            }).ToList();

            return new AnalyzeEmployeePerformanceResponse
            {
                Analyses = analyses,
                Summary = analysisResponse.Summary,
                AnalysisDate = analysisResponse.AnalysisDate
            };
        }
    }
}
