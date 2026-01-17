using AutoMapper;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.PartSuppliers.Queries.GetAll
{
    public class GetAllPartSuppliersQueryHandler : IRequestHandler<GetAllPartSuppliersQuery, GetAllPartSuppliersResponse>
    {
        private readonly IPartSupplierRepository _partSupplierRepository;
        private readonly IMapper _mapper;

        public GetAllPartSuppliersQueryHandler(IPartSupplierRepository partSupplierRepository, IMapper mapper)
        {
            _partSupplierRepository = partSupplierRepository;
            _mapper = mapper;
        }

        public async Task<GetAllPartSuppliersResponse> Handle(GetAllPartSuppliersQuery request, CancellationToken cancellationToken)
        {
            // Query oluştur (optimize: AsNoTracking kullan)
            var query = _partSupplierRepository.Query()
                .AsNoTracking(); // Read-only query

            // Arama filtresi
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(s => 
                    s.CompanyName.ToLower().Contains(searchTerm) ||
                    (s.ContactPerson != null && s.ContactPerson.ToLower().Contains(searchTerm)) ||
                    (s.Phone != null && s.Phone.Contains(searchTerm)) ||
                    (s.Email != null && s.Email.ToLower().Contains(searchTerm)));
            }

            // Aktif filtre
            if (request.IsActiveOnly == true)
            {
                query = query.Where(s => s.IsActive && s.Status != Status.Deleted);
            }
            else
            {
                // Soft delete olmayanları getir
                query = query.Where(s => s.Status != Status.Deleted);
            }

            // Toplam sayı (pagination öncesi, orderBy öncesi - daha performanslı)
            var totalCount = await query.CountAsync(cancellationToken);

            // Pagination ve sıralama (optimize: orderBy'dan sonra skip/take)
            var suppliers = await query
                .OrderBy(s => s.CompanyName)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            // Response items
            var items = suppliers.Select(supplier => _mapper.Map<PartSupplierItem>(supplier)).ToList();

            return new GetAllPartSuppliersResponse
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
        }
    }
}

