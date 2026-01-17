namespace MagicCarRepairAISupported.Domain.Enums
{
    /// <summary>
    /// Ödeme yöntemleri
    /// </summary>
    public enum PaymentMethod
    {
        /// <summary>
        /// Nakit
        /// </summary>
        Cash = 1,

        /// <summary>
        /// Kredi Kartı
        /// </summary>
        CreditCard = 2,

        /// <summary>
        /// Banka Havalesi / EFT
        /// </summary>
        BankTransfer = 3,

        /// <summary>
        /// Çek
        /// </summary>
        Check = 4,

        /// <summary>
        /// Senet
        /// </summary>
        PromissoryNote = 5,

        /// <summary>
        /// Online Ödeme (İyzico, PayTR, vb.)
        /// </summary>
        OnlinePayment = 6
    }
}

