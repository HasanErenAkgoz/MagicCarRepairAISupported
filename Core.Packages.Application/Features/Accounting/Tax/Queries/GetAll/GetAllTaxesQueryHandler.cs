using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Tax.Queries.GetAll
{
    public class GetAllTaxesQueryHandler : IRequestHandler<GetAllTaxesQuery, GetAllTaxesResponse>
    {
        private readonly ITaxRepository _taxRepository;
        private readonly ITenantService _tenantService;

        public GetAllTaxesQueryHandler(
            ITaxRepository taxRepository,
            ITenantService tenantService)
        {
            _taxRepository = taxRepository;
            _tenantService = tenantService;
        }

        public async Task<GetAllTaxesResponse> Handle(GetAllTaxesQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var query = _taxRepository.Query()
                .Where(t => t.ClientId == clientId);

            // Filtreleme
            if (request.TaxType.HasValue)
            {
                query = query.Where(t => t.TaxType == request.TaxType.Value);
            }

            if (request.Status.HasValue)
            {
                query = query.Where(t => t.Status == request.Status.Value);
            }

            if (request.Year.HasValue)
            {
                query = query.Where(t => t.Year == request.Year.Value);
            }

            if (request.Month.HasValue)
            {
                query = query.Where(t => t.Month == request.Month.Value);
            }

            if (request.OverdueOnly == true)
            {
                var now = DateTime.UtcNow;
                query = query.Where(t => t.Status == TaxStatus.Pending && t.DueDate < now);
            }

            // Toplam sayı
            var totalCount = await query.CountAsync(cancellationToken);

            // Sayfalama
            var items = await query
                .OrderByDescending(t => t.Year)
                .ThenByDescending(t => t.Month)
                .ThenBy(t => t.DueDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(t => new TaxItem
                {
                    Id = t.Id,
                    TaxTypeName = t.TaxType.ToString(),
                    Month = t.Month,
                    Year = t.Year,
                    Amount = t.Amount,
                    DueDate = t.DueDate,
                    StatusName = t.Status.ToString(),
                    PaymentDate = t.PaymentDate,
                    IsOverdue = t.Status == TaxStatus.Pending && t.DueDate < DateTime.UtcNow
                })
                .ToListAsync(cancellationToken);

            return new GetAllTaxesResponse
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
