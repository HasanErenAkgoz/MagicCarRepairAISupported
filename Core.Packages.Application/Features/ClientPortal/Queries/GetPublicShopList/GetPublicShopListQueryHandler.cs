using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicShopList
{
    public class GetPublicShopListQueryHandler : IRequestHandler<GetPublicShopListQuery, IDataResult<List<PublicShopDto>>>
    {
        private readonly IClientRepository _clientRepository;

        public GetPublicShopListQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IDataResult<List<PublicShopDto>>> Handle(GetPublicShopListQuery request, CancellationToken cancellationToken)
        {
            // Keşif: onaylı (aktif) tamirhaneler — IsPublicProfileEnabled veri tutarsızlığında liste boş kalmasın
            var query = _clientRepository.Query()
                .Where(c => c.IsActive == true && c.IsPublicProfileEnabled == true);

            // Search term filtresi
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(c => 
                    c.Name.ToLower().Contains(searchTerm) ||
                    c.Code.ToLower().Contains(searchTerm) ||
                    (c.Description != null && c.Description.ToLower().Contains(searchTerm)) ||
                    (c.Address != null && c.Address.ToLower().Contains(searchTerm)));
            }

            // City filtresi (Address içinde arama)
            if (!string.IsNullOrWhiteSpace(request.City))
            {
                var city = request.City.ToLower();
                query = query.Where(c => c.Address != null && c.Address.ToLower().Contains(city));
            }

            var clients = await query
                .OrderBy(c => c.Name)
                .ToListAsync(cancellationToken);

            var shopList = clients.Select(c => new PublicShopDto
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
                Description = c.Description,
                ContactEmail = c.ContactEmail,
                ContactPhone = c.ContactPhone,
                Address = c.Address,
                LogoUrl = c.LogoUrl,
                WebsiteUrl = c.WebsiteUrl,
                WorkingHours = c.WorkingHours,
                Services = c.Services,
                AboutUs = c.AboutUs
            }).ToList();

            return new SuccessDataResult<List<PublicShopDto>>(shopList, "Tamirhane listesi başarıyla getirildi.");
        }
    }
}
