using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services.Cache;
using MagicCarRepairAISupported.Application.Features.Parts.Utils;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Utils;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.CreatePart
{
    public class CreatePartCommandHandler : IRequestHandler<CreatePartCommand, CreatePartResponse>
    {
        private readonly IPartRepository _partRepository;
        private readonly IPartStockRepository _partStockRepository;
        private readonly IPartSupplierRepository _partSupplierRepository;
        private readonly IMapper _mapper;
        private readonly ICacheInvalidationService _cacheInvalidationService;

        public CreatePartCommandHandler(
            IPartRepository partRepository,
            IPartStockRepository partStockRepository,
            IPartSupplierRepository partSupplierRepository,
            IMapper mapper,
            ICacheInvalidationService cacheInvalidationService)
        {
            _partRepository = partRepository;
            _partStockRepository = partStockRepository;
            _partSupplierRepository = partSupplierRepository;
            _mapper = mapper;
            _cacheInvalidationService = cacheInvalidationService;
        }

        public async Task<CreatePartResponse> Handle(CreatePartCommand request, CancellationToken cancellationToken)
        {
            var partCode = RequiredStringDefaults.ResolveCode(request.PartCode, "PRT", 50);

            var existingPart = await _partRepository.GetByPartCodeAsync(partCode, cancellationToken);
            if (existingPart != null)
            {
                throw new DomainException("PART_CODE_EXISTS", new { PartCode = partCode });
            }

            // Supplier kontrolü
            if (request.SupplierId.HasValue)
            {
                var supplier = await _partSupplierRepository.GetByIdAsync(request.SupplierId.Value);
                if (supplier == null)
                {
                    throw new DomainException("SUPPLIER_NOT_FOUND", new { SupplierId = request.SupplierId.Value });
                }
            }

            // Part entity oluştur
            var part = new Part
            {
                PartCode = partCode,
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                BrandType = request.BrandType,
                Brand = request.Brand,
                OEMNumber = request.OEMNumber,
                Barcode = request.Barcode,
                PurchasePrice = request.PurchasePrice,
                SalePrice = request.SalePrice,
                TaxRate = request.TaxRate,
                MinimumStockLevel = request.MinimumStockLevel,
                IsLowStockAlertEnabled = request.IsLowStockAlertEnabled,
                SupplierId = request.SupplierId,
                Unit = request.Unit,
                WarrantyMonths = request.WarrantyMonths,
                Notes = request.Notes,
                CompatibleVehicleBrands = PartFitmentJson.SerializeStringArray(request.CompatibleVehicleBrands),
                CompatibleVehicleModels = PartFitmentJson.SerializeStringArray(request.CompatibleVehicleModels),
                CompatibleYearFrom = request.CompatibleYearFrom,
                CompatibleYearTo = request.CompatibleYearTo,
                AdditionalOemCodes = PartFitmentJson.SerializeStringArray(request.AdditionalOemCodes),
            };

            // Part'ı kaydet
            await _partRepository.AddAsync(part, cancellationToken);
            await _partRepository.SaveChangesAsync();

            // Stok kaydı oluştur (eğer initial stock varsa)
            if (request.InitialStockQuantity > 0)
            {
                var stock = new PartStock
                {
                    PartId = part.Id,
                    Quantity = request.InitialStockQuantity,
                    Location = request.StockLocation
                };

                await _partStockRepository.AddAsync(stock, cancellationToken);
                await _partStockRepository.SaveChangesAsync();
            }

            // Cache invalidation (parallel)
            await Task.WhenAll(
                _cacheInvalidationService.InvalidatePartCacheAsync(part.Id),
                _cacheInvalidationService.InvalidateDashboardCacheAsync()
            );

            // Response
            var response = _mapper.Map<CreatePartResponse>(part);
            if (request.InitialStockQuantity > 0)
            {
                response.StockQuantity = request.InitialStockQuantity;
            }

            return response;
        }
    }
}

