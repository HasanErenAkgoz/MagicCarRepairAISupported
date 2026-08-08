using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetOpen
{
    public class GetOpenQuoteRequestsQueryHandler : IRequestHandler<GetOpenQuoteRequestsQuery, IDataResult<List<GetOpenQuoteRequestsResponse>>>
    {
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetOpenQuoteRequestsQueryHandler(
            IQuoteRequestRepository quoteRequestRepository,
            IMapper mapper,
            ITenantService tenantService)
        {
            _quoteRequestRepository = quoteRequestRepository;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IDataResult<List<GetOpenQuoteRequestsResponse>>> Handle(GetOpenQuoteRequestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId();
                var quoteRequests = await _quoteRequestRepository.GetOpenQuoteRequestsAsync(clientId, cancellationToken);

                // Pagination
                var pagedRequests = quoteRequests
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                var response = pagedRequests.Select(qr => new GetOpenQuoteRequestsResponse
                {
                    Id = qr.Id,
                    RequestNumber = qr.RequestNumber,
                    CustomerName = qr.Customer != null ? qr.Customer.FullName : qr.CustomerName,
                    VehicleInfo = qr.Vehicle != null 
                        ? $"{qr.Vehicle.Brand} {qr.Vehicle.Model} ({qr.Vehicle.LicensePlate})"
                        : $"{qr.VehicleBrand} {qr.VehicleModel} ({qr.VehicleLicensePlate})",
                    ProblemDescription = qr.ProblemDescription,
                    RequestType = qr.RequestType,
                    UrgencyLevel = qr.UrgencyLevel,
                    QuoteDeadline = qr.QuoteDeadline,
                    CreatedDate = qr.CreatedDate,
                    DaysRemaining = (int)(qr.QuoteDeadline - DateTime.UtcNow).TotalDays,
                    EstimatedCostMin = qr.EstimatedCostMin,
                    EstimatedCostMax = qr.EstimatedCostMax,
                }).ToList();

                return new SuccessDataResult<List<GetOpenQuoteRequestsResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<GetOpenQuoteRequestsResponse>>(ex.Message);
            }
        }
    }
}

