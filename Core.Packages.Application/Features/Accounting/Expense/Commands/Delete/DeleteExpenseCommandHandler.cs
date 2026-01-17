using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Delete
{
    public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommand, DeleteExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ITenantService _tenantService;

        public DeleteExpenseCommandHandler(
            IExpenseRepository expenseRepository,
            ITenantService tenantService)
        {
            _expenseRepository = expenseRepository;
            _tenantService = tenantService;
        }

        public async Task<DeleteExpenseResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Gideri bul
            var expense = await _expenseRepository.GetByIdAsync(request.Id);
            if (expense == null)
            {
                throw new DomainException("EXPENSE_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (expense.ClientId != clientId)
            {
                throw new DomainException("EXPENSE_NOT_BELONG_TO_CLIENT", new { ExpenseId = request.Id });
            }

            // Soft delete (Status = 0)
            expense.Status = 0;
            _expenseRepository.Update(expense);
            await _expenseRepository.SaveChangesAsync();

            return new DeleteExpenseResponse
            {
                Id = request.Id,
                Success = true,
                Message = "Expense deleted successfully"
            };
        }
    }
}

