using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetWeeklyRevenue
{
    public class GetWeeklyRevenueQueryHandler : IRequestHandler<GetWeeklyRevenueQuery, IDataResult<GetWeeklyRevenueResponse>>
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetWeeklyRevenueQueryHandler(
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            UserManager<UserEntity> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IDataResult<GetWeeklyRevenueResponse>> Handle(GetWeeklyRevenueQuery request, CancellationToken cancellationToken)
        {
            // Authorization kontrolü
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userIdInt))
            {
                return new ErrorDataResult<GetWeeklyRevenueResponse>("Unauthorized access");
            }

            var user = await _userManager.FindByIdAsync(userIdInt.ToString());
            if (user == null)
            {
                return new ErrorDataResult<GetWeeklyRevenueResponse>("User not found");
            }

            // Sadece SystemAdmin ve Manager erişebilir
            if (user.UserType != UserType.SystemAdmin && user.UserType != UserType.Manager)
            {
                return new ErrorDataResult<GetWeeklyRevenueResponse>("Unauthorized access. Only SystemAdmin and Manager can access this endpoint.");
            }

            int? clientId = null;
            
            // SystemAdmin tüm verileri görebilir (clientId = null), Manager sadece kendi client'ını
            if (user.UserType == UserType.Manager)
            {
                clientId = _tenantService.GetCurrentClientId();
                if (clientId == null)
                {
                    return new ErrorDataResult<GetWeeklyRevenueResponse>("ClientId is required for Manager users");
                }
            }
            // SystemAdmin için clientId null kalır (tüm verileri görebilir)

            // Varsayılan: Son 7 gün
            var endDate = request.EndDate?.Date ?? DateTime.UtcNow.Date;
            var startDate = request.StartDate?.Date ?? endDate.AddDays(-7);

            // Tarih aralığı kontrolü
            if (startDate > endDate)
            {
                return new ErrorDataResult<GetWeeklyRevenueResponse>("StartDate cannot be greater than EndDate");
            }

            // Bu haftanın verileri
            var thisWeekData = await GetDailyRevenueData(startDate, endDate, clientId, cancellationToken);
            var weeklyTotal = thisWeekData.Sum(d => d.Revenue);

            // Önceki haftanın verileri (karşılaştırma için)
            var previousWeekStart = startDate.AddDays(-7);
            var previousWeekEnd = startDate.AddDays(-1);
            var previousWeekData = await GetDailyRevenueData(previousWeekStart, previousWeekEnd, clientId, cancellationToken);
            var previousWeekTotal = previousWeekData.Sum(d => d.Revenue);

            // Değişim yüzdesi
            var changePercent = previousWeekTotal > 0
                ? ((weeklyTotal - previousWeekTotal) / previousWeekTotal) * 100
                : 0;

            // Eksik günleri doldur (0 revenue ile)
            var allDays = Enumerable.Range(0, (int)(endDate - startDate).TotalDays + 1)
                .Select(offset => startDate.AddDays(offset))
                .ToList();

            var filledData = allDays.Select(date =>
            {
                var dateString = date.ToString("yyyy-MM-dd");
                var existing = thisWeekData.FirstOrDefault(d => d.Date == dateString);
                if (existing != null)
                {
                    return existing;
                }

                // Gün isimlerini İngilizce olarak al
                var dayOfWeek = date.DayOfWeek.ToString();
                var dayShort = date.ToString("ddd", CultureInfo.InvariantCulture);

                return new DailyRevenueData
                {
                    Date = dateString, // ISO 8601 format
                    DayOfWeek = dayOfWeek,
                    DayShort = dayShort,
                    Revenue = 0
                };
            }).ToList();

            var response = new GetWeeklyRevenueResponse
            {
                WeeklyTotal = Math.Round(weeklyTotal, 2),
                PreviousWeekTotal = Math.Round(previousWeekTotal, 2),
                ChangePercent = Math.Round(changePercent, 1),
                DailyData = filledData
            };

            return new SuccessDataResult<GetWeeklyRevenueResponse>(response);
        }

        private async Task<List<DailyRevenueData>> GetDailyRevenueData(
            DateTime startDate,
            DateTime endDate,
            int? clientId,
            CancellationToken cancellationToken)
        {
            var query = _workOrderRepository.Query()
                .AsNoTracking()
                .Where(wo => wo.Status == WorkOrderStatus.Delivered &&
                           wo.PaymentStatus == PaymentStatus.Paid &&
                           wo.ActualDeliveryDate.HasValue &&
                           wo.ActualDeliveryDate.Value.Date >= startDate &&
                           wo.ActualDeliveryDate.Value.Date <= endDate);

            // ClientId filtreleme
            if (clientId.HasValue)
            {
                query = query.Where(wo => wo.ClientId == clientId.Value);
            }

            var dailyData = await query
                .GroupBy(wo => wo.ActualDeliveryDate!.Value.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Revenue = g.Sum(wo => wo.TotalAmount)
                })
                .ToListAsync(cancellationToken);

            var result = dailyData.Select(d =>
            {
                var dayOfWeek = d.Date.DayOfWeek.ToString();
                var dayShort = d.Date.ToString("ddd", CultureInfo.InvariantCulture);

                return new DailyRevenueData
                {
                    Date = d.Date.ToString("yyyy-MM-dd"), // ISO 8601 format
                    DayOfWeek = dayOfWeek,
                    DayShort = dayShort,
                    Revenue = d.Revenue
                };
            }).ToList();

            return result;
        }
    }
}
