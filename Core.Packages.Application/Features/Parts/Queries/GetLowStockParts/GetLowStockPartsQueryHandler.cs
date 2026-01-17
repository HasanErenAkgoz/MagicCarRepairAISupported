using AutoMapper;
using MagicCarRepairAISupported.Application.Features.Parts.Queries.GetAllParts;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.GetLowStockParts
{
    public class GetLowStockPartsQueryHandler : IRequestHandler<GetLowStockPartsQuery, GetLowStockPartsResponse>
    {
        private readonly IPartRepository _partRepository;
        private readonly IMapper _mapper;

        public GetLowStockPartsQueryHandler(IPartRepository partRepository, IMapper mapper)
        {
            _partRepository = partRepository;
            _mapper = mapper;
        }

        public async Task<GetLowStockPartsResponse> Handle(GetLowStockPartsQuery request, CancellationToken cancellationToken)
        {
            var lowStockParts = await _partRepository.GetLowStockPartsAsync(cancellationToken);

            var items = lowStockParts.Select(part => new LowStockPartItem
            {
                Id = part.Id,
                PartCode = part.PartCode,
                Name = part.Name,
                Category = part.Category,
                BrandType = part.BrandType,
                CurrentStock = part.Stock?.Quantity ?? 0,
                MinimumStockLevel = part.MinimumStockLevel,
                Location = part.Stock?.Location,
                SupplierName = part.Supplier?.CompanyName,
                IsLowStockAlertEnabled = part.IsLowStockAlertEnabled
            }).ToList();

            return new GetLowStockPartsResponse
            {
                Items = items,
                TotalCount = items.Count
            };
        }
    }
}

