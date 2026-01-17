using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Queries.GetById
{
    public class GetSalaryPaymentByIdQueryHandler : IRequestHandler<GetSalaryPaymentByIdQuery, GetSalaryPaymentByIdResponse>
    {
        private readonly ISalaryPaymentRepository _salaryPaymentRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetSalaryPaymentByIdQueryHandler(
            ISalaryPaymentRepository salaryPaymentRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _salaryPaymentRepository = salaryPaymentRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<GetSalaryPaymentByIdResponse> Handle(GetSalaryPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            var salaryPayment = await _salaryPaymentRepository.GetByIdAsync(request.Id);
            if (salaryPayment == null)
            {
                throw new DomainException("SALARY_PAYMENT_NOT_FOUND", new { Id = request.Id });
            }

            if (salaryPayment.ClientId != clientId)
            {
                throw new DomainException("SALARY_PAYMENT_NOT_BELONG_TO_CLIENT", new { Id = request.Id });
            }

            var response = _mapper.Map<GetSalaryPaymentByIdResponse>(salaryPayment);
            response.EmployeeName = salaryPayment.Employee.FullName;
            response.EmployeeNo = salaryPayment.Employee.EmployeeNo;
            response.PaymentMethodName = salaryPayment.PaymentMethod.ToString();

            return response;
        }
    }
}
