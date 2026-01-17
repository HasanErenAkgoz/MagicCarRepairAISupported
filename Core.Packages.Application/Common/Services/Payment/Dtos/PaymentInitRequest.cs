namespace MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos
{
    /// <summary>
    /// Ödeme başlatma isteği
    /// </summary>
    public class PaymentInitRequest
    {
        /// <summary>
        /// Ödeme tutarı
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Para birimi (TRY, USD, EUR)
        /// </summary>
        public string Currency { get; set; } = "TRY";

        /// <summary>
        /// Fatura ID (opsiyonel)
        /// </summary>
        public int? InvoiceId { get; set; }

        /// <summary>
        /// İş Emri ID (opsiyonel)
        /// </summary>
        public int? WorkOrderId { get; set; }

        /// <summary>
        /// Müşteri ID
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Müşteri adı
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Müşteri soyadı
        /// </summary>
        public string CustomerSurname { get; set; }

        /// <summary>
        /// Müşteri email
        /// </summary>
        public string CustomerEmail { get; set; }

        /// <summary>
        /// Müşteri telefon
        /// </summary>
        public string CustomerPhone { get; set; }

        /// <summary>
        /// Müşteri TC Kimlik No (opsiyonel)
        /// </summary>
        public string? CustomerIdentityNumber { get; set; }

        /// <summary>
        /// Müşteri adresi
        /// </summary>
        public string? CustomerAddress { get; set; }

        /// <summary>
        /// Müşteri şehir
        /// </summary>
        public string? CustomerCity { get; set; }

        /// <summary>
        /// Müşteri ülke
        /// </summary>
        public string? CustomerCountry { get; set; }

        /// <summary>
        /// Müşteri posta kodu
        /// </summary>
        public string? CustomerZipCode { get; set; }

        /// <summary>
        /// Ödeme açıklaması
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Callback URL (3D Secure sonrası yönlendirilecek URL)
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Taksit sayısı (opsiyonel)
        /// </summary>
        public int? InstallmentCount { get; set; }

        /// <summary>
        /// Kart bilgileri (opsiyonel - direkt ödeme için)
        /// </summary>
        public CardInfo? CardInfo { get; set; }
    }

    /// <summary>
    /// Kart bilgileri
    /// </summary>
    public class CardInfo
    {
        /// <summary>
        /// Kart numarası
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Kart sahibi adı
        /// </summary>
        public string CardHolderName { get; set; }

        /// <summary>
        /// Son kullanma ayı (MM)
        /// </summary>
        public string ExpireMonth { get; set; }

        /// <summary>
        /// Son kullanma yılı (YYYY)
        /// </summary>
        public string ExpireYear { get; set; }

        /// <summary>
        /// CVV
        /// </summary>
        public string Cvv { get; set; }
    }
}





