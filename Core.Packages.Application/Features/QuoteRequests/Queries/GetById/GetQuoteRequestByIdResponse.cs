using MagicCarRepairAISupported.Domain.Enums;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Queries.GetById
{
    public class GetQuoteRequestByIdResponse
    {
        public int Id { get; set; }
        public string RequestNumber { get; set; } = string.Empty;
        public int? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int? VehicleId { get; set; }
        public string? VehicleInfo { get; set; }
        public string ProblemDescription { get; set; } = string.Empty;
        public QuoteRequestType RequestType { get; set; }
        public string RequestTypeName { get; set; } = string.Empty;
        public UrgencyLevel UrgencyLevel { get; set; }
        public string UrgencyLevelName { get; set; } = string.Empty;
        public DateTime? DesiredStartDate { get; set; }
        public DateTime? DesiredEndDate { get; set; }
        public QuoteStatus Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public decimal? EstimatedCostMin { get; set; }
        public decimal? EstimatedCostMax { get; set; }
        public DateTime QuoteDeadline { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<QuoteResponseDto> QuoteResponses { get; set; } = new();
        public List<QuoteRequestPhotoDto> Photos { get; set; } = new();
    }

    public class QuoteResponseDto
    {
        public int Id { get; set; }
        public string? QuoteNumber { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string? ClientLogoUrl { get; set; }
        public string? Description { get; set; }
        public int? EstimatedDays { get; set; }
        public decimal QuoteAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int? WarrantyMonths { get; set; }
        public string Status { get; set; } = "Pending";
        public string StatusName { get; set; } = string.Empty;
        public DateTime QuoteDate { get; set; }
        public DateTime? ValidUntilDate { get; set; }

        // ── Sprint 4: Trust + Ranking ──────────────────────────────────────
        /// <summary>0–100 güven puanı.</summary>
        public double TrustScore { get; set; }
        /// <summary>FinalScore = price rank + trust + speed (düşük daha iyi).</summary>
        public double FinalScore { get; set; }
        /// <summary>Sıralama (1 = en iyi).</summary>
        public int Rank { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public double CompletionRate { get; set; }
        public double? AvgQuoteResponseHours { get; set; }
    }

    public class QuoteRequestPhotoDto
    {
        public int Id { get; set; }
        public string MediaUrl { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
