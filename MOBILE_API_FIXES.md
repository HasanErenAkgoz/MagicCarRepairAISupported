# Mobile API — Backend Geliştirici Düzeltme Rehberi

Bu döküman, mobil uygulama ile backend arasındaki uyumsuzlukları ve yapılması gereken
backend değişikliklerini somut kod örnekleriyle açıklar.

---

## Özet

| # | Dosya | Değişiklik | Öncelik |
|---|-------|-----------|---------|
| 1 | `GetMobileWorkOrderDetailQueryHandler.cs` | `vehicle.fuelType` hardcode `""` → entity'den oku | 🔴 Kritik |
| 2 | `GetCustomerByIdResponse.cs` + handler | `vehicles`, `activeWorkOrders`, `totalWorkOrders` ekle | 🔴 Kritik |
| 3 | `GetVehiclesByCustomerResponse.cs` | `fuelType` alanının mapping'de dolu geldiğini doğrula | 🟡 Orta |
| 4 | `CreateMobileWorkOrderCommandHandler.cs` | `vehicleFuelType` araç entity'sine de kaydedilsin | 🟡 Orta |
| 5 | Tüm customer/vehicle endpoint'leri | Response wrapper tutarsızlığı | 🟢 Düşük |

---

## 1. 🔴 Work Order Detail — `vehicle.fuelType` Boş Dönüyor

**Dosya:**
```
Core.Packages.Application/Features/WorkOrders/Queries/GetMobileDetail/
    GetMobileWorkOrderDetailQueryHandler.cs
```

### Sorun

Handler içinde araç bilgileri doldurulurken `FuelType` hardcode boş string yazılıyor:

```csharp
Vehicle = new WorkOrderVehicleDto
{
    // ...
    FuelType = "",   // ← SORUN: Vehicle entity'sinde FuelType var, ama burada okunmuyor
    // ...
}
```

### Düzeltme

`Vehicle` entity'sinde `FuelType` property'si mevcut. Sadece okunması yeterli:

```csharp
Vehicle = new WorkOrderVehicleDto
{
    Id       = workOrder.Vehicle?.Id ?? 0,
    Brand    = workOrder.Vehicle?.Brand ?? "",
    Model    = workOrder.Vehicle?.Model ?? "",
    Year     = workOrder.Vehicle?.Year ?? 0,
    Plate    = workOrder.Vehicle?.LicensePlate ?? "",
    Vin      = workOrder.Vehicle?.Vin,
    FuelType = workOrder.Vehicle?.FuelType,   // ← DÜZELTİLDİ
    Trim     = workOrder.Vehicle?.Trim,
}
```

Work order sorgusu yapılırken `Vehicle`'ın Include edildiğinden emin olun:

```csharp
var workOrder = await _workOrderRepository.Query()
    .Include(wo => wo.Vehicle)   // ← eksikse ekle
    .Include(wo => wo.Customer)
    .FirstOrDefaultAsync(wo => wo.Id == request.Id, cancellationToken);
```

---

## 2. 🔴 Customer Detail — `vehicles`, `activeWorkOrders`, `totalWorkOrders` Eksik

**Dosya:**
```
Core.Packages.Application/Features/Customers/Queries/GetById/
    GetCustomerByIdResponse.cs
    GetCustomerByIdQueryHandler.cs
```

### Sorun

`GET /api/Customers/{id}` endpoint'i araç listesini ve iş emri istatistiklerini
dönmüyor. Mobil uygulama müşteri detay ekranında şunlara ihtiyaç duyuyor:
- Araç listesi (marka, model, yıl, plaka, yakıt tipi)
- Aktif iş emri sayısı
- Toplam iş emri sayısı

### Düzeltme — `GetCustomerByIdResponse.cs`

```csharp
public class GetCustomerByIdResponse
{
    // ... mevcut alanlar değişmeden kalır ...

    // Eksikse ekle:
    public int ActiveWorkOrders { get; set; }
    public int TotalWorkOrders { get; set; }
    public List<CustomerVehicleSummaryDto> Vehicles { get; set; } = new();
}

public class CustomerVehicleSummaryDto
{
    public int    Id           { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string Brand        { get; set; } = string.Empty;
    public string Model        { get; set; } = string.Empty;
    public int    Year         { get; set; }
    public string? Color       { get; set; }
    public string? FuelType    { get; set; }
}
```

### Düzeltme — `GetCustomerByIdQueryHandler.cs`

```csharp
// Customer fetch — Vehicles ve WorkOrders Include edilmeli:
var customer = await _customerRepository.Query()
    .Include(c => c.Vehicles)
    .Include(c => c.WorkOrders)
    .FirstOrDefaultAsync(c => c.Id == request.Id && c.ClientId == clientId, ct);

// ... validasyon ...

var response = _mapper.Map<GetCustomerByIdResponse>(customer);

// Araç listesi:
response.Vehicles = customer.Vehicles.Select(v => new CustomerVehicleSummaryDto
{
    Id           = v.Id,
    LicensePlate = v.LicensePlate,
    Brand        = v.Brand,
    Model        = v.Model,
    Year         = v.Year,
    Color        = v.Color,
    FuelType     = v.FuelType,
}).ToList();

// İş emri sayıları:
response.TotalWorkOrders  = customer.WorkOrders.Count;
response.ActiveWorkOrders = customer.WorkOrders.Count(wo =>
    wo.Status == WorkOrderStatus.Pending ||
    wo.Status == WorkOrderStatus.InProgress);
```

