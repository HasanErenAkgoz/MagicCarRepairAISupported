using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Features.Customers.Queries.GetById;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Exceptions;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.GetById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(
            ICustomerRepository customerRepository,
            IWorkOrderRepository workOrderRepository,
            ITenantService tenantService,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _workOrderRepository = workOrderRepository;
            _tenantService = tenantService;
            _mapper = mapper;
        }

        public async Task<GetCustomerByIdResponse> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Müşteriyi bul (Vehicle'ları da dahil et)
            var customer = await _customerRepository.Query()
                .Include(c => c.Vehicles)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (customer == null || customer.Status == Status.Deleted)
            {
                throw new DomainException("CUSTOMER_NOT_FOUND", new { Id = request.Id });
            }

            // ClientId kontrolü
            if (customer.ClientId != clientId)
            {
                throw new DomainException("CUSTOMER_NOT_BELONG_TO_CLIENT", new { CustomerId = request.Id });
            }

            // Response
            var response = _mapper.Map<GetCustomerByIdResponse>(customer);
            response.FullName = customer.FullName;
            response.Age = customer.GetAge();
            response.IsBirthdayToday = customer.IsBirthdayToday();
            response.VehicleCount = customer.Vehicles?.Count ?? 0;

            // WorkOrders sayımları
            var activeStatuses = new[]
            {
                WorkOrderStatus.VehicleEntered,
                WorkOrderStatus.InProgress,
                WorkOrderStatus.InRepair,
                WorkOrderStatus.WaitingForParts,
                WorkOrderStatus.DiagnosisCompleted,
                WorkOrderStatus.QualityControl,
                WorkOrderStatus.Washing,
                WorkOrderStatus.ReadyForDelivery
            };

            var allWorkOrders = await _workOrderRepository.Query()
                .Where(wo => wo.CustomerId == customer.Id && wo.ClientId == clientId)
                .ToListAsync(cancellationToken);

            response.TotalWorkOrders = allWorkOrders.Count;
            response.ActiveWorkOrders = allWorkOrders.Count(wo => activeStatuses.Contains(wo.Status));

            // Vehicles listesi
            if (customer.Vehicles != null && customer.Vehicles.Any())
            {
                response.Vehicles = customer.Vehicles.Select(v => new CustomerVehicleSummary
                {
                    Id = v.Id,
                    LicensePlate = v.LicensePlate,
                    Brand = v.Brand,
                    Model = v.Model,
                    Year = v.Year,
                    Color = v.Color,
                    FuelType = v.FuelType
                }).ToList();
            }

            return response;
        }
    }
}

