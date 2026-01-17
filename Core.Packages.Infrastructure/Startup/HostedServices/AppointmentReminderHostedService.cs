using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Email;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Startup.HostedServices
{
    public class AppointmentReminderHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AppointmentReminderHostedService> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromHours(1); // Her saat kontrol et

        public AppointmentReminderHostedService(
            IServiceProvider serviceProvider,
            ILogger<AppointmentReminderHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var appointmentRepository = scope.ServiceProvider.GetRequiredService<IAppointmentRepository>();
                    var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                    // 24 saat içinde randevusu olan müşterileri bul
                    var appointments = await appointmentRepository.GetAppointmentsForReminderAsync(stoppingToken);

                    foreach (var appointment in appointments)
                    {
                        try
                        {
                            // Email gönder
                            if (!string.IsNullOrEmpty(appointment.Customer?.Email))
                            {
                                var subject = $"Randevu Hatırlatması - {appointment.AppointmentNumber}";
                                var message = $@"
                                    Sayın {appointment.Customer.FullName},
                                    
                                    Yarın {appointment.AppointmentDate:dd.MM.yyyy} tarihinde saat {appointment.StartTime:hh\\:mm}'de randevunuz bulunmaktadır.
                                    
                                    Randevu Türü: {appointment.AppointmentType}
                                    {(appointment.Description != null ? $"Açıklama: {appointment.Description}" : "")}
                                    
                                    Lütfen randevu saatinde hazır bulununuz.
                                    
                                    İyi günler dileriz.
                                ";

                                await emailService.SendEmailAsync(appointment.Customer.Email, subject, message);
                            }

                            // Bildirim oluştur
                            if (appointment.Customer?.UserId.HasValue == true)
                            {
                                await notificationService.SendNotificationAsync(
                                    Domain.Enums.NotificationType.Email,
                                    appointment.Customer.UserId.Value,
                                    appointment.Customer.Email,
                                    appointment.Customer.PhoneNumber,
                                    "Randevu Hatırlatması",
                                    $"Yarın {appointment.AppointmentDate:dd.MM.yyyy} saat {appointment.StartTime:hh\\:mm}'de randevunuz var.",
                                    "Appointment",
                                    appointment.Id,
                                    null);
                            }

                            // Hatırlatma gönderildi olarak işaretle
                            appointment.ReminderSent = true;
                            appointment.ReminderSentDate = DateTime.UtcNow;
                            appointmentRepository.Update(appointment);
                            await appointmentRepository.SaveChangesAsync();

                            _logger.LogInformation($"Appointment reminder sent for {appointment.AppointmentNumber}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Error sending reminder for appointment {appointment.AppointmentNumber}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in AppointmentReminderHostedService");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
    }
}

