using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Commission;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CommissionEntity = MagicCarRepairAISupported.Domain.Entities.Commission;
using PaymentEntity = MagicCarRepairAISupported.Domain.Entities.Payment;

namespace MagicCarRepairAISupported.Infrastructure.Services.Commission
{
    /// <summary>
    /// Komisyon servisi implementasyonu
    /// </summary>
    public class CommissionService : ICommissionService
    {
        private readonly IEntityRepository<CommissionEntity, int> _commissionRepository;
        private readonly IEntityRepository<PaymentEntity, int> _paymentRepository;
        private readonly ITenantService _tenantService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CommissionService> _logger;

        public CommissionService(
            IEntityRepository<CommissionEntity, int> commissionRepository,
            IEntityRepository<PaymentEntity, int> paymentRepository,
            ITenantService tenantService,
            IConfiguration configuration,
            ILogger<CommissionService> logger)
        {
            _commissionRepository = commissionRepository;
            _paymentRepository = paymentRepository;
            _tenantService = tenantService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<decimal> CalculateCommissionAsync(decimal amount, int clientId)
        {
            // Varsayılan komisyon oranı %2.5
            var commissionRate = _configuration.GetValue<decimal>("Subscription:CommissionRate", 2.5m);
            var commission = amount * (commissionRate / 100m);
            
            // Decimal precision için yuvarlama
            return Math.Round(commission, 2, MidpointRounding.AwayFromZero);
        }

        public async Task<int> RecordCommissionAsync(int paymentId, decimal commissionAmount, decimal commissionRate)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 0;
                if (clientId == 0)
                {
                    throw new InvalidOperationException("Client ID bulunamadı.");
                }

                var commission = new CommissionEntity
                {
                    PaymentId = paymentId,
                    CommissionAmount = commissionAmount,
                    CommissionRate = commissionRate,
                    Status = CommissionStatus.Pending,
                    ClientId = clientId,
                    Description = $"Payment #{paymentId} için komisyon"
                };

                await _commissionRepository.AddAsync(commission, CancellationToken.None);
                await _commissionRepository.SaveChangesAsync();

                _logger.LogInformation("Commission recorded: PaymentId={PaymentId}, Amount={Amount}, Rate={Rate}%",
                    paymentId, commissionAmount, commissionRate);

                return commission.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error recording commission for payment {PaymentId}", paymentId);
                throw;
            }
        }

        public async Task<decimal> GetTotalCommissionsAsync(int clientId, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var query = _commissionRepository.Query()
                    .Where(c => c.ClientId == clientId && c.Status == CommissionStatus.Paid);

                if (startDate.HasValue)
                {
                    query = query.Where(c => c.PaymentDate >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    query = query.Where(c => c.PaymentDate <= endDate.Value);
                }

                var total = await query.SumAsync(c => c.CommissionAmount);
                return total;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting total commissions for client {ClientId}", clientId);
                return 0;
            }
        }

        public async Task<CommissionReport> GetCommissionReportAsync(int clientId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var commissions = await _commissionRepository.Query()
                    .Include(c => c.Payment)
                    .Where(c => c.ClientId == clientId &&
                               c.PaymentDate >= startDate &&
                               c.PaymentDate <= endDate)
                    .ToListAsync();

                var report = new CommissionReport
                {
                    TotalCommissions = commissions.Sum(c => c.CommissionAmount),
                    PendingCommissions = commissions.Where(c => c.Status == CommissionStatus.Pending).Sum(c => c.CommissionAmount),
                    PaidCommissions = commissions.Where(c => c.Status == CommissionStatus.Paid).Sum(c => c.CommissionAmount),
                    TotalTransactions = commissions.Count,
                    Details = commissions.Select(c => new CommissionDetail
                    {
                        PaymentId = c.PaymentId,
                        PaymentNumber = c.Payment?.PaymentNumber ?? "",
                        PaymentAmount = c.Payment?.Amount ?? 0,
                        CommissionAmount = c.CommissionAmount,
                        CommissionRate = c.CommissionRate,
                        PaymentDate = c.PaymentDate ?? DateTime.UtcNow,
                        Status = c.Status.ToString()
                    }).ToList()
                };

                return report;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting commission report for client {ClientId}", clientId);
                return new CommissionReport();
            }
        }
    }
}
