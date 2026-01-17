using AutoMapper;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.GetPartById
{
    public class GetPartByIdQueryHandler : IRequestHandler<GetPartByIdQuery, GetPartByIdResponse>
    {
        private readonly IPartRepository _partRepository;
        private readonly IMapper _mapper;

        public GetPartByIdQueryHandler(IPartRepository partRepository, IMapper mapper)
        {
            _partRepository = partRepository;
            _mapper = mapper;
        }

        public async Task<GetPartByIdResponse> Handle(GetPartByIdQuery request, CancellationToken cancellationToken)
        {
            var part = await _partRepository.GetWithStockAsync(request.Id, cancellationToken);
            
            if (part == null)
            {
                throw new DomainException("PART_NOT_FOUND", new { PartId = request.Id });
            }

            var response = _mapper.Map<GetPartByIdResponse>(part);
            response.StockQuantity = part.Stock?.Quantity;
            response.StockLocation = part.Stock?.Location;
            response.IsLowStock = part.IsLowStock();
            response.SupplierName = part.Supplier?.CompanyName;

            return response;
        }
    }
}

