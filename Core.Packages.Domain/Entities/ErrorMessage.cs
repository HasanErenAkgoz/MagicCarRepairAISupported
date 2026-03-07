using MagicCarRepairAISupported.Domain.Common;

namespace MagicCarRepairAISupported.Domain.Entities
{
    /// <summary>
    /// Stores error messages in multiple languages
    /// </summary>
    public class ErrorMessage : BaseEntity<int>
    {
        public string ErrorCode { get; set; } // Unique error code (e.g., "USER_NOT_FOUND")
        public string Language { get; set; } // Language code (e.g., "tr", "en")
        public string Message { get; set; } // Localized error message
        public string? Description { get; set; } // Additional description
    }
}

