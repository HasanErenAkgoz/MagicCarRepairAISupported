# Backend Gereksinimleri - Fleet Status (Efficiency Overview) API

## Genel Bakış

AdminDashboardScreen'de gösterilen "Efficiency Overview" bölümü için fleet status (araç filosu durumu) verilerini sağlayan bir endpoint gereklidir. Bu endpoint, iş emirlerinin durumlarını (Repairing, Completed, Waiting) ve genel efficiency (verimlilik) yüzdesini döndürmelidir.

## Endpoint

```
GET /api/Dashboard/fleet-status
```

### Authorization
- **Required**: Yes (Bearer Token)
- **Roles**: SystemAdmin, Manager, Employee (Tüm roller erişebilir)

### Query Parameters
- Yok (tüm kullanıcılar için kendi client'larının verilerini görür)

### Response (Başarılı)

```json
{
  "success": true,
  "data": {
    "repairing": 12,
    "completed": 24,
    "waiting": 5,
    "efficiency": 84.5
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

Fleet status verileri **WorkOrders** tablosundan toplanmalıdır:

#### Repairing (Devam Eden İş Emirleri)
Aşağıdaki durumlardaki iş emirleri "Repairing" kategorisine dahil edilmelidir:
- `WorkOrderStatus.InProgress`
- `WorkOrderStatus.InRepair`
- `WorkOrderStatus.DiagnosisCompleted`
- `WorkOrderStatus.QualityControl`
- `WorkOrderStatus.Washing`

#### Completed (Tamamlanmış İş Emirleri)
Aşağıdaki durumdaki iş emirleri "Completed" kategorisine dahil edilmelidir:
- `WorkOrderStatus.Delivered`

#### Waiting (Bekleyen İş Emirleri)
Aşağıdaki durumlardaki iş emirleri "Waiting" kategorisine dahil edilmelidir:
- `WorkOrderStatus.VehicleEntered`
- `WorkOrderStatus.AppointmentScheduled`
- `WorkOrderStatus.WaitingForParts`
- `WorkOrderStatus.ReadyForDelivery`

#### Efficiency
Tamamlanan iş emirlerinin toplam iş emirlerine oranı (yüzde). `Cancelled` durumundaki iş emirleri hesaplamaya dahil edilmemelidir.

### 2. İş Emri Durumları

Backend'deki `WorkOrderStatus` enum değerleri:

**Repairing Kategorisi:**
- `InProgress`: İş emri devam ediyor
- `InRepair`: Tamir aşamasında
- `DiagnosisCompleted`: Teşhis tamamlandı
- `QualityControl`: Kalite kontrolü aşamasında
- `Washing`: Yıkama aşamasında

**Completed Kategorisi:**
- `Delivered`: Teslim edildi (tamamlandı)

**Waiting Kategorisi:**
- `VehicleEntered`: Araç giriş yaptı
- `AppointmentScheduled`: Randevu planlandı
- `WaitingForParts`: Parça bekleniyor
- `ReadyForDelivery`: Teslim için hazır

**Hariç Tutulan:**
- `Cancelled`: İptal edilmiş iş emirleri hesaplamaya dahil edilmemelidir

### 3. Efficiency Hesaplama

Efficiency (verimlilik) yüzdesi şu formülle hesaplanmalıdır:

```
Efficiency = (Completed / Total) × 100
```

Burada:
- **Completed**: Tamamlanan iş emirleri sayısı
- **Total**: Toplam iş emirleri sayısı (Repairing + Completed + Waiting)

**Örnek**:
- Repairing: 12
- Completed: 24
- Waiting: 5
- Total: 41
- Efficiency = (24 / 41) × 100 = 58.54%

**Alternatif Hesaplama** (Opsiyonel):
Eğer sadece aktif iş emirlerini dikkate almak isterseniz (Waiting hariç):
```
Efficiency = (Completed / (Repairing + Completed)) × 100
```

### 4. Authorization Kontrolü

- Tüm roller (SystemAdmin, Manager, Employee) bu endpoint'e erişebilir
- **ClientId bazlı filtreleme**: Manager ve Employee'ler sadece kendi client'larının verilerini görebilir
- SystemAdmin tüm client'ların verilerini görebilir (opsiyonel - ihtiyaca göre)

### 5. Veri Formatı

- **Repairing**: Integer (0 veya pozitif)
- **Completed**: Integer (0 veya pozitif)
- **Waiting**: Integer (0 veya pozitif)
- **Efficiency**: Decimal (0-100 arası, 1 ondalık basamak)

## Veritabanı Sorgusu Örneği

```sql
-- Fleet Status verilerini getir
SELECT 
    -- Repairing: InProgress, InRepair, DiagnosisCompleted, QualityControl, Washing
    COUNT(CASE WHEN wo.Status IN ('InProgress', 'InRepair', 'DiagnosisCompleted', 'QualityControl', 'Washing') THEN 1 END) AS Repairing,
    
    -- Completed: Delivered
    COUNT(CASE WHEN wo.Status = 'Delivered' THEN 1 END) AS Completed,
    
    -- Waiting: VehicleEntered, AppointmentScheduled, WaitingForParts, ReadyForDelivery
    COUNT(CASE WHEN wo.Status IN ('VehicleEntered', 'AppointmentScheduled', 'WaitingForParts', 'ReadyForDelivery') THEN 1 END) AS Waiting,
    
    -- Efficiency: (Completed / Total) * 100 (Cancelled hariç)
    CASE 
        WHEN COUNT(CASE WHEN wo.Status != 'Cancelled' THEN 1 END) > 0 THEN 
            ROUND((COUNT(CASE WHEN wo.Status = 'Delivered' THEN 1 END) * 100.0 / 
                   COUNT(CASE WHEN wo.Status != 'Cancelled' THEN 1 END)), 1)
        ELSE 0 
    END AS Efficiency
FROM WorkOrders wo
WHERE wo.ClientId = @ClientId  -- Manager/Employee için
    AND wo.IsDeleted = 0;  -- Silinmemiş kayıtlar
```

**Not**: Tarih filtresi kullanılmamaktadır. Tüm zamanların verileri gösterilir. İsterseniz son X günün verilerini göstermek için `CreatedDate >= DATE_SUB(CURDATE(), INTERVAL X DAY)` filtresi ekleyebilirsiniz.

## C# Implementation Örneği

### Query

```csharp
using MediatR;

namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetFleetStatus
{
    public class GetFleetStatusQuery : IRequest<IDataResult<GetFleetStatusResponse>>
    {
        // Query parametreleri yok, tüm kullanıcılar için kendi client'larının verilerini görür
    }
}
```

### Response

```csharp
namespace MagicCarRepairAISupported.Application.Features.Dashboard.Queries.GetFleetStatus
{
    public class GetFleetStatusResponse
    {
        public int Repairing { get; set; }
        public int Completed { get; set; }
        public int Waiting { get; set; }
        public decimal Efficiency { get; set; } // 0-100 arası, 1 ondalık basamak
    }
}
```

### QueryHandler Örneği

```csharp
using MagicCarRepairAISupported.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

public class GetFleetStatusQueryHandler : IRequestHandler<GetFleetStatusQuery, IDataResult<GetFleetStatusResponse>>
{
    private readonly IWorkOrderRepository _workOrderRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetFleetStatusQueryHandler(
        IWorkOrderRepository workOrderRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _workOrderRepository = workOrderRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IDataResult<GetFleetStatusResponse>> Handle(
        GetFleetStatusQuery request, 
        CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext.User.GetUserId();
        var user = await _userManager.FindByIdAsync(userId);
        var clientId = user.ClientId;

        // WorkOrders'dan durum bazlı sayıları al
        var workOrders = await _workOrderRepository.GetByClientIdAsync(clientId, cancellationToken);

        // Sadece aktif (silinmemiş) iş emirlerini filtrele
        var activeWorkOrders = workOrders.Where(wo => !wo.IsDeleted).ToList();

        // Repairing: InProgress, InRepair, DiagnosisCompleted, QualityControl, Washing
        var repairing = activeWorkOrders.Count(wo => 
            wo.Status == WorkOrderStatus.InProgress || 
            wo.Status == WorkOrderStatus.InRepair ||
            wo.Status == WorkOrderStatus.DiagnosisCompleted ||
            wo.Status == WorkOrderStatus.QualityControl ||
            wo.Status == WorkOrderStatus.Washing);

        // Completed: Delivered
        var completed = activeWorkOrders.Count(wo => 
            wo.Status == WorkOrderStatus.Delivered);

        // Waiting: VehicleEntered, AppointmentScheduled, WaitingForParts, ReadyForDelivery
        var waiting = activeWorkOrders.Count(wo => 
            wo.Status == WorkOrderStatus.VehicleEntered || 
            wo.Status == WorkOrderStatus.AppointmentScheduled ||
            wo.Status == WorkOrderStatus.WaitingForParts ||
            wo.Status == WorkOrderStatus.ReadyForDelivery);

        // Total: Cancelled hariç tüm durumlar
        var total = activeWorkOrders.Count(wo => 
            wo.Status != WorkOrderStatus.Cancelled);

        // Efficiency hesapla
        var efficiency = total > 0
            ? Math.Round((decimal)completed / total * 100, 1)
            : 0;

        return new SuccessDataResult<GetFleetStatusResponse>(new GetFleetStatusResponse
        {
            Repairing = repairing,
            Completed = completed,
            Waiting = waiting,
            Efficiency = efficiency
        });
    }
}
```

## Controller Endpoint

```csharp
/// <summary>
/// Fleet status bilgilerini getirir (Repairing, Completed, Waiting, Efficiency)
/// </summary>
[HttpGet("fleet-status")]
public async Task<IActionResult> GetFleetStatus()
{
    var query = new GetFleetStatusQuery();
    var result = await _mediator.Send(query);
    return Ok(result);
}
```

## Güvenlik ve Performans

### Güvenlik
- ✅ Authorization kontrolü (Tüm roller erişebilir)
- ✅ ClientId bazlı veri filtreleme
- ✅ SQL injection koruması (parametreli sorgular)

### Performans
- ✅ Veritabanı index'leri: `WorkOrders(ClientId, Status, IsDeleted)`
- ✅ Cache: 2-3 dakika (opsiyonel - veriler sık değişebilir)
- ✅ Sadece gerekli alanları çek (COUNT optimization)

## Frontend Entegrasyonu

Frontend'de bu endpoint'ten gelen veriler şu şekilde kullanılacak:

- **Repairing** → Donut chart'ta mavi segment ve legend'da "Repairing" değeri
- **Completed** → Donut chart'ta yeşil segment ve legend'da "Completed" değeri
- **Waiting** → Donut chart'ta gri segment ve legend'da "Waiting" değeri
- **Efficiency** → Donut chart'ın merkezinde gösterilen yüzde değeri

### Donut Chart Hesaplama

Frontend'de donut chart segmentleri şu şekilde hesaplanır:

```typescript
const total = repairing + completed + waiting;
const circumference = 2 * Math.PI * 40; // r=40

const completedPercent = (completed / total) * 100;
const repairingPercent = (repairing / total) * 100;
const waitingPercent = (waiting / total) * 100;

const completedDash = (completedPercent / 100) * circumference;
const repairingDash = (repairingPercent / 100) * circumference;
const waitingDash = (waitingPercent / 100) * circumference;
```

## Test Senaryoları

1. ✅ SystemAdmin olarak fleet status isteği
2. ✅ Manager olarak fleet status isteği (sadece kendi client'ı)
3. ✅ Employee olarak fleet status isteği (sadece kendi client'ı)
4. ✅ Tüm durumlar için 0 değeri (hiç iş emri yok)
5. ✅ Sadece bir durum için veri var (örn: sadece Completed)
6. ✅ Efficiency hesaplama doğruluğu
7. ✅ Efficiency = 0 durumu (hiç tamamlanmış iş emri yok)
8. ✅ Efficiency = 100 durumu (tüm iş emirleri tamamlanmış)
9. ✅ ClientId bazlı filtreleme doğruluğu
10. ✅ Silinmiş iş emirlerinin hariç tutulması

## Özel Durumlar

### 1. Hiç İş Emri Yok
Eğer hiç iş emri yoksa:
```json
{
  "repairing": 0,
  "completed": 0,
  "waiting": 0,
  "efficiency": 0
}
```

### 2. Sadece Bir Durum Var
Örneğin sadece Completed iş emirleri varsa:
```json
{
  "repairing": 0,
  "completed": 10,
  "waiting": 0,
  "efficiency": 100.0
}
```

### 3. Efficiency Hesaplama
- Toplam = 0 ise → Efficiency = 0
- Toplam > 0 ise → Efficiency = (Completed / Total) × 100
- Yuvarlama: 1 ondalık basamak (örn: 84.5, 95.3)

## Notlar

- **Status Enum Değerleri**: Backend'deki `WorkOrderStatus` enum değerleri dokümanda belirtilmiştir. Mevcut implementation'da bu değerler kullanılmaktadır.
- **Cancelled Durumu**: `Cancelled` durumundaki iş emirleri toplam hesaplamasına dahil edilmemelidir (Efficiency hesaplaması için).
- **Silinmiş Kayıtlar**: `IsDeleted = true` olan iş emirleri hesaplamaya dahil edilmemelidir.
- **Rounding**: `Efficiency` değeri 1 ondalık basamağa yuvarlanmalıdır (örn: 84.5, 95.3).
- **Null Handling**: Eğer hiç veri yoksa, tüm değerler 0 döndürülmelidir.
- **Tarih Filtresi**: Mevcut implementation'da tarih filtresi yoktur. Tüm zamanların verileri gösterilir.

## İlgili Endpoint'ler

- `GET /api/Dashboard/weekly-revenue` - Haftalık gelir analitikleri
- `GET /api/Dashboard/stats` - Genel dashboard istatistikleri

---

**Öncelik:** Orta  
**Etkilenen Tablolar:** WorkOrders  
**Tahmini Süre:** 1-2 saat  
**Bağımlılıklar:** WorkOrderStatus enum, WorkOrderRepository
