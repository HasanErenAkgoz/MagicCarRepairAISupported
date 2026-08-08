using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Reminders.Queries.GetReminders
{
    public class GetRemindersQuery : IRequest<IDataResult<List<ReminderDto>>>
    {
        public int? UserId { get; set; }
        public bool? IsSent { get; set; }
        public ReminderType? Type { get; set; }
    }

    public class ReminderDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime ReminderDate { get; set; }
        public ReminderType Type { get; set; }
        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }
        public bool IsSent { get; set; }
        public DateTime? SentDate { get; set; }
        public bool IsRecurring { get; set; }
        public int? RecurrenceDays { get; set; }
        public int ClientId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class GetRemindersQueryHandler : IRequestHandler<GetRemindersQuery, IDataResult<List<ReminderDto>>>
    {
        private readonly IEntityRepository<Reminder, int> _reminderRepository;
        private readonly ITenantService _tenantService;

        public GetRemindersQueryHandler(
            IEntityRepository<Reminder, int> reminderRepository,
            ITenantService tenantService)
        {
            _reminderRepository = reminderRepository;
            _tenantService = tenantService;
        }

        public async Task<IDataResult<List<ReminderDto>>> Handle(GetRemindersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetRequiredClientId();

                var reminders = await _reminderRepository.GetListAsync(
                    cancellationToken,
                    x => x.ClientId == clientId
                         && (!request.UserId.HasValue || x.UserId == request.UserId.Value)
                         && (!request.IsSent.HasValue || x.IsSent == request.IsSent.Value)
                         && (!request.Type.HasValue || x.Type == request.Type.Value)
                );

                var dtos = reminders.Select(r => new ReminderDto
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    Title = r.Title,
                    Content = r.Content,
                    ReminderDate = r.ReminderDate,
                    Type = r.Type,
                    RelatedEntityType = r.RelatedEntityType,
                    RelatedEntityId = r.RelatedEntityId,
                    IsSent = r.IsSent,
                    SentDate = r.SentDate,
                    IsRecurring = r.IsRecurring,
                    RecurrenceDays = r.RecurrenceDays,
                    ClientId = r.ClientId,
                    CreatedDate = r.CreatedDate
                }).OrderByDescending(r => r.ReminderDate).ToList();

                return new SuccessDataResult<List<ReminderDto>>(dtos);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ReminderDto>>($"Hatirlatmalar getirilirken hata olustu: {ex.Message}");
            }
        }
    }
}
