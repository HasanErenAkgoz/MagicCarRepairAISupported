# Backend Gereksinimleri - Haftalık Revenue Analytics API

## Genel Bakış

AdminDashboardScreen'de gösterilen "Revenue Analytics" bölümü için haftalık gelir verilerini sağlayan bir endpoint gereklidir. Bu endpoint, son 7 günün günlük gelir verilerini ve haftalık toplam geliri döndürmelidir.

## Endpoint

```
GET /api/Dashboard/weekly-revenue
```

### Authorization
- **Required**: Yes (Bearer Token)
- **Roles**: SystemAdmin, Manager (Employee'ler bu endpoint'e erişemez)

### Query Parameters
- `startDate` (optional): Haftanın başlangıç tarihi (ISO 8601 format: `YYYY-MM-DD`)
  - Varsayılan: Bugünden 7 gün önce
- `endDate` (optional): Haftanın bitiş tarihi (ISO 8601 format: `YYYY-MM-DD`)
  - Varsayılan: Bugün

### Response (Başarılı)

```json
{
  "success": true,
  "data": {
    "weeklyTotal": 12450.00,
    "previousWeekTotal": 11480.00,
    "changePercent": 8.5,
    "dailyData": [
      {
        "date": "2024-01-15",
        "dayOfWeek": "Monday",
        "dayShort": "Mon",
        "revenue": 1850.00
      },
      {
        "date": "2024-01-16",
        "dayOfWeek": "Tuesday",
        "dayShort": "Tue",
        "revenue": 2100.00
      },
      {
        "date": "2024-01-17",
        "dayOfWeek": "Wednesday",
        "dayShort": "Wed",
        "revenue": 1950.00
      },
      {
        "date": "2024-01-18",
        "dayOfWeek": "Thursday",
        "dayShort": "Thu",
        "revenue": 2200.00
      },
      {
        "date": "2024-01-19",
        "dayOfWeek": "Friday",
        "dayShort": "Fri",
        "revenue": 1800.00
      },
      {
        "date": "2024-01-20",
        "dayOfWeek": "Saturday",
        "dayShort": "Sat",
        "revenue": 1550.00
      },
      {
        "date": "2024-01-21",
        "dayOfWeek": "Sunday",
        "dayShort": "Sun",
        "revenue": 1000.00
      }
    ]
  },
  "message": null
}
```

### Response (Hata)

```json
{
  "success": false,
  "data": null,
  "message": "Unauthorized access"
}
```

## Backend İşlemleri

### 1. Veri Kaynağı

Gelir verileri muhtemelen şu kaynaklardan toplanmalıdır:
- **WorkOrders** tablosundaki tamamlanmış iş emirlerinin toplam tutarı
- **Invoices** tablosundaki ödenmiş faturaların toplam tutarı
- **Payments** tablosundaki ödemeler

**Önerilen Yaklaşım**: WorkOrders tablosundaki `Status = Completed` ve `PaymentStatus = Paid` olan kayıtların `TotalAmount` alanlarını toplamak.

### 2. Günlük Veri Hesaplama

Her gün için:
- O gün tamamlanan ve ödenen iş emirlerinin toplam tutarını hesapla
- Tarih bazlı gruplama yap (date bazlı)
- Eksik günler için `revenue: 0` döndür

### 3. Haftalık Toplam

- Seçilen haftanın tüm günlerinin toplamını hesapla
- Önceki haftanın toplamını hesapla (karşılaştırma için)
- Değişim yüzdesini hesapla: `((weeklyTotal - previousWeekTotal) / previousWeekTotal) * 100`

### 4. Authorization Kontrolü

- Sadece SystemAdmin ve Manager rolleri bu endpoint'e erişebilir
- Employee'ler erişemez (zaten frontend'de gizleniyor ama backend'de de kontrol edilmeli)
- ClientId bazlı filtreleme: Manager ve Employee'ler sadece kendi client'larının verilerini görebilir

### 5. Veri Formatı

- **Tarih Formatı**: ISO 8601 (`YYYY-MM-DD`)
- **Para Birimi**: Decimal (2 ondalık basamak)
- **Gün İsimleri**: İngilizce (Monday, Tuesday, vb.) ve kısa format (Mon, Tue, vb.)

## Veritabanı Sorgusu Örneği

```sql
-- Son 7 günün günlük gelir verileri
SELECT 
    DATE(wo.CompletedDate) AS Date,
    DAYNAME(DATE(wo.CompletedDate)) AS DayOfWeek,
    DATE_FORMAT(DATE(wo.CompletedDate), '%a') AS DayShort,
    SUM(wo.TotalAmount) AS Revenue
FROM WorkOrders wo
WHERE wo.Status = 'Completed'
    AND wo.PaymentStatus = 'Paid'
    AND wo.ClientId = @ClientId  -- Manager/Employee için
    AND DATE(wo.CompletedDate) >= DATE_SUB(CURDATE(), INTERVAL 7 DAY)
    AND DATE(wo.CompletedDate) <= CURDATE()
GROUP BY DATE(wo.CompletedDate)
ORDER BY DATE(wo.CompletedDate) ASC;
```

## C# Implementation Örneği

### Query

```csharp
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetWeeklyRevenue
{
    public class GetWeeklyRevenueQuery : IRequest<IDataResult<GetWeeklyRevenueResponse>>
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
```

### Response

