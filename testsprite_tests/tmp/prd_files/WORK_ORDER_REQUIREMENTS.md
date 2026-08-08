# Backend Gereksinimleri - İş Emri (Work Order) API

## Genel Bakış

Mobil uygulamanın **İş Emirleri** ekranı (`WorkOrderManagementScreen`) ve **İş Emri Detay** ekranı (`WorkOrderDetailScreen`) için gerekli API endpoint'leri bu dokümanda tanımlanmıştır.

---

## 1. İş Emri Listesi

### Endpoint

```
GET /api/WorkOrders
```

### Authorization
- **Gerekli:** Evet (Bearer Token)
- **Roller:** SystemAdmin, Manager, Employee

### Query Parameters

| Parametre | Tip | Zorunlu | Açıklama |
|---|---|---|---|
| `status` | string | Hayır | Filtrele: `pending`, `inProgress`, `completed`, `cancelled` |
| `page` | int | Hayır | Sayfa numarası (varsayılan: 1) |
| `pageSize` | int | Hayır | Sayfa boyutu (varsayılan: 20) |

### Response (Başarılı)

```json
{
  "success": true,
  "data": [
    {
      "id": "1",
      "orderNo": "WO-2024-001",
      "customerName": "Ahmet Yılmaz",
      "vehiclePlate": "34 ABC 123",
      "vehicleModel": "Toyota Corolla 2020",
      "service": "Motor Bakımı",
      "status": "inProgress",
      "date": "2024-03-09",
      "amount": 2500.00,
      "technicianName": "Mehmet Usta"
    }
  ]
}
```

### WorkOrderListItem Alanları

