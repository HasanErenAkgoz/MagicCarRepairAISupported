namespace MagicCarRepairAISupported.Infrastructure.Configurations.AI
{
    /// <summary>
    /// AI servisleri için configuration seçenekleri
    /// </summary>
    public class AIOptions
    {
        /// <summary>
        /// AI Provider (OpenAI, AzureOpenAI, Mock)
        /// </summary>
        public string Provider { get; set; } = "Mock";

        /// <summary>
        /// OpenAI API Key
        /// </summary>
        public string? ApiKey { get; set; }

        /// <summary>
        /// Azure OpenAI Endpoint (AzureOpenAI kullanılıyorsa)
        /// </summary>
        public string? AzureEndpoint { get; set; }

        /// <summary>
        /// Azure OpenAI Deployment Name
        /// </summary>
        public string? AzureDeploymentName { get; set; }

        /// <summary>
        /// Model adı (gpt-4-turbo-preview, gpt-4-vision-preview, vb.)
        /// </summary>
        public string Model { get; set; } = "gpt-4-turbo-preview";

        /// <summary>
        /// Vision model adı (fotoğraf analizi için)
        /// </summary>
        public string VisionModel { get; set; } = "gpt-4-vision-preview";

        /// <summary>
        /// Base URL (OpenAI için opsiyonel, default: https://api.openai.com/v1)
        /// </summary>
        public string? BaseUrl { get; set; }

        /// <summary>
        /// Maximum retry attempts
        /// </summary>
        public int MaxRetryAttempts { get; set; } = 3;

        /// <summary>
        /// Request timeout in seconds
        /// </summary>
        public int TimeoutSeconds { get; set; } = 60;
    }
}

