using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.ClientPortal.Queries.GetPublicStatistics
{
    public class GetPublicStatisticsQueryHandler : IRequestHandler<GetPublicStatisticsQuery, IDataResult<GetPublicStatisticsResponse>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IServicePortfolioRepository _portfolioRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICertificateRepository _certificateRepository;
        private readonly IServiceRatingRepository _ratingRepository;

        public GetPublicStatisticsQueryHandler(
            IClientRepository clientRepository,
            IWorkOrderRepository workOrderRepository,
            IServicePortfolioRepository portfolioRepository,
            ICustomerRepository customerRepository,
            IEmployeeRepository employeeRepository,
            ICertificateRepository certificateRepository,
            IServiceRatingRepository ratingRepository)
        {
            _clientRepository = clientRepository;
            _workOrderRepository = workOrderRepository;
            _portfolioRepository = portfolioRepository;
            _customerRepository = customerRepository;
            _employeeRepository = employeeRepository;
            _certificateRepository = certificateRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<IDataResult<GetPublicStatisticsResponse>> Handle(GetPublicStatisticsQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetAsync(c => c.Id == request.ClientId, cancellationToken);
            if (client == null || !client.IsActive)
            {
                return new ErrorDataResult<GetPublicStatisticsResponse>("Client profile not found or inactive");
            }

            var completedWorkOrders = await _workOrderRepository.GetListAsync(
                cancellationToken,
                wo => wo.ClientId == request.ClientId && wo.Status == WorkOrderStatus.Delivered);

            var portfolios = await _portfolioRepository.GetPublishedPortfoliosAsync(request.ClientId, cancellationToken);

            var activeCustomers = await _customerRepository.GetListAsync(
                cancellationToken,
                c => c.ClientId == request.ClientId && c.Status == Status.Active);

            var teamMembers = await _employeeRepository.GetListAsync(
                cancellationToken,
                e => e.ClientId == request.ClientId && e.IsPublic && e.EmploymentStatus == EmploymentStatus.Active);

            var certificates = await _certificateRepository.GetActiveCertificatesAsync(request.ClientId, cancellationToken);

            var allRatings = await _ratingRepository.GetByClientIdAsync(request.ClientId, cancellationToken);
            var ratings = allRatings.Where(r => r.Status == RatingStatus.Approved).ToList();

            var avgRating = ratings.Any() ? (decimal)ratings.Average(r => r.Rating) : 0;

            var yearsOfExperience = client.SubscriptionStartDate.HasValue
                ? DateTime.UtcNow.Year - client.SubscriptionStartDate.Value.Year
                : 0;

            var response = new GetPublicStatisticsResponse
            {
                TotalCompletedWorkOrders = completedWorkOrders.Count(),
                TotalPortfolioItems = portfolios.Count(),
                TotalActiveCustomers = activeCustomers.Count(),
                TotalTeamMembers = teamMembers.Count(),
                TotalCertificates = certificates.Count(),
                AverageRating = avgRating,
                TotalRatings = ratings.Count(),
                YearsOfExperience = yearsOfExperience,
                EstablishedDate = client.SubscriptionStartDate
            };

            return new SuccessDataResult<GetPublicStatisticsResponse>(response);
        }
    }
}
