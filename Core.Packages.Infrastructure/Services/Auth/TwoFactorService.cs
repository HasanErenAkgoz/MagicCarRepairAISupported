using MagicCarRepairAISupported.Application.Common.Services.Auth;
using Microsoft.Extensions.Logging;
using OtpNet;
using QRCoder;
using System.Text;

namespace MagicCarRepairAISupported.Infrastructure.Services.Auth
{
    /// <summary>
    /// Two-Factor Authentication servisi implementasyonu (TOTP)
    /// </summary>
    public class TwoFactorService : ITwoFactorService
    {
        private readonly ILogger<TwoFactorService> _logger;

        public TwoFactorService(ILogger<TwoFactorService> logger)
        {
            _logger = logger;
        }

        public async Task<TwoFactorSetupResult> GenerateSecretAsync(string email, string issuer = "MagicCarRepair")
        {
            try
            {
                // TOTP secret oluştur
                var secret = KeyGeneration.GenerateRandomKey(20); // 160-bit key
                var base32Secret = Base32Encoding.ToString(secret);

                // Manual entry key (base32 format)
                var manualEntryKey = Base32Encoding.ToString(secret);

                // QR kod URL'i oluştur (Google Authenticator format)
                var qrCodeUrl = $"otpauth://totp/{Uri.EscapeDataString(issuer)}:{Uri.EscapeDataString(email)}?secret={base32Secret}&issuer={Uri.EscapeDataString(issuer)}&algorithm=SHA1&digits=6&period=30";

                // QR kod görseli oluştur
                using var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(qrCodeUrl, QRCodeGenerator.ECCLevel.Q);
                using var qrCode = new PngByteQRCode(qrCodeData);
                var qrCodeBytes = qrCode.GetGraphic(20);
                var qrCodeBase64 = Convert.ToBase64String(qrCodeBytes);
                var qrCodeDataUrl = $"data:image/png;base64,{qrCodeBase64}";

                return await Task.FromResult(new TwoFactorSetupResult
                {
                    Secret = base32Secret,
                    QrCodeUrl = qrCodeDataUrl,
                    ManualEntryKey = manualEntryKey
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating 2FA secret");
                throw;
            }
        }

        public async Task<bool> VerifyCodeAsync(string secret, string code)
        {
            try
            {
                if (string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(code))
                {
                    return false;
                }

                // Base32 secret'ı byte array'e çevir
                var secretBytes = Base32Encoding.ToBytes(secret);

                // TOTP oluştur
                var totp = new Totp(secretBytes);

                // Kodu doğrula (time window: ±1 period tolerance)
                var isValid = totp.VerifyTotp(code, out var timeStepMatched, new VerificationWindow(1, 1));

                if (isValid)
                {
                    _logger.LogDebug($"2FA code verified successfully. TimeStep: {timeStepMatched}");
                }
                else
                {
                    _logger.LogWarning($"2FA code verification failed for code: {code}");
                }

                return await Task.FromResult(isValid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying 2FA code");
                return false;
            }
        }

        public async Task<List<string>> GenerateRecoveryCodesAsync(int count = 10)
        {
            try
            {
                var codes = new List<string>();
                var random = new Random();

                for (int i = 0; i < count; i++)
                {
                    // 8 karakterlik recovery code (format: XXXX-XXXX)
                    var code = $"{random.Next(1000, 9999)}-{random.Next(1000, 9999)}";
                    codes.Add(code);
                }

                return await Task.FromResult(codes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating recovery codes");
                throw;
            }
        }

        public async Task<bool> VerifyRecoveryCodeAsync(List<string> recoveryCodes, string code)
        {
            try
            {
                if (recoveryCodes == null || !recoveryCodes.Any() || string.IsNullOrEmpty(code))
                {
                    return false;
                }

                // Recovery code'u listeden bul ve kaldır (one-time use)
                var normalizedCode = code.Trim().ToUpper();
                var index = recoveryCodes.FindIndex(c => c.Trim().ToUpper() == normalizedCode);

                if (index >= 0)
                {
                    recoveryCodes.RemoveAt(index);
                    _logger.LogDebug("Recovery code verified and removed from list");
                    return await Task.FromResult(true);
                }

                _logger.LogWarning($"Recovery code verification failed: {code}");
                return await Task.FromResult(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying recovery code");
                return false;
            }
        }
    }
}
