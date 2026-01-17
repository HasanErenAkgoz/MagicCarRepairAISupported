using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Parts.Queries.GetAllParts
{
    public class GetAllPartsQueryHandler : IRequestHandler<GetAllPartsQuery, GetAllPartsResponse>
    {
        private readonly IPartRepository _partRepository;

        public GetAllPartsQueryHandler(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public async Task<GetAllPartsResponse> Handle(GetAllPartsQuery request, CancellationToken cancellationToken)
        {
            // Repository'den IQueryable al ve Include'ları ekle
            var baseQuery = _partRepository.Query();
            var query = baseQuery
                .Include(p => p.Stock)
                .Include(p => p.Supplier)
                .AsNoTracking() // Read-only query, no change tracking needed
                .AsQueryable();

            // Filtreleme
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(p => 
                    p.Name.ToLower().Contains(searchTerm) ||
                    p.PartCode.ToLower().Contains(searchTerm) ||
                    (p.Description != null && p.Description.ToLower().Contains(searchTerm)) ||
                    (p.Barcode != null && p.Barcode.ToLower().Contains(searchTerm)));
            }

            if (request.Category.HasValue)
            {
                query = query.Where(p => p.Category == request.Category.Value);
            }

            if (request.BrandType.HasValue)
            {
                query = query.Where(p => p.BrandType == request.BrandType.Value);
            }

            if (request.LowStockOnly == true)
            {
                query = query.Where(p => p.IsLowStockAlertEnabled && 
                                        p.Stock != null && 
                                        p.Stock.Quantity <= p.MinimumStockLevel);
            }

            // Toplam sayı (filtreleme sonrası, orderBy öncesi - daha performanslı)
            var totalCount = await query.CountAsync(cancellationToken);

            // Sayfalama (optimize: orderBy'dan sonra skip/take)
            var parts = await query
                .OrderBy(p => p.Name)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            // Response
            var response = new GetAllPartsResponse
            {
                Parts = parts.Select(p => new PartDto
                {
                    Id = p.Id,
                    PartCode = p.PartCode,
                    Name = p.Name,
                    Description = p.Description,
                    Category = p.Category,
                    BrandType = p.BrandType,
                    Brand = p.Brand,
                    PurchasePrice = p.PurchasePrice,
                    SalePrice = p.SalePrice,
                    StockQuantity = p.Stock?.Quantity,
                    MinimumStockLevel = p.MinimumStockLevel,
                    IsLowStock = p.IsLowStock(),
                    SupplierName = p.Supplier?.CompanyName
                }).ToList(),
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return response;
        }
    }
}

