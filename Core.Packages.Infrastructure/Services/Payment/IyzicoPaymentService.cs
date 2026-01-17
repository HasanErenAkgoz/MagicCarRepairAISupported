using MagicCarRepairAISupported.Application.Common.Services;
using MagicCarRepairAISupported.Application.Common.Services.Payment;
using MagicCarRepairAISupported.Application.Common.Services.Payment.Dtos;
using MagicCarRepairAISupported.Domain.Entities;
using MagicCarRepairAISupported.Domain.Enums;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace MagicCarRepairAISupported.Infrastructure.Services.Payment
{
    /// <summary>
    /// İyzico ödeme servisi implementasyonu
    /// </summary>
    public class IyzicoPaymentService : IPaymentService
    {
        private readonly IEntityRepository<Domain.Entities.Payment, int> _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantService _tenantService;
        private readonly ILogger<IyzicoPaymentService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IyzicoOptions _options;

        public IyzicoPaymentService(
            IEntityRepository<Domain.Entities.Payment, int> paymentRepository,
            IUnitOfWork unitOfWork,
            ITenantService tenantService,
            ILogger<IyzicoPaymentService> logger,
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

        public async Task<PaymentInitResponse> InitializePaymentAsync(PaymentInitRequest request, CancellationToken cancellationToken = default)
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

                // İyzico API çağrısı
                var iyzicoRequest = new
                {
                    locale = "tr",
                    conversationId = payment.PaymentNumber,
                    price = request.Amount.ToString("F2"),
                    paidPrice = request.Amount.ToString("F2"),
                    currency = request.Currency,
                    installment = request.InstallmentCount ?? 1,
                    basketId = payment.Id.ToString(),
                    paymentChannel = "WEB",
                    paymentGroup = "PRODUCT",
                    callbackUrl = request.CallbackUrl,
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
                        city = request.CustomerCity ?? "Istanbul",
                        country = request.CustomerCountry ?? "Turkey",
                        zipCode = request.CustomerZipCode ?? ""
                    },
                    shippingAddress = new
                    {
                        contactName = $"{request.CustomerName} {request.CustomerSurname}",
                        city = request.CustomerCity ?? "Istanbul",
                        country = request.CustomerCountry ?? "Turkey",
                        address = request.CustomerAddress ?? "",
                        zipCode = request.CustomerZipCode ?? ""
                    },
                    billingAddress = new
                    {
                        contactName = $"{request.CustomerName} {request.CustomerSurname}",
                        city = request.CustomerCity ?? "Istanbul",
                        country = request.CustomerCountry ?? "Turkey",
                        address = request.CustomerAddress ?? "",
                        zipCode = request.CustomerZipCode ?? ""
                    },
                    basketItems = new[]
                    {
                        new
                        {
                            id = request.InvoiceId?.ToString() ?? request.WorkOrderId?.ToString() ?? "1",
                            name = request.Description ?? "Ödeme",
                            category1 = "Servis",
                            category2 = "Ödeme",
                            itemType = "PHYSICAL",
                            price = request.Amount.ToString("F2")
                        }
                    }
                };

                var requestBody = JsonSerializer.Serialize(iyzicoRequest);
                var authorization = GenerateAuthorizationHeader("POST", "/payment/auth", requestBody);

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", authorization);
                _httpClient.DefaultRequestHeaders.Add("x-iyzi-client-version", "iyzipay-dotnet-2.1.48");

                var response = await _httpClient.PostAsync(
                    $"{_options.BaseUrl}/payment/auth",
                    new StringContent(requestBody, Encoding.UTF8, "application/json"),
                    cancellationToken);

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var iyzicoResponse = JsonSerializer.Deserialize<IyzicoPaymentResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (iyzicoResponse?.Status == "success" && iyzicoResponse.PaymentStatus == "SUCCESS")
                {
                    // Ödeme başarılı
                    payment.PaymentStatus = PaymentStatus.Paid;
                    payment.GatewayPaymentId = iyzicoResponse.PaymentId;
                    payment.GatewayConversationId = iyzicoResponse.ConversationId;
                    payment.CardLastFourDigits = iyzicoResponse.CardLastFourDigits;
                    payment.BankName = iyzicoResponse.BankName;
                    payment.InstallmentCount = request.InstallmentCount;
                    payment.PaymentDate = DateTime.UtcNow;

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return new PaymentInitResponse
                    {
                        Success = true,
                        PaymentId = payment.Id,
                        GatewayPaymentId = payment.GatewayPaymentId,
                        GatewayConversationId = payment.GatewayConversationId
                    };
                }
                else if (iyzicoResponse?.Status == "success" && iyzicoResponse.PaymentStatus == "INITIALIZE_THREEDS")
                {
                    // 3D Secure gerekiyor
                    payment.GatewayPaymentId = iyzicoResponse.PaymentId;
                    payment.GatewayConversationId = iyzicoResponse.ConversationId;

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return new PaymentInitResponse
                    {
                        Success = true,
                        PaymentId = payment.Id,
                        GatewayPaymentId = payment.GatewayPaymentId,
                        GatewayConversationId = payment.GatewayConversationId,
                        HtmlContent = iyzicoResponse.HtmlContent,
                        RedirectUrl = iyzicoResponse.RedirectUrl
                    };
                }
                else
                {
                    // Ödeme başarısız
                    payment.PaymentStatus = PaymentStatus.Unpaid;
                    payment.GatewayResponseMessage = iyzicoResponse?.ErrorMessage ?? "Ödeme başarısız";
                    payment.GatewayResponseCode = iyzicoResponse?.ErrorCode;

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return new PaymentInitResponse
                    {
                        Success = false,
                        PaymentId = payment.Id,
                        ErrorMessage = iyzicoResponse?.ErrorMessage ?? "Ödeme başarısız",
                        ErrorCode = iyzicoResponse?.ErrorCode
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İyzico ödeme başlatma hatası");
                return new PaymentInitResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ErrorCode = "SYSTEM_ERROR"
                };
            }
        }

        public async Task<PaymentCallbackResponse> HandlePaymentCallbackAsync(PaymentCallbackRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                // Payment'ı bul
                var payment = await _paymentRepository.Query()
                    .FirstOrDefaultAsync(p => p.GatewayPaymentId == request.GatewayPaymentId, cancellationToken);

                if (payment == null)
                {
                    return new PaymentCallbackResponse
                    {
                        Success = false,
                        ErrorMessage = "Ödeme bulunamadı",
                        ErrorCode = "PAYMENT_NOT_FOUND"
                    };
                }

                // İyzico ödeme durumu kontrolü
                var statusResponse = await CheckPaymentStatusAsync(request.GatewayPaymentId, cancellationToken);

                if (statusResponse.PaymentStatus == PaymentStatus.Paid)
                {
                    payment.PaymentStatus = PaymentStatus.Paid;
                    payment.PaymentDate = DateTime.UtcNow;

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return new PaymentCallbackResponse
                    {
                        Success = true,
                        PaymentId = payment.Id,
                        PaymentStatus = PaymentStatus.Paid,
                        PaidAmount = payment.Amount,
                        CardLastFourDigits = payment.CardLastFourDigits,
                        BankName = payment.BankName,
                        InstallmentCount = payment.InstallmentCount
                    };
                }
                else
                {
                    payment.PaymentStatus = PaymentStatus.Unpaid;
                    payment.GatewayResponseMessage = "Ödeme başarısız";

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return new PaymentCallbackResponse
                    {
                        Success = false,
                        PaymentId = payment.Id,
                        PaymentStatus = PaymentStatus.Unpaid,
                        ErrorMessage = "Ödeme başarısız"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İyzico callback işleme hatası");
                return new PaymentCallbackResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ErrorCode = "SYSTEM_ERROR"
                };
            }
        }

        public async Task<PaymentStatusResponse> CheckPaymentStatusAsync(string paymentId, CancellationToken cancellationToken = default)
        {
            try
            {
                var authorization = GenerateAuthorizationHeader("POST", "/payment/detail", $"{{\"paymentId\":\"{paymentId}\"}}");

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", authorization);
                _httpClient.DefaultRequestHeaders.Add("x-iyzi-client-version", "iyzipay-dotnet-2.1.48");

                var response = await _httpClient.PostAsync(
                    $"{_options.BaseUrl}/payment/detail",
                    new StringContent($"{{\"paymentId\":\"{paymentId}\"}}", Encoding.UTF8, "application/json"),
                    cancellationToken);

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var iyzicoResponse = JsonSerializer.Deserialize<IyzicoPaymentDetailResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (iyzicoResponse?.Status == "success" && iyzicoResponse.PaymentStatus == "SUCCESS")
                {
                    return new PaymentStatusResponse
                    {
                        PaymentStatus = PaymentStatus.Paid,
                        PaidAmount = decimal.Parse(iyzicoResponse.PaidPrice ?? "0"),
                        PaymentDate = DateTime.UtcNow,
                        GatewayPaymentId = paymentId
                    };
                }

                return new PaymentStatusResponse
                {
                    PaymentStatus = PaymentStatus.Unpaid,
                    GatewayPaymentId = paymentId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İyzico ödeme durumu kontrol hatası");
                return new PaymentStatusResponse
                {
                    PaymentStatus = PaymentStatus.Unpaid,
                    GatewayPaymentId = paymentId
                };
            }
        }

        public async Task<RefundResponse> RefundPaymentAsync(RefundRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var payment = await _paymentRepository.Query()
                    .FirstOrDefaultAsync(p => p.Id == request.PaymentId || p.GatewayPaymentId == request.GatewayPaymentId, cancellationToken);

                if (payment == null)
                {
                    return new RefundResponse
                    {
                        Success = false,
                        ErrorMessage = "Ödeme bulunamadı",
                        ErrorCode = "PAYMENT_NOT_FOUND"
                    };
                }

                var refundAmount = request.RefundAmount ?? payment.Amount;
                var refundRequest = new
                {
                    locale = "tr",
                    conversationId = payment.PaymentNumber,
                    paymentTransactionId = request.GatewayPaymentId,
                    price = refundAmount.ToString("F2"),
                    currency = "TRY",
                    ip = "127.0.0.1"
                };

                var requestBody = JsonSerializer.Serialize(refundRequest);
                var authorization = GenerateAuthorizationHeader("POST", "/payment/refund", requestBody);

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", authorization);
                _httpClient.DefaultRequestHeaders.Add("x-iyzi-client-version", "iyzipay-dotnet-2.1.48");

                var response = await _httpClient.PostAsync(
                    $"{_options.BaseUrl}/payment/refund",
                    new StringContent(requestBody, Encoding.UTF8, "application/json"),
                    cancellationToken);

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var iyzicoResponse = JsonSerializer.Deserialize<IyzicoRefundResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (iyzicoResponse?.Status == "success")
                {
                    payment.IsRefunded = true;
                    payment.RefundDate = DateTime.UtcNow;
                    payment.RefundAmount = refundAmount;
                    payment.RefundDescription = request.Description;
                    payment.GatewayRefundId = iyzicoResponse.PaymentId;
                    payment.PaymentStatus = PaymentStatus.Refunded;

                    await _unitOfWork.SaveChangesAsync(cancellationToken);

                    return new RefundResponse
                    {
                        Success = true,
                        RefundAmount = refundAmount,
                        GatewayRefundId = iyzicoResponse.PaymentId
                    };
                }

                return new RefundResponse
                {
                    Success = false,
                    ErrorMessage = iyzicoResponse?.ErrorMessage ?? "İade başarısız",
                    ErrorCode = iyzicoResponse?.ErrorCode
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İyzico iade hatası");
                return new RefundResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ErrorCode = "SYSTEM_ERROR"
                };
            }
        }

        public async Task<List<InstallmentOption>> GetInstallmentOptionsAsync(decimal amount, CancellationToken cancellationToken = default)
        {
            try
            {
                var installmentRequest = new
                {
                    locale = "tr",
                    conversationId = Guid.NewGuid().ToString(),
                    binNumber = "554960", // Örnek BIN numarası
                    price = amount.ToString("F2")
                };

                var requestBody = JsonSerializer.Serialize(installmentRequest);
                var authorization = GenerateAuthorizationHeader("POST", "/payment/installment", requestBody);

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", authorization);
                _httpClient.DefaultRequestHeaders.Add("x-iyzi-client-version", "iyzipay-dotnet-2.1.48");

                var response = await _httpClient.PostAsync(
                    $"{_options.BaseUrl}/payment/installment",
                    new StringContent(requestBody, Encoding.UTF8, "application/json"),
                    cancellationToken);

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
                var iyzicoResponse = JsonSerializer.Deserialize<IyzicoInstallmentResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var options = new List<InstallmentOption>();

                if (iyzicoResponse?.Status == "success" && iyzicoResponse.InstallmentDetails != null)
                {
                    foreach (var detail in iyzicoResponse.InstallmentDetails)
                    {
                        if (detail.InstallmentPrices != null)
                        {
                            foreach (var price in detail.InstallmentPrices)
                            {
                                options.Add(new InstallmentOption
                                {
                                    InstallmentCount = price.InstallmentNumber,
                                    MonthlyAmount = decimal.Parse(price.Price ?? "0") / price.InstallmentNumber,
                                    TotalAmount = decimal.Parse(price.Price ?? "0"),
                                    InterestRate = price.InstallmentNumber > 1 ? (decimal.Parse(price.Price ?? "0") - amount) / amount * 100 : 0,
                                    HasInterest = price.InstallmentNumber > 1
                                });
                            }
                        }
                    }
                }

                return options;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İyzico taksit seçenekleri hatası");
                return new List<InstallmentOption>();
            }
        }

        private string GenerateAuthorizationHeader(string method, string path, string requestBody)
        {
            var randomString = Guid.NewGuid().ToString().Replace("-", "");
            var hash = CreateHash(_options.ApiKey, _options.SecretKey, randomString, requestBody);
            var authorization = $"IYZWS {_options.ApiKey}:{hash}";

            return authorization;
        }

        private string CreateHash(string apiKey, string secretKey, string randomString, string requestBody)
        {
            var hashString = $"{apiKey}{randomString}{secretKey}{requestBody}";
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(hashString));
            return Convert.ToBase64String(hashBytes);
        }
    }

    /// <summary>
    /// İyzico seçenekleri
    /// </summary>
    public class IyzicoOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = "https://api.iyzipay.com";
    }

    // İyzico response modelleri
    internal class IyzicoPaymentResponse
    {
        public string? Status { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaymentId { get; set; }
        public string? ConversationId { get; set; }
        public string? HtmlContent { get; set; }
        public string? RedirectUrl { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ErrorCode { get; set; }
        public string? CardLastFourDigits { get; set; }
        public string? BankName { get; set; }
    }

    internal class IyzicoPaymentDetailResponse
    {
        public string? Status { get; set; }
        public string? PaymentStatus { get; set; }
        public string? PaidPrice { get; set; }
    }

    internal class IyzicoRefundResponse
    {
        public string? Status { get; set; }
        public string? PaymentId { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ErrorCode { get; set; }
    }

    internal class IyzicoInstallmentResponse
    {
        public string? Status { get; set; }
        public List<IyzicoInstallmentDetail>? InstallmentDetails { get; set; }
    }

    internal class IyzicoInstallmentDetail
    {
        public List<IyzicoInstallmentPrice>? InstallmentPrices { get; set; }
    }

    internal class IyzicoInstallmentPrice
    {
        public int InstallmentNumber { get; set; }
        public string? Price { get; set; }
    }
}

