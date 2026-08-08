# Mobile API — Eksik / Hatalı Endpoint Raporu

Mobil uygulama ile backend arasındaki tüm uyumsuzlukları listeler.
Her madde için **öncelik**, **etki** ve **önerilen çözüm** belirtilmiştir.

---

## İçindekiler

1. [Kritik — PhotoType Enum Uyumsuzluğu](#1-kritik--phototype-enum-uyumsuzluğu)
2. [Kritik — Customer List Eksik Alanlar](#2-kritik--customer-list-eksik-alanlar)
3. [Orta — Customer Detail Eksik Alanlar](#3-orta--customer-detail-eksik-alanlar)
4. [Orta — Vehicle FuelType Alanı Yok](#4-orta--vehicle-fueltype-alanı-yok)
5. [Düşük — Response Wrapper Tutarsızlıkları](#5-düşük--response-wrapper-tutarsızlıkları)

---

## 1. Kritik — PhotoType Enum Uyumsuzluğu

**Endpoint:** `POST /api/WorkOrders/{id}/photos`

### Sorun

Mobil uygulama ile backend'in `photoType` enum değerleri farklı:

| Değer | Mobil (gönderilen) | Backend (beklenen) |
|-------|-------------------|-------------------|
| 0     | —                 | Before            |
| 1     | **Entry**         | **Process** ← çakışma |
| 2     | **Process**       | **After** ← çakışma |
| 3     | **Exit**          | **Other** ← çakışma |
| 4     | Damage            | *(yok)*           |
| 5     | Other             | *(yok)*           |

Mobil `1` gönderdiğinde backend bunu "Process" olarak kaydediyor, oysa kullanıcı "Giriş (Entry)" seçmiştir.

### Önerilen Çözüm

Backend enum'u mobil ile hizalayın:

```csharp
public enum WorkOrderPhotoType
{
    Entry   = 1,   // Giriş fotoğrafı
    Process = 2,   // Süreç fotoğrafı
    Exit    = 3,   // Çıkış fotoğrafı
    Damage  = 4,   // Hasar fotoğrafı
    Other   = 5    // Diğer
}
```

`AddWorkOrderPhotoCommand` içinde `PhotoType` property'sinin tipi bu enum olacak şekilde güncellenmeli.
Veritabanında mevcut kayıtlar varsa migration ile değer dönüşümü yapılmalı.

---

## 2. Kritik — Customer List Eksik Alanlar

**Endpoint:** `GET /api/Customers`

### Sorun

`GetAllCustomersResponse` DTO'sunda aşağıdaki alanlar eksik:

| Alan | Tür | Açıklama |
|------|-----|----------|
| `vehicleCount` | `int` | Müşteriye ait toplam araç sayısı |
| `activeWorkOrders` | `int` | Açık (pending/inProgress) iş emri sayısı |
| `totalWorkOrders` | `int` | Toplam iş emri sayısı |

Şu an mobil uygulamada tüm müşteriler için bu değerler `0` gösteriliyor.

### Önerilen Çözüm

`GetAllCustomersQueryHandler`'da ilgili sayımları JOIN veya ayrı sorgu ile hesaplayın:

```csharp
public class GetAllCustomersResponse
{
    // ... mevcut alanlar ...

    // Eklenecek:
    public int VehicleCount { get; set; }
    public int ActiveWorkOrders { get; set; }
    public int TotalWorkOrders { get; set; }
}
```

Handler örneği:

```csharp
var customers = await _customerRepository.Query()
    .Where(c => c.ClientId == clientId)
    .Select(c => new GetAllCustomersResponse
    {
        // ... mevcut mapping ...
        VehicleCount       = c.Vehicles.Count,
        ActiveWorkOrders   = c.WorkOrders.Count(wo =>
            wo.Status == WorkOrderStatus.Pending ||
            wo.Status == WorkOrderStatus.InProgress),
        TotalWorkOrders    = c.WorkOrders.Count,
    })
    .ToListAsync(cancellationToken);
```

> **Not:** Performans için `Include()` yerine `Select()` projection kullanın.

---

## 3. Orta — Customer Detail Eksik Alanlar

**Endpoint:** `GET /api/Customers/{id}`

### Sorun

`GetCustomerByIdResponse` DTO'sunda aşağıdaki alanlar eksik:

| Alan | Tür | Açıklama |
|------|-----|----------|
| `activeWorkOrders` | `int` | Açık iş emri sayısı |
| `totalWorkOrders` | `int` | Toplam iş emri sayısı |

Ayrıca araçlar (`vehicles`) bu endpoint'te dönmüyor. Mobil uygulama araçları almak için ayrıca `GET /api/Vehicles/customer/{id}` çağrısı yapıyor — bu ek bir network isteği demek.

### Önerilen Çözüm A — Mevcut endpoint'e ekle

```csharp
public class GetCustomerByIdResponse
{
    // ... mevcut alanlar ...

    // Eklenecek:
    public int ActiveWorkOrders { get; set; }
    public int TotalWorkOrders { get; set; }
    public List<CustomerVehicleSummary> Vehicles { get; set; } = new();
}

public class CustomerVehicleSummary
{
    public int Id { get; set; }
    public string LicensePlate { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string? Color { get; set; }
    public string? FuelType { get; set; }   // bkz. Madde 4
}
```

### Önerilen Çözüm B — Mobil'e özel endpoint (tercih edilen)

Tek call ile ihtiyaç duyulan her şeyi döndüren bir endpoint:

```
GET /api/Customers/{id}/mobile-detail
```

Response:
```json
{
  "id": 1,
  "firstName": "Ahmet",
  "lastName": "Yılmaz",
  "email": "ahmet@example.com",
  "phoneNumber": "+90 555 123 4567",
  "createdDate": "2024-01-15T00:00:00Z",
  "vehicleCount": 2,
  "activeWorkOrders": 1,
  "totalWorkOrders": 5,
  "vehicles": [
    {
      "id": 10,
      "licensePlate": "34 ABC 123",
      "brand": "Toyota",
      "model": "Corolla",
      "year": 2020,
      "color": "Beyaz",
      "fuelType": "Benzin"
    }
  ]
}
```

---

## 4. Orta — Vehicle FuelType Alanı Yok

**Endpoint:** `GET /api/Vehicles/customer/{customerId}`

### Sorun

`Vehicle` entity'sinde `FuelType` alanı yok. Work Order oluştururken `vehicleFuelType` string olarak kaydediliyor (iş emrine gömülü) ama araç kaydına yazılmıyor.

`GetWorkOrderDetailResponse`'daki `vehicle.fuelType` alanı handler içinde boş string (`""`) olarak hardcode edilmiş:

```csharp
// WorkOrders detail handler içinde:
FuelType = "",  // Vehicle entity'sinde FuelType property'si yok
```

### Önerilen Çözüm

`Vehicle` entity'sine `FuelType` alanını ekleyin:

```csharp
public class Vehicle : Entity<int>
{
    // ... mevcut alanlar ...
    public string? FuelType { get; set; }  // "Benzin", "Dizel", "Elektrik", "Hybrid", "LPG"
}
```

Migration:
```
Add-Migration AddFuelTypeToVehicle
Update-Database
```

`GetVehiclesByCustomerResponse` ve `GetVehicleByIdResponse` DTO'larına `fuelType` alanını ekleyin:

```csharp
public class GetVehiclesByCustomerResponse
{
    // ... mevcut alanlar ...
    public string? FuelType { get; set; }
}
```

`CreateMobileWorkOrderCommand` işlenirken `vehicleFuelType`, araç kaydına da yazılmalı.

---

## 5. Düşük — Response Wrapper Tutarsızlıkları

### Sorun

Farklı endpoint'ler farklı response formatları dönüyor:

| Endpoint | Mevcut Format | Mobil Beklentisi |
|----------|--------------|-----------------|
| `GET /api/Customers` | `[ ... ]` (plain array) | `{ success, data[] }` |
| `GET /api/Customers/{id}` | `{ id, firstName, ... }` (plain object) | `{ success, data }` |
| `GET /api/Vehicles/customer/{id}` | `[ ... ]` (plain array) | `{ success, data[] }` |
| `GET /api/WorkOrders` | `{ success, data[], totalCount, ... }` | `{ success, data[] }` |
| `GET /api/WorkOrders/{id}` | `{ success, data }` | `{ success, data }` ✓ |
| `POST /api/WorkOrders/mobile` | `{ success, message, data }` | `{ success, data }` ✓ |
| `POST /api/WorkOrders/{id}/photos` | `{ photoId, workOrderId, filePath }` (plain object) | `{ success, data }` |

> **Not:** Mobil uygulama şu an customer endpoint'lerini adapter ile sarıyor (service layer'da manual mapping yapılıyor). Tutarlı bir wrapper kullanılırsa bu geçici çözüm kaldırılabilir.

### Önerilen Çözüm

Tüm API yanıtlarında standart bir wrapper kullanın:

```csharp
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public string? ErrorCode { get; set; }
}
```

Global bir `ActionFilter` veya response middleware ile tüm controller'lara uygulanabilir.

---

## Özet — Öncelik Sırası

| # | Endpoint | Sorun | Öncelik |
|---|----------|-------|---------|
| 1 | `POST /api/WorkOrders/{id}/photos` | PhotoType enum yanlış eşleşiyor | 🔴 Kritik |
| 2 | `GET /api/Customers` | `vehicleCount`, `activeWorkOrders`, `totalWorkOrders` eksik | 🔴 Kritik |
| 3 | `GET /api/Customers/{id}` | `activeWorkOrders`, `totalWorkOrders` eksik; vehicles ayrı call | 🟡 Orta |
| 4 | `GET /api/Vehicles/customer/{id}` | `fuelType` alanı yok | 🟡 Orta |
| 5 | Tüm customer/vehicle endpoint'leri | Response wrapper tutarsız | 🟢 Düşük |
