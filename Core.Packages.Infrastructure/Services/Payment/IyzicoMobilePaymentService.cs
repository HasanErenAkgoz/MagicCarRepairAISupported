using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Payment;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MagicCarRepairAISupported.Infrastructure.Services.Payment
{
    /// <summary>
    /// İyzico mobil ödeme servisi (Mobile SDK için)
    /// </summary>
    public class IyzicoMobilePaymentService : IMobilePaymentService
    {
        private readonly IEntityRepository<Domain.Entities.Payment, int> _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantService _tenantService;
        private readonly ILogger<IyzicoMobilePaymentService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IyzicoOptions _options;

        public IyzicoMobilePaymentService(
            IEntityRepository<Domain.Entities.Payment, int> paymentRepository,
            IUnitOfWork unitOfWork,
            ITenantService tenantService,
            ILogger<IyzicoMobilePaymentService> logger,
            HttpClient httpClient,
            IOptions<IyzicoOptions> options)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
            _tenantService = tenantService;
            _logger = logger;
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<MobilePaymentInitResponse> InitializeMobilePaymentAsync(MobilePaymentInitRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                // Payment entity oluştur
                var payment = new Domain.Entities.Payment
                {
                    PaymentNumber = Domain.Entities.Payment.GeneratePaymentNumber(),
                    InvoiceId = request.InvoiceId,
                    WorkOrderId = request.WorkOrderId,
                    CustomerId = request.CustomerId,
                    Amount = request.Amount,
                    PaymentMethod = PaymentMethod.OnlinePayment,
                    PaymentStatus = PaymentStatus.Unpaid,
                    PaymentDate = DateTime.UtcNow,
                    PaymentGateway = PaymentGateway.Iyzico,
                    Description = request.Description,
                    ClientId = _tenantService.GetCurrentClientId() ?? 0
                };

                await _paymentRepository.AddAsync(payment, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // İyzico API çağrısı (Mobile için)
                var iyzicoRequest = new
                {
                    locale = "tr",
                    conversationId = payment.PaymentNumber,
                    price = request.Amount.ToString("F2"),
                    paidPrice = request.Amount.ToString("F2"),
                    currency = request.Currency,
                    installment = request.InstallmentCount ?? 1,
                    basketId = payment.Id.ToString(),
                    paymentChannel = "MOBILE", // Mobile channel
                    paymentGroup = "PRODUCT",
                    enabledInstallments = new[] { 1, 2, 3, 6, 9 },
                    buyer = new
                    {
                        id = request.CustomerId.ToString(),
                        name = request.CustomerName,
                        surname = request.CustomerSurname,
                        gsmNumber = request.CustomerPhone,
                        email = request.CustomerEmail,
                        identityNumber = request.CustomerIdentityNumber ?? "",
                        lastLoginDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                        registrationDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                        registrationAddress = request.CustomerAddress ?? "",
                        ip = "127.0.0.1",
                        city = "Istanbul",
                        country = "Turkey",
                        zipCode = ""
                    },
                    basketItems = new[]
                    {
                        new
                        {
                            id = payment.Id.ToString(),
                            name = request.Description ?? "Ödeme",
                            category1 = "Ödeme",
                            itemType = "PHYSICAL",
                            price = request.Amount.ToString("F2")
                        }
                    }
                };

                var requestJson = JsonSerializer.Serialize(iyzicoRequest);
                var authorization = GenerateAuthorizationHeader("POST", "/payment/iyzipos/checkoutform/initialize/auth/ecom", requestJson);

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", authorization);
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

                var apiUrl = _options.BaseUrl + "/payment/iyzipos/checkoutform/initialize/auth/ecom";
                var response = await _httpClient.PostAsync(apiUrl, new StringContent(requestJson, Encoding.UTF8, "application/json"), cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    _logger.LogError($"İyzico API error: {errorContent}");
                    return new MobilePaymentInitResponse
                    {
                        Success = false,
                        ErrorMessage = "Ödeme oturumu başlatılamadı."
                    };
                }

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var iyzicoResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);

                if (iyzicoResponse.TryGetProperty("status", out var status) && status.GetString() == "success")
                {
                    var checkoutFormContent = iyzicoResponse.GetProperty("checkoutFormContent").GetString();
                    var token = iyzicoResponse.GetProperty("token").GetString();

                    return new MobilePaymentInitResponse
                    {
                        Success = true,
                        CheckoutFormContent = checkoutFormContent,
                        Token = token,
                        ConversationId = payment.PaymentNumber
                    };
                }
                else
                {
                    var errorMessage = iyzicoResponse.TryGetProperty("errorMessage", out var errorMsg) 
                        ? errorMsg.GetString() 
                        : "Bilinmeyen hata";
                    
                    return new MobilePaymentInitResponse
                    {
                        Success = false,
                        ErrorMessage = errorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing mobile payment");
                return new MobilePaymentInitResponse
                {
                    Success = false,
                    ErrorMessage = $"Ödeme başlatılırken hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<MobilePaymentVerifyResponse> VerifyPaymentAsync(string conversationId, CancellationToken cancellationToken = default)
        {
            try
            {
                var authorization = GenerateAuthorizationHeader("POST", "/payment/iyzipos/checkoutform/auth/ecom/detail", "");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", authorization);
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

                var request = new
                {
                    locale = "tr",
                    conversationId = conversationId,
                    token = conversationId // Token conversationId ile aynı olabilir
                };

                var requestJson = JsonSerializer.Serialize(request);
                var apiUrl = _options.BaseUrl + "/payment/iyzipos/checkoutform/auth/ecom/detail";
                var response = await _httpClient.PostAsync(apiUrl, new StringContent(requestJson, Encoding.UTF8, "application/json"), cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return new MobilePaymentVerifyResponse
                    {
                        Success = false,
                        ErrorMessage = "Ödeme doğrulanamadı."
                    };
                }

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var iyzicoResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);

                if (iyzicoResponse.TryGetProperty("status", out var status) && status.GetString() == "success")
                {
                    var paymentStatus = iyzicoResponse.GetProperty("paymentStatus").GetString();
                    
                    // Payment entity'yi güncelle
                    var payment = await _paymentRepository.GetListAsync(cancellationToken, p => p.PaymentNumber == conversationId);
                    var paymentEntity = payment.FirstOrDefault();
                    
                    if (paymentEntity != null)
                    {
                        paymentEntity.PaymentStatus = paymentStatus == "SUCCESS" ? PaymentStatus.Paid : PaymentStatus.Unpaid;
                        _paymentRepository.Update(paymentEntity);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                    }

                    return new MobilePaymentVerifyResponse
                    {
                        Success = paymentStatus == "SUCCESS",
                        PaymentStatus = paymentStatus,
                        PaymentId = paymentEntity?.Id
                    };
                }

                return new MobilePaymentVerifyResponse
                {
                    Success = false,
                    ErrorMessage = "Ödeme doğrulanamadı."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying payment");
                return new MobilePaymentVerifyResponse
                {
                    Success = false,
                    ErrorMessage = $"Ödeme doğrulanırken hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<List<MobileInstallmentOption>> GetInstallmentOptionsAsync(decimal amount, string binNumber, CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new
                {
                    locale = "tr",
                    binNumber = binNumber,
                    price = amount.ToString("F2")
                };

                var requestJson = JsonSerializer.Serialize(request);
                var authorization = GenerateAuthorizationHeader("POST", "/payment/iyzipos/checkoutform/installment", requestJson);

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", authorization);
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

                var apiUrl = _options.BaseUrl + "/payment/iyzipos/checkoutform/installment";
                var response = await _httpClient.PostAsync(apiUrl, new StringContent(requestJson, Encoding.UTF8, "application/json"), cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return new List<MobileInstallmentOption>();
                }

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var iyzicoResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);

                var options = new List<MobileInstallmentOption>();
                if (iyzicoResponse.TryGetProperty("installmentDetails", out var installmentDetails))
                {
                    foreach (var detail in installmentDetails.EnumerateArray())
                    {
                        if (detail.TryGetProperty("installmentPrices", out var prices))
                        {
                            foreach (var price in prices.EnumerateArray())
                            {
                                options.Add(new MobileInstallmentOption
                                {
                                    InstallmentNumber = price.TryGetProperty("installmentNumber", out var num) ? num.GetInt32() : 1,
                                    TotalAmount = price.TryGetProperty("totalPrice", out var total) ? decimal.Parse(total.GetString()) : amount,
                                    MonthlyAmount = price.TryGetProperty("installmentPrice", out var monthly) ? decimal.Parse(monthly.GetString()) : amount
                                });
                            }
                        }
                    }
                }

                return options;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting installment options");
                return new List<MobileInstallmentOption>();
            }
        }

        private string GenerateAuthorizationHeader(string method, string path, string requestBody)
        {
            var randomString = Guid.NewGuid().ToString("N");
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            var hashString = $"{_options.ApiKey}{randomString}{timestamp}{path}{requestBody}";
            var hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(hashString)));

            return $"IYZWS {_options.ApiKey}:{hash}:{randomString}:{timestamp}";
        }
    }
}
