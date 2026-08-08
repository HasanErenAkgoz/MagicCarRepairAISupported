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

        /// <summary>
        /// Public HTTP(S) base URL of this API (no trailing slash). Used for Vision photo URLs
        /// and for employee invite email links (<c>GET /api/Auth/set-password</c>).
        /// <para>
        /// Local caveat: <c>http://localhost:5169</c> works only on the same PC browser.
        /// A phone opening the mail cannot reach your PC’s localhost — use your LAN IP
        /// (e.g. <c>http://192.168.1.10:5169</c>), ngrok, or Android emulator host alias
        /// <c>http://10.0.2.2:5169</c> plus <c>adb reverse</c> as appropriate.
        /// </para>
        /// </summary>
        public string? ServerBaseUrl { get; set; }
    }
}

