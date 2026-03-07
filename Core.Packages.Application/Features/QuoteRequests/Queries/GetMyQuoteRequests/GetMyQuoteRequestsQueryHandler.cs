using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;
using System.Text.Json;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetMyQuoteRequests
{
    public class GetMyQuoteRequestsQueryHandler : IRequestHandler<GetMyQuoteRequestsQuery, IDataResult<List<QuoteRequestDto>>>
    {
        private readonly IQuoteRequestRepository _quoteRequestRepository;

        public GetMyQuoteRequestsQueryHandler(IQuoteRequestRepository quoteRequestRepository)
        {
            _quoteRequestRepository = quoteRequestRepository;
        }

        public async Task<IDataResult<List<QuoteRequestDto>>> Handle(GetMyQuoteRequestsQuery request, CancellationToken cancellationToken)
        {
            var quoteRequests = await _quoteRequestRepository.GetQuoteRequestsByCustomerAsync(request.CustomerId, cancellationToken);

            var dtos = quoteRequests.Select(qr =>
            {
                List<string> photoPaths = new List<string>();
                try
                {
                    if (!string.IsNullOrEmpty(qr.PhotoPaths))
                    {
                        photoPaths = JsonSerializer.Deserialize<List<string>>(qr.PhotoPaths) ?? new List<string>();
                    }
                }
                catch
                {
                    photoPaths = new List<string>();
                }

                return new QuoteRequestDto
                {
                    Id = qr.Id,
                    CustomerId = qr.CustomerId,
                    VehicleId = qr.VehicleId,
                    PhotoPaths = photoPaths,
                    Description = qr.Description,
                    Status = qr.Status.ToString(), // QuoteStatus enum
                    EstimatedCost = qr.EstimatedCost,
                    EstimatedDescription = qr.EstimatedDescription,
                    CreatedDate = qr.CreatedDate ?? DateTime.UtcNow
                };
            }).ToList();

            return new SuccessDataResult<List<QuoteRequestDto>>(dtos, "Fiyat teklifi istekleri başarıyla getirildi.");
        }
    }
}
