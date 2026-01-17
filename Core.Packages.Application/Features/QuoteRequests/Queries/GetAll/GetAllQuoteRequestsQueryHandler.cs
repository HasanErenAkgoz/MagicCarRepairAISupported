using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetAll
{
    public class GetAllQuoteRequestsQueryHandler : IRequestHandler<GetAllQuoteRequestsQuery, IDataResult<List<GetAllQuoteRequestsResponse>>>
    {
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetAllQuoteRequestsQueryHandler(
            IQuoteRequestRepository quoteRequestRepository,
            IMapper mapper,
            ITenantService tenantService)
        {
            _quoteRequestRepository = quoteRequestRepository;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IDataResult<List<GetAllQuoteRequestsResponse>>> Handle(GetAllQuoteRequestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clientId = _tenantService.GetCurrentClientId();

                List<Domain.Entities.QuoteRequest> quoteRequests;

                if (request.Status.HasValue)
                {
                    quoteRequests = await _quoteRequestRepository.GetQuoteRequestsByStatusAsync(request.Status.Value, clientId, cancellationToken);
                }
                else
                {
                    // Tüm talepleri getir (repository'de Query() kullanarak)
                    var query = _quoteRequestRepository.Query()
                        .Include(qr => qr.Customer)
                        .Include(qr => qr.Vehicle)
                        .Include(qr => qr.QuoteResponses)
                        .AsQueryable();

                    if (request.RequestType.HasValue)
                    {
                        query = query.Where(qr => qr.RequestType == request.RequestType.Value);
                    }

                    if (request.CustomerId.HasValue)
                    {
                        query = query.Where(qr => qr.CustomerId == request.CustomerId.Value);
                    }

                    if (request.ClientId.HasValue)
                    {
                        query = query.Where(qr => qr.ClientId == request.ClientId.Value);
                    }

                    quoteRequests = await query
                        .OrderByDescending(qr => qr.CreatedDate)
                        .Skip((request.PageNumber - 1) * request.PageSize)
                        .Take(request.PageSize)
                        .ToListAsync(cancellationToken);
                }

                var response = _mapper.Map<List<GetAllQuoteRequestsResponse>>(quoteRequests);
                return new SuccessDataResult<List<GetAllQuoteRequestsResponse>>(response);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<GetAllQuoteRequestsResponse>>(ex.Message);
            }
        }
    }
}

