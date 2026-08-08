# Work Orders API — Backend Dokümantasyonu

Mobil uygulama (`MagicCarRepairMobile`) için gerekli iş emri endpoint'lerini tanımlar.
Tüm endpoint'ler `Bearer` token ile korunmalıdır.

---

## Genel Kurallar

### Base URL
```
http://{host}:5169/api
```

### Authentication
Tüm endpoint'ler `Authorization: Bearer <token>` header'ı gerektirir.

### Standart Response Zarfı
Mevcut projedeki tüm endpoint'ler aynı zarfı kullanmaktadır (`IDataResult<T>`):

```json
{
  "success": true,
  "message": "İşlem başarılı",
  "data": { ... }
}
```

Hata durumunda:
```json
{
  "success": false,
  "message": "Hata açıklaması",
  "data": null
}
```

### Tarih Formatı
Tüm tarih/saat alanları **ISO 8601** formatında döndürülmelidir:
```
"2024-03-09T08:30:00"
```

### Status Değerleri
İş emri durumu için geçerli string değerler:
| Değer | Anlam |
|---|---|
| `"pending"` | Bekliyor |
| `"inProgress"` | Devam Ediyor |
| `"completed"` | Tamamlandı |
| `"cancelled"` | İptal Edildi |

---

## Endpoint'ler

---

### 1. İş Emirlerini Listele

```
GET /api/WorkOrders
Authorization: Bearer <token>
```

Giriş yapan kullanıcının bağlı olduğu servise ait tüm iş emirlerini döndürür.

#### Response `200 OK`

```json
{
  "success": true,
  "message": null,
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
      "amount": 1740.00,
      "technicianName": "Mehmet Usta"
    },
    {
      "id": "2",
      "orderNo": "WO-2024-002",
      "customerName": "Fatma Kaya",
      "vehiclePlate": "06 XYZ 456",
      "vehicleModel": "BMW 3 Serisi 2022",
      "service": "Fren Sistemi",
      "status": "pending",
      "date": "2024-03-09",
      "amount": 3024.00,
      "technicianName": null
    }
  ]
}
```

#### Alan Açıklamaları
| Alan | Tip | Zorunlu | Açıklama |
|---|---|---|---|
| `id` | string | ✅ | İş emri ID |
| `orderNo` | string | ✅ | Gösterim için sipariş numarası (ör. `WO-2024-001`) |
| `customerName` | string | ✅ | Müşteri tam adı |
| `vehiclePlate` | string | ✅ | Araç plakası |
| `vehicleModel` | string | ✅ | `{Marka} {Model} {Yıl}` formatında (ör. `Toyota Corolla 2020`) |
| `service` | string | ✅ | Kısa hizmet başlığı |
| `status` | string | ✅ | Bkz. Status Değerleri tablosu |
| `date` | string | ✅ | `YYYY-MM-DD` formatı |
| `amount` | number | ✅ | Toplam tutar (KDV dahil), kuruş yoksa `.00` |
| `technicianName` | string\|null | ❌ | Atanan teknisyen adı, atanmamışsa `null` |

---

### 2. İş Emri Detayı

```
GET /api/WorkOrders/{id}
Authorization: Bearer <token>
```

Belirtilen ID'li iş emrinin tüm detaylarını döndürür. Timeline dahil.

#### Response `200 OK`

