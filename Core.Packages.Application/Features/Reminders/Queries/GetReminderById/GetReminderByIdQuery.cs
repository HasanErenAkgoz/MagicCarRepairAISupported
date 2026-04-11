using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Reminders.Queries.GetReminders;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Reminders.Queries.GetReminderById
{
    public class GetReminderByIdQuery : IRequest<IDataResult<ReminderDto>>
    {
        public int Id { get; set; }
    }

    public class GetReminderByIdQueryHandler : IRequestHandler<GetReminderByIdQuery, IDataResult<ReminderDto>>
    {
        private readonly IEntityRepository<Reminder, int> _reminderRepository;
        private readonly ITenantService _tenantService;

        public GetReminderByIdQueryHandler(
            IEntityRepository<Reminder, int> reminderRepository,
            ITenantService tenantService)
        {
            _reminderRepository = reminderRepository;
            _tenantService = tenantService;
        }

        public async Task<IDataResult<ReminderDto>> Handle(GetReminderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                var reminder = await _reminderRepository.GetByIdAsync(request.Id, cancellationToken);

                if (reminder == null)
                {
                    return new ErrorDataResult<ReminderDto>("Hatirlatma bulunamadi.");
                }

                if (reminder.ClientId != clientId)
                {
                    return new ErrorDataResult<ReminderDto>("Bu hatirlatmaya erisim yetkiniz yok.");
                }

                var dto = new ReminderDto
                {
                    Id = reminder.Id,
                    UserId = reminder.UserId,
                    Title = reminder.Title,
                    Content = reminder.Content,
                    ReminderDate = reminder.ReminderDate,
                    Type = reminder.Type,
                    RelatedEntityType = reminder.RelatedEntityType,
                    RelatedEntityId = reminder.RelatedEntityId,
                    IsSent = reminder.IsSent,
                    SentDate = reminder.SentDate,
                    IsRecurring = reminder.IsRecurring,
                    RecurrenceDays = reminder.RecurrenceDays,
                    ClientId = reminder.ClientId,
                    CreatedDate = reminder.CreatedDate
                };

                return new SuccessDataResult<ReminderDto>(dto);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<ReminderDto>($"Hatirlatma getirilirken hata olustu: {ex.Message}");
            }
        }
    }
}
