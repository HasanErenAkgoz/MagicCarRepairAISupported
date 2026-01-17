using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Create
{
    public class CreateQuoteRequestCommandHandler : IRequestHandler<CreateQuoteRequestCommand, IDataResult<CreateQuoteRequestResponse>>
    {
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IEntityRepository<Customer, int> _customerRepository;
        private readonly IEntityRepository<Vehicle, int> _vehicleRepository;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public CreateQuoteRequestCommandHandler(
            IQuoteRequestRepository quoteRequestRepository,
            IEntityRepository<Customer, int> customerRepository,
            IEntityRepository<Vehicle, int> vehicleRepository,
            IMapper mapper,
            ITenantService tenantService)
        {
            _quoteRequestRepository = quoteRequestRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IDataResult<CreateQuoteRequestResponse>> Handle(CreateQuoteRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId() ?? 1;

                // Customer kontrolü (eğer CustomerId verilmişse)
                if (request.CustomerId.HasValue)
                {
                    var customer = await _customerRepository.GetByIdAsync(request.CustomerId.Value);
                    if (customer == null || customer.ClientId != clientId)
                    {
                        return new ErrorDataResult<CreateQuoteRequestResponse>("Customer not found");
                    }
                }

                // Vehicle kontrolü (eğer VehicleId verilmişse)
                if (request.VehicleId.HasValue)
                {
                    var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId.Value);
                    if (vehicle == null || vehicle.ClientId != clientId)
                    {
                        return new ErrorDataResult<CreateQuoteRequestResponse>("Vehicle not found");
                    }

                    // Vehicle'ın Customer'a ait olduğunu kontrol et
                    if (request.CustomerId.HasValue && vehicle.CustomerId != request.CustomerId.Value)
                    {
                        return new ErrorDataResult<CreateQuoteRequestResponse>("Vehicle does not belong to customer");
                    }
                }

                // Misafir kullanıcı için iletişim bilgileri kontrolü
                if (!request.CustomerId.HasValue)
                {
                    if (string.IsNullOrWhiteSpace(request.CustomerEmail) && string.IsNullOrWhiteSpace(request.CustomerPhone))
                    {
                        return new ErrorDataResult<CreateQuoteRequestResponse>("Customer email or phone is required for guest users");
                    }
                }

                // QuoteRequest oluştur
                var quoteRequest = new QuoteRequest
                {
                    CustomerId = request.CustomerId,
                    VehicleId = request.VehicleId,
                    VehicleBrand = request.VehicleBrand,
                    VehicleModel = request.VehicleModel,
                    VehicleYear = request.VehicleYear,
                    VehicleLicensePlate = request.VehicleLicensePlate,
                    ProblemDescription = request.ProblemDescription,
                    RequestType = request.RequestType,
                    UrgencyLevel = request.UrgencyLevel,
                    DesiredStartDate = request.DesiredStartDate,
                    DesiredEndDate = request.DesiredEndDate,
                    CustomerEmail = request.CustomerEmail,
                    CustomerPhone = request.CustomerPhone,
                    CustomerName = request.CustomerName,
                    Status = QuoteStatus.Open,
                    ClientId = clientId,
                    QuoteDeadline = request.QuoteDeadline ?? DateTime.UtcNow.AddDays(7), // Varsayılan 7 gün
                    CreatedDate = DateTime.UtcNow
                };

                // RequestNumber oluşturulacak, önce kaydet
                await _quoteRequestRepository.AddAsync(quoteRequest, cancellationToken);
                
                // RequestNumber oluştur
                quoteRequest.GenerateRequestNumber();
                _quoteRequestRepository.Update(quoteRequest);

                var response = _mapper.Map<CreateQuoteRequestResponse>(quoteRequest);
                return new SuccessDataResult<CreateQuoteRequestResponse>(response, "Quote request created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<CreateQuoteRequestResponse>(ex.Message);
            }
        }
    }
}
