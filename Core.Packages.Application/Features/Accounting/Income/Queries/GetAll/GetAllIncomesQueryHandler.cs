using AutoMapper;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Income.Queries.GetAll
{
    public class GetAllIncomesQueryHandler : IRequestHandler<GetAllIncomesQuery, List<GetAllIncomesResponse>>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IMapper _mapper;

        public GetAllIncomesQueryHandler(IIncomeRepository incomeRepository, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllIncomesResponse>> Handle(GetAllIncomesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Entities.Income> query = _incomeRepository.Query()
                .Include(i => i.WorkOrder);

            // Filtreler
            if (request.IncomeType.HasValue)
            {
                query = query.Where(i => i.IncomeType == request.IncomeType.Value);
            }

            if (request.PaymentMethod.HasValue)
            {
                query = query.Where(i => i.PaymentMethod == request.PaymentMethod.Value);
            }

            if (request.WorkOrderId.HasValue)
            {
                query = query.Where(i => i.WorkOrderId == request.WorkOrderId.Value);
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(i => i.TransactionDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(i => i.TransactionDate <= request.EndDate.Value);
            }

            // Pagination
            if (request.PageNumber.HasValue && request.PageSize.HasValue)
            {
                query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                             .Take(request.PageSize.Value);
            }

            // Çalıştır ve map et
            var incomes = await query
                .OrderByDescending(i => i.TransactionDate)
                .ToListAsync(cancellationToken);

            var response = _mapper.Map<List<GetAllIncomesResponse>>(incomes);

            // Enum isimleri ve WorkOrder numarası ekle
            foreach (var item in response)
            {
                var income = incomes.First(i => i.Id == item.Id);
                item.IncomeTypeName = income.IncomeType.ToString();
                item.PaymentMethodName = income.PaymentMethod.ToString();
                item.WorkOrderNumber = income.WorkOrder?.WorkOrderNumber;
            }

            return response;
        }
    }
}

