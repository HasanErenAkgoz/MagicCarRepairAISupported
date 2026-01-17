using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Income.Queries.GetById
{
    public class GetIncomeByIdQueryHandler : IRequestHandler<GetIncomeByIdQuery, GetIncomeByIdResponse>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetIncomeByIdQueryHandler(
            IIncomeRepository incomeRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<GetIncomeByIdResponse> Handle(GetIncomeByIdQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Geliri bul (WorkOrder ve Customer'ı da dahil et)
            var income = await _incomeRepository.Query()
                .Include(i => i.WorkOrder)
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (income == null)
            {
                throw new DomainException("INCOME_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (income.ClientId != clientId)
            {
                throw new DomainException("INCOME_NOT_BELONG_TO_CLIENT", new { IncomeId = request.Id });
            }

            // Response
            var response = _mapper.Map<GetIncomeByIdResponse>(income);
            response.IncomeTypeName = income.IncomeType.ToString();
            response.PaymentMethodName = income.PaymentMethod.ToString();
            response.WorkOrderNumber = income.WorkOrder?.WorkOrderNumber;
            response.CustomerName = income.Customer?.FullName;

            return response;
        }
    }
}

