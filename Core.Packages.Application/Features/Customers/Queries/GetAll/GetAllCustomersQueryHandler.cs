using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.GetAll
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<GetAllCustomersResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper, ITenantService tenantService)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<List<GetAllCustomersResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var clientId = _tenantService.GetCurrentClientId() ?? 1;

            // Query oluştur — her zaman clientId filtresi uygula (dashboard ile tutarlı)
            var query = _customerRepository.Query()
                .Where(c => c.ClientId == clientId);

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

            // Age hesapla (güvenli)
            foreach (var item in response)
            {
                var customer = customers.FirstOrDefault(c => c.Id == item.Id);
                if (customer != null)
                {
                    try { item.Age = customer.GetAge(); }
                    catch { item.Age = 0; }
                }
            }

            return response;
        }
    }
}

