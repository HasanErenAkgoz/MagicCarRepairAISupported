using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Accounting.Expense.Commands.Create;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using SalaryPaymentEntity = MagicCarRepairAISupported.Domain.Entities.SalaryPayment;

namespace MagicCarRepairAISupported.Application.Features.Accounting.SalaryPayment.Commands.Create
{
    public class CreateSalaryPaymentCommandHandler : IRequestHandler<CreateSalaryPaymentCommand, CreateSalaryPaymentResponse>
    {
        private readonly ISalaryPaymentRepository _salaryPaymentRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMediator _mediator;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public CreateSalaryPaymentCommandHandler(
            ISalaryPaymentRepository salaryPaymentRepository,
            IEmployeeRepository employeeRepository,
            IMediator mediator,
            ITenantService tenantService,
            IMapper mapper)
        {
            _salaryPaymentRepository = salaryPaymentRepository;
            _employeeRepository = employeeRepository;
            _mediator = mediator;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<CreateSalaryPaymentResponse> Handle(CreateSalaryPaymentCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Employee kontrolü
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
            {
                throw new DomainException("EMPLOYEE_NOT_FOUND", new { EmployeeId = request.EmployeeId });
            }

            if (employee.ClientId != clientId)
            {
                throw new DomainException("EMPLOYEE_NOT_BELONG_TO_CLIENT", new { EmployeeId = request.EmployeeId });
            }

            // Aynı dönem için ödeme kontrolü
            var existingPayment = await _salaryPaymentRepository.GetByEmployeeAndPeriodAsync(
                request.EmployeeId, 
                request.Year, 
                request.Month, 
                cancellationToken);

            if (existingPayment != null)
            {
                throw new DomainException("SALARY_PAYMENT_ALREADY_EXISTS", new 
                { 
                    EmployeeId = request.EmployeeId,
                    Year = request.Year,
                    Month = request.Month
                });
            }

            // Ay ve yıl validasyonu
            if (request.Month < 1 || request.Month > 12)
            {
                throw new DomainException("INVALID_MONTH", new { Month = request.Month });
            }

            if (request.Year < 2000 || request.Year > 2100)
            {
                throw new DomainException("INVALID_YEAR", new { Year = request.Year });
            }

            // Entity oluştur
            var salaryPayment = new SalaryPaymentEntity
            {
                EmployeeId = request.EmployeeId,
                Month = request.Month,
                Year = request.Year,
                GrossSalary = request.GrossSalary,
                SocialSecurityDeduction = request.SocialSecurityDeduction,
                UnemploymentInsuranceDeduction = request.UnemploymentInsuranceDeduction,
                IncomeTaxDeduction = request.IncomeTaxDeduction,
                StampTax = request.StampTax,
                OtherDeductions = request.OtherDeductions,
                PaymentDate = request.PaymentDate,
                PaymentMethod = request.PaymentMethod,
                Description = request.Description,
                PaymentReferenceNumber = request.PaymentReferenceNumber,
                ClientId = clientId
            };

            // Net maaşı hesapla
            salaryPayment.CalculateNetSalary();

            // Kaydet
            await _salaryPaymentRepository.AddAsync(salaryPayment, cancellationToken);
            await _salaryPaymentRepository.SaveChangesAsync();

            // Business Rule: Personel maaşı ödendiğinde otomatik gider kaydı oluştur
            var createExpenseCommand = new CreateExpenseCommand
            {
                ExpenseType = ExpenseType.Salary,
                Amount = salaryPayment.NetSalary, // Net maaş gider olarak kaydedilir
                PaymentMethod = request.PaymentMethod,
                TransactionDate = request.PaymentDate,
                EmployeeId = request.EmployeeId,
                Description = $"{employee.FullName} - {request.Month}/{request.Year} maaş ödemesi",
                InvoiceNumber = request.PaymentReferenceNumber
            };

            await _mediator.Send(createExpenseCommand, cancellationToken);

            // Response
            var response = _mapper.Map<CreateSalaryPaymentResponse>(salaryPayment);
            response.EmployeeName = employee.FullName;
            response.PaymentMethodName = salaryPayment.PaymentMethod.ToString();

            return response;
        }
    }
}
