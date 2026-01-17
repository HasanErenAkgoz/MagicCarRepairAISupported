using MagicCarRepairAISupported.Application.Shared.Result;
using MagicCarRepairAISupported.Domain.Enums;
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.QuoteRequests.Commands.Create
{
    public class CreateQuoteRequestCommand : IRequest<IDataResult<CreateQuoteRequestResponse>>
    {
        /// <summary>
        /// Müşteri ID (Opsiyonel - misafir kullanıcılar için)
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Araç ID (Opsiyonel - yeni araç için)
        /// </summary>
        public int? VehicleId { get; set; }

        /// <summary>
        /// Araç bilgileri (VehicleId yoksa kullanılır)
        /// </summary>
        public string? VehicleBrand { get; set; }
        public string? VehicleModel { get; set; }
        public int? VehicleYear { get; set; }
        public string? VehicleLicensePlate { get; set; }

        /// <summary>
        /// Sorun açıklaması
        /// </summary>
        public string ProblemDescription { get; set; } = string.Empty;

        /// <summary>
        /// Talep tipi
        /// </summary>
        public QuoteRequestType RequestType { get; set; }

        /// <summary>
        /// Aciliyet seviyesi
        /// </summary>
        public UrgencyLevel UrgencyLevel { get; set; } = UrgencyLevel.Normal;

        /// <summary>
        /// İstenen başlangıç tarihi
        /// </summary>
        public DateTime? DesiredStartDate { get; set; }

        /// <summary>
        /// İstenen bitiş tarihi
        /// </summary>
        public DateTime? DesiredEndDate { get; set; }

        /// <summary>
        /// Son teklif tarihi (Varsayılan: 7 gün sonra)
        /// </summary>
        public DateTime? QuoteDeadline { get; set; }

        /// <summary>
        /// Müşteri iletişim bilgileri (Misafir kullanıcılar için)
        /// </summary>
        public string? CustomerEmail { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerName { get; set; }
    }
}
