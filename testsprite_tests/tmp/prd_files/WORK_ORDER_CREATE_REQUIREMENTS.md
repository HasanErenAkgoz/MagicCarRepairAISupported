# Yeni İş Emri Oluşturma — Backend Gereksinimleri

## Genel Bakış

Mobile uygulama "Yeni İş Emri" ekranından müşteri bilgisi, araç bilgisi ve hizmet detaylarını
tek seferde gönderir. Backend bu verileri alarak gerekirse yeni müşteri / araç kaydı oluşturmalı,
ardından iş emrini kaydetmelidir.

---

## Eksik Endpoint

### `POST /api/WorkOrders`

**Açıklama:** Yeni iş emri oluşturur. Müşteri ve araç aynı anda inline olarak gönderilir.
Backend, plakayla eşleşen araç varsa mevcut kaydı kullanabilir; yoksa yeni kayıt açar.
Aynı durum müşteri için de geçerlidir (telefon numarasıyla eşleştirme önerilir).

**Authorization:** `Bearer {token}` — zorunlu

---

## Request Body

```json
{
  "customerName": "Ahmet Yılmaz",
  "customerPhone": "05321234567",
  "customerEmail": "ahmet@example.com",

  "vehiclePlate": "34 ABC 123",
  "vehicleBrand": "Toyota",
  "vehicleModel": "Corolla",
  "vehicleYear": 2021,
  "vehicleFuelType": "Benzin",
  "vehicleVin": "JT2BF22K1W0123456",

  "serviceTitle": "Yağ değişimi + filtre",
  "serviceDescription": "Motor yağı ve yağ filtresi değişimi yapılacak.",
  "technicianNotes": null,
  "technicianId": null,
  "status": "pending",

  "parts": [
    { "name": "Motor Yağı 5W-30", "quantity": 4, "unitPrice": 85.00 },
    { "name": "Yağ Filtresi",     "quantity": 1, "unitPrice": 45.00 }
  ],
  "labor": [
    { "description": "Yağ değişimi işçiliği", "hours": 0.5, "hourlyRate": 200.00 }
  ],
  "taxRate": 18
}
```

### Alan Açıklamaları

| Alan | Tip | Zorunlu | Açıklama |
|------|-----|---------|----------|
| `customerName` | string | ✅ | Müşterinin tam adı |
| `customerPhone` | string | ✅ | Türkiye formatı: `05XXXXXXXXX` |
| `customerEmail` | string | ❌ | Opsiyonel e-posta |
| `vehiclePlate` | string | ✅ | Araç plakası (büyük harf) |
| `vehicleBrand` | string | ✅ | Araç markası |
| `vehicleModel` | string | ✅ | Araç modeli |
| `vehicleYear` | int | ✅ | Üretim yılı (1900–2025) |
| `vehicleFuelType` | string | ❌ | Yakıt tipi (Benzin, Dizel, Elektrik…) |
| `vehicleVin` | string | ❌ | Şasi numarası |
| `serviceTitle` | string | ✅ | Hizmet başlığı |
| `serviceDescription` | string | ❌ | Hizmet açıklaması |
| `technicianNotes` | string | ❌ | Teknisyen iç notu |
| `technicianId` | int | ❌ | Teknisyen ID (atanmışsa) |
| `status` | string | ✅ | Başlangıçta `"pending"` gönderilir |
| `parts` | array | ✅ | Boş dizi gönderilebilir |
| `labor` | array | ✅ | Boş dizi gönderilebilir |
| `taxRate` | decimal | ✅ | KDV oranı (örn. `18`) |

---

## Response — Başarılı `200 OK`

```json
{
  "success": true,
  "message": "İş emri oluşturuldu.",
  "data": {
    "id": "e9a1b2c3-...",
    "orderNo": "WO-2024-0042",
    "status": "pending",
    "createdAt": "2024-03-14T10:30:00Z",
    "updatedAt": "2024-03-14T10:30:00Z",
    "customer": {
      "id": 7,
      "name": "Ahmet Yılmaz",
      "phone": "05321234567",
      "email": "ahmet@example.com"
    },
    "vehicle": {
      "id": 12,
      "brand": "Toyota",
      "model": "Corolla",
      "year": 2021,
      "plate": "34 ABC 123",
      "fuelType": "Benzin",
      "vin": "JT2BF22K1W0123456",
      "photos": []
    },
    "serviceTitle": "Yağ değişimi + filtre",
    "serviceDescription": "Motor yağı ve yağ filtresi değişimi yapılacak.",
    "technicianNotes": null,
    "technician": null,
    "parts": [
      { "id": "p1", "name": "Motor Yağı 5W-30", "quantity": 4, "unitPrice": 85.00, "total": 340.00 },
      { "id": "p2", "name": "Yağ Filtresi",     "quantity": 1, "unitPrice": 45.00, "total": 45.00 }
    ],
    "labor": [
      { "id": "l1", "description": "Yağ değişimi işçiliği", "hours": 0.5, "hourlyRate": 200.00, "total": 100.00 }
    ],
    "partsSubtotal": 385.00,
    "laborSubtotal": 100.00,
    "taxRate": 18,
    "taxAmount": 87.30,
    "total": 572.30,
    "timeline": [
      {
        "id": "t1",
        "status": "pending",
        "note": "İş emri oluşturuldu.",
        "createdAt": "2024-03-14T10:30:00Z",
        "createdBy": "Servis Yöneticisi"
      }
    ]
  }
}
```

