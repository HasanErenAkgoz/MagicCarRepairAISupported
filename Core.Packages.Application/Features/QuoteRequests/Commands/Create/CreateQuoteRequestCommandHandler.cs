using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Create
{
    public class CreateQuoteRequestCommandHandler : IRequestHandler<CreateQuoteRequestCommand, IDataResult<CreateQuoteRequestResponse>>
    {
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITenantService _tenantService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateQuoteRequestCommandHandler(
            IQuoteRequestRepository quoteRequestRepository,
            ICustomerRepository customerRepository,
            IVehicleRepository vehicleRepository,
            IClientRepository clientRepository,
            ITenantService tenantService,
            IUnitOfWork unitOfWork)
        {
            _quoteRequestRepository = quoteRequestRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
            _clientRepository = clientRepository;
            _tenantService = tenantService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<CreateQuoteRequestResponse>> Handle(CreateQuoteRequestCommand request, CancellationToken cancellationToken)
        {
            // 1. Customer kontrolü
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                return new ErrorDataResult<CreateQuoteRequestResponse>("Müşteri bulunamadı.");
            }

            // 2. Vehicle kontrolü (eğer belirtilmişse)
            if (request.VehicleId.HasValue)
            {
                var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId.Value);
                if (vehicle == null)
                {
                    return new ErrorDataResult<CreateQuoteRequestResponse>("Araç bulunamadı.");
                }
                if (vehicle.CustomerId != request.CustomerId)
                {
                    return new ErrorDataResult<CreateQuoteRequestResponse>("Araç bu müşteriye ait değil.");
                }
            }

            // 3. ClientId - Customer'ın ClientId'sini kullan
            var clientId = customer.ClientId;

            // 4. PhotoPaths'i JSON'a çevir
            var photoPathsJson = JsonSerializer.Serialize(request.PhotoPaths ?? new List<string>());

            // 5. QuoteRequest oluştur
            var quoteRequest = new QuoteRequest
            {
                CustomerId = request.CustomerId,
                VehicleId = request.VehicleId,
                PhotoPaths = photoPathsJson,
                Description = request.Description,
                Status = QuoteStatus.Open,
                EstimatedCost = request.EstimatedCost,
                EstimatedDescription = request.EstimatedDescription,
                ClientId = clientId
            };

            await _quoteRequestRepository.AddAsync(quoteRequest, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new CreateQuoteRequestResponse
            {
                QuoteRequestId = quoteRequest.Id,
                CustomerId = quoteRequest.CustomerId,
                Message = "Fiyat teklifi isteği başarıyla oluşturuldu."
            };

            return new SuccessDataResult<CreateQuoteRequestResponse>(response, response.Message);
        }
    }
}