```json
{
  "success": true,
  "message": null,
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
        "/uploads/vehicles/1/side.jpg"
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

#### Alan Açıklamaları — WorkOrderDetail

| Alan | Tip | Zorunlu | Açıklama |
|---|---|---|---|
| `id` | string | ✅ | |
| `orderNo` | string | ✅ | |
| `status` | string | ✅ | Bkz. Status Değerleri |
| `createdAt` | string | ✅ | ISO 8601 |
| `updatedAt` | string | ✅ | ISO 8601 |
| `customer` | object | ✅ | Bkz. Customer nesnesi |
| `vehicle` | object | ✅ | Bkz. Vehicle nesnesi |
| `serviceTitle` | string | ✅ | Hizmet başlığı |
| `serviceDescription` | string\|null | ❌ | Detaylı açıklama |
| `technicianNotes` | string\|null | ❌ | Teknisyen notu |
| `technician` | object\|null | ❌ | Atanan teknisyen, yoksa `null` |
| `parts` | array | ✅ | Parça listesi (boş olabilir `[]`) |
| `labor` | array | ✅ | İş gücü listesi (boş olabilir `[]`) |
| `partsSubtotal` | number | ✅ | Parçalar ara toplamı |
| `laborSubtotal` | number | ✅ | İş gücü ara toplamı |
| `taxRate` | number | ✅ | KDV oranı (ör. `20` = %20) |
| `taxAmount` | number | ✅ | KDV tutarı |
| `total` | number | ✅ | Genel toplam (KDV dahil) |
| `timeline` | array | ✅ | Olay geçmişi (boş olabilir `[]`) |

#### Vehicle.photos
Fotoğraf URL'leri relative path (`/uploads/...`) veya tam URL (`https://...`) olabilir.
Mobil uygulama relative path'leri `http://{SERVER_BASE_URL}{path}` formatına çevirir.

#### Timeline nesnesi
| Alan | Tip | Açıklama |
|---|---|---|
| `id` | string | Unique ID |
| `status` | string | `created`, `assigned`, `inProgress`, `completed`, `cancelled` |
| `note` | string\|null | Olay açıklaması |
| `createdAt` | string | ISO 8601 |
| `createdBy` | string | Olayı oluşturan kişinin adı |

#### Timeline — Otomatik Olay Oluşturma Kuralları
Backend aşağıdaki durumlarda timeline'a otomatik kayıt eklemeli:

| Tetikleyici | status değeri | Örnek note |
|---|---|---|
| İş emri oluşturulduğunda | `"created"` | `"İş emri oluşturuldu"` |
| Teknisyen atandığında | `"assigned"` | `"{TechnicianName}'e atandı"` |
| Status `inProgress`'e geçtiğinde | `"inProgress"` | `"Çalışma başladı"` |
| Status `completed`'a geçtiğinde | `"completed"` | `"İş tamamlandı"` |
| Status `cancelled`'a geçtiğinde | `"cancelled"` | `"İş emri iptal edildi"` |
| `PUT /WorkOrders/{id}` ile güncelleme | `"updated"` | `"İş emri güncellendi"` |

`createdBy` alanı, işlemi yapan kullanıcının adı + soyadı olmalıdır.

#### Not Found `404`
```json
{
  "success": false,
  "message": "İş emri bulunamadı.",
  "data": null
}
```

---

### 3. İş Emrini Güncelle

```
PUT /api/WorkOrders/{id}
Authorization: Bearer <token>
Content-Type: application/json
```

İş emrinin düzenlenebilir tüm alanlarını günceller.
Başarılı response'da güncellenmiş `WorkOrderDetail` (timeline dahil) döndürülmelidir.

#### Request Body

```json
{
  "serviceTitle": "Motor Bakımı ve Filtre Değişimi",
  "serviceDescription": "Güncellenmiş açıklama",
  "technicianNotes": "Müşteri bilgilendirildi.",
  "technicianId": 3,
  "status": "inProgress",
  "parts": [
    { "name": "Motor Yağı 5W-30 (4L)", "quantity": 1, "unitPrice": 450.00 },
    { "name": "Yağ Filtresi",           "quantity": 1, "unitPrice": 120.00 }
  ],
  "labor": [
    { "description": "Motor bakım işçiliği", "hours": 2, "hourlyRate": 350.00 }
  ],
  "taxRate": 20
}
```

#### Request Alanları
| Alan | Tip | Zorunlu | Açıklama |
|---|---|---|---|
| `serviceTitle` | string | ✅ | Hizmet başlığı |
| `serviceDescription` | string\|null | ❌ | Detaylı açıklama |
| `technicianNotes` | string\|null | ❌ | Teknisyen notu |
| `technicianId` | number\|null | ❌ | Teknisyen ID, kaldırmak için `null` |
| `status` | string | ✅ | Bkz. Status Değerleri |
| `parts` | array | ✅ | Parça listesi (boş olabilir `[]`) — mevcut parçaların **tamamının yerini alır** |
| `parts[].name` | string | ✅ | |
| `parts[].quantity` | number | ✅ | |
| `parts[].unitPrice` | number | ✅ | |
| `labor` | array | ✅ | İş gücü listesi — mevcut kayıtların **tamamının yerini alır** |
| `labor[].description` | string | ✅ | |
| `labor[].hours` | number | ✅ | |
| `labor[].hourlyRate` | number | ✅ | |
| `taxRate` | number | ✅ | KDV oranı (ör. `20`) |

