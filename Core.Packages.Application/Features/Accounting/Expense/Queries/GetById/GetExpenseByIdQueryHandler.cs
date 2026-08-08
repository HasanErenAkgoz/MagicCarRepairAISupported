using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Expense.Queries.GetById
{
    public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, GetExpenseByIdResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetExpenseByIdQueryHandler(
            IExpenseRepository expenseRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<GetExpenseByIdResponse> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            // Gideri bul (Employee'ı da dahil et)
            var expense = await _expenseRepository.Query()
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (expense == null)
            {
                throw new DomainException("EXPENSE_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (expense.ClientId != clientId)
            {
                throw new DomainException("EXPENSE_NOT_BELONG_TO_CLIENT", new { ExpenseId = request.Id });
            }

            // Response
            var response = _mapper.Map<GetExpenseByIdResponse>(expense);
            response.ExpenseTypeName = expense.ExpenseType.ToString();
            response.PaymentMethodName = expense.PaymentMethod.ToString();
            response.EmployeeName = expense.Employee?.FullName;

            return response;
        }
    }
}

