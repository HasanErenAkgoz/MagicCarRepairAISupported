using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services.Cache;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Parts.Commands.UpdatePart
{
    public class UpdatePartCommandHandler : IRequestHandler<UpdatePartCommand, UpdatePartResponse>
    {
        private readonly IPartRepository _partRepository;
        private readonly IPartSupplierRepository _partSupplierRepository;
        private readonly IMapper _mapper;
        private readonly ICacheInvalidationService _cacheInvalidationService;

        public UpdatePartCommandHandler(
            IPartRepository partRepository,
            IPartSupplierRepository partSupplierRepository,
            IMapper mapper,
            ICacheInvalidationService cacheInvalidationService)
        {
            _partRepository = partRepository;
            _partSupplierRepository = partSupplierRepository;
            _mapper = mapper;
            _cacheInvalidationService = cacheInvalidationService;
        }

        public async Task<UpdatePartResponse> Handle(UpdatePartCommand request, CancellationToken cancellationToken)
        {
            // Part'ı bul
            var part = await _partRepository.GetByIdAsync(request.Id);
            if (part == null)
            {
                throw new DomainException("PART_NOT_FOUND", new { PartId = request.Id });
            }

            // Business Rule: PartCode benzersiz olmalı (mevcut part hariç)
            if (part.PartCode != request.PartCode)
            {
                var existingPart = await _partRepository.GetByPartCodeAsync(request.PartCode, cancellationToken);
                if (existingPart != null)
                {
                    throw new DomainException("PART_CODE_EXISTS", new { PartCode = request.PartCode });
                }
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

            // Part bilgilerini güncelle
            part.PartCode = request.PartCode;
            part.Name = request.Name;
            part.Description = request.Description;
            part.Category = request.Category;
            part.BrandType = request.BrandType;
            part.Brand = request.Brand;
            part.OEMNumber = request.OEMNumber;
            part.Barcode = request.Barcode;
            part.PurchasePrice = request.PurchasePrice;
            part.SalePrice = request.SalePrice;
            part.TaxRate = request.TaxRate;
            part.MinimumStockLevel = request.MinimumStockLevel;
            part.IsLowStockAlertEnabled = request.IsLowStockAlertEnabled;
            part.SupplierId = request.SupplierId;
            part.Unit = request.Unit;
            part.WarrantyMonths = request.WarrantyMonths;
            part.Notes = request.Notes;

            // Güncelle
            _partRepository.Update(part);
            await _partRepository.SaveChangesAsync();

            // Cache invalidation
            await _cacheInvalidationService.InvalidatePartCacheAsync(part.Id);
            await _cacheInvalidationService.InvalidateDashboardCacheAsync();

            // Response
            var response = _mapper.Map<UpdatePartResponse>(part);
            if (part.Stock != null)
            {
                response.StockQuantity = part.Stock.Quantity;
                response.StockLocation = part.Stock.Location;
                response.IsLowStock = part.IsLowStock();
            }
            response.SupplierName = part.Supplier?.CompanyName;

            return response;
        }
    }
}

