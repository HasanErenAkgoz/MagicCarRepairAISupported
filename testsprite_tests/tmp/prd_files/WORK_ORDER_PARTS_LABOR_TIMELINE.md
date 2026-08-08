# Backend Gereksinimleri — Parça & İşçilik ve Zaman Çizelgesi API

## Mevcut Durum (Frontend)

`WorkOrderDetailScreen` ekranı **3 sekme** içerir:

| Sekme | Hangi API | Durum |
|---|---|---|
| Genel | `GET /api/WorkOrders/{id}` | ✅ Mevcut (WORK_ORDER_REQUIREMENTS.md) |
| Parça & İşçilik | `GET /api/WorkOrders/{id}` | ⚠️ Sadece okuma — yazma endpoint'leri **eksik** |
| Zaman Çizelgesi | `GET /api/WorkOrders/{id}` | ⚠️ Sadece okuma — manuel not endpoint'i **eksik** |

**Kritik:** Şu an her iki sekme yalnızca iş emri detay endpoint'inden gelen veriyi **gösterir**. Parça ekleme, düzenleme, silme ve manuel zaman çizelgesi notu ekleme için aşağıdaki endpoint'lerin eklenmesi gerekir.

---

## 1. Parça (Part) CRUD

### 1.1 Parça Ekle

```
POST /api/WorkOrders/{workOrderId}/parts
```

**Authorization:** Bearer Token | Roller: SystemAdmin, Manager, Employee

**Request Body:**
```json
{
  "name": "Motor Yağı 5W-30 (4L)",
  "quantity": 2,
  "unitPrice": 450.00
}
```

**Response (Başarılı):**
```json
{
  "success": true,
  "data": {
    "id": "p4",
    "name": "Motor Yağı 5W-30 (4L)",
    "quantity": 2,
    "unitPrice": 450.00,
    "total": 900.00
  }
}
```

> **Not:** `total = quantity × unitPrice` backend tarafında hesaplanmalıdır.

---

### 1.2 Parça Güncelle

```
PUT /api/WorkOrders/{workOrderId}/parts/{partId}
```

**Authorization:** Bearer Token | Roller: SystemAdmin, Manager, Employee

**Request Body:**
```json
{
  "name": "Motor Yağı 5W-30 (4L)",
  "quantity": 3,
  "unitPrice": 450.00
}
```

**Response:** Güncellenmiş parça nesnesi (`1.1` ile aynı yapı).

---

### 1.3 Parça Sil

```
DELETE /api/WorkOrders/{workOrderId}/parts/{partId}
```

**Authorization:** Bearer Token | Roller: SystemAdmin, Manager, Employee

**Response (Başarılı):**
```json
{
  "success": true,
  "message": "Parça silindi."
}
```

---

## 2. İşçilik (Labor) CRUD

### 2.1 İşçilik Kalemi Ekle

```
POST /api/WorkOrders/{workOrderId}/labor
```

**Authorization:** Bearer Token | Roller: SystemAdmin, Manager, Employee

**Request Body:**
```json
{
  "description": "Motor bakım işçiliği",
  "hours": 2.5,
  "hourlyRate": 350.00
}
```

**Response (Başarılı):**
```json
{
  "success": true,
  "data": {
    "id": "l2",
    "description": "Motor bakım işçiliği",
    "hours": 2.5,
    "hourlyRate": 350.00,
    "total": 875.00
  }
}
```

> **Not:** `total = hours × hourlyRate` backend tarafında hesaplanmalıdır.

---

### 2.2 İşçilik Kalemi Güncelle

```
PUT /api/WorkOrders/{workOrderId}/labor/{laborId}
```

**Request Body:** `2.1` ile aynı yapı.

**Response:** Güncellenmiş işçilik nesnesi.

---

### 2.3 İşçilik Kalemi Sil

```
DELETE /api/WorkOrders/{workOrderId}/labor/{laborId}
```

**Response:**
```json
{
  "success": true,
  "message": "İşçilik kalemi silindi."
}
```

---

## 3. Maliyet Özeti — Otomatik Yeniden Hesaplama

Parça veya işçilik eklenip/güncellenip/silindiğinde backend **maliyet özetini otomatik güncellemelidir.**

`GET /api/WorkOrders/{id}` response'unda dönen alanlar:

| Alan | Hesaplama |
|---|---|
| `partsSubtotal` | Tüm `parts[i].total` toplamı |
| `laborSubtotal` | Tüm `labor[i].total` toplamı |
| `taxAmount` | `(partsSubtotal + laborSubtotal) × taxRate / 100` |
| `total` | `partsSubtotal + laborSubtotal + taxAmount` |

> **Öneri:** Parça/işçilik mutasyon endpoint'lerinin response'unda güncellenmiş maliyet özetini de döndürün. Mobil uygulama böylece ekranı taze veriyle güncelleyebilir.

