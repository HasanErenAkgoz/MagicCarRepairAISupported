using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Reminders.Commands.CreateReminder
{
    public class CreateReminderCommandHandler : IRequestHandler<CreateReminderCommand, IDataResult<CreateReminderResponse>>
    {
        private readonly IEntityRepository<Reminder, int> _reminderRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateReminderCommandHandler(
            IEntityRepository<Reminder, int> reminderRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _reminderRepository = reminderRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<CreateReminderResponse>> Handle(CreateReminderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                var reminder = new Reminder
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Content = request.Content,
                    ReminderDate = request.ReminderDate,
                    Type = (Domain.Entities.ReminderType)request.Type,
                    RelatedEntityType = request.RelatedEntityType,
                    RelatedEntityId = request.RelatedEntityId,
                    IsRecurring = request.IsRecurring,
                    RecurrenceDays = request.RecurrenceDays,
                    ClientId = clientId
                };

                await _reminderRepository.AddAsync(reminder, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new SuccessDataResult<CreateReminderResponse>(
                    new CreateReminderResponse
                    {
                        ReminderId = reminder.Id,
                        Message = "Hatırlatma başarıyla oluşturuldu."
                    },
                    "Hatırlatma başarıyla oluşturuldu."
                );
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CreateReminderResponse>($"Hatırlatma oluşturulurken hata oluştu: {ex.Message}");
            }
        }
    }
}