> **Not:** Timeline'a otomatik olarak "İş emri oluşturuldu." kaydı eklenmelidir.

---

## Response — Hata Durumları

### `400 Bad Request` — Validation

```json
{
  "success": false,
  "message": "Geçersiz istek.",
  "errors": {
    "vehiclePlate": ["Plaka boş olamaz."],
    "serviceTitle": ["Hizmet başlığı zorunludur."]
  }
}
```

### `401 Unauthorized`

```json
{ "success": false, "message": "Yetkisiz erişim." }
```

### `500 Internal Server Error`

```json
{ "success": false, "message": "Sunucu hatası." }
```

---

## Backend İş Mantığı

### Müşteri Eşleştirme

```
1. customerPhone ile mevcut müşteri ara
2. Bulunursa: mevcut müşteri ID'sini kullan
3. Bulunmazsa: yeni müşteri kaydı oluştur (customerName, customerPhone, customerEmail)
```

### Araç Eşleştirme

```
1. vehiclePlate (büyük harf normalize edilmiş) ile mevcut araç ara
2. Bulunursa: mevcut araç ID'sini kullan (model/yıl güncelleme yapılmaz)
3. Bulunmazsa: yeni araç kaydı oluştur, müşteriyle ilişkilendir
```

### İş Emri Numarası (orderNo)

```
Format: WO-{YIL}-{SIRADAKI_NUMARA_4_BASAMAK}
Örnek:  WO-2024-0042
```

### Maliyet Hesabı

```
partsSubtotal = SUM(quantity × unitPrice)
laborSubtotal = SUM(hours × hourlyRate)
taxAmount     = (partsSubtotal + laborSubtotal) × (taxRate / 100)
total         = partsSubtotal + laborSubtotal + taxAmount
```

### Otomatik Timeline Kaydı

İş emri oluşturulduğunda timeline'a otomatik kayıt eklenmeli:

```json
{
  "status": "pending",
  "note": "İş emri oluşturuldu.",
  "createdAt": "<şimdiki zaman UTC>",
  "createdBy": "<token sahibi kullanıcının tam adı>"
}
```

---

## C# DTO Örneği

```csharp
public class CreateWorkOrderRequest
{
    // Customer
    [Required] public string CustomerName  { get; set; } = "";
    [Required] public string CustomerPhone { get; set; } = "";
    public string? CustomerEmail { get; set; }

    // Vehicle
    [Required] public string VehiclePlate { get; set; } = "";
    [Required] public string VehicleBrand { get; set; } = "";
    [Required] public string VehicleModel { get; set; } = "";
    [Required][Range(1900, 2030)] public int VehicleYear { get; set; }
    public string? VehicleFuelType { get; set; }
    public string? VehicleVin      { get; set; }

    // Service
    [Required] public string ServiceTitle       { get; set; } = "";
    public string? ServiceDescription { get; set; }
    public string? TechnicianNotes    { get; set; }
    public int?    TechnicianId       { get; set; }

    [Required] public string Status { get; set; } = "pending";

    public List<WorkOrderPartRequest>  Parts { get; set; } = new();
    public List<WorkOrderLaborRequest> Labor { get; set; } = new();

    [Required][Range(0, 100)] public decimal TaxRate { get; set; }
}

public class WorkOrderPartRequest
{
    [Required] public string  Name      { get; set; } = "";
    [Required] public int     Quantity  { get; set; }
    [Required] public decimal UnitPrice { get; set; }
}

public class WorkOrderLaborRequest
{
    [Required] public string  Description { get; set; } = "";
    [Required] public decimal Hours       { get; set; }
    [Required] public decimal HourlyRate  { get; set; }
}
```

---

## Mevcut Durum

| Endpoint | Durum |
|----------|-------|
| `POST /api/WorkOrders` | ❌ Eksik — bu dokümana göre implemente edilmeli |
| `GET /api/WorkOrders` | ✅ Mevcut |
| `GET /api/WorkOrders/{id}` | ✅ Mevcut |
| `PUT /api/WorkOrders/{id}` | ✅ Mevcut |
| `POST /api/WorkOrders/{id}/status` | ✅ Mevcut |

**Öncelik:** Yüksek
