using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using MagicCarRepairAISupported.Application.Common.Services.Notification;
using MagicCarRepairAISupported.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using FirebaseMessaging = FirebaseAdmin.Messaging.FirebaseMessaging;
using FirebaseNotification = FirebaseAdmin.Messaging.Notification;
using FirebaseMessage = FirebaseAdmin.Messaging.Message;

namespace MagicCarRepairAISupported.Infrastructure.Services.Notification
{
    /// <summary>
    /// Firebase Cloud Messaging (FCM) servisi implementasyonu
    /// </summary>
    public class FCMNotificationService : IFCMNotificationService
    {
        private readonly ILogger<FCMNotificationService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserDeviceTokenRepository _deviceTokenRepository;
        private FirebaseMessaging? _messaging;

        public FCMNotificationService(
            ILogger<FCMNotificationService> logger,
            IConfiguration configuration,
            IUserDeviceTokenRepository deviceTokenRepository)
        {
            _logger = logger;
            _configuration = configuration;
            _deviceTokenRepository = deviceTokenRepository;
            InitializeFirebase();
        }

        private void InitializeFirebase()
        {
            try
            {
                var firebaseConfigPath = _configuration["FCM:ServiceAccountPath"];
                var firebaseProjectId = _configuration["FCM:ProjectId"];

                if (string.IsNullOrEmpty(firebaseConfigPath) && string.IsNullOrEmpty(firebaseProjectId))
                {
                    _logger.LogWarning("FCM configuration not found. Push notifications will be disabled.");
                    return;
                }

                // Firebase Admin SDK'yı initialize et
                if (FirebaseApp.DefaultInstance == null)
                {
                    if (!string.IsNullOrEmpty(firebaseConfigPath) && File.Exists(firebaseConfigPath))
                    {
                        FirebaseApp.Create(new AppOptions
                        {
                            Credential = GoogleCredential.FromFile(firebaseConfigPath),
                            ProjectId = firebaseProjectId
                        });
                    }
                    else if (!string.IsNullOrEmpty(firebaseProjectId))
                    {
                        // Environment variable'dan credential al
                        var credentialJson = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS_JSON");
                        if (!string.IsNullOrEmpty(credentialJson))
                        {
                            FirebaseApp.Create(new AppOptions
                            {
                                Credential = GoogleCredential.FromJson(credentialJson),
                                ProjectId = firebaseProjectId
                            });
                        }
                        else
                        {
                            _logger.LogWarning("FCM credentials not found. Push notifications will be disabled.");
                            return;
                        }
                    }
                }

                _messaging = FirebaseMessaging.DefaultInstance;
                _logger.LogInformation("FCM initialized successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize FCM. Push notifications will be disabled.");
            }
        }

        public async Task<bool> SendToTokenAsync(string token, string title, string body, Dictionary<string, object>? data = null, CancellationToken cancellationToken = default)
        {
            if (_messaging == null)
            {
                _logger.LogWarning("FCM not initialized. Push notification not sent.");
                return false;
            }

            try
            {
                var message = new FirebaseMessage
                {
                    Token = token,
                    Notification = new FirebaseNotification
                    {
                        Title = title,
                        Body = body
                    },
                    Data = data?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.ToString() ?? string.Empty),
                    Android = new AndroidConfig
                    {
                        Priority = FirebaseAdmin.Messaging.Priority.High,
                        Notification = new AndroidNotification
                        {
                            Sound = "default",
                            ChannelId = "default"
                        }
                    },
                    Apns = new ApnsConfig
                    {
                        Aps = new Aps
                        {
                            Alert = new ApsAlert
                            {
                                Title = title,
                                Body = body
                            },
                            Sound = "default",
                            Badge = 1
                        }
                    }
                };

                var response = await _messaging.SendAsync(message, cancellationToken);
                _logger.LogInformation($"FCM notification sent successfully. Message ID: {response}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send FCM notification to token: {token}");
                return false;
            }
        }

