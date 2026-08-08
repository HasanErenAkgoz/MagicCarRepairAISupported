using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetById
{
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, GetVehicleByIdResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IInsurancePolicyRepository _insurancePolicyRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetVehicleByIdQueryHandler(
            IVehicleRepository vehicleRepository,
            IInsurancePolicyRepository insurancePolicyRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _insurancePolicyRepository = insurancePolicyRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<GetVehicleByIdResponse> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            // Aracı bul (Customer ve Photos dahil)
            var vehicle = await _vehicleRepository.Query()
                .Include(v => v.Customer)
                .Include(v => v.Photos)
                    .ThenInclude(p => p.UploadedFile)
                .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

            if (vehicle == null)
            {
                throw new DomainException("VEHICLE_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (vehicle.ClientId != clientId)
            {
                throw new DomainException("VEHICLE_NOT_BELONG_TO_CLIENT", new { VehicleId = request.Id });
            }

            // Response
            var response = _mapper.Map<GetVehicleByIdResponse>(vehicle);
            response.StatusName = vehicle.Status.ToString();
            response.VehicleTypeName = vehicle.VehicleType.ToString();
            response.CustomerName = vehicle.Customer?.FullName ?? "Unknown";

            // Fotoğrafları map et
            response.Photos = (vehicle.Photos ?? Enumerable.Empty<Domain.Entities.VehiclePhoto>())
                .OrderBy(p => p.DisplayOrder)
                .Select(p => new VehiclePhotoDto
                {
                    Id = p.Id,
                    FilePath = !string.IsNullOrWhiteSpace(p.FilePath) ? p.FilePath : (p.UploadedFile?.FilePath ?? string.Empty),
                    MediaUrl = $"/api/media/vehicles/{vehicle.Id}/photos/{p.Id}",
                    PhotoType = p.PhotoType,
                    Description = p.Description,
                    DisplayOrder = p.DisplayOrder,
                    UploadDate = p.UploadDate,
                })
                .ToList();

            // Sigorta/Kasko poliçelerini çek
            var policies = await _insurancePolicyRepository.GetByVehicleIdAsync(request.Id, cancellationToken);
            response.InsurancePolicies = policies
                .Select(p => new InsurancePolicyDto
                {
                    Id = p.Id,
                    PolicyNumber = p.PolicyNumber,
                    InsuranceCompanyName = p.InsuranceCompany?.CompanyName ?? "N/A",
                    InsuranceType = p.InsuranceType,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    PremiumAmount = p.PremiumAmount,
                    Status = p.Status,
                    DaysUntilExpiration = p.DaysUntilExpiration()
                })
                .ToList();

            return response;
        }
    }
}
