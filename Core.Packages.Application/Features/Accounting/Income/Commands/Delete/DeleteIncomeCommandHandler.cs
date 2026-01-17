using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Delete
{
    public class DeleteIncomeCommandHandler : IRequestHandler<DeleteIncomeCommand, DeleteIncomeResponse>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ITenantService _tenantService;

        public DeleteIncomeCommandHandler(
            IIncomeRepository incomeRepository,
            ITenantService tenantService)
        {
            _incomeRepository = incomeRepository;
            _tenantService = tenantService;
        }

        public async Task<DeleteIncomeResponse> Handle(DeleteIncomeCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Geliri bul
            var income = await _incomeRepository.GetByIdAsync(request.Id);
            if (income == null)
            {
                throw new DomainException("INCOME_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (income.ClientId != clientId)
            {
                throw new DomainException("INCOME_NOT_BELONG_TO_CLIENT", new { IncomeId = request.Id });
            }

            // Soft delete (Status = 0)
            income.Status = 0;
            _incomeRepository.Update(income);
            await _incomeRepository.SaveChangesAsync();

            return new DeleteIncomeResponse
            {
                Id = request.Id,
                Success = true,
                Message = "Income deleted successfully"
            };
        }
    }
}

