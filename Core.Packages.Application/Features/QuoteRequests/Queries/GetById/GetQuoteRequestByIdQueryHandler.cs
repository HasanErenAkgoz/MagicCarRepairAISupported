using AutoMapper;
using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetById
{
    public class GetQuoteRequestByIdQueryHandler : IRequestHandler<GetQuoteRequestByIdQuery, IDataResult<GetQuoteRequestByIdResponse>>
    {
        private readonly IQuoteRequestRepository _quoteRequestRepository;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetQuoteRequestByIdQueryHandler(
            IQuoteRequestRepository quoteRequestRepository,
            IMapper mapper,
            ITenantService tenantService)
        {
            _quoteRequestRepository = quoteRequestRepository;
            _mapper = mapper;
            _tenantService = tenantService;
        }

        public async Task<IDataResult<GetQuoteRequestByIdResponse>> Handle(GetQuoteRequestByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var quoteRequest = await _quoteRequestRepository.GetQuoteRequestDetailsAsync(request.Id, cancellationToken);
                if (quoteRequest == null)
                {
                    return new ErrorDataResult<GetQuoteRequestByIdResponse>("Quote request not found");
                }

                var response = _mapper.Map<GetQuoteRequestByIdResponse>(quoteRequest);

                // QuoteResponses'ı map et
                response.QuoteResponses = quoteRequest.QuoteResponses.Select(qres => new QuoteResponseDto
                {
                    Id = qres.Id,
                    QuoteNumber = qres.QuoteNumber,
                    ClientName = qres.Client?.Name ?? "Unknown",
                    Description = qres.Description,
                    EstimatedDays = qres.EstimatedDays,
                    QuoteAmount = qres.QuoteAmount,
                    NetAmount = qres.NetAmount,
                    WarrantyMonths = qres.WarrantyMonths,
                    Status = qres.Status,
                    StatusName = qres.Status.ToString(),
                    QuoteDate = qres.QuoteDate,
                    ValidUntilDate = qres.ValidUntilDate
                }).ToList();

                // Photos'ı map et
                response.Photos = quoteRequest.Photos.Select(photo => new QuoteRequestPhotoDto
                {
                    Id = photo.Id,
                    FilePath = photo.FilePath,
                    Description = photo.Description,
                    PhotoType = photo.PhotoType,
                    PhotoTypeName = photo.PhotoType.ToString(),
                    UploadDate = photo.UploadDate
                }).ToList();

                return new SuccessDataResult<GetQuoteRequestByIdResponse>(response);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetQuoteRequestByIdResponse>(ex.Message);
            }
        }
    }
}

