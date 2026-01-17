using AutoMapper;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Expense.Queries.GetAll
{
    public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, List<GetAllExpensesResponse>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;

        public GetAllExpensesQueryHandler(IExpenseRepository expenseRepository, IMapper mapper)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllExpensesResponse>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entities.Expense> query = _expenseRepository.Query()
                .Include(e => e.Employee);

            // Filtreler
            if (request.ExpenseType.HasValue)
            {
                query = query.Where(e => e.ExpenseType == request.ExpenseType.Value);
            }

            if (request.PaymentMethod.HasValue)
            {
                query = query.Where(e => e.PaymentMethod == request.PaymentMethod.Value);
            }

            if (request.EmployeeId.HasValue)
            {
                query = query.Where(e => e.EmployeeId == request.EmployeeId.Value);
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(e => e.TransactionDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(e => e.TransactionDate <= request.EndDate.Value);
            }

            // Pagination
            if (request.PageNumber.HasValue && request.PageSize.HasValue)
            {
                query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                             .Take(request.PageSize.Value);
            }

            // Çalıştır ve map et
            var expenses = await query
                .OrderByDescending(e => e.TransactionDate)
                .ToListAsync(cancellationToken);

            var response = _mapper.Map<List<GetAllExpensesResponse>>(expenses);

            // Enum isimleri ve Employee adı ekle
            foreach (var item in response)
            {
                var expense = expenses.First(e => e.Id == item.Id);
                item.ExpenseTypeName = expense.ExpenseType.ToString();
                item.PaymentMethodName = expense.PaymentMethod.ToString();
                item.EmployeeName = expense.Employee?.FullName;
            }

            return response;
        }
    }
}

