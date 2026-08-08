using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.GetAll
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<GetAllCustomersResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetAllCustomersQueryHandler(
            ICustomerRepository customerRepository, 
            IWorkOrderRepository workOrderRepository,
            IMapper mapper, 
            ITenantService tenantService)
        {
            _customerRepository = customerRepository;
            _workOrderRepository = workOrderRepository;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<List<GetAllCustomersResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetRequiredClientId();

            // Query oluştur — her zaman clientId filtresi uygula (dashboard ile tutarlı)
            // WorkOrders ve Vehicles ilişkilerini dahil et (sayım için)
            var query = _customerRepository.Query()
                .Where(c => c.ClientId == clientId && c.Status != Status.Deleted);

            // Arama filtresi
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(c =>
                    c.FirstName.ToLower().Contains(searchTerm) ||
                    c.LastName.ToLower().Contains(searchTerm) ||
                    c.Email.ToLower().Contains(searchTerm) ||
                    c.PhoneNumber.Contains(searchTerm) ||
                    (c.IdentityNo != null && c.IdentityNo.Contains(searchTerm)));
            }

            // Pagination
            if (request.PageNumber.HasValue && request.PageSize.HasValue)
            {
                query = query.Skip((request.PageNumber.Value - 1) * request.PageSize.Value)
                             .Take(request.PageSize.Value);
            }

            // Çalıştır ve map et
            var customers = await query
                .OrderBy(c => c.FirstName)
                .ThenBy(c => c.LastName)
                .ToListAsync(cancellationToken);

            var response = _mapper.Map<List<GetAllCustomersResponse>>(customers);

            // Age ve sayımları hesapla (güvenli)
            foreach (var item in response)
            {
                var customer = customers.FirstOrDefault(c => c.Id == item.Id);
                if (customer != null)
                {
                    try { item.Age = customer.GetAge(); }
                    catch { item.Age = 0; }
                }
            }

            // WorkOrders ve Vehicles sayımlarını hesapla
            // Performans için ayrı sorgu ile toplu hesaplama
            var customerIds = response.Select(r => r.Id).ToList();
            
            // Active work order statuses — use List<T> (not T[]) so Contains does not bind to ReadOnlySpan
            // overloads that break EF Core expression compilation on .NET 9+.
            var activeStatuses = new List<WorkOrderStatus>
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

            // WorkOrders sayımları - WorkOrder repository üzerinden
            var workOrderCounts = await _workOrderRepository.Query()
                .Where(wo => customerIds.Contains(wo.CustomerId) && wo.ClientId == clientId)
                .GroupBy(wo => wo.CustomerId)
                .Select(g => new
                {
                    CustomerId = g.Key,
                    TotalWorkOrders = g.Count(),
                    ActiveWorkOrders = g.Count(wo => activeStatuses.Contains(wo.Status))
                })
                .ToListAsync(cancellationToken);

            // Vehicles sayımları
            var vehicleCounts = await _customerRepository.Query()
                .Where(c => customerIds.Contains(c.Id))
                .Select(c => new
                {
                    CustomerId = c.Id,
                    VehicleCount = c.Vehicles.Count
                })
                .ToListAsync(cancellationToken);

            // Response'a sayımları ekle
            foreach (var item in response)
            {
                var woCount = workOrderCounts.FirstOrDefault(w => w.CustomerId == item.Id);
                var vCount = vehicleCounts.FirstOrDefault(v => v.CustomerId == item.Id);
                
                item.TotalWorkOrders = woCount?.TotalWorkOrders ?? 0;
                item.ActiveWorkOrders = woCount?.ActiveWorkOrders ?? 0;
                item.VehicleCount = vCount?.VehicleCount ?? 0;
            }

            return response;
        }
    }
}

