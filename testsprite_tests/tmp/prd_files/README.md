# Backend Dokümantasyonu

Bu klasör, frontend geliştirme ekibi tarafından backend ekibine iletilmek üzere hazırlanan gereksinim dokümanlarını içerir.

---

## Dokümanlar

### 1. [FORGOT_PASSWORD_REQUIREMENTS.md](./FORGOT_PASSWORD_REQUIREMENTS.md)
**Şifremi Unuttum — SMS/OTP Tabanlı Sistem**

- OTP tabanlı şifre sıfırlama akışı
- 3 ana endpoint: `forgot-password`, `verify-reset-otp`, `reset-password`
- SMS servisi entegrasyonu
- Güvenlik önlemleri ve best practices
- Veritabanı şeması önerileri

**Öncelik:** Yüksek | **Durum:** Beklemede

---

### 2. [USER_TYPE_ENUM_FIX.md](./USER_TYPE_ENUM_FIX.md)
**UserType Enum Serialization Düzeltmesi**

- `userType` alanının string değil number olarak gönderilmesi gereksinimi
- .NET ve TypeScript için çözüm örnekleri
- Tüm authentication endpoint'lerinde kontrol gerekliliği

**Öncelik:** Yüksek | **Durum:** Beklemede

---

### 3. [WEEKLY_REVENUE_ANALYTICS.md](./WEEKLY_REVENUE_ANALYTICS.md)
**Haftalık Revenue Analytics API**

- `GET /api/Dashboard/weekly-revenue`
- Son 7 günün günlük gelir verileri ve haftalık toplam
- Önceki hafta karşılaştırması ve değişim yüzdesi
- C# implementation örneği ve veritabanı sorgusu

**Öncelik:** Orta | **Durum:** Beklemede

---

### 4. [FLEET_STATUS_REQUIREMENTS.md](./FLEET_STATUS_REQUIREMENTS.md)
**Fleet Status (Efficiency Overview) API**

- `GET /api/Dashboard/fleet-status`
- İş emirlerinin durumları: Tamirde, Tamamlandı, Bekliyor
- Efficiency (verimlilik) yüzdesi hesaplama
- Donut chart için frontend entegrasyon detayları

**Öncelik:** Orta | **Durum:** Beklemede

---

### 5. [TWO_FACTOR_AUTHENTICATION_REQUIREMENTS.md](./TWO_FACTOR_AUTHENTICATION_REQUIREMENTS.md)
**Two-Factor Authentication (2FA) API**

- `POST /api/Auth/enable-2fa` ve `POST /api/Auth/disable-2fa`
- QR code oluşturma (opsiyonel — authenticator app)
- Backup codes, rate limiting, audit logging

**Öncelik:** Orta | **Durum:** Beklemede

---

### 6. [REMEMBER_ME_REQUIREMENTS.md](./REMEMBER_ME_REQUIREMENTS.md)
**Remember Me (Beni Hatırla) Özelliği**

- Frontend implementasyonu tamamlandı — backend değişikliği **gerekmez**
- Opsiyonel: token expiration süreleri, device tracking, session management

**Öncelik:** Düşük | **Durum:** Çalışıyor

---

### 7. [WORK_ORDER_REQUIREMENTS.md](./WORK_ORDER_REQUIREMENTS.md)
**İş Emri (Work Order) Temel API**

- `GET /api/WorkOrders` — liste (filtreleme, sayfalama)
- `GET /api/WorkOrders/{id}` — detay (müşteri, araç, parçalar, işçilik, maliyet, timeline)
- `POST /api/WorkOrders/{id}/status` — durum güncelleme ve izin verilen geçişler
- `vehicle.photos` — araç fotoğraf slider için URL listesi
- Statik dosya sunumu: `wwwroot/uploads/vehicles/{id}/`

**Öncelik:** Yüksek | **Durum:** Beklemede

---

### 8. [WORK_ORDER_PARTS_LABOR_TIMELINE.md](./WORK_ORDER_PARTS_LABOR_TIMELINE.md)
**Parça & İşçilik CRUD + Zaman Çizelgesi + Araç Fotoğrafı API**

- Mevcut durum analizi: hangi sekme hangi API'yi kullanıyor
- Parça ekle / güncelle / sil — `POST/PUT/DELETE /api/WorkOrders/{id}/parts`
- İşçilik kalemi ekle / güncelle / sil — `POST/PUT/DELETE /api/WorkOrders/{id}/labor`
- Otomatik maliyet özeti yeniden hesaplama kuralları
- Manuel zaman çizelgesi notu — `POST /api/WorkOrders/{id}/timeline`
- Araç fotoğrafı yükle / sil — `POST/DELETE /api/Vehicles/{id}/photos`
- **9 eksik endpoint özet tablosu**

**Öncelik:** Yüksek | **Durum:** Beklemede

---

### 9. [WORK_ORDER_CREATE_REQUIREMENTS.md](./WORK_ORDER_CREATE_REQUIREMENTS.md)
**Yeni İş Emri Oluşturma API**

- `POST /api/WorkOrders` — yeni iş emri oluşturma (şu an eksik)
- Müşteri + araç + hizmet bilgisi tek request'te gönderilir
- Mevcut müşteri/araç telefon & plaka ile eşleştirilir, yoksa yeni kayıt açılır
- Otomatik `orderNo` üretimi (WO-{YIL}-{XXXX} formatı)
- Maliyet özeti otomatik hesaplanır ve dönülür
- Timeline'a "İş emri oluşturuldu." kaydı otomatik eklenir
- C# DTO örnekleri ve iş mantığı detayları

**Öncelik:** Yüksek | **Durum:** Beklemede

---

## Genel Notlar

- Tüm dokümanlar frontend ekibi tarafından hazırlanmıştır
- Değişiklikler tamamlandıktan sonra frontend ekibi test edecektir
- Tüm endpoint'ler `Authorization: Bearer {token}` header'ı gerektirir
- Tarih/saat alanları ISO 8601 UTC formatında olmalıdır
- Hata yanıtları `{ "success": false, "message": "..." }` formatında döndürülmelidir
