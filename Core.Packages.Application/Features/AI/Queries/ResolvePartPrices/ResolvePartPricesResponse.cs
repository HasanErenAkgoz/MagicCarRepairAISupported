using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.AI.Queries.ResolvePartPrices
{
    public class ResolvePartPricesResponse
    {
        public List<PartPriceOptions> Parts { get; set; } = new();
        public string VehicleLabel { get; set; } = string.Empty;
    }

    /// <summary>Bir parça için tüm servislerden toplanan fiyat seçenekleri.</summary>
    public class PartPriceOptions
    {
        public string PartName { get; set; } = string.Empty;
        public List<ShopPriceOffer> Offers { get; set; } = new();
    }

    /// <summary>Tek bir servisten gelen fiyat teklifi + güven + sıralama skoru.</summary>
    public class ShopPriceOffer
    {
        public int ClientId { get; set; }
        public string ShopName { get; set; } = string.Empty;
        public string? ShopLogoUrl { get; set; }

        // Parça bilgisi
        public int PartId { get; set; }
        public string? PartCode { get; set; }
        public string? OemNumber { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public PartMatchScore MatchScore { get; set; }

        // Servis güven metrikleri
        public double AverageRating { get; set; }      // 0–5
        public int ReviewCount { get; set; }
        public double CompletionRate { get; set; }     // 0–1 (tamamlanan/toplam iş emri)
        public double? AvgQuoteResponseHours { get; set; }
        public double TrustScore { get; set; }         // 0–100

        // Konum
        public double? DistanceKm { get; set; }

        /// <summary>
        /// Genel sıralama skoru: fiyat + güven + mesafe + hız.
        /// Düşük = daha iyi (küçük fiyat + yüksek güven + yakın servis).
        /// </summary>
        public double FinalScore { get; set; }
    }
}