---

## 3. 🟡 Vehicle List — `fuelType` Mapping Doğrulaması

**Dosya:**
```
Core.Packages.Application/Features/Vehicles/Queries/GetByCustomer/
    GetVehiclesByCustomerResponse.cs
    GetVehiclesByCustomerQueryHandler.cs
```

`Vehicle` entity'sinde `FuelType` property'si mevcut. Response DTO'ya ve
mapping'e eklenip eklenmediğini doğrulayın:

**`GetVehiclesByCustomerResponse.cs`:**
```csharp
public class GetVehiclesByCustomerResponse
{
    public int     Id           { get; set; }
    public string  LicensePlate { get; set; } = string.Empty;
    public string  Brand        { get; set; } = string.Empty;
    public string  Model        { get; set; } = string.Empty;
    public int     Year         { get; set; }
    public string? Color        { get; set; }
    public string? FuelType     { get; set; }   // ← eksikse ekle
    public long    Kilometers   { get; set; }
    public VehicleStatus Status { get; set; }
    public string  StatusName   { get; set; } = string.Empty;
}
```

AutoMapper veya manuel mapping'de:
```csharp
FuelType = vehicle.FuelType,   // ← eksikse ekle
```

---

## 4. 🟡 CreateMobileWorkOrder — `vehicleFuelType` Araç Kaydına Yazılsın

**Dosya:**
```
Core.Packages.Application/Features/WorkOrders/Commands/CreateMobile/
    CreateMobileWorkOrderCommandHandler.cs
```

### Sorun

`POST /api/WorkOrders/mobile` isteğinde `vehicleFuelType` geliyor. Bu değer iş
emrine yazılıyor ancak araç entity'sine (`Vehicle.FuelType`) kaydedilmiyor.
Sonraki work order'larda araç için fuelType tekrar boş görünüyor.

### Düzeltme

```csharp
// Araç yeni oluşturuluyorsa:
var vehicle = new Vehicle
{
    LicensePlate = command.VehiclePlate,
    Brand        = command.VehicleBrand,
    Model        = command.VehicleModel,
    Year         = command.VehicleYear,
    FuelType     = command.VehicleFuelType,   // ← ekle
    Vin          = command.VehicleVin,
    CustomerId   = customer.Id,
    ClientId     = clientId,
};

// Araç zaten varsa ve FuelType henüz boşsa güncelle:
if (existingVehicle.FuelType is null or "" &&
    !string.IsNullOrWhiteSpace(command.VehicleFuelType))
{
    existingVehicle.FuelType = command.VehicleFuelType;
}
```

---

## 5. 🟢 Response Wrapper Tutarsızlığı

Bazı endpoint'ler `{ success, data }` wrapper kullanırken bazıları direkt
obje/array dönüyor. Mobil uygulama şu an her ikisini de handle ediyor, ancak
uzun vadede tutarlılık için standardize edilmesi önerilir.

### Mevcut Durum

| Endpoint | Format |
|----------|--------|
| `GET /api/Customers` | `[...]` — plain array |
| `GET /api/Customers/{id}` | `{...}` — plain object |
| `GET /api/Vehicles/customer/{id}` | `[...]` — plain array |
| `GET /api/WorkOrders` | `{ success, data[], totalCount, ... }` ✓ |
| `GET /api/WorkOrders/{id}` | `{ success, data }` ✓ |
| `POST /api/WorkOrders/mobile` | `{ success, message, data }` ✓ |
| `POST /api/WorkOrders/{id}/photos` | `{ photoId, workOrderId, filePath }` — plain object |

### Önerilen Standart

```csharp
// Başarılı:
return Ok(new { success = true, data = result });

// Hatalı (global middleware'de):
return BadRequest(new { success = false, errorCode = "...", message = "..." });
```

> **⚠️ Dikkat:** Bu değişiklik yapılırsa mobil uygulama service katmanının da
> güncellenmesi gerekir. Önce koordine edin, sonra uygulayın.

---

## Test Senaryoları

Backend değişiklikleri yapıldıktan sonra mobil tarafında şunları doğrulayın:

```
[ ] GET /api/Customers
    → Müşteriler listeleniyor
    → vehicleCount, activeWorkOrders, totalWorkOrders dolu geliyor

[ ] GET /api/Customers/{id}
    → Müşteri detayı açılıyor
    → vehicles dizisi dolu geliyor (marka/model/plaka/yakıt)
    → activeWorkOrders, totalWorkOrders dolu geliyor

[ ] GET /api/Vehicles/customer/{id}
    → Araç listesi geliyor
    → fuelType alanı dolu (araç kaydında varsa)

[ ] GET /api/WorkOrders/{id}
    → vehicle.fuelType dolu geliyor (boş string değil)

[ ] POST /api/WorkOrders/mobile (yeni araçla)
    → İş emri oluşturuluyor
    → Araç kaydına fuelType yazılıyor
    → Sonraki GET /api/Customers/{id} çağrısında araçta fuelType görünüyor

[ ] POST /api/WorkOrders/{id}/photos
    → photoType 1=Entry, 2=Process, 3=Exit, 4=Damage, 5=Other olarak kaydediliyor
```
