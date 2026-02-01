using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetTodayRevenue
{
    public class GetTodayRevenueQueryHandler : IRequestHandler<GetTodayRevenueQuery, GetTodayRevenueResponse>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ITenantService _tenantService;

        public GetTodayRevenueQueryHandler(
            IIncomeRepository incomeRepository,
            ITenantService tenantService)
        {
            _incomeRepository = incomeRepository;
            _tenantService = tenantService;
        }

        public async Task<GetTodayRevenueResponse> Handle(GetTodayRevenueQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;
            
            var today = DateTime.UtcNow.Date;
            var tomorrow = today.AddDays(1);
            var yesterday = today.AddDays(-1);
            var dayBeforeYesterday = yesterday;

            // Bugünün geliri
            var todayRevenue = await _incomeRepository.Query()
                .AsNoTracking()
                .Where(i => i.ClientId == clientId && 
                           i.TransactionDate >= today && 
                           i.TransactionDate < tomorrow)
                .SumAsync(i => (decimal?)i.Amount, cancellationToken) ?? 0;

            // Dünün geliri
            var yesterdayRevenue = await _incomeRepository.Query()
                .AsNoTracking()
                .Where(i => i.ClientId == clientId && 
                           i.TransactionDate >= yesterday && 
                           i.TransactionDate < today)
                .SumAsync(i => (decimal?)i.Amount, cancellationToken) ?? 0;

            // Değişim yüzdesi hesapla
            decimal changePercent = 0;
            if (yesterdayRevenue > 0)
            {
                changePercent = ((todayRevenue - yesterdayRevenue) / yesterdayRevenue) * 100;
            }
            else if (todayRevenue > 0)
            {
                changePercent = 100; // Dün 0, bugün var -> %100 artış
            }

            return new GetTodayRevenueResponse
            {
                TodayRevenue = todayRevenue,
                YesterdayRevenue = yesterdayRevenue,
                ChangePercent = changePercent
            };
        }
    }
}
