using MagicCarRepairAISupported.Domain.Common;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Password reset OTP entity
    /// </summary>
    public class PasswordResetOtp : BaseEntity<int>
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string OtpHash { get; set; } // Hash'lenmiş OTP (SHA-256, bcrypt, vb.)
        public DateTime ExpiresAt { get; set; }
        public bool Used { get; set; } = false;
        public int Attempts { get; set; } = 0; // Yanlış deneme sayısı
        public string? ResetToken { get; set; } // OTP doğrulandıktan sonra oluşturulan reset token
        public DateTime? ResetTokenExpiresAt { get; set; }
        
        // Navigation property
        public virtual User User { get; set; }
        
        /// <summary>
        /// OTP'nin geçerli olup olmadığını kontrol eder
        /// </summary>
        public bool IsValid()
        {
            return !Used && ExpiresAt > DateTime.UtcNow && Attempts < 3;
        }
        
        /// <summary>
        /// Reset token'ın geçerli olup olmadığını kontrol eder
        /// </summary>
        public bool IsResetTokenValid()
        {
            return !string.IsNullOrEmpty(ResetToken) && 
                   ResetTokenExpiresAt.HasValue && 
                   ResetTokenExpiresAt.Value > DateTime.UtcNow;
        }
    }
}