**Örnek — parça ekleme response'una maliyet özeti eklenmesi:**
```json
{
  "success": true,
  "data": {
    "part": { "id": "p4", "name": "...", "quantity": 2, "unitPrice": 450.00, "total": 900.00 },
    "updatedCosts": {
      "partsSubtotal": 1650.00,
      "laborSubtotal": 700.00,
      "taxAmount": 470.00,
      "total": 2820.00
    }
  }
}
```

---

## 4. Zaman Çizelgesi — Manuel Not Ekleme

Durum değişikliği dışında teknisyen veya yönetici manuel not bırakabilmelidir.

### 4.1 Manuel Not Ekle

```
POST /api/WorkOrders/{workOrderId}/timeline
```

**Authorization:** Bearer Token | Roller: SystemAdmin, Manager, Employee

**Request Body:**
```json
{
  "note": "Müşteri öğleden sonra aracı teslim almak istiyor."
}
```

**Response (Başarılı):**
```json
{
  "success": true,
  "data": {
    "id": "t5",
    "status": "note",
    "note": "Müşteri öğleden sonra aracı teslim almak istiyor.",
    "createdAt": "2024-03-09T11:45:00",
    "createdBy": "Mehmet Usta"
  }
}
```

> **Not:** `status` alanı `"note"` olarak set edilmeli. Frontend bu değer için özel ikon gösterir.
> `createdBy` alanı token'daki kimliğe göre backend tarafından doldurulmalı.

---

## 5. Araç Fotoğrafı Yükleme / Silme

### 5.1 Fotoğraf Yükle

```
POST /api/Vehicles/{vehicleId}/photos
Content-Type: multipart/form-data
```

**Authorization:** Bearer Token | Roller: SystemAdmin, Manager, Employee

**Form Fields:**

| Alan | Tip | Açıklama |
|---|---|---|
| `photo` | File | Resim dosyası (jpg, jpeg, png, webp) |

**Kısıtlamalar:**
- Maksimum boyut: **10 MB**
- İzin verilen formatlar: `image/jpeg`, `image/png`, `image/webp`
- Araç başına maksimum fotoğraf: **10 adet**

**Response (Başarılı):**
```json
{
  "success": true,
  "data": {
    "photoId": "ph3",
    "url": "/uploads/vehicles/1/photo_abc123.jpg"
  }
}
```

**Önerilen kayıt yolu:**
```
wwwroot/uploads/vehicles/{vehicleId}/{uuid}.{ext}
```

**Program.cs:**
```csharp
app.UseStaticFiles(); // wwwroot klasörü otomatik erişilebilir
```

---

### 5.2 Fotoğraf Sil

```
DELETE /api/Vehicles/{vehicleId}/photos/{photoId}
```

**Authorization:** Bearer Token | Roller: SystemAdmin, Manager, Employee

**Response (Başarılı):**
```json
{
  "success": true,
  "message": "Fotoğraf silindi."
}
```

> Fiziksel dosyanın da `wwwroot`'tan silinmesi gerekir.

---

## 6. Özet — Eksik Endpoint Listesi

| # | Method | Endpoint | Açıklama | Öncelik |
|---|---|---|---|---|
| 1 | POST | `/api/WorkOrders/{id}/parts` | Parça ekle | Yüksek |
| 2 | PUT | `/api/WorkOrders/{id}/parts/{partId}` | Parça güncelle | Yüksek |
| 3 | DELETE | `/api/WorkOrders/{id}/parts/{partId}` | Parça sil | Yüksek |
| 4 | POST | `/api/WorkOrders/{id}/labor` | İşçilik kalemi ekle | Yüksek |
| 5 | PUT | `/api/WorkOrders/{id}/labor/{laborId}` | İşçilik kalemi güncelle | Yüksek |
| 6 | DELETE | `/api/WorkOrders/{id}/labor/{laborId}` | İşçilik kalemi sil | Yüksek |
| 7 | POST | `/api/WorkOrders/{id}/timeline` | Manuel not ekle | Orta |
| 8 | POST | `/api/Vehicles/{id}/photos` | Araç fotoğrafı yükle | Orta |
| 9 | DELETE | `/api/Vehicles/{id}/photos/{photoId}` | Araç fotoğrafı sil | Orta |

---

## 7. Mevcut Endpoint'ler (Referans)

Aşağıdakiler `WORK_ORDER_REQUIREMENTS.md`'de zaten tanımlıdır:

| Method | Endpoint | Açıklama |
|---|---|---|
| GET | `/api/WorkOrders` | İş emri listesi |
| GET | `/api/WorkOrders/{id}` | İş emri detayı (parça + işçilik + timeline dahil) |
| POST | `/api/WorkOrders/{id}/status` | Durum güncelle |