        public async Task<Dictionary<string, bool>> SendToTokensAsync(List<string> tokens, string title, string body, Dictionary<string, object>? data = null, CancellationToken cancellationToken = default)
        {
            var results = new Dictionary<string, bool>();

            if (_messaging == null)
            {
                _logger.LogWarning("FCM not initialized. Push notifications not sent.");
                foreach (var token in tokens)
                {
                    results[token] = false;
                }
                return results;
            }

            try
            {
                var message = new MulticastMessage
                {
                    Tokens = tokens,
                    Notification = new FirebaseNotification
                    {
                        Title = title,
                        Body = body
                    },
                    Data = data?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.ToString() ?? string.Empty),
                    Android = new AndroidConfig
                    {
                        Priority = FirebaseAdmin.Messaging.Priority.High,
                        Notification = new AndroidNotification
                        {
                            Sound = "default",
                            ChannelId = "default"
                        }
                    },
                    Apns = new ApnsConfig
                    {
                        Aps = new Aps
                        {
                            Alert = new ApsAlert
                            {
                                Title = title,
                                Body = body
                            },
                            Sound = "default",
                            Badge = 1
                        }
                    }
                };

                var response = await _messaging.SendMulticastAsync(message, cancellationToken);
                
                // Sonuçları işle
                for (int i = 0; i < tokens.Count && i < response.Responses.Count; i++)
                {
                    var token = tokens[i];
                    var sendResponse = response.Responses[i];
                    results[token] = sendResponse.IsSuccess;
                    
                    if (!sendResponse.IsSuccess)
                    {
                        _logger.LogWarning($"Failed to send FCM notification to token {token}: {sendResponse.Exception?.Message}");
                    }
                }

                _logger.LogInformation($"FCM multicast notification sent. Success: {response.SuccessCount}, Failure: {response.FailureCount}");
                return results;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send FCM multicast notification");
                foreach (var token in tokens)
                {
                    results[token] = false;
                }
                return results;
            }
        }

        public async Task<bool> SendToUserAsync(int userId, string title, string body, Dictionary<string, object>? data = null, CancellationToken cancellationToken = default)
        {
            if (_messaging == null)
            {
                _logger.LogWarning("FCM not initialized. Push notification not sent.");
                return false;
            }

            try
            {
                // Kullanıcının aktif device token'larını al
                var deviceTokens = await _deviceTokenRepository.GetActiveTokensByUserIdAsync(userId, cancellationToken);
                
                if (deviceTokens == null || !deviceTokens.Any())
                {
                    _logger.LogWarning($"No active device tokens found for user {userId}");
                    return false;
                }

                // Token string'lerini al
                var tokens = deviceTokens.Select(dt => dt.Token).Where(t => !string.IsNullOrEmpty(t)).ToList();
                
                if (!tokens.Any())
                {
                    _logger.LogWarning($"No valid tokens found for user {userId}");
                    return false;
                }

                // Multicast message gönder
                var results = await SendToTokensAsync(tokens, title, body, data, cancellationToken);
                
                // En az bir başarılı gönderim varsa true döndür
                var successCount = results.Values.Count(r => r);
                _logger.LogInformation($"Sent notification to user {userId}. Success: {successCount}/{tokens.Count}");
                
                return successCount > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send notification to user {userId}");
                return false;
            }
        }

        public async Task<bool> SendToTopicAsync(string topic, string title, string body, Dictionary<string, object>? data = null, CancellationToken cancellationToken = default)
        {
            if (_messaging == null)
            {
                _logger.LogWarning("FCM not initialized. Push notification not sent.");
                return false;
            }

            try
            {
                var message = new FirebaseMessage
                {
                    Topic = topic,
                    Notification = new FirebaseNotification
                    {
                        Title = title,
                        Body = body
                    },
                    Data = data?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.ToString() ?? string.Empty),
                    Android = new AndroidConfig
                    {
                        Priority = FirebaseAdmin.Messaging.Priority.High
                    },
                    Apns = new ApnsConfig
                    {
                        Aps = new Aps
                        {
                            Alert = new ApsAlert
                            {
                                Title = title,
                                Body = body
                            }
                        }
                    }
                };

                var response = await _messaging.SendAsync(message, cancellationToken);
                _logger.LogInformation($"FCM topic notification sent successfully. Topic: {topic}, Message ID: {response}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send FCM topic notification to topic: {topic}");
                return false;
            }
        }
    }
}