| Alan | Tip | Zorunlu | Açıklama |
|---|---|---|---|
| `id` | string | Evet | Benzersiz iş emri ID |
| `orderNo` | string | Evet | Görüntülenen iş emri numarası (ör: WO-2024-001) |
| `customerName` | string | Evet | Müşteri tam adı |
| `vehiclePlate` | string | Evet | Araç plakası |
| `vehicleModel` | string | Evet | Marka + Model + Yıl (ör: Toyota Corolla 2020) |
| `service` | string | Evet | Ana servis başlığı |
| `status` | string | Evet | Bkz. [Durum Değerleri](#durum-değerleri) |
| `date` | string | Evet | ISO 8601 tarih (YYYY-MM-DD) |
| `amount` | decimal | Evet | Toplam tutar (₺) |
| `technicianName` | string | Hayır | Atanan teknisyen adı — atanmadıysa `null` |

---

## 2. İş Emri Detayı

### Endpoint

```
GET /api/WorkOrders/{id}
```

### Authorization
- **Gerekli:** Evet (Bearer Token)
- **Roller:** SystemAdmin, Manager, Employee

### Path Parameters

| Parametre | Tip | Açıklama |
|---|---|---|
| `id` | string | İş emri ID |

### Response (Başarılı)

```json
{
  "success": true,
  "data": {
    "id": "1",
    "orderNo": "WO-2024-001",
    "status": "inProgress",
    "createdAt": "2024-03-09T08:00:00",
    "updatedAt": "2024-03-09T10:30:00",

    "customer": {
      "id": 1,
      "name": "Ahmet Yılmaz",
      "phone": "+90 532 123 4567",
      "email": "ahmet@example.com"
    },

    "vehicle": {
      "id": 1,
      "brand": "Toyota",
      "model": "Corolla",
      "year": 2020,
      "plate": "34 ABC 123",
      "vin": "1HGBH41JXMN109186",
      "fuelType": "Benzin",
      "trim": "1.6 Dream",
      "photos": [
        "/uploads/vehicles/1/front.jpg",
        "/uploads/vehicles/1/side.jpg",
        "/uploads/vehicles/1/rear.jpg"
      ]
    },

    "serviceTitle": "Motor Bakımı",
    "serviceDescription": "Standart motor bakımı — yağ değişimi, filtre değişimi ve genel kontrol.",
    "technicianNotes": "Sol ön fren pabucu eskimiş, müşteriye bilgi verildi.",

    "technician": {
      "id": 3,
      "name": "Mehmet Usta"
    },

    "parts": [
      {
        "id": "p1",
        "name": "Motor Yağı 5W-30 (4L)",
        "quantity": 1,
        "unitPrice": 450.00,
        "total": 450.00
      },
      {
        "id": "p2",
        "name": "Yağ Filtresi",
        "quantity": 1,
        "unitPrice": 120.00,
        "total": 120.00
      }
    ],

    "labor": [
      {
        "id": "l1",
        "description": "Motor bakım işçiliği",
        "hours": 2,
        "hourlyRate": 350.00,
        "total": 700.00
      }
    ],

    "partsSubtotal": 570.00,
    "laborSubtotal": 700.00,
    "taxRate": 20,
    "taxAmount": 254.00,
    "total": 1524.00,

    "timeline": [
      {
        "id": "t1",
        "status": "created",
        "note": "İş emri oluşturuldu",
        "createdAt": "2024-03-09T08:00:00",
        "createdBy": "Yönetici"
      },
      {
        "id": "t2",
        "status": "assigned",
        "note": "Mehmet Usta'ya atandı",
        "createdAt": "2024-03-09T08:30:00",
        "createdBy": "Yönetici"
      },
      {
        "id": "t3",
        "status": "inProgress",
        "note": "Çalışma başladı",
        "createdAt": "2024-03-09T09:00:00",
        "createdBy": "Mehmet Usta"
      }
    ]
  }
}
```

### Araç Fotoğrafları (`vehicle.photos`) — Önemli

Mobil uygulama bu alanı **araç fotoğraf slider'ı** için kullanır.

- Dizi **boş** (`[]`) veya **`null`** gelirse slider gösterilmez — hata vermez.
- Her eleman sunucudaki dosyanın **relative veya absolute URL'i** olmalıdır.

**Kabul edilen formatlar:**

```
"/uploads/vehicles/1/front.jpg"          → Mobil prefix ekler: http://host:5169/uploads/...
"https://cdn.example.com/car/photo.jpg"  → Doğrudan kullanılır
```

**Önerilen depolama yapısı:**

```
wwwroot/
  uploads/
    vehicles/
      {vehicleId}/
        front.jpg
        side.jpg
        rear.jpg
```

**Statik dosya sunumu** (`Program.cs`):
```csharp
app.UseStaticFiles(); // wwwroot klasörü otomatik sunulur
```

---

## 3. İş Emri Durumu Güncelleme

### Endpoint

```
POST /api/WorkOrders/{id}/status
```

### Authorization
- **Gerekli:** Evet (Bearer Token)
- **Roller:** SystemAdmin, Manager, Employee

### Request Body

```json
{
  "status": "completed"
}
```

### Response (Başarılı)

Güncellenen iş emrinin tam detayını döner (yukarıdaki `/api/WorkOrders/{id}` response'u ile aynı yapı):

```json
{
  "success": true,
  "data": { ... }
}
```

### Response (Hatalı Geçiş)

```json
{
  "success": false,
  "message": "Bu iş emri zaten tamamlandı."
}
```

### İzin Verilen Durum Geçişleri

```
pending     → inProgress
pending     → cancelled
inProgress  → completed
inProgress  → cancelled
```

> `completed` ve `cancelled` durumundan geri dönüş **yoktur**.

---

## Durum Değerleri

| Değer | Türkçe | Renk (mobil) |
|---|---|---|
| `pending` | Bekliyor | Sarı `#fbbf24` |
| `inProgress` | Devam Ediyor | Mavi `#60a5fa` |
| `completed` | Tamamlandı | Yeşil `#34d399` |
| `cancelled` | İptal | Kırmızı `#f87171` |

---

## Zaman Çizelgesi (`timeline`) Event Tipleri

Mobil uygulama `status` alanına göre ikon seçer. Aşağıdaki değerler tanımlanmıştır:

| `status` değeri | Açıklama |
|---|---|
| `created` | İş emri oluşturuldu |
| `assigned` | Teknisyene atandı |
| `inProgress` | Çalışma başladı |
| `completed` | Tamamlandı |
| `cancelled` | İptal edildi |

Tanımlanmayan değerler için varsayılan ikon gösterilir — backend yeni tipler ekleyebilir.

---

## Hata Yanıtları (Genel)

```json
{
  "success": false,
  "message": "İş emri bulunamadı."
}
```

| HTTP Kodu | Durum |
|---|---|
| `200` | Başarılı |
| `400` | Geçersiz istek (ör: izin verilmeyen durum geçişi) |
| `401` | Yetkisiz (token geçersiz/eksik) |
| `403` | Erişim reddedildi (rol yetersiz) |
| `404` | İş emri bulunamadı |
| `500` | Sunucu hatası |

---

## Mobil Uygulama İle Entegrasyon Notları

1. **Token:** Tüm endpoint'ler `Authorization: Bearer {token}` header'ı gerektirir.
2. **Accept-Language:** Mobil uygulama `tr` veya `en` gönderir — hata mesajları buna göre yerelleştirilmeli.
3. **Fotoğraf URL'leri:** Mutlak URL (`http://...`) veya `/` ile başlayan relative path olmalıdır. Mobil uygulama `SERVER_BASE_URL` (port 5169) ile birleştirir.
4. **Tarihler:** Tüm tarih/saat alanları **ISO 8601** formatında, UTC olarak döndürülmeli (`2024-03-09T08:00:00Z`).
5. **Decimal:** Para birimleri en az 2 ondalık basamakla (`450.00`) döndürülmeli.
