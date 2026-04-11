using Azure;
using Azure.AI.OpenAI;
using MagicCarRepairAISupported.Application.Common.Services.AI;
using MagicCarRepairAISupported.Application.Common.Services.AI.Dtos;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Repositories;
using MagicCarRepairAISupported.Infrastructure.Configurations.AI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MagicCarRepairAISupported.Infrastructure.Services.AI
{
    /// <summary>
    /// OpenAI destekli arıza tespiti servisi
    /// </summary>
    public class OpenAIDiagnosisService : IAIDiagnosisService
    {
        private readonly ILogger<OpenAIDiagnosisService> _logger;
        private readonly AIOptions _aiOptions;
        private readonly OpenAIClient? _openAIClient;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IPartRepository _partRepository;
        private readonly string _webRootPath;

        public OpenAIDiagnosisService(
            ILogger<OpenAIDiagnosisService> logger,
            IOptions<AIOptions> aiOptions,
            IVehicleRepository vehicleRepository,
            IWorkOrderRepository workOrderRepository,
            IPartRepository partRepository,
            IWebHostEnvironment env)
        {
            _logger = logger;
            _aiOptions = aiOptions.Value;
            _vehicleRepository = vehicleRepository;
            _workOrderRepository = workOrderRepository;
            _partRepository = partRepository;
            _webRootPath = env.WebRootPath ?? string.Empty;

            // Initialize OpenAI client
            try
            {
                if (_aiOptions.Provider == "AzureOpenAI" && !string.IsNullOrEmpty(_aiOptions.AzureEndpoint))
                {
                    _openAIClient = new OpenAIClient(
                        new Uri(_aiOptions.AzureEndpoint!),
                        new AzureKeyCredential(_aiOptions.ApiKey ?? throw new InvalidOperationException("Azure OpenAI API Key is required")),
                        CreateOpenAIClientOptions());
                }
                else if (_aiOptions.Provider == "OpenAI" && !string.IsNullOrEmpty(_aiOptions.ApiKey))
                {
                    var endpoint = new Uri(NormalizeOpenAIBaseUrl(_aiOptions.BaseUrl));
                    _openAIClient = new OpenAIClient(endpoint, new AzureKeyCredential(_aiOptions.ApiKey), CreateOpenAIClientOptions());
                }
                else
                {
                    _logger.LogWarning("OpenAI client not initialized. Provider: {Provider}, API Key: {HasKey}", 
                        _aiOptions.Provider, !string.IsNullOrEmpty(_aiOptions.ApiKey));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize OpenAI client");
            }
        }

        public async Task<DiagnosisResultDto> DiagnoseFromTextAsync(string complaint, int? vehicleId = null, List<string>? photoUrls = null, CancellationToken cancellationToken = default)
        {
            if (_openAIClient == null)
            {
                _logger.LogWarning("OpenAI client not available, falling back to mock service behavior");
                return await GetMockResultAsync(complaint, vehicleId, cancellationToken);
            }

            try
            {
                _logger.LogInformation("AI Diagnosis: Analyzing complaint text. VehicleId: {VehicleId}, Photos: {PhotoCount}", vehicleId, photoUrls?.Count ?? 0);

                // Get vehicle context if available
                string vehicleContext = string.Empty;
                string workOrderHistory = string.Empty;

                if (vehicleId.HasValue)
                {
                    var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId.Value);
                    if (vehicle != null)
                    {
                        var parts = new List<string>
                        {
                            $"- Marka/Model: {vehicle.Brand} {vehicle.Model}",
                            $"- Yıl: {vehicle.Year}",
                            $"- Plaka: {vehicle.LicensePlate}",
                        };
                        if (vehicle.Kilometers > 0)
                            parts.Add($"- Kilometre: {vehicle.Kilometers:N0} km");
                        if (!string.IsNullOrEmpty(vehicle.FuelType))
                            parts.Add($"- Yakıt Tipi: {vehicle.FuelType}");
                        if (!string.IsNullOrEmpty(vehicle.ModelVariant))
                            parts.Add($"- Motor/Varyant: {vehicle.ModelVariant}");
                        if (!string.IsNullOrEmpty(vehicle.Trim))
                            parts.Add($"- Versiyon: {vehicle.Trim}");
                        if (!string.IsNullOrEmpty(vehicle.Vin))
                            parts.Add($"- VIN: {vehicle.Vin}");
                        vehicleContext = "Araç Bilgileri:\n" + string.Join("\n", parts) + "\n";

                        // Get work order history for this vehicle
                        var workOrders = await _workOrderRepository.GetByVehicleIdAsync(vehicleId.Value, cancellationToken);
                        if (workOrders.Any())
                        {
                            var history = workOrders
                                .OrderByDescending(wo => wo.EntryDate)
                                .Take(3)
                                .Select(wo => $"- {wo.EntryDate:yyyy-MM-dd}: {wo.CustomerComplaints ?? "N/A"} (Durum: {wo.Status})");
                            
                            workOrderHistory = "\nGeçmiş İş Emirleri:\n" + string.Join("\n", history);
                        }
                    }
                }

                bool hasPhotos = photoUrls != null && photoUrls.Count > 0;
                if (hasPhotos)
                {
                    // Helps detect client-side “previous photo still included” issues without logging full URLs.
                    var photoHints = photoUrls!
                        .Take(5)
                        .Select(u =>
                        {
                            try
                            {
                                if (u.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                                    return new Uri(u).AbsolutePath.Split('/', StringSplitOptions.RemoveEmptyEntries).LastOrDefault() ?? "<url>";
                                return Path.GetFileName(u);
                            }
                            catch
                            {
                                return "<bad-url>";
                            }
                        })
                        .ToArray();

                    _logger.LogInformation("AI Vision: PhotoCount={PhotoCount}, PhotoHints={PhotoHints}", photoUrls!.Count, string.Join(",", photoHints));
                }

                // Detect diagnosis type from complaint keywords
                var diagnosisType = DetectDiagnosisType(complaint);

                // Build prompt
                var systemPrompt = @"Sen deneyimli bir oto servis teknisyeni ve kaporta-boya uzmanısın. Görevin müşterinin anlattığı semptomu ve (varsa) paylaşılan araç fotoğraflarını analiz etmek.

FOTOĞRAF ANALİZİ (eğer fotoğraf gönderildiyse):
- Fotoğraflardaki hasar bölgelerini, çizikleri, ezikleri, kırıkları, boya hasarını dikkatlice incele.
- Her panel veya bölge için ayrı ayrı değerlendirme yap.
- Kaporta/Boya hasarını diğer mekanik sorunlardan ayır.
- FOTOĞRAFLARIN AÇISINI TESPİT ET: ön / arka / sol yan / sağ yan / 3-çeyrek / iç mekan.
- SADECE GÖRÜNENİ YAZ: Fotoğrafta net görmediğin bir parçayı (ör. kaput/ön tampon) damagedParts'e ekleme.
- Eğer hasarın yandan olduğu anlaşılıyorsa, ön parçaları varsayma; görünmeyen parçalar için en fazla criticalChecks altında “kontrol edilmesi gerekenler” olarak belirt.
- Şikayet metni genel (“kaporta hasarı”) ise, fotoğraf kanıtı yoksa sadece genel öneri ver ve “belirsiz” kal.

TEMEL KURAL: Yalnızca müşterinin şikayetiyle DOĞRUDAN ilgili arıza ve parçaları öner.
- Her semptomu kendi ilgili sistemiyle eşleştir.
- Kaporta hasarında mekanik parçalar önerme; mekanik sorunlarda kaporta parçaları önerme.

Araç sistemleri ve ilgili şikayetler:
- Cam/Elektrikli Cam Krikosu: cam açılıp kapanmıyor, yavaş çalışıyor, ses çıkarıyor
- Fren Sistemi: fren sesi, titreme, uzayan fren mesafesi, pedal batması
- Motor/Yakıt: çalışmıyor, güç kaybı, sarsıntı, aşırı yakıt tüketimi
- Süspansiyon/Direksiyon: ses, titreme, direksiyon boşluğu, çekme
- Klima/Isıtma: soğutmuyor, ısıtmıyor, kötü koku, fan sesi
- Şanzıman: vites geçmiyor, kayma, ses, titreme
- Elektrik/Aydınlatma: yanmıyor, devamlı yanıyor, akü, marş
- Egzoz: ses, is, kötü koku
- Kapı/Kilit/Ayna: açılmıyor, kapanmıyor, kilitlenmiyor
- Kaporta/Boya (kaza hasarı): ezik, çizik, kırık panel, lamba kırığı, tampon hasarı, boya soyulması
  * Kaporta işçilikleri: düzeltme (dent repair), boyama, astar, zımparalama, vernik
  * Kaporta parçaları: ön/arka tampon, kaput, çamurluk, kapı paneli, ön/arka lamba, ayna

Kaporta/Boya fiyat rehberi (TRY, 2024):
- Panel boyama: 2.000-8.000₺ (panele göre)
- Tampon değişimi + boya: 3.000-12.000₺
- Çamurluk değişimi + boya: 4.000-15.000₺
- Kaput değişimi + boya: 5.000-20.000₺
- Kapı paneli değişimi + boya: 5.000-18.000₺
- Göçük düzeltme (küçük): 500-2.000₺
- Göçük düzeltme (büyük): 1.500-5.000₺
- Cam değişimi (ön): 3.000-8.000₺

Olasılık skoru (probabilityScore) kuralları:
- 85-100: Şikayetle kesinlikle ilgili, fotoğrafta net görünüyor
- 65-84: Muhtemel neden, kontrol edilmeli
- 45-64: Olası ama daha az ihtimalli
- 44 altı: Önerme, listeye ekleme

Sadece probabilityScore >= 55 olan parçaları listeye ekle.

Yanıtını YALNIZCA JSON formatında döndür (başka metin ekleme):
{
  ""possibleIssues"": [
    {""issueName"": ""Arıza Adı"", ""description"": ""Bu semptomu neden yapıyor veya fotoğrafta ne görüldü"", ""probabilityScore"": 70, ""category"": ""Elektrik|Motor|Fren|Süspansiyon|Klima|Şanzıman|Egzoz|Kaporta|Boya|Diğer""}
  ],
  ""recommendedParts"": [
    {""partName"": ""Parça Adı"", ""category"": ""Kategori"", ""estimatedPrice"": 500, ""quantity"": 1, ""probabilityScore"": 80}
  ],
  ""recommendedLabors"": [
    {""laborName"": ""İşçilik Adı"", ""description"": ""Yapılacak işlem"", ""estimatedPrice"": 300, ""estimatedHours"": 1.5, ""probabilityScore"": 90}
  ],
  ""estimatedDays"": 1,
  ""estimatedCost"": 1200,
  ""confidenceScore"": 85,
  ""recommendations"": ""Teknisyen önerisi""
}";

                // Extend prompt for accident mode
                if (diagnosisType == DiagnosisType.Accident)
                {
                    systemPrompt += @"

KAZA/HASAR ANALİZİ MODU (Bu şikayet kaza/hasar içeriyor):
Fotoğraflardaki araç kaza hasarını detaylı analiz et. JSON yanıtına şu ek alanları MUTLAKA ekle.

ÖNCE SAHNE (ZORUNLU): ""sceneDescription"" alanına 3-5 cümle Türkçe yaz:
- Kamera açısı: ön / arka / sol yan / sağ yan / çapraz (hangisi?)
- Kadrajda NET görünen karoser bölgeleri (ör. sol ön kapı + sol ön çamurluk)
- Kadrajda GÖRÜNMEYEN veya NET OLMAYAN bölgeler (ör. kaput/ön tampon kadrajda yoksa açıkça yaz)
Bu alan yazılmadan damagedParts doldurma.

ÇOK ÖNEMLİ KURAL (hallucination yok):
- damagedParts listesine SADECE kadrajda görsel olarak doğrulanabilen parçaları koy.
- Yan çekimde ön tampon/kaput KADRAJDA YOKSA, bunları damagedParts'e ASLA ekleme.
- Ön bölge hasarı SADECE ön tampon/kaput/farlar kadrajda görünüyorsa yazılabilir.
- Görünmeyen riskler için sadece criticalChecks kullan (ör. ""ön darbe şüphesi varsa radyatör kontrolü"" gibi).

ÖRNEK ŞEMA (örnekteki parça adları sadece format içindir; sen gerçek görüntüye göre doldur):
""diagnosisType"": 1,
""sceneDescription"": ""Örnek: Sol yan çekim; sol ön kapı ve sol ön çamurluk görünüyor. Kaput ve ön tampon kadrajda görünmüyor."",
""damagedParts"": [
  {
    ""partName"": ""Sol ön çamurluk"",
    ""damageLevel"": 2,
    ""recommendedAction"": 2,
    ""confidencePercent"": 85,
    ""estimatedCostMin"": 3000,
    ""estimatedCostMax"": 8000,
    ""notes"": ""Kadrajda görülen ezik/çizik; örnek""
  }
],
""criticalChecks"": [
  {
    ""componentName"": ""Şase/Longeron (kontrol)"",
    ""riskLevel"": 2,
    ""warning"": ""Yan darbe şiddetine göre şase ölçümü önerilir (fotoğraftan kesin hüküm yok)"",
    ""requiresImmediateInspection"": true
  }
],
""estimatedRepairRange"": {
  ""min"": 5000,
  ""max"": 15000,
  ""currency"": ""TRY""
},
""mobileDisplayMarkdown"": ""...(aşağıdaki MOBİL ŞABLONU ile doldur, tek string; satır sonları \\n ile)...""

DamageLevel değerleri: None=0, Light=1, Medium=2, Heavy=3, Critical=4
RepairAction değerleri: None=0, Paint=1, Repair=2, Replace=3
RiskLevel değerleri: Low=0, Medium=1, High=2, Critical=3

Panel seçimi: SADECE kadrajda görünenleri değerlendir (kaput, tampon, çamurluk, kapı, far, cam vb.).
Gizli riskleri criticalChecks'e ekle: radyatör, şase, hava yastığı sistemi vb. (fotoğrafta görünmeyen ama kontrol gerektirebilecekler).

MOBİL GÖRÜNÜM (ZORUNLU): ""mobileDisplayMarkdown"" alanına, aşağıdaki yapıyı TAKİP EDEN tek bir Markdown metni yaz. Türkçe olacak; kısa, net, madde madde. Tahminleri dürüst belirt.
Yapı şöyle olmalı (emoji ve başlıkları koru):

Kısa net analiz:

🔧 **Hasar Durumu**
- Parça: kısa durum → düzeltme/değişim/tahmin (ok → kullan)

⚠️ **Kritik Kontrol (en önemlisi)**
- Bileşen adları (criticalChecks ile uyumlu)

👉 Bunlar hasarlıysa maliyet uçar

---

💰 **Tahmini Masraf (2026 TR)**
- Ana kalemler: (ör. Kaput, Tampon, Farlar) ve **aralık** (ör. **15–25K** TL bandı, binlik kısaltma K kullan)

👉 **TOPLAM:**
- **Minimum:** ~XK TL
- **Ortalama:** aralık
- **Şase/radyatör varsa:** üst band notu

---

🚨 **Yorum (dürüst)**
Kısa paragraf: hasarın şiddeti, şase/kontrol ihtiyacı.

İstersen:
👉 İki kısa madde (ör. pert analizi, sigorta vs gerçek hasar)

Bu metin JSON içinde tek string; gerçek satır sonları için \\n kullan.";
                }

                var userPrompt = new StringBuilder();
                userPrompt.AppendLine("Müşteri Şikayeti:");
                userPrompt.AppendLine(complaint);
                userPrompt.AppendLine();
                
                if (!string.IsNullOrEmpty(vehicleContext))
                {
                    userPrompt.AppendLine(vehicleContext);
                }
                
                if (!string.IsNullOrEmpty(workOrderHistory))
                {
                    userPrompt.AppendLine(workOrderHistory);
                }

                if (hasPhotos && diagnosisType == DiagnosisType.Accident)
                {
                    userPrompt.AppendLine();
                    userPrompt.AppendLine("Fotoğraf notu: Önce sceneDescription ile kamera açısını ve kadrajda görünen bölgeleri yaz. Kadrajda görünmeyen kaput/ön tampon/far gibi parçaları damagedParts listesine ekleme; gerekirse sadece criticalChecks altında belirt.");
                }

                // Use vision model when photos are present
                var modelToUse = (hasPhotos && !string.IsNullOrEmpty(_aiOptions.VisionModel))
                    ? _aiOptions.VisionModel
                    : _aiOptions.Model;

                // Accident mode: use Auto detail level for better damage detection
                var imageDetailLevel = diagnosisType == DiagnosisType.Accident
                    ? ChatMessageImageDetailLevel.Auto
                    : ChatMessageImageDetailLevel.Low;

                var deploymentName = ResolveDeploymentName(
                    _aiOptions.Provider == "AzureOpenAI"
                        ? (_aiOptions.AzureDeploymentName ?? modelToUse)
                        : modelToUse);

                // Build user message — multimodal when photos present
                ChatRequestUserMessage userMessage;
                if (hasPhotos)
                {
                    var contentItems = new List<ChatMessageContentItem>();
                    contentItems.Add(new ChatMessageTextContentItem(userPrompt.ToString()));

                    foreach (var photoUrl in photoUrls!)
                    {
                        try
                        {
                            if (photoUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                            {
                                // Public URL (production)
                                // Low: daha az vision token, genelde daha hızlı (Auto’dan düşük gecikme)
                                contentItems.Add(new ChatMessageImageContentItem(new Uri(photoUrl), imageDetailLevel));
                                _logger.LogInformation("AI Vision: Added remote photo {Url}", photoUrl);
                            }
                            else
                            {
                                // Relative path → read from disk as base64 (works on localhost too)
                                var diskPath = Path.Combine(
                                    _webRootPath,
                                    photoUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));

                                if (File.Exists(diskPath))
                                {
                                    var bytes = await File.ReadAllBytesAsync(diskPath, cancellationToken);
                                    var ext = Path.GetExtension(diskPath).ToLowerInvariant();
                                    var mime = ext == ".png" ? "image/png" : "image/jpeg";
                                    contentItems.Add(new ChatMessageImageContentItem(
                                        BinaryData.FromBytes(bytes), mime, imageDetailLevel));
                                    _logger.LogInformation("AI Vision: Added local photo from disk {Path}", diskPath);
                                }
                                else
                                {
                                    _logger.LogWarning("AI Vision: Photo file not found on disk {Path}", diskPath);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "AI Vision: Skipping photo {Url}", photoUrl);
                        }
                    }
                    userMessage = new ChatRequestUserMessage(contentItems);
                }
                else
                {
                    userMessage = new ChatRequestUserMessage(userPrompt.ToString());
                }

                var chatCompletionsOptions = new ChatCompletionsOptions
                {
                    DeploymentName = deploymentName,
                    Messages =
                    {
                        new ChatRequestSystemMessage(systemPrompt),
                        userMessage
                    },
                    // Vision + kaza: düşük sıcaklık, örnek JSON’a yapışmayı azaltır
                    Temperature = (hasPhotos && diagnosisType == DiagnosisType.Accident) ? 0.08f : 0.2f,
                    // Accident mode: panel analizi + mobil Markdown özeti için daha fazla token
                    MaxTokens = diagnosisType == DiagnosisType.Accident ? 3200 : 1400
                };

                // For OpenAI, use non-Azure format
                if (_aiOptions.Provider == "OpenAI")
                {
                    chatCompletionsOptions.ResponseFormat = ChatCompletionsResponseFormat.JsonObject;
                }

                var response = await GetChatCompletionsWithRetryAsync(chatCompletionsOptions, cancellationToken);
                var content = GetFirstAssistantTextOrThrow(response);

                _logger.LogInformation("AI Diagnosis: Received response from OpenAI");

                // Parse JSON response
                var result = ParseDiagnosisResponse(content);

                if (result.DiagnosisType == DiagnosisType.Accident && !string.IsNullOrWhiteSpace(result.SceneDescription))
                    _logger.LogInformation("AI Diagnosis: sceneDescription={Scene}", result.SceneDescription);

                // DB eşleştirme: AI'ın önerdiği parçaları envanter DB'siyle karşılaştır
                await MatchPartsWithDatabaseAsync(result, cancellationToken);

                if (result.DiagnosisType == DiagnosisType.Accident && string.IsNullOrWhiteSpace(result.MobileDisplayMarkdown))
                    result.MobileDisplayMarkdown = AccidentMobileMarkdownFormatter.BuildFallback(result);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AI diagnosis. Falling back to mock result.");
                return await GetMockResultAsync(complaint, vehicleId, cancellationToken);
            }
        }

        public async Task<DiagnosisResultDto> DiagnoseFromVoiceAsync(byte[] audioData, int? vehicleId = null, CancellationToken cancellationToken = default)
        {
            if (_openAIClient == null)
            {
                _logger.LogWarning("OpenAI client not available, falling back to mock service behavior");
                return await GetMockResultAsync("Sesli şikayet (mock)", vehicleId, cancellationToken);
            }

            try
            {
                _logger.LogInformation("AI Diagnosis: Analyzing voice complaint. VehicleId: {VehicleId}, AudioSize: {AudioSize} bytes", 
                    vehicleId, audioData.Length);

                // Convert audio to text using Whisper (if available) or return mock
                // For now, we'll use a mock implementation until Whisper API is properly integrated
                _logger.LogWarning("Voice-to-text conversion not yet implemented. Using mock diagnosis.");
                return await GetMockResultAsync("Sesli şikayet analizi (henüz implement edilmedi)", vehicleId, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in voice diagnosis. Falling back to mock result.");
                return await GetMockResultAsync("Sesli şikayet (hata)", vehicleId, cancellationToken);
            }
        }

        public async Task<GenerateDescriptionResultDto> GenerateShopDescriptionAsync(string shopName, string? address = null, string? phone = null, CancellationToken cancellationToken = default)
        {
            if (_openAIClient == null)
            {
                _logger.LogWarning("OpenAI client not available, returning mock description.");
                return new GenerateDescriptionResultDto
                {
                    Description = $"{shopName}, müşteri memnuniyetini ön planda tutan, uzman kadrosuyla kaliteli oto servis hizmeti sunan güvenilir bir tamir ve bakım merkezidir."
                };
            }

            try
            {
                _logger.LogInformation("AI GenerateDescription: Generating description for shop: {ShopName}", shopName);

                var systemPrompt = "Sen bir pazarlama uzmanısın. Senden bir oto tamircisi veya servis yeri için kısa, özgün ve profesyonel bir tanıtım metni yazmanı istiyorum. Metin 2-3 cümle olmalı, samimi ve güven verici bir dil kullanmalı, SEO dostu anahtar kelimeler içermeli. Sadece metni döndür, başka açıklama ekleme.";

                var userPromptBuilder = new StringBuilder();
                userPromptBuilder.AppendLine($"Servis Adı: {shopName}");
                if (!string.IsNullOrWhiteSpace(address))
                    userPromptBuilder.AppendLine($"Adres: {address}");
                if (!string.IsNullOrWhiteSpace(phone))
                    userPromptBuilder.AppendLine($"Telefon: {phone}");
                userPromptBuilder.AppendLine("\nBu servise özgün, kısa ve profesyonel bir tanıtım metni yaz.");

                var deploymentName = ResolveDeploymentName(
                    _aiOptions.Provider == "AzureOpenAI"
                        ? (_aiOptions.AzureDeploymentName ?? _aiOptions.Model)
                        : _aiOptions.Model);

                var chatCompletionsOptions = new ChatCompletionsOptions
                {
                    DeploymentName = deploymentName,
                    Messages =
                    {
                        new ChatRequestSystemMessage(systemPrompt),
                        new ChatRequestUserMessage(userPromptBuilder.ToString())
                    },
                    Temperature = 0.9f,
                    MaxTokens = 300
                };

                var response = await GetChatCompletionsWithRetryAsync(chatCompletionsOptions, cancellationToken);
                var description = GetFirstAssistantTextOrNull(response)?.Trim() ?? string.Empty;

                _logger.LogInformation("AI GenerateDescription: Description generated successfully.");

                return new GenerateDescriptionResultDto { Description = description };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating shop description. Returning mock.");
                return new GenerateDescriptionResultDto
                {
                    Description = $"{shopName}, müşteri memnuniyetini ön planda tutan, uzman kadrosuyla kaliteli oto servis hizmeti sunan güvenilir bir tamir ve bakım merkezidir."
                };
            }
        }

        private OpenAIClientOptions CreateOpenAIClientOptions()
        {
            // Azure.Core retry happens inside the SDK pipeline. We turn it off to avoid "retry x retry"
            // and keep retry behavior centralized in GetChatCompletionsWithRetryAsync().
            var options = new OpenAIClientOptions();
            options.Retry.MaxRetries = 0;

            // This is per-try network timeout inside the SDK pipeline.
            // (We still wrap with our own retry/backoff above this layer.)
            if (_aiOptions.TimeoutSeconds > 0)
                options.Retry.NetworkTimeout = TimeSpan.FromSeconds(_aiOptions.TimeoutSeconds);

            return options;
        }

        /// <summary>
        /// Config'ten gelen model/deployment adını normalize eder; boşsa güvenli varsayılan döner.
        /// </summary>
        private string ResolveDeploymentName(string? modelOrDeploymentCandidate)
        {
            if (!string.IsNullOrWhiteSpace(modelOrDeploymentCandidate))
                return modelOrDeploymentCandidate.Trim();
            if (!string.IsNullOrWhiteSpace(_aiOptions.Model))
                return _aiOptions.Model.Trim();
            return "gpt-4o-mini";
        }

        private static string? GetFirstAssistantTextOrNull(Response<ChatCompletions> response)
        {
            var choice = response.Value?.Choices?.FirstOrDefault();
            return choice?.Message?.Content;
        }

        private static string GetFirstAssistantTextOrThrow(Response<ChatCompletions> response)
        {
            var text = GetFirstAssistantTextOrNull(response);
            if (string.IsNullOrWhiteSpace(text))
                throw new InvalidOperationException("OpenAI yanıtı boş veya geçersiz: choices veya assistant mesajı yok.");
            return text;
        }

        private async Task<Response<ChatCompletions>> GetChatCompletionsWithRetryAsync(
            ChatCompletionsOptions options,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(options);

            var client = _openAIClient ?? throw new InvalidOperationException("OpenAI client not initialized.");
            if (string.IsNullOrWhiteSpace(options.DeploymentName))
                throw new InvalidOperationException("AI model/deployment adı boş. AIOptions:Model veya AzureDeploymentName ayarlayın.");
            if (options.Messages == null || options.Messages.Count == 0)
                throw new InvalidOperationException("AI chat mesaj listesi boş.");

            var deploymentName = options.DeploymentName;

            var maxAttempts = Math.Max(1, _aiOptions.MaxRetryAttempts);

            for (var attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    return await client.GetChatCompletionsAsync(options, cancellationToken);
                }
                catch (NullReferenceException ex)
                {
                    _logger.LogError(ex,
                        "OpenAI SDK NullReferenceException. DeploymentName={DeploymentName}, MessageCount={MessageCount}, Provider={Provider}",
                        deploymentName, options.Messages.Count, _aiOptions.Provider);
                    throw new InvalidOperationException(
                        "OpenAI çağrısı beklenmedik şekilde başarısız (SDK null reference). Model adı, API anahtarı ve endpoint yapılandırmasını doğrulayın.",
                        ex);
                }
                catch (RequestFailedException ex) when (IsTransientOpenAIError(ex.Status) && attempt < maxAttempts)
                {
                    var delay = GetRetryDelay(attempt);
                    _logger.LogWarning(ex,
                        "Transient AI request failure (Status: {Status}) on attempt {Attempt}/{MaxAttempts}. Retrying in {DelayMs}ms. Provider: {Provider}, Model/Deployment: {DeploymentName}",
                        ex.Status, attempt, maxAttempts, (int)delay.TotalMilliseconds, _aiOptions.Provider, deploymentName);
                    await Task.Delay(delay, cancellationToken);
                }
                catch (HttpRequestException ex) when (attempt < maxAttempts)
                {
                    var delay = GetRetryDelay(attempt);
                    _logger.LogWarning(ex,
                        "Transient AI network failure on attempt {Attempt}/{MaxAttempts}. Retrying in {DelayMs}ms. Provider: {Provider}, Model/Deployment: {DeploymentName}",
                        attempt, maxAttempts, (int)delay.TotalMilliseconds, _aiOptions.Provider, deploymentName);
                    await Task.Delay(delay, cancellationToken);
                }
                catch (HttpIOException ex) when (attempt < maxAttempts)
                {
                    // e.g. "The response ended prematurely. (ResponseEnded)" — proxy/edge closed connection mid-body
                    var delay = GetRetryDelay(attempt);
                    _logger.LogWarning(ex,
                        "Transient AI HTTP I/O failure on attempt {Attempt}/{MaxAttempts}. Retrying in {DelayMs}ms. Provider: {Provider}, Model/Deployment: {DeploymentName}",
                        attempt, maxAttempts, (int)delay.TotalMilliseconds, _aiOptions.Provider, deploymentName);
                    await Task.Delay(delay, cancellationToken);
                }
                catch (TaskCanceledException ex) when (attempt < maxAttempts && !cancellationToken.IsCancellationRequested)
                {
                    // Often HTTP timeout (not user cancel)
                    var delay = GetRetryDelay(attempt);
                    _logger.LogWarning(ex,
                        "Transient AI request timeout on attempt {Attempt}/{MaxAttempts}. Retrying in {DelayMs}ms. Provider: {Provider}, Model/Deployment: {DeploymentName}",
                        attempt, maxAttempts, (int)delay.TotalMilliseconds, _aiOptions.Provider, deploymentName);
                    await Task.Delay(delay, cancellationToken);
                }
            }

            throw new InvalidOperationException("Failed to get chat completions after retries.");
        }

        private static bool IsTransientOpenAIError(int status)
        {
            // Cloudflare/edge transient errors + rate limiting
            return status is 408 or 429 or 500 or 502 or 503 or 504;
        }

        private static TimeSpan GetRetryDelay(int attempt)
        {
            // Exponential backoff (1s, 2s, 4s) + small jitter
            var baseMs = (int)(1000 * Math.Pow(2, attempt - 1));
            var jitterMs = RandomNumberGenerator.GetInt32(0, 250);
            return TimeSpan.FromMilliseconds(baseMs + jitterMs);
        }

        private static string NormalizeOpenAIBaseUrl(string? baseUrl)
        {
            var url = string.IsNullOrWhiteSpace(baseUrl) ? "https://api.openai.com" : baseUrl.Trim();

            // With Azure.AI.OpenAI beta versions, providing ".../v1" can lead to path duplication
            // depending on which API surface is used.
            if (url.EndsWith("/v1", StringComparison.OrdinalIgnoreCase))
                url = url[..^3];

            url = url.TrimEnd('/');
            return url;
        }

        private async Task MatchPartsWithDatabaseAsync(DiagnosisResultDto result, CancellationToken cancellationToken)
        {
            if (result.RecommendedParts.Count == 0) return;

            // Önceden sıralı N arama → LLM sonrası ek gecikme; paralel + sınırlı eşzamanlılık
            const int maxConcurrent = 4;
            using var semaphore = new SemaphoreSlim(maxConcurrent, maxConcurrent);
            var tasks = result.RecommendedParts.Select(part => MatchOnePartWithDatabaseAsync(part, semaphore, cancellationToken));
            await Task.WhenAll(tasks);
        }

        private async Task MatchOnePartWithDatabaseAsync(
            RecommendedPartDto part,
            SemaphoreSlim semaphore,
            CancellationToken cancellationToken)
        {
            await semaphore.WaitAsync(cancellationToken);
            try
            {
                var matches = await _partRepository.SearchAsync(part.PartName, cancellationToken);
                var best = FindBestMatch(part.PartName, matches);

                if (best != null)
                {
                    var withStock = await _partRepository.GetWithStockAsync(best.Id, cancellationToken);
                    var stockQty = withStock?.Stock?.Quantity ?? 0;

                    part.PartId = best.Id;
                    part.RealPrice = best.SalePrice;
                    part.IsInDatabase = true;
                    part.IsInStock = stockQty > 0;
                    part.StockQuantity = stockQty;
                }
                else
                {
                    part.IsInDatabase = false;
                    part.IsInStock = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "DB match failed for part: {PartName}", part.PartName);
                part.IsInDatabase = false;
                part.IsInStock = false;
            }
            finally
            {
                semaphore.Release();
            }
        }

        private static Part? FindBestMatch(string aiPartName, List<Part> candidates)
        {
            if (candidates.Count == 0) return null;

            var normalized = NormalizeToken(aiPartName);
            var aiTokens = normalized.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Part? best = null;
            int bestScore = 0;

            foreach (var candidate in candidates)
            {
                var dbNorm = NormalizeToken(candidate.Name);
                int score;

                if (dbNorm == normalized)
                {
                    score = 100;
                }
                else if (dbNorm.Contains(normalized) || normalized.Contains(dbNorm))
                {
                    score = 80;
                }
                else
                {
                    var dbTokens = dbNorm.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var common = aiTokens.Intersect(dbTokens).Count();
                    score = common > 0 ? (common * 60) / Math.Max(aiTokens.Length, dbTokens.Length) : 0;
                }

                if (score > bestScore && score >= 50)
                {
                    bestScore = score;
                    best = candidate;
                }
            }

            return best;
        }

        private static DiagnosisType DetectDiagnosisType(string complaint)
        {
            var lower = complaint.ToLowerInvariant();
            string[] accidentKeywords = ["kaza", "çarptı", "çarpma", "hasar", "göçük", "kaporta", "ezik",
                "deformasyon", "pert", "kırdı", "kırık", "ezildi", "darbe", "dent", "crash", "accident", "collision", "dented", "damaged"];
            string[] maintenanceKeywords = ["bakım", "servis", "yağ değişimi", "filtre değişimi", "periyodik", "maintenance", "oil change"];

            if (accidentKeywords.Any(k => lower.Contains(k))) return DiagnosisType.Accident;
            if (maintenanceKeywords.Any(k => lower.Contains(k))) return DiagnosisType.Maintenance;
            return DiagnosisType.Mechanical;
        }

        private static string NormalizeToken(string input) =>
            input.ToLowerInvariant()
                 .Replace("ı", "i").Replace("ğ", "g").Replace("ü", "u")
                 .Replace("ş", "s").Replace("ö", "o").Replace("ç", "c")
                 .Trim();

        private DiagnosisResultDto ParseDiagnosisResponse(string jsonContent)
        {
            try
            {
                using var doc = JsonDocument.Parse(jsonContent);
                var root = doc.RootElement;

                var result = new DiagnosisResultDto
                {
                    PossibleIssues = root.GetProperty("possibleIssues").EnumerateArray()
                        .Select(issue => new DiagnosisItemDto
                        {
                            IssueName = issue.GetProperty("issueName").GetString() ?? "",
                            Description = issue.TryGetProperty("description", out var desc) ? desc.GetString() : null,
                            ProbabilityScore = issue.TryGetProperty("probabilityScore", out var prob) ? prob.GetInt32() : 50,
                            Category = issue.TryGetProperty("category", out var cat) ? cat.GetString() : null
                        }).ToList(),

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

                    EstimatedDays = root.TryGetProperty("estimatedDays", out var days) ? days.GetInt32() : null,
                    EstimatedCost = root.TryGetProperty("estimatedCost", out var cost) ? cost.GetDecimal() : null,
                    ConfidenceScore = root.TryGetProperty("confidenceScore", out var conf) ? conf.GetInt32() : 75,
                    Recommendations = root.TryGetProperty("recommendations", out var rec) ? rec.GetString() : null
                };

                // Parse DiagnosisType
                if (root.TryGetProperty("diagnosisType", out var diagTypeEl))
                    result.DiagnosisType = (DiagnosisType)diagTypeEl.GetInt32();

                // Parse DamagedParts
                if (root.TryGetProperty("damagedParts", out var damagedPartsEl) && damagedPartsEl.ValueKind == JsonValueKind.Array)
                {
                    foreach (var partEl in damagedPartsEl.EnumerateArray())
                    {
                        result.DamagedParts.Add(new DamagedPartDto
                        {
                            PartName = partEl.TryGetProperty("partName", out var pn) ? pn.GetString() ?? "" : "",
                            DamageLevel = partEl.TryGetProperty("damageLevel", out var dl) ? (DamageLevel)dl.GetInt32() : DamageLevel.None,
                            RecommendedAction = partEl.TryGetProperty("recommendedAction", out var ra) ? (RepairAction)ra.GetInt32() : RepairAction.None,
                            ConfidencePercent = partEl.TryGetProperty("confidencePercent", out var cp) ? cp.GetInt32() : 0,
                            EstimatedCostMin = partEl.TryGetProperty("estimatedCostMin", out var ecMin) && ecMin.ValueKind != JsonValueKind.Null ? ecMin.GetDecimal() : null,
                            EstimatedCostMax = partEl.TryGetProperty("estimatedCostMax", out var ecMax) && ecMax.ValueKind != JsonValueKind.Null ? ecMax.GetDecimal() : null,
                            Notes = partEl.TryGetProperty("notes", out var notes) ? notes.GetString() : null,
                        });
                    }
                }

                // Parse CriticalChecks
                if (root.TryGetProperty("criticalChecks", out var criticalChecksEl) && criticalChecksEl.ValueKind == JsonValueKind.Array)
                {
                    foreach (var checkEl in criticalChecksEl.EnumerateArray())
                    {
                        result.CriticalChecks.Add(new CriticalCheckDto
                        {
                            ComponentName = checkEl.TryGetProperty("componentName", out var cn) ? cn.GetString() ?? "" : "",
                            RiskLevel = checkEl.TryGetProperty("riskLevel", out var rl) ? (RiskLevel)rl.GetInt32() : RiskLevel.Low,
                            Warning = checkEl.TryGetProperty("warning", out var w) ? w.GetString() ?? "" : "",
                            RequiresImmediateInspection = checkEl.TryGetProperty("requiresImmediateInspection", out var rii) && rii.GetBoolean(),
                        });
                    }
                }

                // Parse EstimatedRepairRange
                if (root.TryGetProperty("estimatedRepairRange", out var repairRangeEl) && repairRangeEl.ValueKind == JsonValueKind.Object)
                {
                    result.EstimatedRepairRange = new EstimatedRepairRangeDto
                    {
                        Min = repairRangeEl.TryGetProperty("min", out var minEl) ? minEl.GetDecimal() : 0,
                        Max = repairRangeEl.TryGetProperty("max", out var maxEl) ? maxEl.GetDecimal() : 0,
                        Currency = repairRangeEl.TryGetProperty("currency", out var currEl) ? currEl.GetString() ?? "TRY" : "TRY",
                    };
                }

                if (root.TryGetProperty("mobileDisplayMarkdown", out var mobileMdEl) && mobileMdEl.ValueKind == JsonValueKind.String)
                    result.MobileDisplayMarkdown = mobileMdEl.GetString();

                if (root.TryGetProperty("sceneDescription", out var sceneEl) && sceneEl.ValueKind == JsonValueKind.String)
                    result.SceneDescription = sceneEl.GetString();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error parsing AI response: {Content}", jsonContent);
                // Return a basic result if parsing fails
                return new DiagnosisResultDto
                {
                    ConfidenceScore = 50,
                    Recommendations = "AI yanıtı parse edilemedi. Manuel kontrol önerilir.",
                    PossibleIssues = new List<DiagnosisItemDto>
                    {
                        new DiagnosisItemDto
                        {
                            IssueName = "Genel Arıza",
                            Description = jsonContent,
                            ProbabilityScore = 50,
                            Category = "Diğer"
                        }
                    }
                };
            }
        }

        private async Task<DiagnosisResultDto> GetMockResultAsync(string complaint, int? vehicleId, CancellationToken cancellationToken)
        {
            await Task.Delay(300, cancellationToken);

            var c = complaint.ToLowerInvariant();

            string[] accidentKeywords = ["kaza", "çarptı", "çarpma", "hasar", "göçük", "kaporta", "ezik", "darbe", "dent", "crash", "accident"];
            if (accidentKeywords.Any(k => c.Contains(k)))
            {
                var accidentMock = new DiagnosisResultDto
                {
                    DiagnosisType = DiagnosisType.Accident,
                    ConfidenceScore = 82,
                    EstimatedDays = 7,
                    EstimatedCost = 120_000m,
                    Recommendations = "Bu hasar “ön ağır hasar” bandına girebilir. Airbag açmadıysa iyi, ama şase kontrolü şart.",
                    PossibleIssues =
                    [
                        new DiagnosisItemDto { IssueName = "Ön Kaporta Hasarı", Description = "Ön tampon ve kaput bölgesinde kaza hasarı", ProbabilityScore = 92, Category = "Kaporta" },
                        new DiagnosisItemDto { IssueName = "Şase Hasarı Riski", Description = "Darbe şiddetine göre şase bütünlüğü tehlikte olabilir", ProbabilityScore = 65, Category = "Şase" }
                    ],
                    RecommendedParts =
                    [
                        new RecommendedPartDto { PartName = "Ön Tampon", Category = "Kaporta", EstimatedPrice = 15_000m, Quantity = 1, ProbabilityScore = 95 },
                        new RecommendedPartDto { PartName = "Kaput", Category = "Kaporta", EstimatedPrice = 20_000m, Quantity = 1, ProbabilityScore = 80 }
                    ],
                    RecommendedLabors =
                    [
                        new RecommendedLaborDto { LaborName = "Kaporta Düzeltme ve Boya", Description = "Hasarlı panellerin düzeltilmesi ve boyanması", EstimatedPrice = 30_000m, EstimatedHours = 12, ProbabilityScore = 90 }
                    ],
                    DamagedParts =
                    [
                        new DamagedPartDto { PartName = "Ön Tampon", DamageLevel = DamageLevel.Heavy, RecommendedAction = RepairAction.Replace, ConfidencePercent = 95, EstimatedCostMin = 10_000m, EstimatedCostMax = 20_000m, Notes = "Kırık/dağılmış → değişim" },
                        new DamagedPartDto { PartName = "Kaput", DamageLevel = DamageLevel.Heavy, RecommendedAction = RepairAction.Replace, ConfidencePercent = 90, EstimatedCostMin = 15_000m, EstimatedCostMax = 25_000m, Notes = "Ağır göçük → kesin değişim" },
                        new DamagedPartDto { PartName = "Sağ far", DamageLevel = DamageLevel.Heavy, RecommendedAction = RepairAction.Replace, ConfidencePercent = 85, EstimatedCostMin = 5_000m, EstimatedCostMax = 15_000m, Notes = "Büyük ihtimal kırık" }
                    ],
                    CriticalChecks =
                    [
                        new CriticalCheckDto { ComponentName = "Radyatör / klima radyatörü", RiskLevel = RiskLevel.High, Warning = "Ön darbe soğutmayı etkileyebilir", RequiresImmediateInspection = true },
                        new CriticalCheckDto { ComponentName = "Şase uçları (ön şase kolları)", RiskLevel = RiskLevel.High, Warning = "Geometri ve bütünlük kontrolü", RequiresImmediateInspection = true },
                        new CriticalCheckDto { ComponentName = "Motor & şanzıman", RiskLevel = RiskLevel.Medium, Warning = "Şiddetli darbede takoz/mil kontrolü", RequiresImmediateInspection = false }
                    ],
                    EstimatedRepairRange = new EstimatedRepairRangeDto { Min = 80_000m, Max = 180_000m, Currency = "TRY" }
                };
                accidentMock.MobileDisplayMarkdown = AccidentMobileMarkdownFormatter.BuildFallback(accidentMock);
                return accidentMock;
            }

            // Detect symptom category from complaint keywords
            bool isWindow  = c.Contains("cam") || c.Contains("krikok") || c.Contains("pencere");
            bool isBrake   = c.Contains("fren") || c.Contains("titreme") || c.Contains("pedal");
            bool isEngine  = c.Contains("motor") || c.Contains("güç") || c.Contains("çalışmıyor") || c.Contains("marş");
            bool isSteering= c.Contains("direksiyon") || c.Contains("volan") || c.Contains("çekiyor");
            bool isAC      = c.Contains("klima") || c.Contains("soğutmuyor") || c.Contains("ısıtmıyor") || c.Contains("havalandırma");
            bool isExhaust = c.Contains("egzoz") || c.Contains("duman") || c.Contains("koku") || c.Contains("is");
            bool isSuspension = c.Contains("süspansiyon") || c.Contains("amortisör") || c.Contains("ses") || c.Contains("vızıltı");
            bool isFuel    = c.Contains("yakıt") || c.Contains("benzin") || c.Contains("mazot") || c.Contains("tüketim");
            bool isDoor    = c.Contains("kapı") || c.Contains("kilit") || c.Contains("ayna") || c.Contains("açılmıyor");

            if (isWindow)
            {
                return BuildMockResult(
                    "Cam Krikosu Arızası", "Cam krikosu motoru veya mekanizması arızalı olabilir. Cam yavaş çalışması veya kapanmakta zorlanması cam krikosu ile ilgilidir.", "Elektrik", 88,
                    new[] {
                        ("Cam Krikosu Motoru", "Elektrikli Sistem", 1200m, 1, 88),
                        ("Cam Krikosu Mekanizması", "Elektrikli Sistem", 400m, 1, 75),
                        ("Cam Krikosu Switchi", "Elektrikli Sistem", 150m, 1, 60)
                    },
                    new[] {
                        ("Cam Krikosu Söküm/Takım", "Kapı paneli sökülüp krikok değiştirilir", 250m, 1.5m, 95),
                        ("Elektrik Testi", "Krikok elektrik hattı test edilir", 100m, 0.5m, 85)
                    },
                    1, 1800m, "Cam krikosu motorunu ve mekanizmasını kontrol edin. Zorla açıp kapatmayın, mekanizma daha fazla zarar görebilir.");
            }

            if (isBrake)
            {
                return BuildMockResult(
                    "Fren Sistemi Arızası", "Fren balataları veya disk aşınmış olabilir.", "Fren", 85,
                    new[] {
                        ("Fren Balatası (Ön)", "Fren Sistemi", 350m, 2, 90),
                        ("Fren Diski (Ön)", "Fren Sistemi", 600m, 2, 75),
                        ("Fren Balatası (Arka)", "Fren Sistemi", 280m, 2, 65)
                    },
                    new[] {
                        ("Fren Balata Değişimi", "Ön/arka fren balataları ve diskler kontrol edilir", 300m, 1.5m, 95),
                        ("Fren Sistemi Kontrolü", "Fren hidroliği ve boru hattı kontrolü", 100m, 0.5m, 80)
                    },
                    1, 1350m, "Fren sistemini vakit geçirmeden kontrol ettirin. Sürüş güvenliğini etkiler.");
            }

            if (isAC)
            {
                return BuildMockResult(
                    "Klima Sistemi Arızası", "Klima gazı düşük olabilir veya kompresör arızalı.", "Klima", 82,
                    new[] {
                        ("Klima Gazı (R134a)", "Klima Sistemi", 400m, 1, 85),
                        ("Klima Kompresörü", "Klima Sistemi", 2500m, 1, 60),
                        ("Polen Filtresi", "Klima Sistemi", 120m, 1, 70)
                    },
                    new[] {
                        ("Klima Gaz Dolumu", "Gaz kaçağı kontrolü ve dolum yapılır", 350m, 1m, 90),
                        ("Kompresör Kontrolü", "Kompresör ve valf kontrolü", 150m, 0.5m, 75)
                    },
                    1, 870m, "Klima gazı seviyesini ve kompresörü kontrol ettirin.");
            }

            if (isEngine || isFuel)
            {
                return BuildMockResult(
                    "Motor/Yakıt Sistemi Arızası", "Motor ateşleme sistemi veya yakıt sistemi arızalı olabilir.", "Motor", 78,
                    new[] {
                        ("Ateşleme Bujisi", "Motor Sistemi", 80m, 4, 80),
                        ("Hava Filtresi", "Motor Sistemi", 150m, 1, 70),
                        ("Yakıt Filtresi", "Yakıt Sistemi", 200m, 1, 65)
                    },
                    new[] {
                        ("Motor Diagnostik Testi", "OBD cihazıyla arıza kodu okunur", 150m, 0.5m, 95),
                        ("Buji Değişimi", "Tüm bujiler kontrol edilip gerekirse değiştirilir", 200m, 1m, 80)
                    },
                    2, 780m, "Motor diagnostik testini yaptırın. Arıza kodları sorunu net belirleyecektir.");
            }

            if (isSuspension)
            {
                return BuildMockResult(
                    "Süspansiyon Arızası", "Amortisör veya rot-balansman sorunu olabilir.", "Süspansiyon", 80,
                    new[] {
                        ("Amortisör (Ön)", "Süspansiyon", 800m, 2, 82),
                        ("Rot Başı", "Süspansiyon", 250m, 2, 75),
                        ("Takoz (Yaylık)", "Süspansiyon", 180m, 2, 68)
                    },
                    new[] {
                        ("Süspansiyon Kontrolü", "Amortisör, rot, rotil ve mafsallar kontrol edilir", 150m, 1m, 95),
                        ("Rot-Balans Ayarı", "Direksiyon geometrisi ayarlanır", 200m, 1m, 85)
                    },
                    2, 1580m, "Süspansiyon sistemini kontrol ettirin. Uzun süre ertelenmesi lastik aşınmasına yol açar.");
            }

            if (isDoor)
            {
                return BuildMockResult(
                    "Kapı/Kilit Mekanizması Arızası", "Kapı kilidi veya menteşesi sorunlu olabilir.", "Kaporta", 80,
                    new[] {
                        ("Kapı Kilidi Motoru", "Elektrikli Sistem", 350m, 1, 82),
                        ("Kapı Menteşesi", "Kaporta", 200m, 2, 70),
                        ("Kapı Kolu", "Kaporta", 250m, 1, 65)
                    },
                    new[] {
                        ("Kapı Mekanizması Kontrolü", "Kilit, menteşe ve kol kontrol edilir", 100m, 0.5m, 95),
                        ("Kapı Hizalama", "Kapı hizası ve kapama ayarı yapılır", 150m, 1m, 70)
                    },
                    1, 800m, "Kapı mekanizmasını kontrol ettirin.");
            }

            // Generic fallback — no guessing specific parts
            return new DiagnosisResultDto
            {
                ConfidenceScore = 55,
                EstimatedDays = 1,
                EstimatedCost = 200m,
                Recommendations = "Şikayeti daha ayrıntılı açıklarsanız daha doğru teşhis yapılabilir. Servis teknisyenine aracınızı getirip incelemesini öneririz.",
                PossibleIssues = new List<DiagnosisItemDto>
                {
                    new DiagnosisItemDto
                    {
                        IssueName = "Belirsiz Arıza",
                        Description = "Semptom birden fazla sistemi işaret edebilir. Teknisyen incelemesi gerekli.",
                        ProbabilityScore = 55,
                        Category = "Diğer"
                    }
                },
                RecommendedParts = new List<RecommendedPartDto>(),
                RecommendedLabors = new List<RecommendedLaborDto>
                {
                    new RecommendedLaborDto
                    {
                        LaborName = "Genel Arıza Tespiti",
                        Description = "Aracın teknisyen tarafından incelenmesi",
                        EstimatedPrice = 200m,
                        EstimatedHours = 1,
                        ProbabilityScore = 100
                    }
                }
            };
        }

        private DiagnosisResultDto BuildMockResult(
            string issueName, string issueDesc, string category, int issueScore,
            (string name, string cat, decimal price, int qty, int score)[] parts,
            (string name, string desc, decimal price, decimal hours, int score)[] labors,
            int days, decimal cost, string recommendation)
        {
            return new DiagnosisResultDto
            {
                ConfidenceScore = issueScore,
                EstimatedDays = days,
                EstimatedCost = cost,
                Recommendations = recommendation,
                PossibleIssues = new List<DiagnosisItemDto>
                {
                    new DiagnosisItemDto { IssueName = issueName, Description = issueDesc, ProbabilityScore = issueScore, Category = category }
                },
                RecommendedParts = parts.Select(p => new RecommendedPartDto
                {
                    PartName = p.name, Category = p.cat, EstimatedPrice = p.price, Quantity = p.qty, ProbabilityScore = p.score
                }).ToList(),
                RecommendedLabors = labors.Select(l => new RecommendedLaborDto
                {
                    LaborName = l.name, Description = l.desc, EstimatedPrice = l.price, EstimatedHours = l.hours, ProbabilityScore = l.score
                }).ToList()
            };
        }
    }
}

