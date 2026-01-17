using MagicCarRepairAISupported.Application.Shared.Result;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteResponses.Commands.Submit
{
    public class SubmitQuoteResponseCommand : IRequest<IDataResult<SubmitQuoteResponseResponse>>
    {
        /// <summary>
        /// Teklif talebi ID
        /// </summary>
        public int QuoteRequestId { get; set; }

        /// <summary>
        /// Teklif açıklaması
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Tahmini süre (Gün)
        /// </summary>
        public int EstimatedDays { get; set; }

        /// <summary>
        /// Teklif tutarı
        /// </summary>
        public decimal QuoteAmount { get; set; }

        /// <summary>
        /// İndirim oranı (%)
        /// </summary>
        public decimal DiscountRate { get; set; } = 0;

        /// <summary>
        /// KDV oranı (%)
        /// </summary>
        public decimal TaxRate { get; set; } = 20;

        /// <summary>
        /// Garanti süresi (Ay)
        /// </summary>
        public int WarrantyMonths { get; set; } = 0;

        /// <summary>
        /// Geçerlilik tarihi (Varsayılan: 30 gün sonra)
        /// </summary>
        public DateTime? ValidUntilDate { get; set; }

        /// <summary>
        /// Notlar
        /// </summary>
        public string? Notes { get; set; }
    }
}
