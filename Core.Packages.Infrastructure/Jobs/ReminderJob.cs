using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MagicCarRepairAISupported.Infrastructure.Jobs
{
    /// <summary>
    /// Hatırlatma job'ı (Background service)
    /// </summary>
    public class ReminderJob : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ReminderJob> _logger;
        private readonly TimeSpan _checkInterval = TimeSpan.FromMinutes(5); // Her 5 dakikada bir kontrol et

        public ReminderJob(IServiceProvider serviceProvider, ILogger<ReminderJob> logger)
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
                    await ProcessReminders(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing reminders");
                }

                await Task.Delay(_checkInterval, stoppingToken);
            }
        }

        private async Task ProcessReminders(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var reminderRepository = scope.ServiceProvider.GetRequiredService<IEntityRepository<Reminder, int>>();
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var now = DateTime.UtcNow;
            var reminders = await reminderRepository.GetListAsync(cancellationToken, r => !r.IsSent && r.ReminderDate <= now);
            
            var dueReminders = reminders.ToList();

            foreach (var reminder in dueReminders)
            {
                try
                {
                    // Push notification gönder
                    await notificationService.SendPushNotificationAsync(
                        reminder.UserId,
                        reminder.Title,
                        reminder.Content,
                        reminder.RelatedEntityType,
                        reminder.RelatedEntityId
                    );

                    // Hatırlatmayı gönderildi olarak işaretle
                    reminder.IsSent = true;
                    reminder.SentDate = now;
                    reminderRepository.Update(reminder);

                    // Tekrarlayan hatırlatma ise yeni bir hatırlatma oluştur
                    if (reminder.IsRecurring && reminder.RecurrenceDays.HasValue)
                    {
                        var newReminder = new Reminder
                        {
                            UserId = reminder.UserId,
                            Title = reminder.Title,
                            Content = reminder.Content,
                            ReminderDate = now.AddDays(reminder.RecurrenceDays.Value),
                            Type = reminder.Type,
                            RelatedEntityType = reminder.RelatedEntityType,
                            RelatedEntityId = reminder.RelatedEntityId,
                            IsRecurring = true,
                            RecurrenceDays = reminder.RecurrenceDays,
                            ClientId = reminder.ClientId
                        };

                        await reminderRepository.AddAsync(newReminder, cancellationToken);
                    }

                    _logger.LogInformation($"Reminder sent: {reminder.Id} to user {reminder.UserId}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error sending reminder {reminder.Id}");
                }
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
