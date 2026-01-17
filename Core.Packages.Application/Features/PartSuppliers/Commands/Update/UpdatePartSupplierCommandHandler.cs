using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Update
{
    public class UpdatePartSupplierCommandHandler : IRequestHandler<UpdatePartSupplierCommand, UpdatePartSupplierResponse>
    {
        private readonly IPartSupplierRepository _partSupplierRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public UpdatePartSupplierCommandHandler(
            IPartSupplierRepository partSupplierRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _partSupplierRepository = partSupplierRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<UpdatePartSupplierResponse> Handle(UpdatePartSupplierCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_NOT_FOUND");

            // PartSupplier'ı bul
            var supplier = await _partSupplierRepository.GetByIdAsync(request.Id);
            if (supplier == null)
            {
                throw new DomainException("SUPPLIER_NOT_FOUND", new { SupplierId = request.Id });
            }

            // Business Rule: CompanyName benzersiz olmalı (mevcut supplier hariç)
            if (supplier.CompanyName != request.CompanyName)
            {
                var existingSupplier = await _partSupplierRepository.GetByCompanyNameAsync(request.CompanyName, cancellationToken);
                if (existingSupplier != null)
                {
                    throw new DomainException("SUPPLIER_COMPANY_NAME_EXISTS", new { CompanyName = request.CompanyName });
                }
            }

            // PartSupplier bilgilerini güncelle
            supplier.CompanyName = request.CompanyName;
            supplier.ContactPerson = request.ContactPerson;
            supplier.Phone = request.Phone;
            supplier.Email = request.Email;
            supplier.Address = request.Address;
            supplier.City = request.City;
            supplier.Country = request.Country;
            supplier.TaxNumber = request.TaxNumber;
            supplier.TaxOffice = request.TaxOffice;
            supplier.PaymentTerms = request.PaymentTerms;
            supplier.Notes = request.Notes;
            supplier.IsActive = request.IsActive;

            // Güncelle
            _partSupplierRepository.Update(supplier);
            await _partSupplierRepository.SaveChangesAsync();

            // Response
            return _mapper.Map<UpdatePartSupplierResponse>(supplier);
        }
    }
}

