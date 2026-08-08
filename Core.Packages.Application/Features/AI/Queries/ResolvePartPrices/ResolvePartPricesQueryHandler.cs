using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Repositories;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.ResolvePartPrices
{
    public class ResolvePartPricesQueryHandler
        : IRequestHandler<ResolvePartPricesQuery, IDataResult<ResolvePartPricesResponse>>
    {
        private readonly IPartRepository _partRepository;
        private readonly IShopTrustService _trustService;

        public ResolvePartPricesQueryHandler(
            IPartRepository partRepository,
            IShopTrustService trustService)
        {
            _partRepository = partRepository;
            _trustService   = trustService;
        }

        public async Task<IDataResult<ResolvePartPricesResponse>> Handle(
            ResolvePartPricesQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.VehicleBrand) ||
                string.IsNullOrWhiteSpace(request.VehicleModel) ||
                request.VehicleYear < 1900 ||
                request.PartNames.Count == 0)
            {
                return new ErrorDataResult<ResolvePartPricesResponse>("Araç bilgileri ve parça adları zorunludur.");
            }

            // 1. Çapraz tenant envanter araması
            var matches = await _partRepository.SearchCrossTenantAsync(
                request.VehicleBrand,
                request.VehicleModel,
                request.VehicleYear,
                request.PartNames,
                cancellationToken);

            var vehicleLabel = $"{request.VehicleBrand} {request.VehicleModel} {request.VehicleYear}";

            if (matches.Count == 0)
                return new SuccessDataResult<ResolvePartPricesResponse>(
                    new ResolvePartPricesResponse { VehicleLabel = vehicleLabel });

            // 2. Güven metrikleri — ilgili clientId'ler için bir kez
            var clientIds = matches.Select(m => m.ClientId).Distinct().ToList();
            var trustMap  = await _trustService.GetTrustMetricsAsync(clientIds, cancellationToken);

            // 3. Parça adı bazında grupla ve teklifleri oluştur
            var response = new ResolvePartPricesResponse { VehicleLabel = vehicleLabel };

            foreach (var partName in request.PartNames)
            {
                var partNameLower = partName.ToLower();
                var partMatches   = matches.Where(m => m.SearchedPartName == partNameLower).ToList();
                if (partMatches.Count == 0) continue;

                var offers = new List<ShopPriceOffer>();

                foreach (var m in partMatches)
                {
                    var trust     = trustMap.GetValueOrDefault(m.ClientId) ?? ShopTrustMetrics.Empty;
                    var distKm    = HaversineKm(request.UserLatitude, request.UserLongitude,
                                                m.ShopLatitude, m.ShopLongitude);

                    if (request.RadiusKm.HasValue && distKm.HasValue && distKm.Value > request.RadiusKm.Value)
                        continue;

                    var trustScore = ComputeTrustScore(trust);
                    offers.Add(new ShopPriceOffer
                    {
                        ClientId              = m.ClientId,
                        ShopName              = m.ShopName,
                        ShopLogoUrl           = m.ShopLogoUrl,
                        PartId                = m.PartId,
                        PartCode              = m.PartCode,
                        OemNumber             = m.OemNumber,
                        Price                 = m.SalePrice,
                        StockQuantity         = m.StockQuantity,
                        MatchScore            = m.MatchScore,
                        AverageRating         = trust.AverageRating,
                        ReviewCount           = trust.ReviewCount,
                        CompletionRate        = trust.CompletionRate,
                        AvgQuoteResponseHours = trust.AvgQuoteResponseHours,
                        TrustScore            = trustScore,
                        DistanceKm            = distKm,
                        FinalScore            = ComputeFinalScore(m.SalePrice, trustScore, distKm,
                                                                  trust.AvgQuoteResponseHours)
                    });
                }

                offers.Sort((a, b) => a.FinalScore.CompareTo(b.FinalScore));

                response.Parts.Add(new PartPriceOptions { PartName = partName, Offers = offers });
            }

            return new SuccessDataResult<ResolvePartPricesResponse>(response);
        }

        // ── Hesaplama yardımcıları ──────────────────────────────────────

        /// <summary>
        /// TrustScore = 0–100.
        /// Rating %35 · Tamamlanma %25 · Yanıt hızı %20 · Yorum sayısı %20
        /// </summary>
        private static double ComputeTrustScore(ShopTrustMetrics m)
        {
            var ratingPart     = (m.AverageRating / 5d) * 35d;
            var completionPart = m.CompletionRate * 25d;
            var speedPart      = m.AvgQuoteResponseHours.HasValue
                ? Math.Max(0d, 20d - (m.AvgQuoteResponseHours.Value / 24d) * 20d)
                : 10d;
            var reviewPart     = Math.Min(20d, (m.ReviewCount / 50d) * 20d);
            return Math.Round(ratingPart + completionPart + speedPart + reviewPart, 1);
        }

        /// <summary>
        /// FinalScore: düşük = daha iyi teklif.
        /// Ağırlıklar: Fiyat %40, TrustScore cezası %30, Mesafe %20, Hız %10
        /// </summary>
        private static double ComputeFinalScore(
            decimal price, double trustScore, double? distanceKm, double? avgResponseHours)
        {
            var pricePart    = (double)price * 0.40d;
            var trustPenalty = (100d - trustScore) * 50d * 0.30d;
            var distPenalty  = (distanceKm ?? 50d) * 100d * 0.20d;
            var speedPenalty = (avgResponseHours ?? 12d) * 10d * 0.10d;
            return pricePart + trustPenalty + distPenalty + speedPenalty;
        }

        private static double? HaversineKm(
            double? lat1, double? lon1, double? lat2, double? lon2)
        {
            if (!lat1.HasValue || !lon1.HasValue || !lat2.HasValue || !lon2.HasValue) return null;
            const double R   = 6371d;
            var dLat         = ToRad(lat2.Value - lat1.Value);
            var dLon         = ToRad(lon2.Value - lon1.Value);
            var a            = Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
                             + Math.Cos(ToRad(lat1.Value)) * Math.Cos(ToRad(lat2.Value))
                             * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            return Math.Round(R * 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a)), 1);
        }

        private static double ToRad(double deg) => deg * Math.PI / 180d;
    }
}
