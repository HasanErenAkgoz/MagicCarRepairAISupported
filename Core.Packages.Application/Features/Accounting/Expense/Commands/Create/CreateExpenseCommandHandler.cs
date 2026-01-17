using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Create
{
    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, CreateExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public CreateExpenseCommandHandler(
            IExpenseRepository expenseRepository,
            IEmployeeRepository employeeRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _employeeRepository = employeeRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<CreateExpenseResponse> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Employee kontrolü (eğer belirtilmişse)
            if (request.EmployeeId.HasValue)
            {
                var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId.Value);
                if (employee == null)
                {
                    throw new DomainException("EMPLOYEE_NOT_FOUND", new { EmployeeId = request.EmployeeId.Value });
                }

                if (employee.ClientId != clientId)
                {
                    throw new DomainException("EMPLOYEE_NOT_BELONG_TO_CLIENT", new { EmployeeId = request.EmployeeId.Value });
                }
            }

            // Entity oluştur
            var expense = new Domain.Entities.Expense
            {
                ExpenseType = request.ExpenseType,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                TransactionDate = request.TransactionDate,
                SupplierName = request.SupplierName,
                InvoiceNumber = request.InvoiceNumber,
                Description = request.Description,
                EmployeeId = request.EmployeeId,
                PartPurchaseId = request.PartPurchaseId,
                ClientId = clientId
            };

            // Kaydet
            await _expenseRepository.AddAsync(expense, cancellationToken);
            await _expenseRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<CreateExpenseResponse>(expense);
            response.ExpenseTypeName = expense.ExpenseType.ToString();
            response.PaymentMethodName = expense.PaymentMethod.ToString();

            // Employee adını ekle
            if (request.EmployeeId.HasValue)
            {
                var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId.Value);
                response.EmployeeName = employee?.FullName;
            }

            return response;
        }
    }
}

