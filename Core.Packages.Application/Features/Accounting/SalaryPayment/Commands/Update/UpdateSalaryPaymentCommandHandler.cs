using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Commands.Update
{
    public class UpdateSalaryPaymentCommandHandler : IRequestHandler<UpdateSalaryPaymentCommand, UpdateSalaryPaymentResponse>
    {
        private readonly ISalaryPaymentRepository _salaryPaymentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public UpdateSalaryPaymentCommandHandler(
            ISalaryPaymentRepository salaryPaymentRepository,
            IEmployeeRepository employeeRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _salaryPaymentRepository = salaryPaymentRepository;
            _employeeRepository = employeeRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<UpdateSalaryPaymentResponse> Handle(UpdateSalaryPaymentCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // SalaryPayment'ı bul
            var salaryPayment = await _salaryPaymentRepository.GetByIdAsync(request.Id);
            if (salaryPayment == null)
            {
                throw new DomainException("SALARY_PAYMENT_NOT_FOUND", new { Id = request.Id });
            }

            if (salaryPayment.ClientId != clientId)
            {
                throw new DomainException("SALARY_PAYMENT_NOT_BELONG_TO_CLIENT", new { Id = request.Id });
            }

            // Güncelle
            salaryPayment.GrossSalary = request.GrossSalary;
            salaryPayment.SocialSecurityDeduction = request.SocialSecurityDeduction;
            salaryPayment.UnemploymentInsuranceDeduction = request.UnemploymentInsuranceDeduction;
            salaryPayment.IncomeTaxDeduction = request.IncomeTaxDeduction;
            salaryPayment.StampTax = request.StampTax;
            salaryPayment.OtherDeductions = request.OtherDeductions;
            salaryPayment.PaymentDate = request.PaymentDate;
            salaryPayment.PaymentMethod = request.PaymentMethod;
            salaryPayment.Description = request.Description;
            salaryPayment.PaymentReferenceNumber = request.PaymentReferenceNumber;

            // Net maaşı yeniden hesapla
            salaryPayment.CalculateNetSalary();

            // Kaydet
            _salaryPaymentRepository.Update(salaryPayment);
            await _salaryPaymentRepository.SaveChangesAsync();

            // Response
            var response = _mapper.Map<UpdateSalaryPaymentResponse>(salaryPayment);
            var employee = await _employeeRepository.GetByIdAsync(salaryPayment.EmployeeId);
            response.EmployeeName = employee?.FullName ?? string.Empty;
            response.PaymentMethodName = salaryPayment.PaymentMethod.ToString();

            return response;
        }
    }
}
