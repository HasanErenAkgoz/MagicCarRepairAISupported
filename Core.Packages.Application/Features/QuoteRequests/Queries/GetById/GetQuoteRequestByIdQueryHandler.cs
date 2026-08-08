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
        private readonly IShopTrustService _trustService;
        private readonly IMapper _mapper;
        private readonly ITenantService _tenantService;

        public GetQuoteRequestByIdQueryHandler(
            IQuoteRequestRepository quoteRequestRepository,
            IShopTrustService trustService,
            IMapper mapper,
            ITenantService tenantService)
        {
            _quoteRequestRepository = quoteRequestRepository;
            _trustService = trustService;
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

                // Sprint 4: Trust metrikleri al (çapraz tenant sorgusu)
                var clientIds = quoteRequest.QuoteResponses.Select(r => r.ClientId).Distinct();
                var trustMap = await _trustService.GetTrustMetricsAsync(clientIds, cancellationToken);

                // QuoteResponses'ı map et + güven / FinalScore hesapla
                var dtos = quoteRequest.QuoteResponses.Select(qres =>
                {
                    var trust = trustMap.GetValueOrDefault(qres.ClientId, ShopTrustMetrics.Empty);
                    var trustScore = ComputeTrustScore(trust);
                    return new QuoteResponseDto
                    {
                        Id = qres.Id,
                        QuoteNumber = qres.QuoteNumber,
                        ClientId = qres.ClientId,
                        ClientName = qres.Client?.Name ?? "Unknown",
                        ClientLogoUrl = qres.Client?.LogoUrl,
                        Description = qres.Description,
                        EstimatedDays = qres.EstimatedDays,
                        QuoteAmount = qres.QuoteAmount,
                        NetAmount = qres.NetAmount,
                        WarrantyMonths = qres.WarrantyMonths,
                        Status = qres.Status,
                        StatusName = qres.Status.ToString(),
                        QuoteDate = qres.QuoteDate,
                        ValidUntilDate = qres.ValidUntilDate,
                        TrustScore = trustScore,
                        AverageRating = trust.AverageRating,
                        ReviewCount = trust.ReviewCount,
                        CompletionRate = trust.CompletionRate,
                        AvgQuoteResponseHours = trust.AvgQuoteResponseHours,
                        FinalScore = ComputeFinalScore((double)qres.NetAmount, trustScore, trust.AvgQuoteResponseHours),
                    };
                }).ToList();

                // FinalScore'a göre sırala ve Rank ata (1 = en iyi)
                var sorted = dtos.OrderBy(d => d.FinalScore).ToList();
                for (int i = 0; i < sorted.Count; i++)
                    sorted[i].Rank = i + 1;

                response.QuoteResponses = sorted;

                // Photos'ı map et (PhotoPaths JSON string'den parse et)
                response.Photos = new List<QuoteRequestPhotoDto>();
                if (!string.IsNullOrEmpty(quoteRequest.PhotoPaths) && quoteRequest.PhotoPaths != "[]")
                {
                    try
                    {
                        var photoPaths = System.Text.Json.JsonSerializer.Deserialize<List<string>>(quoteRequest.PhotoPaths) ?? new List<string>();
                        response.Photos = photoPaths.Select((path, idx) => new QuoteRequestPhotoDto
                        {
                            Id = idx + 1,
                            FilePath = path,
                            UploadDate = quoteRequest.CreatedDate ?? DateTime.UtcNow
                        }).ToList();
                    }
                    catch { /* JSON parse hatası - Photos boş bırak */ }
                }

                return new SuccessDataResult<GetQuoteRequestByIdResponse>(response);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<GetQuoteRequestByIdResponse>(ex.Message);
            }
        }

        /// <summary>TrustScore 0–100. ResolvePartPricesQueryHandler ile aynı formül.</summary>
        private static double ComputeTrustScore(ShopTrustMetrics m)
        {
            var ratingScore      = (m.AverageRating / 5.0) * 40;
            var reviewScore      = Math.Min(m.ReviewCount / 50.0, 1.0) * 20;
            var completionScore  = m.CompletionRate * 30;
            var responseScore    = m.AvgQuoteResponseHours.HasValue
                ? Math.Max(0, 1 - m.AvgQuoteResponseHours.Value / 48.0) * 10
                : 0;
            return Math.Round(ratingScore + reviewScore + completionScore + responseScore, 1);
        }

        /// <summary>FinalScore: düşük = daha iyi. Fiyat normalleştirmesi hesap anında yapılmadığı için
        /// güven ve hız bileşenleri negatif ağırlıkla ters düzeltilir.</summary>
        private static double ComputeFinalScore(double price, double trustScore, double? avgResponseHours)
        {
            var priceNorm    = price / 10_000.0;               // ~₺10k başvuru birimi
            var trustPenalty = (100 - trustScore) / 100.0 * 3; // güven azaldıkça ceza artar
            var speedPenalty = avgResponseHours.HasValue
                ? Math.Min(avgResponseHours.Value / 48.0, 1.0) * 1
                : 0.5;
            return priceNorm + trustPenalty + speedPenalty;
        }
    }
}

