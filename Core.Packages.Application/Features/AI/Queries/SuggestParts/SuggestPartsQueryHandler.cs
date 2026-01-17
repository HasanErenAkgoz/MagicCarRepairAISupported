using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Domain.Exceptions;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.SuggestParts
{
    public class SuggestPartsQueryHandler : IRequestHandler<SuggestPartsQuery, SuggestPartsResponse>
    {
        private readonly IPartSuggestionService _suggestionService;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public SuggestPartsQueryHandler(
            IPartSuggestionService suggestionService,
            ITenantService tenantService,
            IMapper mapper)
        {
            _suggestionService = suggestionService;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<SuggestPartsResponse> Handle(SuggestPartsQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Request DTO oluştur
            var suggestionRequest = new PartSuggestionRequestDto
            {
                WorkOrderId = request.WorkOrderId,
                VehicleId = request.VehicleId,
                CustomerComplaint = request.CustomerComplaint,
                PartCategory = request.PartCategory,
                NumberOfSuggestions = request.NumberOfSuggestions,
                PreferInStock = request.PreferInStock,
                MinPrice = request.MinPrice,
                MaxPrice = request.MaxPrice
            };

            // Öneri yap
            var suggestionResponse = await _suggestionService.SuggestPartsAsync(suggestionRequest, cancellationToken);

            // Response mapping
            var suggestions = suggestionResponse.Suggestions.Select(s => new PartSuggestionDto
            {
                PartId = s.PartId,
                PartName = s.PartName,
                PartCode = s.PartCode,
                Category = s.Category,
                BrandType = s.BrandType,
                Brand = s.Brand,
                OEMNumber = s.OEMNumber,
                SalePrice = s.SalePrice,
                CurrentStock = s.CurrentStock,
                Reason = s.Reason,
                SuitabilityScore = s.SuitabilityScore,
                Priority = s.Priority
            }).ToList();

            return new SuggestPartsResponse
            {
                Suggestions = suggestions,
                Explanation = suggestionResponse.Explanation,
                AnalysisDate = suggestionResponse.AnalysisDate
            };
        }
    }
}
