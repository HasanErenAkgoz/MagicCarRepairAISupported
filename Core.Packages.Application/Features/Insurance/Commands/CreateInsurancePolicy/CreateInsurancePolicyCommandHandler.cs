using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Insurance.Commands.CreateInsurancePolicy
{
    public class CreateInsurancePolicyCommandHandler : IRequestHandler<CreateInsurancePolicyCommand, CreateInsurancePolicyResponse>
    {
        private readonly IInsurancePolicyRepository _insurancePolicyRepository;
        private readonly IInsuranceCompanyRepository _insuranceCompanyRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public CreateInsurancePolicyCommandHandler(
            IInsurancePolicyRepository insurancePolicyRepository,
            IInsuranceCompanyRepository insuranceCompanyRepository,
            IVehicleRepository vehicleRepository,
            ICustomerRepository customerRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _insurancePolicyRepository = insurancePolicyRepository;
            _insuranceCompanyRepository = insuranceCompanyRepository;
            _vehicleRepository = vehicleRepository;
            _customerRepository = customerRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<CreateInsurancePolicyResponse> Handle(CreateInsurancePolicyCommand request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? throw new DomainException("CLIENT_ID_REQUIRED");

            // Validate policy number uniqueness
            var existingPolicy = await _insurancePolicyRepository.GetByPolicyNumberAsync(request.PolicyNumber, cancellationToken);
            if (existingPolicy != null)
            {
                throw new DomainException("INSURANCE_POLICY_NUMBER_ALREADY_EXISTS", new { PolicyNumber = request.PolicyNumber });
            }

            // Validate vehicle
            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle == null || vehicle.ClientId != clientId)
            {
                throw new DomainException("VEHICLE_NOT_FOUND", new { VehicleId = request.VehicleId });
            }

            // Validate customer
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null || customer.ClientId != clientId)
            {
                throw new DomainException("CUSTOMER_NOT_FOUND", new { CustomerId = request.CustomerId });
            }

            // Validate insurance company
            var insuranceCompany = await _insuranceCompanyRepository.GetByIdAsync(request.InsuranceCompanyId);
            if (insuranceCompany == null || insuranceCompany.ClientId != clientId || !insuranceCompany.IsActive)
            {
                throw new DomainException("INSURANCE_COMPANY_NOT_FOUND_OR_INACTIVE", new { InsuranceCompanyId = request.InsuranceCompanyId });
            }

            // Validate dates
            if (request.EndDate <= request.StartDate)
            {
                throw new DomainException("INSURANCE_END_DATE_MUST_BE_AFTER_START_DATE");
            }

            var policy = new InsurancePolicy
            {
                PolicyNumber = request.PolicyNumber,
                VehicleId = request.VehicleId,
                CustomerId = request.CustomerId,
                InsuranceCompanyId = request.InsuranceCompanyId,
                InsuranceType = request.InsuranceType,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PremiumAmount = request.PremiumAmount,
                CoverageAmount = request.CoverageAmount,
                DeductiblePercentage = request.DeductiblePercentage,
                DeductibleAmount = request.DeductibleAmount,
                PolicyFileId = request.PolicyFileId,
                Notes = request.Notes,
                Status = InsuranceStatus.Active,
                ClientId = clientId
            };

            await _insurancePolicyRepository.AddAsync(policy, cancellationToken);

            return _mapper.Map<CreateInsurancePolicyResponse>(policy);
        }
    }
}

