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
        public DateTime QuoteDeadline { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<QuoteResponseDto> QuoteResponses { get; set; } = new();
        public List<QuoteRequestPhotoDto> Photos { get; set; } = new();
    }

    public class QuoteResponseDto
    {
        public int Id { get; set; }
        public string QuoteNumber { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int EstimatedDays { get; set; }
        public decimal QuoteAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int WarrantyMonths { get; set; }
        public QuoteResponseStatus Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime QuoteDate { get; set; }
        public DateTime ValidUntilDate { get; set; }
    }

    public class QuoteRequestPhotoDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string? Description { get; set; }
        public QuoteRequestPhotoType PhotoType { get; set; }
        public string PhotoTypeName { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
    }
}

