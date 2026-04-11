using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicProfile
{
    public class GetPublicProfileQueryHandler : IRequestHandler<GetPublicProfileQuery, IDataResult<GetPublicProfileResponse>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IServicePortfolioRepository _portfolioRepository;
        private readonly ICertificateRepository _certificateRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IServiceRatingRepository _ratingRepository;

        public GetPublicProfileQueryHandler(
            IClientRepository clientRepository,
            IServicePortfolioRepository portfolioRepository,
            ICertificateRepository certificateRepository,
            IEmployeeRepository employeeRepository,
            IServiceRatingRepository ratingRepository)
        {
            _clientRepository = clientRepository;
            _portfolioRepository = portfolioRepository;
            _certificateRepository = certificateRepository;
            _employeeRepository = employeeRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<IDataResult<GetPublicProfileResponse>> Handle(GetPublicProfileQuery request, CancellationToken cancellationToken)
        {
            var client = default(Domain.Entities.Client);

            if (request.ClientId.HasValue)
            {
                client = await _clientRepository.GetAsync(c => c.Id == request.ClientId.Value, cancellationToken);
            }
            else if (!string.IsNullOrEmpty(request.ClientCode))
            {
                client = await _clientRepository.GetAsync(c => c.Code == request.ClientCode, cancellationToken);
            }
            else
            {
                return new ErrorDataResult<GetPublicProfileResponse>("Client ID or Client Code is required");
            }

            if (client == null || !client.IsActive)
            {
                return new ErrorDataResult<GetPublicProfileResponse>("Client profile not found or inactive");
            }

            // Get statistics
            var portfolios = await _portfolioRepository.GetPublishedPortfoliosAsync(client.Id, cancellationToken);
            var certificates = await _certificateRepository.GetActiveCertificatesAsync(client.Id, cancellationToken);
            var employees = await _employeeRepository.GetListAsync(cancellationToken, e => e.ClientId == client.Id && e.IsPublic && e.EmploymentStatus == Domain.Enums.EmploymentStatus.Active);
            
            var allRatings = await _ratingRepository.GetByClientIdAsync(client.Id, cancellationToken);
            var ratings = allRatings.Where(r => r.Status == Domain.Enums.RatingStatus.Approved).ToList();
            var avgRating = ratings.Any() ? (decimal)ratings.Average(r => r.Rating) : 0;

            var response = new GetPublicProfileResponse
            {
                Id = client.Id,
                Name = client.Name,
                LogoUrl = client.LogoUrl,
                WebsiteUrl = client.WebsiteUrl,
                AboutUs = client.AboutUs,
                WorkingHours = client.WorkingHours,
                Services = client.Services,
                SocialMediaLinks = client.SocialMediaLinks,
                ContactEmail = client.ContactEmail,
                ContactPhone = client.ContactPhone,
                Address = client.Address,
                TotalPortfolioItems = portfolios.Count,
                TotalCertificates = certificates.Count,
                TotalTeamMembers = employees.Count(),
                AverageRating = avgRating,
                TotalRatings = ratings.Count()
            };

            return new SuccessDataResult<GetPublicProfileResponse>(response);
        }
    }
}
