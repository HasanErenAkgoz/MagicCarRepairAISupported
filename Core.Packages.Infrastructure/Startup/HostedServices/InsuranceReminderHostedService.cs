using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Startup.HostedServices
{
    /// <summary>
    /// Periyodik sigorta poliçesi hatırlatma servisi
    /// Son 30 gün içinde süresi dolacak poliçeler için müşterilere hatırlatma gönderir
    /// </summary>
    public class InsuranceReminderHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<InsuranceReminderHostedService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromHours(6); // Her 6 saatte bir kontrol et
        private const int DaysBeforeExpiration = 30; // 30 gün önceden hatırlat

        public InsuranceReminderHostedService(
            IServiceProvider serviceProvider,
            ILogger<InsuranceReminderHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("InsuranceReminderHostedService started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var insurancePolicyRepository = scope.ServiceProvider.GetRequiredService<IInsurancePolicyRepository>();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                    // Son 30 gün içinde süresi dolacak aktif poliçeleri bul
                    var expiringPolicies = await insurancePolicyRepository.GetExpiringPoliciesAsync(
                        DaysBeforeExpiration, 
                        stoppingToken);

                    _logger.LogInformation($"Found {expiringPolicies.Count} expiring insurance policies");

                    foreach (var policy in expiringPolicies)
                    {
                        try
                        {
                            // Müşteri bilgisi kontrolü
                            if (policy.Customer == null)
                            {
                                _logger.LogWarning($"Policy {policy.PolicyNumber} has no customer associated");
                                continue;
                            }

                            var daysUntilExpiration = policy.DaysUntilExpiration();
                            var vehicleInfo = policy.Vehicle != null 
                                ? $"{policy.Vehicle.Brand} {policy.Vehicle.Model} ({policy.Vehicle.LicensePlate})"
                                : "Bilinmeyen Araç";

                            // Email gönder
                            if (!string.IsNullOrEmpty(policy.Customer.Email))
                            {
                                var subject = $"Sigorta Poliçesi Hatırlatması - {policy.PolicyNumber}";
                                var message = $@"
                                    Sayın {policy.Customer.FullName},
                                    
                                    Sigorta poliçenizin süresi yakında dolacaktır.
                                    
                                    Poliçe Bilgileri:
                                    - Poliçe No: {policy.PolicyNumber}
                                    - Araç: {vehicleInfo}
                                    - Sigorta Türü: {policy.InsuranceType}
                                    - Sigorta Şirketi: {policy.InsuranceCompany?.CompanyName ?? "N/A"}
                                    - Başlangıç Tarihi: {policy.StartDate:dd.MM.yyyy}
                                    - Bitiş Tarihi: {policy.EndDate:dd.MM.yyyy}
                                    - Kalan Süre: {daysUntilExpiration} gün
                                    
                                    Lütfen poliçenizi yenilemek için bizimle iletişime geçiniz.
                                    
                                    İyi günler dileriz.
                                ";

                                await emailService.SendEmailAsync(policy.Customer.Email, subject, message);
                                _logger.LogInformation($"Insurance reminder email sent to {policy.Customer.Email} for policy {policy.PolicyNumber}");
                            }

                            // Bildirim oluştur (eğer müşterinin kullanıcı hesabı varsa)
                            if (policy.Customer.UserId.HasValue)
                            {
                                await notificationService.SendNotificationAsync(
                                    string.IsNullOrEmpty(policy.Customer.Email)
                                        ? Domain.Enums.NotificationType.Push
                                        : Domain.Enums.NotificationType.Email,
                                    policy.Customer.UserId.Value,
                                    policy.Customer.Email ?? string.Empty,
                                    policy.Customer.PhoneNumber,
                                    "Sigorta Poliçesi Hatırlatması",
                                    $"{vehicleInfo} için sigorta poliçeniz {daysUntilExpiration} gün sonra dolacak. Poliçe No: {policy.PolicyNumber}",
                                    "Insurance",
                                    policy.Id,
                                    null);

                                _logger.LogInformation($"Insurance reminder notification sent to user {policy.Customer.UserId} for policy {policy.PolicyNumber}");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Error sending reminder for insurance policy {policy.PolicyNumber}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in InsuranceReminderHostedService");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("InsuranceReminderHostedService stopped");
        }
    }
}
