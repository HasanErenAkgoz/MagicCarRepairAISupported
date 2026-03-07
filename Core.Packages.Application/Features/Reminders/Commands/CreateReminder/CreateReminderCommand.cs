using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Reminders.Commands.CreateReminder
{
    public class CreateReminderCommand : IRequest<IDataResult<CreateReminderResponse>>
    {
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime ReminderDate { get; set; }
        public ReminderType Type { get; set; }
        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }
        public bool IsRecurring { get; set; } = false;
        public int? RecurrenceDays { get; set; }
    }

    public enum ReminderType
    {
        WorkOrder = 1,
        Appointment = 2,
        Payment = 3,
        Maintenance = 4,
        Custom = 5
    }

    public class CreateReminderResponse
    {
        public int ReminderId { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
