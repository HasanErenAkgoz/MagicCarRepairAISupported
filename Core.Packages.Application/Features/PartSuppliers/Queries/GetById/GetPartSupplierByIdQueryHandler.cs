using AutoMapper;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Queries.GetById
{
    public class GetPartSupplierByIdQueryHandler : IRequestHandler<GetPartSupplierByIdQuery, GetPartSupplierByIdResponse>
    {
        private readonly IPartSupplierRepository _partSupplierRepository;
        private readonly IMapper _mapper;

        public GetPartSupplierByIdQueryHandler(IPartSupplierRepository partSupplierRepository, IMapper mapper)
        {
            _partSupplierRepository = partSupplierRepository;
            _mapper = mapper;
        }

        public async Task<GetPartSupplierByIdResponse> Handle(GetPartSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplier = await _partSupplierRepository.GetByIdAsync(request.Id);
            
            if (supplier == null || supplier.Status == Status.Deleted)
            {
                throw new DomainException("SUPPLIER_NOT_FOUND", new { SupplierId = request.Id });
            }

            return _mapper.Map<GetPartSupplierByIdResponse>(supplier);
        }
    }
}

