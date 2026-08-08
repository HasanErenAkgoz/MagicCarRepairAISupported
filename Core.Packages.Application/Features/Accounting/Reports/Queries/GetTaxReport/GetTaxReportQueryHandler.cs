using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Reports.Queries.GetTaxReport
{
    public class GetTaxReportQueryHandler : IRequestHandler<GetTaxReportQuery, GetTaxReportResponse>
    {
        private readonly ITaxRepository _taxRepository;
        private readonly ITenantService _tenantService;

        public GetTaxReportQueryHandler(
            ITaxRepository taxRepository,
            ITenantService tenantService)
        {
            _taxRepository = taxRepository;
            _tenantService = tenantService;
        }

        public async Task<GetTaxReportResponse> Handle(GetTaxReportQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            var query = _taxRepository.Query()
                .Where(t => t.ClientId == clientId);

            // Filtreleme
            if (request.Year.HasValue)
            {
                query = query.Where(t => t.Year == request.Year.Value);
            }

            if (request.Month.HasValue)
            {
                query = query.Where(t => t.Month == request.Month.Value);
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(t => t.DueDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(t => t.DueDate <= request.EndDate.Value);
            }

            var taxes = await query.ToListAsync(cancellationToken);

            var totalTaxAmount = taxes.Sum(t => t.Amount);
            var totalPaidAmount = taxes.Where(t => t.Status == TaxStatus.Paid).Sum(t => t.Amount);
            var totalUnpaidAmount = taxes.Where(t => t.Status != TaxStatus.Paid).Sum(t => t.Amount);
            var overdueTaxes = taxes.Where(t => t.Status != TaxStatus.Paid && t.DueDate < DateTime.Today);
            var totalOverdueAmount = overdueTaxes.Sum(t => t.Amount);

            // Vergi türüne göre özet
            var taxTypeSummaries = taxes
                .GroupBy(t => t.TaxType)
                .Select(g => new TaxTypeSummary
                {
                    TaxType = g.Key,
                    TaxTypeName = g.Key.ToString(),
                    TotalAmount = g.Sum(t => t.Amount),
                    PaidAmount = g.Where(t => t.Status == TaxStatus.Paid).Sum(t => t.Amount),
                    UnpaidAmount = g.Where(t => t.Status != TaxStatus.Paid).Sum(t => t.Amount),
                    Count = g.Count()
                })
                .ToList();

            // Vergi detayları
            var taxItems = taxes.Select(t => new TaxItem
            {
                Id = t.Id,
                TaxType = t.TaxType,
                TaxTypeName = t.TaxType.ToString(),
                Month = t.Month,
                Year = t.Year,
                Amount = t.Amount,
                DueDate = t.DueDate,
                Status = t.Status,
                StatusName = t.Status.ToString(),
                PaymentDate = t.PaymentDate,
                TaxOffice = t.TaxOffice,
                TaxNumber = t.TaxNumber,
                IsOverdue = t.Status != TaxStatus.Paid && t.DueDate < DateTime.Today,
                DaysUntilDue = t.Status != TaxStatus.Paid ? (t.DueDate - DateTime.Today).Days : 0
            })
            .OrderBy(t => t.DueDate)
            .ToList();

            return new GetTaxReportResponse
            {
                Year = request.Year,
                Month = request.Month,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalTaxAmount = totalTaxAmount,
                TotalPaidAmount = totalPaidAmount,
                TotalUnpaidAmount = totalUnpaidAmount,
                TotalOverdueAmount = totalOverdueAmount,
                TotalTaxCount = taxes.Count,
                PaidTaxCount = taxes.Count(t => t.Status == TaxStatus.Paid),
                UnpaidTaxCount = taxes.Count(t => t.Status != TaxStatus.Paid),
                OverdueTaxCount = overdueTaxes.Count(),
                TaxTypeSummaries = taxTypeSummaries,
                TaxItems = taxItems
            };
        }
    }
}
