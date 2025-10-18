using System;

namespace Core.Packages.Domain.Exceptions
{
    /// <summary>
    /// Domain katmanında iş kuralları ihlal edildiğinde fırlatılan exception
    /// </summary>
    public class DomainException : Exception
    {
      
        public DomainException(string message) : base(message)
        {
        }

    
        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }


        public DomainException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

    
        public DomainException(string errorCode, object? parameters = null, string language = "tr") 
            : base($"Translation key: {errorCode}")
        {
            ErrorCode = errorCode;
            Parameters = parameters;
            Language = language;
        }

   
        public string? ErrorCode { get; }

      
        public object? Details { get; set; }

      
        public object? Parameters { get; }

       
        public string Language { get; } = "tr";

        public bool RequiresTranslation => !string.IsNullOrEmpty(ErrorCode) && Parameters != null;
    }
}
