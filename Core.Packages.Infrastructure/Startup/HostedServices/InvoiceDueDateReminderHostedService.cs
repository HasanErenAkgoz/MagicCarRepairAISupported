using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Infrastructure.Startup.HostedServices
{
    /// <summary>
    /// Fatura vade takibi ve hatırlatma servisi
    /// Vadesi yaklaşan ve vadesi geçmiş faturalar için müşterilere hatırlatma gönderir
    /// </summary>
    public class InvoiceDueDateReminderHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<InvoiceDueDateReminderHostedService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromHours(6); // Her 6 saatte bir kontrol et
        private const int DaysBeforeDue = 7; // 7 gün önceden hatırlat

        public InvoiceDueDateReminderHostedService(
            IServiceProvider serviceProvider,
            ILogger<InvoiceDueDateReminderHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("InvoiceDueDateReminderHostedService started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CheckInvoicesAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in InvoiceDueDateReminderHostedService");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }

            _logger.LogInformation("InvoiceDueDateReminderHostedService stopped");
        }

        private async Task CheckInvoicesAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var invoiceRepository = scope.ServiceProvider.GetRequiredService<IInvoiceRepository>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

            var today = DateTime.Today;
            var dueDateThreshold = today.AddDays(DaysBeforeDue);

            // Vadesi yaklaşan faturalar (7 gün içinde)
            // Not: Invoice entity'sinde ReminderSent property'si yok, bu yüzden sadece vade kontrolü yapıyoruz
            var upcomingInvoices = await invoiceRepository.Query()
                .Include(i => i.Customer)
                .Where(i => (i.Status == InvoiceStatus.Pending || i.Status == InvoiceStatus.PartiallyPaid) &&
                           i.DueDate.HasValue &&
                           i.DueDate.Value >= today &&
                           i.DueDate.Value <= dueDateThreshold)
                .ToListAsync(cancellationToken);

            foreach (var invoice in upcomingInvoices)
            {
                try
                {
                    if (!invoice.DueDate.HasValue) continue;
                    var daysUntilDue = (invoice.DueDate.Value - today).Days;
                    var subject = $"Fatura Ödeme Hatırlatması - {invoice.InvoiceNumber}";
                    var message = $@"
                        Sayın {invoice.Customer?.FullName ?? "Müşteri"},
                        
                        {invoice.InvoiceNumber} numaralı faturanızın ödeme vadesi {daysUntilDue} gün sonra ({invoice.DueDate.Value:dd.MM.yyyy}) dolmaktadır.
                        
                        Fatura Tutarı: {invoice.TotalAmount:N2} TL
                        Vade Tarihi: {invoice.DueDate.Value:dd.MM.yyyy}
                        
                        Lütfen ödemenizi zamanında yapmanızı rica ederiz.
                        
                        İyi günler dileriz.
                    ";

                    if (!string.IsNullOrEmpty(invoice.Customer?.Email))
                    {
                        await emailService.SendEmailAsync(invoice.Customer.Email, subject, message);
                    }

                    if (invoice.Customer?.UserId.HasValue == true)
                    {
                        await notificationService.SendNotificationAsync(
                            NotificationType.Email,
                            invoice.Customer.UserId.Value,
                            invoice.Customer.Email,
                            invoice.Customer.PhoneNumber,
                            "Fatura Ödeme Hatırlatması",
                            $"{invoice.InvoiceNumber} numaralı faturanızın ödeme vadesi {daysUntilDue} gün sonra dolmaktadır.",
                            "Invoice",
                            invoice.Id,
                            null);
                    }

                    // Not: Invoice entity'sinde ReminderSent property'si yok, bu yüzden sadece log tutuyoruz
                    invoiceRepository.Update(invoice);
                    await invoiceRepository.SaveChangesAsync();

                    _logger.LogInformation($"Invoice reminder sent for {invoice.InvoiceNumber}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error sending invoice reminder for {invoice.InvoiceNumber}");
                }
            }

            // Vadesi geçmiş faturalar
            var overdueInvoices = await invoiceRepository.Query()
                .Include(i => i.Customer)
                .Where(i => (i.Status == InvoiceStatus.Pending || i.Status == InvoiceStatus.PartiallyPaid || i.Status == InvoiceStatus.Overdue) &&
                           i.DueDate.HasValue &&
                           i.DueDate.Value < today)
                .ToListAsync(cancellationToken);

            foreach (var invoice in overdueInvoices)
            {
                try
                {
                    if (!invoice.DueDate.HasValue) continue;
                    var daysOverdue = (today - invoice.DueDate.Value).Days;
                    var subject = $"Vadesi Geçmiş Fatura - {invoice.InvoiceNumber}";
                    var message = $@"
                        Sayın {invoice.Customer?.FullName ?? "Müşteri"},
                        
                        {invoice.InvoiceNumber} numaralı faturanızın ödeme vadesi {daysOverdue} gün önce ({invoice.DueDate.Value:dd.MM.yyyy}) dolmuştur.
                        
                        Fatura Tutarı: {invoice.TotalAmount:N2} TL
                        Vade Tarihi: {invoice.DueDate.Value:dd.MM.yyyy}
                        Gecikme: {daysOverdue} gün
                        
                        Lütfen ödemenizi en kısa sürede yapmanızı rica ederiz.
                        
                        İyi günler dileriz.
                    ";

                    if (!string.IsNullOrEmpty(invoice.Customer?.Email))
                    {
                        await emailService.SendEmailAsync(invoice.Customer.Email, subject, message);
                    }

                    if (invoice.Customer?.UserId.HasValue == true)
                    {
                        await notificationService.SendNotificationAsync(
                            NotificationType.Email,
                            invoice.Customer.UserId.Value,
                            invoice.Customer.Email,
                            invoice.Customer.PhoneNumber,
                            "Vadesi Geçmiş Fatura",
                            $"{invoice.InvoiceNumber} numaralı faturanızın ödeme vadesi {daysOverdue} gün önce dolmuştur.",
                            "Invoice",
                            invoice.Id,
                            null);
                    }

                    // Vade geçmiş faturaların durumunu güncelle
                    if (invoice.Status != InvoiceStatus.Overdue)
                    {
                        invoice.UpdatePaymentStatus();
                    }
                    invoiceRepository.Update(invoice);
                    await invoiceRepository.SaveChangesAsync();

                    _logger.LogInformation($"Overdue invoice reminder sent for {invoice.InvoiceNumber}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error sending overdue invoice reminder for {invoice.InvoiceNumber}");
                }
            }
        }
    }
}
