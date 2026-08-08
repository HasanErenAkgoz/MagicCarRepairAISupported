using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.Income.Commands.Create
{
    public class CreateIncomeCommandHandler : IRequestHandler<CreateIncomeCommand, CreateIncomeResponse>
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public CreateIncomeCommandHandler(
            IIncomeRepository incomeRepository,
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<CreateIncomeResponse> Handle(CreateIncomeCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            // WorkOrder kontrolü (eğer belirtilmişse)
            if (request.WorkOrderId.HasValue)
            {
                var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId.Value);
                if (workOrder == null)
                {
                    throw new DomainException("WORKORDER_NOT_FOUND", new { WorkOrderId = request.WorkOrderId.Value });
                }

                if (workOrder.ClientId != clientId)
                {
                    throw new DomainException("WORKORDER_NOT_BELONG_TO_CLIENT", new { WorkOrderId = request.WorkOrderId.Value });
                }
            }

            // Entity oluştur
            var income = new Domain.Entities.Income
            {
                WorkOrderId = request.WorkOrderId,
                IncomeType = request.IncomeType,
                Amount = request.Amount,
                PaymentMethod = request.PaymentMethod,
                TransactionDate = request.TransactionDate,
                Description = request.Description,
                InvoiceNumber = request.InvoiceNumber,
                CustomerId = request.CustomerId,
                ClientId = clientId
            };

            // Kaydet
            await _incomeRepository.AddAsync(income, cancellationToken);
            await _incomeRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<CreateIncomeResponse>(income);
            response.IncomeTypeName = income.IncomeType.ToString();
            response.PaymentMethodName = income.PaymentMethod.ToString();

            // WorkOrder numarasını ekle
            if (request.WorkOrderId.HasValue)
            {
                var workOrder = await _workOrderRepository.GetByIdAsync(request.WorkOrderId.Value);
                response.WorkOrderNumber = workOrder?.WorkOrderNumber;
            }

            return response;
        }
    }
}