> **Not:** `parts` ve `labor` listeleri **replace** semantiğiyle çalışır. Gönderilen liste mevcut tüm kayıtların yerini alır. Backend `total`, `partsSubtotal`, `laborSubtotal`, `taxAmount`, `total` alanlarını sunucu tarafında hesaplamalıdır.

#### Response `200 OK`
Güncellenmiş `WorkOrderDetail` nesnesi döner (format: yukarıdaki GET detay response'uyla aynı).
`timeline` array'i güncelleme olayını da içermelidir.

#### Validation Hataları `400`
```json
{
  "success": false,
  "message": "Hizmet başlığı boş olamaz.",
  "data": null
}
```

---

### 4. İş Emri Durum Güncelle

```
POST /api/WorkOrders/{id}/status
Authorization: Bearer <token>
Content-Type: application/json
```

Yalnızca status alanını günceller. Durum geçişi yapıldığında timeline'a otomatik olay eklenir.

#### Request Body

```json
{
  "status": "inProgress"
}
```

#### Geçerli Durum Geçişleri
| Mevcut Durum | Geçilebilir Durumlar |
|---|---|
| `pending` | `inProgress`, `cancelled` |
| `inProgress` | `completed`, `cancelled` |
| `completed` | — (değiştirilemez) |
| `cancelled` | — (değiştirilemez) |

#### Response `200 OK`
Güncellenmiş `WorkOrderDetail` nesnesi döner.
`timeline` array'i yeni status olayını da içermelidir.

#### Geçersiz Geçiş `400`
```json
{
  "success": false,
  "message": "Tamamlanmış iş emirlerinin durumu değiştirilemez.",
  "data": null
}
```

---

### 5. Yeni İş Emri Oluştur *(Opsiyonel — şu an mobilde UI yok)*

```
POST /api/WorkOrders
Authorization: Bearer <token>
Content-Type: application/json
```

Yeni iş emri oluşturur. Oluşturulduğunda timeline'a `created` olayı eklenir.

#### Request Body

```json
{
  "customerId": 1,
  "vehicleId": 1,
  "serviceTitle": "Motor Bakımı",
  "serviceDescription": "Yağ ve filtre değişimi.",
  "technicianId": null,
  "parts": [],
  "labor": [],
  "taxRate": 20
}
```

#### Response `201 Created`
Oluşturulan `WorkOrderDetail` nesnesi döner.

---

## Özet Tablo

| Method | Path | Açıklama |
|---|---|---|
| `GET` | `/api/WorkOrders` | Servis iş emirlerini listele |
| `GET` | `/api/WorkOrders/{id}` | İş emri detayı (timeline dahil) |
| `PUT` | `/api/WorkOrders/{id}` | Tüm alanları güncelle |
| `POST` | `/api/WorkOrders/{id}/status` | Yalnızca durumu güncelle |
| `POST` | `/api/WorkOrders` | Yeni iş emri oluştur |

---

## Ek Notlar

### Yetkilendirme Kısıtlamaları (Öneri)
- **Manager / Employee**: Kendi servislerine ait iş emirlerine erişebilir.
- **Customer**: Kendi iş emirlerini görebilir (sadece GET), güncelleme yapamaz.
- **SystemAdmin**: Tüm iş emirlerine erişebilir.

### Fotoğraf URL'leri
`vehicle.photos` alanındaki URL'ler relative path ise (`/uploads/...`) mobil uygulama önüne `SERVER_BASE_URL` ekler.
Absolute URL (`https://...`) geliyorsa doğrudan kullanılır. Karışık format desteklenmektedir.

### Hesaplama Tutarlılığı
`parts[].total`, `labor[].total`, `partsSubtotal`, `laborSubtotal`, `taxAmount`, `total`
alanları **sunucu tarafında** hesaplanmalı ve response'da dolu gönderilmelidir.
Mobil uygulama bu değerleri görüntülemek için kullanır; hesaplamayı kendi yapmaz.
