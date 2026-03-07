using AutoMapper;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Vehicles.Queries.GetAll
{
    public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, List<GetAllVehiclesResponse>>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public GetAllVehiclesQueryHandler(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllVehiclesResponse>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            // Query oluştur (Customer'ı da dahil et)
            IQueryable<Vehicle> query = _vehicleRepository.Query()
                .Include(v => v.Customer);

            // Müşteri filtresi
            if (request.CustomerId.HasValue)
            {
                query = query.Where(v => v.CustomerId == request.CustomerId.Value);
            }

            // Arama filtresi
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(v =>
                    v.LicensePlate.ToLower().Contains(searchTerm) ||
                    v.Brand.ToLower().Contains(searchTerm) ||
                    v.Model.ToLower().Contains(searchTerm));
            }

            // Pagination
            if (request.PageNumber.HasValue && request.PageSize.HasValue)
            {
                query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                             .Take(request.PageSize.Value);
            }

            // Çalıştır ve map et
            var vehicles = await query
                .OrderBy(v => v.Brand)
                .ThenBy(v => v.Model)
                .ToListAsync(cancellationToken);

            var response = _mapper.Map<List<GetAllVehiclesResponse>>(vehicles);

            // Enum isimleri ve müşteri adı ekle
            foreach (var item in response)
            {
                var vehicle = vehicles.First(v => v.Id == item.Id);
                item.StatusName = vehicle.Status.ToString();
                item.VehicleTypeName = vehicle.VehicleType.ToString();
                item.CustomerName = vehicle.Customer?.FullName ?? "Unknown";
            }

            return response;
        }
    }
}

