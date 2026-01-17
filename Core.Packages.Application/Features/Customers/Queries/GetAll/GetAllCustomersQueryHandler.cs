using AutoMapper;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.Customers.Queries.GetAll
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<GetAllCustomersResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllCustomersResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            // Query oluştur
            var query = _customerRepository.Query();

            // Arama filtresi
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(c =>
                    c.FirstName.ToLower().Contains(searchTerm) ||
                    c.LastName.ToLower().Contains(searchTerm) ||
                    c.Email.ToLower().Contains(searchTerm) ||
                    c.PhoneNumber.Contains(searchTerm) ||
                    c.IdentityNo.Contains(searchTerm));
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

            // Age hesapla
            foreach (var item in response)
            {
                var customer = customers.First(c => c.Id == item.Id);
                item.Age = customer.GetAge();
            }

            return response;
        }
    }
}

