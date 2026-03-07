namespace MagicCarRepairAISupported.Application.Common.Exceptions
{
    /// <summary>
    /// Kullanıcı dostu hata mesajları için özel exception
    /// </summary>
    public class UserFriendlyException : Exception
    {
        public ErrorCategory Category { get; }
        public string? ErrorCode { get; }
        public object? AdditionalData { get; }

        public UserFriendlyException(
            string message,
            ErrorCategory category = ErrorCategory.Business,
            string? errorCode = null,
            object? additionalData = null)
            : base(message)
        {
            Category = category;
            ErrorCode = errorCode;
            AdditionalData = additionalData;
        }

        public UserFriendlyException(
            string message,
            Exception innerException,
            ErrorCategory category = ErrorCategory.Business,
            string? errorCode = null,
            object? additionalData = null)
            : base(message, innerException)
        {
            Category = category;
            ErrorCode = errorCode;
            AdditionalData = additionalData;
        }
    }

    public enum ErrorCategory
    {
        Validation = 1,    // Form validasyon hataları
        Business = 2,      // İş mantığı hataları
        System = 3,        // Sistem hataları
        Authentication = 4, // Kimlik doğrulama hataları
        Authorization = 5,  // Yetkilendirme hataları
    }
}
