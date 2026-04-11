using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Infrastructure.Startup.HostedServices
{
    /// <summary>
    /// Her gün çalışır. SubscriptionEndDate'i geçmiş ve hâlâ aktif olan
    /// client'ları otomatik olarak pasife çeker; manager'lara bildirim gönderir.
    /// </summary>
    public class SubscriptionExpirationHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SubscriptionExpirationHostedService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromHours(24);

        public SubscriptionExpirationHostedService(
            IServiceProvider serviceProvider,
            ILogger<SubscriptionExpirationHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("SubscriptionExpirationHostedService başlatıldı.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckAndExpireSubscriptionsAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "SubscriptionExpirationHostedService genel hata.");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private async Task CheckAndExpireSubscriptionsAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var clientRepository    = scope.ServiceProvider.GetRequiredService<IClientRepository>();
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
            var emailService        = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var userManager         = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();

            var now = DateTime.UtcNow;

            // IsActive=true ve SubscriptionEndDate geçmiş olan tüm client'ları bul
            var expiredClients = (await clientRepository.GetListAsync(
                cancellationToken,
                c => c.IsActive &&
                     c.SubscriptionEndDate.HasValue &&
                     c.SubscriptionEndDate.Value < now))
                .ToList();

            if (expiredClients.Count == 0)
            {
                _logger.LogDebug("Süresi dolan abonelik bulunamadı.");
                return;
            }

            _logger.LogInformation("{Count} adet süresi dolan abonelik bulundu.", expiredClients.Count);

            foreach (var client in expiredClients)
            {
                try
                {
                    // Client'ı pasife çek
                    client.IsActive = false;
                    clientRepository.Update(client);
                    await clientRepository.SaveChangesAsync();

                    _logger.LogInformation(
                        "Client pasife çekildi. Id={ClientId}, Name={ClientName}, EndDate={EndDate}",
                        client.Id, client.Name, client.SubscriptionEndDate);

                    // Manager kullanıcıları bul ve bildirim gönder
                    var managers = userManager.Users
                        .Where(u => u.ClientId == client.Id && u.UserType == UserType.Manager)
                        .ToList();

                    foreach (var manager in managers)
                    {
                        await SendExpirationNotificationsAsync(
                            manager, client.Name, client.SubscriptionEndDate!.Value,
                            notificationService, emailService, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Client pasife çekilirken hata. ClientId={ClientId}", client.Id);
                }
            }
        }

        private async Task SendExpirationNotificationsAsync(
            UserEntity manager,
            string shopName,
            DateTime endDate,
            INotificationService notificationService,
            IEmailService emailService,
            CancellationToken cancellationToken)
        {
            const string title = "Abonelik Süreniz Sona Erdi";
            var content = $"'{shopName}' tamirhane hesabınızın abonelik süresi {endDate:dd.MM.yyyy} tarihinde doldu. Hesabınız otomatik olarak askıya alındı.";

            // 1. Push Bildirimi
            try
            {
                await notificationService.SendPushNotificationAsync(
                    userId: manager.Id,
                    title: title,
                    content: content,
                    relatedEntityType: "Client",
                    relatedEntityId: manager.ClientId,
                    extraData: new Dictionary<string, object>
                    {
                        { "type", "subscription_expired" },
                        { "shopName", shopName },
                        { "endDate", endDate.ToString("yyyy-MM-dd") }
                    });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Push bildirimi gönderilemedi. UserId={UserId}", manager.Id);
            }

            // 2. E-posta Bildirimi
            if (!string.IsNullOrEmpty(manager.Email))
            {
                try
                {
                    var emailBody = BuildExpirationEmailBody(manager.FirstName, shopName, endDate);
                    await emailService.SendEmailAsync(
                        to: manager.Email,
                        subject: title,
                        body: emailBody);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "E-posta gönderilemedi. Email={Email}", manager.Email);
                }
            }

            // 3. SMS Bildirimi (varsa)
            if (!string.IsNullOrEmpty(manager.PhoneNumber))
            {
                try
                {
                    await notificationService.SendNotificationAsync(
                        type: NotificationType.Sms,
                        userId: manager.Id,
                        email: null,
                        phoneNumber: manager.PhoneNumber,
                        title: title,
                        content: $"'{shopName}' aboneliğiniz {endDate:dd.MM.yyyy} tarihinde doldu. Hesabınız askıya alındı.",
                        relatedEntityType: "Client",
                        relatedEntityId: manager.ClientId);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "SMS gönderilemedi. UserId={UserId}", manager.Id);
                }
            }
        }

        private static string BuildExpirationEmailBody(string firstName, string shopName, DateTime endDate)
        {
            return $"""
                <!DOCTYPE html>
                <html lang="tr">
                <head>
                  <meta charset="UTF-8" />
                  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
                  <title>Abonelik Süresi Doldu</title>
                </head>
                <body style="margin:0;padding:0;background-color:#f4f4f4;font-family:Arial,sans-serif;">
                  <table width="100%" cellpadding="0" cellspacing="0" style="background-color:#f4f4f4;padding:40px 0;">
                    <tr>
                      <td align="center">
                        <table width="600" cellpadding="0" cellspacing="0" style="background-color:#ffffff;border-radius:12px;overflow:hidden;box-shadow:0 4px 20px rgba(0,0,0,0.08);">

                          <!-- Header -->
                          <tr>
                            <td style="background:linear-gradient(135deg,#f59e0b,#d97706);padding:40px 40px 30px;text-align:center;">
                              <div style="font-size:48px;margin-bottom:12px;">⏰</div>
                              <h1 style="color:#ffffff;margin:0;font-size:26px;font-weight:700;">Abonelik Süreniz Doldu</h1>
                              <p style="color:rgba(255,255,255,0.85);margin:10px 0 0;font-size:15px;">Magic Car Repair — Abonelik Bildirimi</p>
                            </td>
                          </tr>

                          <!-- Body -->
                          <tr>
                            <td style="padding:40px;">
                              <p style="color:#333;font-size:16px;margin:0 0 20px;">Merhaba <strong>{firstName}</strong>,</p>
                              <p style="color:#555;font-size:15px;line-height:1.6;margin:0 0 24px;">
                                <strong>'{shopName}'</strong> tamirhane hesabınızın abonelik süresi
                                <strong>{endDate:dd.MM.yyyy}</strong> tarihinde sona ermiştir.
                                Hesabınız otomatik olarak askıya alınmıştır.
                              </p>

                              <table width="100%" cellpadding="0" cellspacing="0" style="background:#fffbeb;border-radius:8px;padding:20px;margin-bottom:30px;border-left:4px solid #f59e0b;">
                                <tr><td style="color:#d97706;font-size:14px;font-weight:600;margin-bottom:8px;">Etkilenen işlemler:</td></tr>
                                <tr><td style="padding:6px 0;color:#555;font-size:14px;">❌ &nbsp; Sisteme giriş yapılamaz</td></tr>
                                <tr><td style="padding:6px 0;color:#555;font-size:14px;">❌ &nbsp; Yeni iş emri oluşturulamaz</td></tr>
                                <tr><td style="padding:6px 0;color:#555;font-size:14px;">❌ &nbsp; Müşteri işlemleri gerçekleştirilemez</td></tr>
                              </table>

                              <p style="color:#555;font-size:15px;line-height:1.6;margin:0 0 30px;">
                                Aboneliğinizi yenilemek için sistem yöneticinizle iletişime geçin.
                              </p>

                              <p style="color:#888;font-size:13px;text-align:center;margin:0;">
                                Herhangi bir sorunuz için destek ekibimizle iletişime geçebilirsiniz.
                              </p>
                            </td>
                          </tr>

                          <!-- Footer -->
                          <tr>
                            <td style="background:#f8f9ff;padding:20px 40px;text-align:center;border-top:1px solid #e8e8e8;">
                              <p style="color:#aaa;font-size:12px;margin:0;">
                                © 2026 Magic Car Repair · Tüm hakları saklıdır
                              </p>
                            </td>
                          </tr>

                        </table>
                      </td>
                    </tr>
                  </table>
                </body>
                </html>
                """;
        }
    }
}
