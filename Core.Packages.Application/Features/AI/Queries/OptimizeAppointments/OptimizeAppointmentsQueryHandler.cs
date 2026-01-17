using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Domain.Exceptions;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.OptimizeAppointments
{
    public class OptimizeAppointmentsQueryHandler : IRequestHandler<OptimizeAppointmentsQuery, OptimizeAppointmentsResponse>
    {
        private readonly IAppointmentOptimizationService _optimizationService;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public OptimizeAppointmentsQueryHandler(
            IAppointmentOptimizationService optimizationService,
            ITenantService tenantService,
            IMapper mapper)
        {
            _optimizationService = optimizationService;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<OptimizeAppointmentsResponse> Handle(OptimizeAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Request DTO oluştur
            var optimizationRequest = new AppointmentOptimizationRequestDto
            {
                CustomerId = request.CustomerId,
                VehicleId = request.VehicleId,
                AppointmentType = request.AppointmentType.ToString(),
                PreferredStartDate = request.PreferredStartDate,
                PreferredEndDate = request.PreferredEndDate,
                PreferredStartTime = request.PreferredStartTime,
                PreferredEndTime = request.PreferredEndTime,
                EstimatedDurationMinutes = request.EstimatedDurationMinutes,
                Priority = request.Priority,
                NumberOfSuggestions = request.NumberOfSuggestions
            };

            // Optimizasyon yap
            var optimizationResponse = await _optimizationService.OptimizeAppointmentsAsync(optimizationRequest, cancellationToken);

            // Response mapping
            var suggestions = optimizationResponse.Suggestions.Select(s => new AppointmentSuggestionDto
            {
                SuggestedDate = s.SuggestedDate,
                SuggestedStartTime = s.SuggestedStartTime,
                SuggestedEndTime = s.SuggestedEndTime,
                SuggestedEmployeeId = s.SuggestedEmployeeId,
                SuggestedEmployeeName = s.SuggestedEmployeeName,
                SuitabilityScore = s.SuitabilityScore,
                Reason = s.Reason,
                EmployeeWorkload = s.EmployeeWorkload
            }).ToList();

            return new OptimizeAppointmentsResponse
            {
                Suggestions = suggestions,
                Explanation = optimizationResponse.Explanation
            };
        }
    }
}
