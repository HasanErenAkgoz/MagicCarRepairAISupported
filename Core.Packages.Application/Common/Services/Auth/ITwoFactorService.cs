namespace MagicCarRepairAISupported.Application.Common.Services.Auth
{
    /// <summary>
    /// Two-Factor Authentication (2FA) servisi
    /// </summary>
    public interface ITwoFactorService
    {
        /// <summary>
        /// Yeni 2FA secret oluşturur ve QR kod URL'i döner
        /// </summary>
        Task<TwoFactorSetupResult> GenerateSecretAsync(string email, string issuer = "MagicCarRepair");

        /// <summary>
        /// TOTP kodunu doğrular
        /// </summary>
        Task<bool> VerifyCodeAsync(string secret, string code);

        /// <summary>
        /// Recovery code'ları oluşturur
        /// </summary>
        Task<List<string>> GenerateRecoveryCodesAsync(int count = 10);

        /// <summary>
        /// Recovery code'u doğrular ve kullanıldı olarak işaretler
        /// </summary>
        Task<bool> VerifyRecoveryCodeAsync(List<string> recoveryCodes, string code);
    }

    public class TwoFactorSetupResult
    {
        public string Secret { get; set; } = string.Empty;
        public string QrCodeUrl { get; set; } = string.Empty;
        public string ManualEntryKey { get; set; } = string.Empty;
    }
}
