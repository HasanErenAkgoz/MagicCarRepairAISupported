using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Infrastructure.Configurations.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace MagicCarRepairAISupported.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            if (string.IsNullOrEmpty(_emailSettings.FromEmail) || string.IsNullOrEmpty(_emailSettings.SmtpServer))
            {
                _logger.LogWarning(
                    "E-posta gönderilmedi: EmailSettings boş. FromEmail veya SmtpServer tanımlı değil.");
                return false;
            }

            if (string.IsNullOrEmpty(to))
            {
                _logger.LogWarning("E-posta gönderilmedi: alıcı adresi (to) boş.");
                return false;
            }

            // HTML şablonlarında satır sonlarını <br> yapmak Gmail/Outlook'ta araya gereksiz boşluk sokar.
            var raw = body ?? string.Empty;
            var trimmed = raw.TrimStart();
            var safeBody = trimmed.StartsWith('<')
                ? raw
                : raw.Replace("\n", "<br>", StringComparison.Ordinal);

            try
            {
                using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort);
                client.Credentials = new NetworkCredential(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
                client.EnableSsl = true;

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.FromEmail),
                    Subject = subject ?? string.Empty,
                    Body = safeBody,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);
                await client.SendMailAsync(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SMTP e-posta gönderimi başarısız. Alıcı: {To}", to);
                return false;
            }
        }
    }
}