```csharp
namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetWeeklyRevenue
{
    public class GetWeeklyRevenueResponse
    {
        public decimal WeeklyTotal { get; set; }
        public decimal PreviousWeekTotal { get; set; }
        public decimal ChangePercent { get; set; }
        public List<DailyRevenueData> DailyData { get; set; } = new();
    }

    public class DailyRevenueData
    {
        public DateTime Date { get; set; }
        public string DayOfWeek { get; set; } // "Monday", "Tuesday", etc.
        public string DayShort { get; set; } // "Mon", "Tue", etc.
        public decimal Revenue { get; set; }
    }
}
```

### QueryHandler Örneği

```csharp
public class GetWeeklyRevenueQueryHandler : IRequestHandler<GetWeeklyRevenueQuery, IDataResult<GetWeeklyRevenueResponse>>
{
    private readonly IWorkOrderRepository _workOrderRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public async Task<IDataResult<GetWeeklyRevenueResponse>> Handle(
        GetWeeklyRevenueQuery request, 
        CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        var user = await _userManager.FindByIdAsync(userId);
        var clientId = user.ClientId;

        // Varsayılan: Son 7 gün
        var endDate = request.EndDate ?? DateTime.UtcNow.Date;
        var startDate = request.StartDate ?? endDate.AddDays(-6);

        // Bu haftanın verileri
        var thisWeekData = await GetDailyRevenueData(startDate, endDate, clientId, cancellationToken);
        var weeklyTotal = thisWeekData.Sum(d => d.Revenue);

        // Önceki haftanın verileri (karşılaştırma için)
        var previousWeekStart = startDate.AddDays(-7);
        var previousWeekEnd = startDate.AddDays(-1);
        var previousWeekData = await GetDailyRevenueData(previousWeekStart, previousWeekEnd, clientId, cancellationToken);
        var previousWeekTotal = previousWeekData.Sum(d => d.Revenue);

        // Değişim yüzdesi
        var changePercent = previousWeekTotal > 0
            ? ((weeklyTotal - previousWeekTotal) / previousWeekTotal) * 100
            : 0;

        // Eksik günleri doldur (0 revenue ile)
        var allDays = Enumerable.Range(0, 7)
            .Select(offset => startDate.AddDays(offset))
            .ToList();

        var filledData = allDays.Select(date =>
        {
            var existing = thisWeekData.FirstOrDefault(d => d.Date.Date == date.Date);
            return existing ?? new DailyRevenueData
            {
                Date = date,
                DayOfWeek = date.DayOfWeek.ToString(),
                DayShort = date.ToString("ddd"),
                Revenue = 0
            };
        }).ToList();

        return new SuccessDataResult<GetWeeklyRevenueResponse>(new GetWeeklyRevenueResponse
        {
            WeeklyTotal = weeklyTotal,
            PreviousWeekTotal = previousWeekTotal,
            ChangePercent = Math.Round(changePercent, 1),
            DailyData = filledData
        });
    }

    private async Task<List<DailyRevenueData>> GetDailyRevenueData(
        DateTime startDate, 
        DateTime endDate, 
        int? clientId,
        CancellationToken cancellationToken)
    {
        // WorkOrders'dan günlük gelir verilerini çek
        // Implementation detayları...
    }
}
```

## Controller Endpoint

```csharp
/// <summary>
/// Haftalık gelir analitik verilerini getirir
/// </summary>
[HttpGet("weekly-revenue")]
public async Task<IActionResult> GetWeeklyRevenue([FromQuery] GetWeeklyRevenueQuery query)
{
    var result = await _mediator.Send(query);
    return Ok(result);
}
```

## Güvenlik ve Performans

### Güvenlik
- ✅ Authorization kontrolü (SystemAdmin, Manager)
- ✅ ClientId bazlı veri filtreleme
- ✅ SQL injection koruması (parametreli sorgular)

### Performans
- ✅ Veritabanı index'leri: `WorkOrders(ClientId, Status, PaymentStatus, CompletedDate)`
- ✅ Cache: 5 dakika (opsiyonel)
- ✅ Sadece gerekli alanları çek (SELECT optimization)

## Test Senaryoları

1. ✅ SystemAdmin olarak haftalık revenue isteği
2. ✅ Manager olarak haftalık revenue isteği (sadece kendi client'ı)
3. ✅ Employee olarak istek (403 Forbidden)
4. ✅ Özel tarih aralığı ile istek
5. ✅ Eksik günler için 0 revenue döndürme
6. ✅ Önceki hafta ile karşılaştırma hesaplama
7. ✅ Negatif değişim yüzdesi (-%5.2 gibi)
8. ✅ Önceki hafta verisi yoksa (ilk hafta) changePercent = 0

## Frontend Entegrasyonu

Frontend'de bu endpoint'ten gelen veriler şu şekilde kullanılacak:

- `weeklyTotal` → "Weekly Revenue" değeri
- `changePercent` → Badge'deki yüzde değeri (+8.5% veya -3.2%)
- `dailyData` → Grafikteki 7 nokta (her gün için)
- `dailyData[].dayShort` → Grafik altındaki gün etiketleri (Mon, Tue, vb.)

## Notlar

- **Para Birimi**: Response'da para birimi belirtilmez, frontend'de formatlanır ($, ₺, vb.)
- **Tarih Zaman Dilimi**: UTC kullanılmalı, frontend'de kullanıcının timezone'ına çevrilir
- **Eksik Veriler**: Eğer bir gün için veri yoksa, `revenue: 0` döndürülmeli
- **Rounding**: `changePercent` 1 ondalık basamağa yuvarlanmalı
- **Null Handling**: Eğer hiç veri yoksa, tüm günler için `revenue: 0` döndürülmeli

---

**Öncelik:** Orta  
**Etkilenen Tablolar:** WorkOrders, Invoices (opsiyonel)  
**Tahmini Süre:** 2-3 saat
