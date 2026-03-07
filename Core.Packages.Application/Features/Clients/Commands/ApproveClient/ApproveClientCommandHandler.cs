using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserEntity = MagicCarRepairAISupported.Domain.Entities.User;

namespace MagicCarRepairAISupported.Application.Features.Clients.Commands.ApproveClient
{
    public class ApproveClientCommandHandler : IRequestHandler<ApproveClientCommand, IDataResult<ApproveClientResponse>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        private readonly IEmailService _emailService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<ApproveClientCommandHandler> _logger;

        public ApproveClientCommandHandler(
            IClientRepository clientRepository,
            IUnitOfWork unitOfWork,
            INotificationService notificationService,
            IEmailService emailService,
            UserManager<UserEntity> userManager,
            ILogger<ApproveClientCommandHandler> logger)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
            _emailService = emailService;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IDataResult<ApproveClientResponse>> Handle(ApproveClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetByIdAsync(request.ClientId, cancellationToken);
            if (client == null)
            {
                return new ErrorDataResult<ApproveClientResponse>("Tamirhane bulunamadı.");
            }

            if (client.IsActive)
            {
                return new ErrorDataResult<ApproveClientResponse>("Bu tamirhane zaten onaylanmış.");
            }

            // Client'ı aktif yap
            client.IsActive = true;
            _clientRepository.Update(client);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Tamirhanenin Manager kullanıcılarını bul
            var managerUsers = _userManager.Users
                .Where(u => u.ClientId == client.Id && u.UserType == UserType.Manager)
                .ToList();

            // Her Manager'a bildirim gönder
            foreach (var manager in managerUsers)
            {
                await SendApprovalNotificationsAsync(manager, client.Name, cancellationToken);
            }

            var response = new ApproveClientResponse
            {
                ClientId = client.Id,
                ClientName = client.Name,
                Message = $"'{client.Name}' tamirhanesi başarıyla onaylandı. Artık sistemi kullanabilir."
            };

            return new SuccessDataResult<ApproveClientResponse>(response, response.Message);
        }

        private async Task SendApprovalNotificationsAsync(UserEntity manager, string shopName, CancellationToken cancellationToken)
        {
            const string title = "Tamirhane Hesabınız Onaylandı 🎉";
            var content = $"Tebrikler! '{shopName}' tamirhane hesabınız admin tarafından onaylandı. Artık sisteme giriş yaparak tüm özellikleri kullanabilirsiniz.";

            // 1. Push Bildirimi
            try
            {
                await _notificationService.SendPushNotificationAsync(
                    userId: manager.Id,
                    title: title,
                    content: content,
                    relatedEntityType: "Client",
                    relatedEntityId: manager.ClientId,
                    extraData: new Dictionary<string, object>
                    {
                        { "type", "account_approved" },
                        { "shopName", shopName }
                    });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Push bildirimi gönderilemedi. UserId: {UserId}", manager.Id);
            }

            // 2. E-posta Bildirimi
            if (!string.IsNullOrEmpty(manager.Email))
            {
                try
                {
                    var emailBody = BuildApprovalEmailBody(manager.FirstName, shopName);
                    await _emailService.SendEmailAsync(
                        to: manager.Email,
                        subject: title,
                        body: emailBody);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "E-posta gönderilemedi. Email: {Email}", manager.Email);
                }
            }

            // 3. SMS Bildirimi (varsa)
            if (!string.IsNullOrEmpty(manager.PhoneNumber))
            {
                try
                {
                    await _notificationService.SendNotificationAsync(
                        type: NotificationType.Sms,
                        userId: manager.Id,
                        email: null,
                        phoneNumber: manager.PhoneNumber,
                        title: title,
                        content: $"Tebrikler! '{shopName}' tamirhane hesabınız onaylandı. Sisteme giriş yapabilirsiniz.",
                        relatedEntityType: "Client",
                        relatedEntityId: manager.ClientId);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "SMS bildirimi gönderilemedi. UserId: {UserId}", manager.Id);
                }
            }
        }

        private static string BuildApprovalEmailBody(string firstName, string shopName)
        {
            return $"""
                <!DOCTYPE html>
                <html lang="tr">
                <head>
                  <meta charset="UTF-8" />
                  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
                  <title>Hesap Onaylandı</title>
                </head>
                <body style="margin:0;padding:0;background-color:#f4f4f4;font-family:Arial,sans-serif;">
                  <table width="100%" cellpadding="0" cellspacing="0" style="background-color:#f4f4f4;padding:40px 0;">
                    <tr>
                      <td align="center">
                        <table width="600" cellpadding="0" cellspacing="0" style="background-color:#ffffff;border-radius:12px;overflow:hidden;box-shadow:0 4px 20px rgba(0,0,0,0.08);">

                          <!-- Header -->
                          <tr>
                            <td style="background:linear-gradient(135deg,#667eea,#764ba2);padding:40px 40px 30px;text-align:center;">
                              <div style="font-size:48px;margin-bottom:12px;">🎉</div>
                              <h1 style="color:#ffffff;margin:0;font-size:26px;font-weight:700;">Hesabınız Onaylandı!</h1>
                              <p style="color:rgba(255,255,255,0.85);margin:10px 0 0;font-size:15px;">Magic Car Repair sistemi artık kullanıma hazır</p>
                            </td>
                          </tr>

                          <!-- Body -->
                          <tr>
                            <td style="padding:40px;">
                              <p style="color:#333;font-size:16px;margin:0 0 20px;">Merhaba <strong>{firstName}</strong>,</p>
                              <p style="color:#555;font-size:15px;line-height:1.6;margin:0 0 24px;">
                                <strong>'{shopName}'</strong> tamirhane hesabınız başarıyla onaylandı. 
                                Artık tüm özelliklere erişebilir ve işletmenizi yönetebilirsiniz.
                              </p>

                              <!-- Features List -->
                              <table width="100%" cellpadding="0" cellspacing="0" style="background:#f8f9ff;border-radius:8px;padding:20px;margin-bottom:30px;">
                                <tr><td style="padding:8px 0;color:#555;font-size:14px;">✅ &nbsp; İş emri oluşturma ve yönetimi</td></tr>
                                <tr><td style="padding:8px 0;color:#555;font-size:14px;">✅ &nbsp; Müşteri ve araç kayıtları</td></tr>
                                <tr><td style="padding:8px 0;color:#555;font-size:14px;">✅ &nbsp; Stok takibi ve faturalama</td></tr>
                                <tr><td style="padding:8px 0;color:#555;font-size:14px;">✅ &nbsp; Analitik ve raporlar</td></tr>
                                <tr><td style="padding:8px 0;color:#555;font-size:14px;">✅ &nbsp; Yapay zeka destekli asistan</td></tr>
                              </table>

                              <!-- CTA Button -->
                              <div style="text-align:center;margin-bottom:30px;">
                                <a href="#" style="display:inline-block;background:linear-gradient(135deg,#667eea,#764ba2);color:#ffffff;text-decoration:none;padding:14px 36px;border-radius:8px;font-size:16px;font-weight:600;">
                                  Uygulamaya Giriş Yap
                                </a>
                              </div>

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
