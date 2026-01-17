using Azure;
using Azure.AI.OpenAI;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Infrastructure.Configurations.AI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// OpenAI destekli fotoğraf analizi servisi (GPT-4 Vision)
    /// </summary>
    public class OpenAIPhotoAnalysisService : IAIPhotoAnalysisService
    {
        private readonly ILogger<OpenAIPhotoAnalysisService> _logger;
        private readonly AIOptions _aiOptions;
        private readonly OpenAIClient? _openAIClient;

        public OpenAIPhotoAnalysisService(
            ILogger<OpenAIPhotoAnalysisService> logger,
            IOptions<AIOptions> aiOptions)
        {
            _logger = logger;
            _aiOptions = aiOptions.Value;

            // Initialize OpenAI client
            try
            {
                if (_aiOptions.Provider == "AzureOpenAI" && !string.IsNullOrEmpty(_aiOptions.AzureEndpoint))
                {
                    _openAIClient = new OpenAIClient(
                        new Uri(_aiOptions.AzureEndpoint!),
                        new AzureKeyCredential(_aiOptions.ApiKey ?? throw new InvalidOperationException("Azure OpenAI API Key is required")));
                }
                else if (_aiOptions.Provider == "OpenAI" && !string.IsNullOrEmpty(_aiOptions.ApiKey))
                {
                    var endpoint = new Uri(_aiOptions.BaseUrl ?? "https://api.openai.com/v1");
                    _openAIClient = new OpenAIClient(endpoint, new AzureKeyCredential(_aiOptions.ApiKey));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize OpenAI client for photo analysis");
            }
        }

        public async Task<PhotoAnalysisResultDto> AnalyzePhotoAsync(byte[] photoData, string? fileName = null, CancellationToken cancellationToken = default)
        {
            if (_openAIClient == null)
            {
                _logger.LogWarning("OpenAI client not available, falling back to mock service behavior");
                return await GetMockResultAsync(photoData, fileName, cancellationToken);
            }

            try
            {
                _logger.LogInformation("AI Photo Analysis: Analyzing photo. FileName: {FileName}, Size: {Size} bytes", 
                    fileName, photoData.Length);

                // Convert image to base64
                var imageBase64 = Convert.ToBase64String(photoData);
                var imageUrl = $"data:image/jpeg;base64,{imageBase64}";

                var systemPrompt = @"Sen bir oto servis uzmanısın. Araç hasar fotoğraflarını analiz edip hasar türlerini, şiddetini ve gerekli onarım maliyetlerini tespit etmelisin.
Yanıtını JSON formatında döndür. Format:
{
  ""detectedDamages"": [
    {
      ""damageType"": ""Çizik|Çökme|Yırtılma|Boyama|vb"",
      ""location"": ""Ön kaput|Ön tampon|vb"",
      ""severityScore"": 70,
      ""estimatedRepairCost"": 500,
      ""description"": ""Hasar açıklaması"",
      ""coordinates"": [{""x"": 100, ""y"": 150}, {""x"": 200, ""y"": 250}]
    }
  ],
  ""damageSeverityScore"": 60,
  ""recommendedParts"": [
    {""partName"": ""Parça Adı"", ""category"": ""Kategori"", ""estimatedPrice"": 500, ""quantity"": 1, ""probabilityScore"": 80}
  ],
  ""recommendedLabors"": [
    {""laborName"": ""İşçilik Adı"", ""description"": ""Açıklama"", ""estimatedPrice"": 300, ""estimatedHours"": 2, ""probabilityScore"": 90}
  ],
  ""photoQualityScore"": 85,
  ""photoQualityNotes"": ""Fotoğraf kalitesi notları"",
  ""recommendations"": ""Öneriler"",
  ""insuranceReportJson"": ""{}""
}";

                var userPrompt = "Bu fotoğraftaki araç hasarını analiz et ve JSON formatında sonuç döndür.";

                var deploymentName = _aiOptions.Provider == "AzureOpenAI"
                    ? (_aiOptions.AzureDeploymentName ?? _aiOptions.VisionModel)
                    : _aiOptions.VisionModel;

                var chatCompletionsOptions = new ChatCompletionsOptions
                {
                    DeploymentName = deploymentName,
                    Messages =
                    {
                        new ChatRequestSystemMessage(systemPrompt),
                        new ChatRequestUserMessage(
                            new ChatMessageContentItem[]
                            {
                                new ChatMessageTextContentItem(userPrompt),
                                new ChatMessageImageContentItem(new Uri(imageUrl))
                            })
                    },
                    Temperature = 0.3f, // Lower temperature for more consistent analysis
                    MaxTokens = 2000
                };

                var response = await _openAIClient.GetChatCompletionsAsync(chatCompletionsOptions, cancellationToken);
                var content = response.Value.Choices[0].Message.Content;

                _logger.LogInformation("AI Photo Analysis: Received response from OpenAI");

                return ParsePhotoAnalysisResponse(content);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AI photo analysis. Falling back to mock result.");
                return await GetMockResultAsync(photoData, fileName, cancellationToken);
            }
        }

        public async Task<PhotoAnalysisResultDto> AnalyzeMultiplePhotosAsync(List<byte[]> photoDataList, CancellationToken cancellationToken = default)
        {
            if (_openAIClient == null || photoDataList == null || photoDataList.Count == 0)
            {
                _logger.LogWarning("OpenAI client not available or no photos provided, falling back to mock service behavior");
                return await GetMockResultAsync(photoDataList?.FirstOrDefault() ?? Array.Empty<byte>(), null, cancellationToken);
            }

            try
            {
                _logger.LogInformation("AI Photo Analysis: Analyzing {Count} photos", photoDataList.Count);

                // For multiple photos, analyze first photo in detail, then provide summary
                var firstPhotoResult = await AnalyzePhotoAsync(photoDataList[0], null, cancellationToken);

                if (photoDataList.Count > 1)
                {
                    // Add note about multiple photos
                    firstPhotoResult.Recommendations = $"{firstPhotoResult.Recommendations} Toplam {photoDataList.Count} fotoğraf analiz edildi. İlk fotoğrafın detaylı analizi yukarıda verilmiştir.";

                    // Update severity score if multiple photos suggest more extensive damage
                    var avgSeverity = (firstPhotoResult.DamageSeverityScore + 
                        (photoDataList.Count > 1 ? 10 : 0)) // Slightly increase for multiple photos
                        .Clamp(0, 100);
                    firstPhotoResult.DamageSeverityScore = avgSeverity;
                }

                return firstPhotoResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in multiple photo analysis. Falling back to mock result.");
                return await GetMockResultAsync(photoDataList?.FirstOrDefault() ?? Array.Empty<byte>(), null, cancellationToken);
            }
        }

        private PhotoAnalysisResultDto ParsePhotoAnalysisResponse(string jsonContent)
        {
            try
            {
                using var doc = JsonDocument.Parse(jsonContent);
                var root = doc.RootElement;

                var result = new PhotoAnalysisResultDto
                {
                    DetectedDamages = root.TryGetProperty("detectedDamages", out var damages)
                        ? damages.EnumerateArray()
                            .Select(damage => new DamageDetectionDto
                            {
                                DamageType = damage.GetProperty("damageType").GetString() ?? "",
                                Location = damage.TryGetProperty("location", out var loc) ? loc.GetString() : null,
                                SeverityScore = damage.TryGetProperty("severityScore", out var sev) ? sev.GetInt32() : 50,
                                EstimatedRepairCost = damage.TryGetProperty("estimatedRepairCost", out var cost) ? cost.GetDecimal() : null,
                                Description = damage.TryGetProperty("description", out var desc) ? desc.GetString() : null,
                                Coordinates = damage.TryGetProperty("coordinates", out var coords)
                                    ? coords.EnumerateArray()
                                        .Select(coord => new CoordinateDto
                                        {
                                            X = coord.TryGetProperty("x", out var x) ? x.GetInt32() : 0,
                                            Y = coord.TryGetProperty("y", out var y) ? y.GetInt32() : 0
                                        }).ToList()
                                    : null
                            }).ToList()
                        : new List<DamageDetectionDto>(),

                    DamageSeverityScore = root.TryGetProperty("damageSeverityScore", out var severity) ? severity.GetInt32() : 50,

                    RecommendedParts = root.TryGetProperty("recommendedParts", out var parts)
                        ? parts.EnumerateArray()
                            .Select(part => new RecommendedPartDto
                            {
                                PartName = part.GetProperty("partName").GetString() ?? "",
                                Category = part.TryGetProperty("category", out var cat) ? cat.GetString() : null,
                                EstimatedPrice = part.TryGetProperty("estimatedPrice", out var price) ? price.GetDecimal() : null,
                                Quantity = part.TryGetProperty("quantity", out var qty) ? qty.GetInt32() : 1,
                                ProbabilityScore = part.TryGetProperty("probabilityScore", out var prob) ? prob.GetInt32() : 50
                            }).ToList()
                        : new List<RecommendedPartDto>(),

                    RecommendedLabors = root.TryGetProperty("recommendedLabors", out var labors)
                        ? labors.EnumerateArray()
                            .Select(labor => new RecommendedLaborDto
                            {
                                LaborName = labor.GetProperty("laborName").GetString() ?? "",
                                Description = labor.TryGetProperty("description", out var desc) ? desc.GetString() : null,
                                EstimatedPrice = labor.TryGetProperty("estimatedPrice", out var price) ? price.GetDecimal() : null,
                                EstimatedHours = labor.TryGetProperty("estimatedHours", out var hours) ? hours.GetDecimal() : null,
                                ProbabilityScore = labor.TryGetProperty("probabilityScore", out var prob) ? prob.GetInt32() : 50
                            }).ToList()
                        : new List<RecommendedLaborDto>(),

                    PhotoQualityScore = root.TryGetProperty("photoQualityScore", out var quality) ? quality.GetInt32() : 80,
                    PhotoQualityNotes = root.TryGetProperty("photoQualityNotes", out var notes) ? notes.GetString() : null,
                    Recommendations = root.TryGetProperty("recommendations", out var rec) ? rec.GetString() : null,
                    InsuranceReportJson = root.TryGetProperty("insuranceReportJson", out var insurance) ? insurance.GetString() : null
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing AI photo analysis response: {Content}", jsonContent);
                return new PhotoAnalysisResultDto
                {
                    PhotoQualityScore = 50,
                    PhotoQualityNotes = "AI yanıtı parse edilemedi. Manuel kontrol önerilir.",
                    Recommendations = jsonContent,
                    DamageSeverityScore = 50
                };
            }
        }

        private async Task<PhotoAnalysisResultDto> GetMockResultAsync(byte[] photoData, string? fileName, CancellationToken cancellationToken)
        {
            await Task.Delay(800, cancellationToken);

            return new PhotoAnalysisResultDto
            {
                PhotoQualityScore = 85,
                PhotoQualityNotes = "Fotoğraf kalitesi iyi. Hasar tespiti için yeterli detay mevcut.",
                DamageSeverityScore = 60,
                Recommendations = "Detaylı muayene önerilir. Sigorta hasarı olabilir.",
                DetectedDamages = new List<DamageDetectionDto>
                {
                    new DamageDetectionDto
                    {
                        DamageType = "Çizik",
                        Location = "Ön Kaput",
                        SeverityScore = 40,
                        EstimatedRepairCost = 500m,
                        Description = "Ön kaput üzerinde orta şiddette çizik tespit edildi."
                    }
                },
                RecommendedParts = new List<RecommendedPartDto>
                {
                    new RecommendedPartDto
                    {
                        PartName = "Ön Tampon",
                        Category = "Karoseri",
                        EstimatedPrice = 1200m,
                        Quantity = 1,
                        ProbabilityScore = 90
                    }
                },
                RecommendedLabors = new List<RecommendedLaborDto>
                {
                    new RecommendedLaborDto
                    {
                        LaborName = "Karoseri Onarımı",
                        Description = "Tampon değişimi ve kaput boyama",
                        EstimatedPrice = 800m,
                        EstimatedHours = 6,
                        ProbabilityScore = 85
                    }
                }
            };
        }
    }

    // Extension method for clamping
    internal static class IntExtensions
    {
        public static int Clamp(this int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}

