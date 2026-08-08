using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Update
{
    public class UpdateExpenseCommandHandler : IRequestHandler<UpdateExpenseCommand, UpdateExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public UpdateExpenseCommandHandler(
            IExpenseRepository expenseRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<UpdateExpenseResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

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

            // Güncelleme
            if (request.ExpenseType.HasValue)
                expense.ExpenseType = request.ExpenseType.Value;
            if (request.Amount.HasValue)
                expense.Amount = request.Amount.Value;
            if (request.PaymentMethod.HasValue)
                expense.PaymentMethod = request.PaymentMethod.Value;
            if (request.TransactionDate.HasValue)
                expense.TransactionDate = request.TransactionDate.Value;
            if (request.SupplierName != null)
                expense.SupplierName = request.SupplierName;
            if (request.InvoiceNumber != null)
                expense.InvoiceNumber = request.InvoiceNumber;
            if (request.Description != null)
                expense.Description = request.Description;

            _expenseRepository.Update(expense);
            await _expenseRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<UpdateExpenseResponse>(expense);
            response.ExpenseTypeName = expense.ExpenseType.ToString();
            response.PaymentMethodName = expense.PaymentMethod.ToString();
            return response;
        }
    }
}

