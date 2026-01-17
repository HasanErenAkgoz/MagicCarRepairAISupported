using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.Barcode
{
    public class GetPartByBarcodeQueryHandler : IRequestHandler<GetPartByBarcodeQuery, GetPartByBarcodeResponse>
    {
        private readonly IPartRepository _partRepository;
        private readonly IPartStockRepository _partStockRepository;
        private readonly ITenantService _tenantService;

        public GetPartByBarcodeQueryHandler(
            IPartRepository partRepository,
            IPartStockRepository partStockRepository,
            ITenantService tenantService)
        {
            _partRepository = partRepository;
            _partStockRepository = partStockRepository;
            _tenantService = tenantService;
        }

        public async Task<GetPartByBarcodeResponse> Handle(GetPartByBarcodeQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            if (string.IsNullOrWhiteSpace(request.Barcode))
            {
                throw new DomainException("BARCODE_REQUIRED");
            }

            // Barcode ile parça bul
            var part = await _partRepository.Query()
                .Where(p => p.ClientId == clientId && p.Barcode == request.Barcode)
                .FirstOrDefaultAsync(cancellationToken);

            if (part == null)
            {
                throw new DomainException("PART_NOT_FOUND_BY_BARCODE", new { Barcode = request.Barcode });
            }

            // Stok bilgisi
            var stock = await _partStockRepository.GetByPartIdAsync(part.Id, cancellationToken);

            return new GetPartByBarcodeResponse
            {
                Id = part.Id,
                PartCode = part.PartCode,
                Name = part.Name,
                Description = part.Description,
                Category = part.Category,
                BrandType = part.BrandType,
                Brand = part.Brand,
                OEMNumber = part.OEMNumber,
                Barcode = part.Barcode,
                PurchasePrice = part.PurchasePrice,
                SalePrice = part.SalePrice,
                TaxRate = part.TaxRate,
                StockQuantity = stock?.Quantity,
                StockLocation = stock?.Location,
                Unit = part.Unit
            };
        }
    }
}
