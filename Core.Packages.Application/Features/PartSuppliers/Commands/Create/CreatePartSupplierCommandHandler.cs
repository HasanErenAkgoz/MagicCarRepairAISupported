using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Commands.Create
{
    public class CreatePartSupplierCommandHandler : IRequestHandler<CreatePartSupplierCommand, CreatePartSupplierResponse>
    {
        private readonly IPartSupplierRepository _partSupplierRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public CreatePartSupplierCommandHandler(
            IPartSupplierRepository partSupplierRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _partSupplierRepository = partSupplierRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<CreatePartSupplierResponse> Handle(CreatePartSupplierCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_NOT_FOUND");

            // Business Rule: CompanyName benzersiz olmalı (aynı client içinde)
            var existingSupplier = await _partSupplierRepository.GetByCompanyNameAsync(request.CompanyName, cancellationToken);
            if (existingSupplier != null)
            {
                throw new DomainException("SUPPLIER_COMPANY_NAME_EXISTS", new { CompanyName = request.CompanyName });
            }

            // PartSupplier entity oluştur
            var supplier = new PartSupplier
            {
                CompanyName = request.CompanyName,
                ContactPerson = request.ContactPerson,
                Phone = request.Phone,
                Email = request.Email,
                Address = request.Address,
                City = request.City,
                Country = request.Country,
                TaxNumber = request.TaxNumber,
                TaxOffice = request.TaxOffice,
                PaymentTerms = request.PaymentTerms,
                Notes = request.Notes,
                IsActive = request.IsActive,
                ClientId = clientId,
                Status = Status.Active
            };

            // PartSupplier'ı kaydet
            await _partSupplierRepository.AddAsync(supplier, cancellationToken);
            await _partSupplierRepository.SaveChangesAsync();

            // Response
            return _mapper.Map<CreatePartSupplierResponse>(supplier);
        }
    }
}

