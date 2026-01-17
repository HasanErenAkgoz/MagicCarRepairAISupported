# 🎯 MagicCarRepairAISupported - Kullanıcı Deneyimi Rehberi

## 📊 Sistem Durumu: **KULLANIMA HAZIR** ✅

Sistem şu anda **temel oto servis yönetimi** için kullanıma hazır durumda. Aşağıda kullanıcıların yapabilecekleri detaylı olarak listelenmiştir.

---

## 👥 Kullanıcı Rolleri ve Yetkileri

### 1. **Sistem Yöneticisi (System Admin)**
- Tüm servisleri (Client/Tenant) yönetebilir
- Tüm rolleri ve izinleri yönetebilir
- Sistem genelinde erişim

### 2. **Servis Yöneticisi (Manager)**
- Kendi servisinin tüm işlemlerini yönetebilir
- Personel, müşteri, araç, iş emri yönetimi
- Stok ve teklif yönetimi

### 3. **Personel (Employee)**
- İş emirlerini görüntüleyebilir ve güncelleyebilir
- Parça ekleyebilir, işçilik kaydedebilir
- Fotoğraf yükleyebilir
- Stok durumunu kontrol edebilir

### 4. **Müşteri (Customer)**
- Teklif talebi oluşturabilir
- Teklifleri görüntüleyebilir ve kabul/reddedebilir
- İş emri durumunu takip edebilir (SignalR ile real-time)
- Bildirimleri alabilir

---

## 🚀 Kullanıcıların Yapabilecekleri

### 🔐 1. Kimlik Doğrulama ve Yetkilendirme

#### ✅ Yapılabilenler:
- **Kayıt Olma**: Yeni kullanıcı kaydı
- **Giriş Yapma**: Email/şifre ile giriş
- **Şifre Sıfırlama**: Unutulan şifre için sıfırlama
- **Token Yönetimi**: JWT token ile yetkilendirme
- **Multi-Tenant**: Her servis kendi verilerine erişir (otomatik izolasyon)

#### 📍 API Endpoints:
```
POST   /api/auth/register          - Kayıt ol
POST   /api/auth/login            - Giriş yap
POST   /api/auth/forgot-password   - Şifre sıfırlama talebi
POST   /api/auth/reset-password   - Şifre sıfırla
```

---

### 👨‍💼 2. Personel Yönetimi

#### ✅ Yapılabilenler:
- **Personel Ekleme**: Yeni personel kaydı (Ad, Soyad, TC, Pozisyon, Maaş, vb.)
- **Personel Güncelleme**: Mevcut personel bilgilerini güncelleme
- **Personel Silme**: Soft delete ile personel silme
- **Personel Listeleme**: Filtreleme ve sayfalama ile personel listesi
  - Pozisyona göre filtreleme (Mekanik, Kaporta, Boyacı, vb.)
  - Çalışma durumuna göre filtreleme (Aktif, İzinli, İşten Ayrıldı)
- **Personel Detayı**: ID'ye göre personel detayları

#### 📍 API Endpoints:
```
POST   /api/employees                    - Yeni personel ekle
GET    /api/employees                    - Tüm personelleri listele
GET    /api/employees/{id}               - Personel detayı
GET    /api/employees/by-position/{pos}  - Pozisyona göre personeller
PUT    /api/employees/{id}               - Personel güncelle
DELETE /api/employees/{id}               - Personel sil
```

#### 💡 Kullanım Senaryoları:
- Servis yöneticisi yeni mekanik ekler
- Personel listesini pozisyona göre filtreler
- İşten ayrılan personeli soft delete yapar

---

### 🔧 3. Parça ve Stok Yönetimi

#### ✅ Yapılabilenler:
- **Parça Ekleme**: Yeni parça kaydı (Kod, Ad, Kategori, Marka, Fiyat, vb.)
- **Parça Listeleme**: 
  - Arama (parça adı/kodu)
  - Kategoriye göre filtreleme (Motor, Fren, Elektrik, vb.)
  - Marka tipine göre filtreleme (Orijinal/Emsal)
  - Düşük stoklu parçaları gösterme
  - Sayfalama
- **Parça Detayı**: ID'ye göre parça bilgileri ve stok durumu
- **Stok Takibi**: Her parça için stok miktarı ve minimum seviye
- **Stok Alarmları**: Otomatik stok alarm sistemi
  - Minimum stok seviyesi altına düşen parçalar için alarm
  - Kritik stok uyarıları
  - Email/SMS bildirimleri

#### 📍 API Endpoints:
```
POST   /api/parts              - Yeni parça ekle
GET    /api/parts              - Parçaları listele (filtreleme ile)
GET    /api/parts/{id}         - Parça detayı
GET    /api/stock-alerts/active - Aktif stok alarmları
POST   /api/stock-alerts/{id}/resolve - Stok alarmını çöz
```

#### 💡 Kullanım Senaryoları:
- Yeni parça eklenir (örn: Fren balata)
- Stok seviyesi minimumun altına düşünce otomatik alarm oluşur
- Yönetici alarmları görüntüler ve otomatik sipariş oluşturur

---

### 📋 4. İş Emri (WorkOrder) Yönetimi

#### ✅ Yapılabilenler:
- **İş Emri Oluşturma**: 
  - Araç girişi kaydı
  - Müşteri şikayetleri kayıt
  - Km ve yakıt seviyesi kayıt
  - Öncelik belirleme (Düşük, Normal, Yüksek, Acil)
  - Tahmini tamamlanma tarihi
  - Personel atama
- **İş Emri Durum Güncelleme**: 
  - 11 farklı durum (Randevu Alındı, Araç Girişi, İşlemde, Hazır, Teslim Edildi, vb.)
  - Durum değişikliklerinde otomatik timeline kaydı
  - Müşteriye real-time bildirim (SignalR)
- **Parça Ekleme**: 
  - İş emrine parça ekleme
  - Otomatik stok düşüşü
  - Fiyat hesaplama (KDV dahil)
  - Stok yetersizse uyarı
- **İşçilik Ekleme**: 
  - Personel bazlı işçilik kaydı
  - Başlangıç/bitiş saati
  - Otomatik süre ve tutar hesaplama
- **Fotoğraf Ekleme**: 
  - Giriş, işlem sırası, çıkış fotoğrafları
  - Hasar fotoğrafları
  - Timeline'a bağlı fotoğraflar
- **İş Emri Listeleme**: 
  - Duruma göre filtreleme
  - Müşteriye göre filtreleme
  - Araç bazlı filtreleme
  - Personel bazlı filtreleme
  - Tarih aralığı filtreleme
  - Sayfalama
- **Aktif İş Emirleri**: Devam eden iş emirlerini listeleme
- **İş Emri Detayı**: Tüm bilgiler, parçalar, işçilik, timeline, fotoğraflar
- **İş Emri Timeline**: Tüm durum değişiklikleri ve notlar
- **İş Emri Tamamlama**: İş emrini tamamlandı olarak işaretleme
- **İş Emri Teslim**: İş emrini teslim edildi olarak işaretleme ve ödeme durumu

#### 📍 API Endpoints:
```
POST   /api/workorders                    - Yeni iş emri oluştur
GET    /api/workorders                    - Tüm iş emirlerini listele
GET    /api/workorders/{id}               - İş emri detayı
GET    /api/workorders/active             - Aktif iş emirleri
GET    /api/workorders/{id}/timeline       - İş emri timeline
PUT    /api/workorders/{id}/status         - Durum güncelle
POST   /api/workorders/{id}/items          - Parça ekle
POST   /api/workorders/{id}/labor          - İşçilik ekle
POST   /api/workorders/{id}/photos         - Fotoğraf ekle
POST   /api/workorders/{id}/complete        - İş emrini tamamla
POST   /api/workorders/{id}/deliver        - İş emrini teslim et
DELETE /api/workorders/{id}/items/{itemId} - Parça çıkar
```

#### 💡 Kullanım Senaryoları:
1. **Araç Girişi**: Müşteri aracı getirir → İş emri oluşturulur → Giriş fotoğrafları çekilir
2. **Arıza Tespiti**: Mekanik arızayı tespit eder → Parçalar eklenir → İşçilik kaydedilir
3. **İşlem Takibi**: Müşteri SignalR ile real-time durum güncellemelerini görür
4. **Tamamlama**: İş tamamlanır → Fotoğraflar eklenir → Müşteriye bildirim → Teslim edilir

---

### 💰 5. Çoklu Servis Teklif Sistemi (Bidding System)

#### ✅ Yapılabilenler:
- **Teklif Talebi Oluşturma** (Müşteri):
  - Sorun açıklaması
  - Araç bilgileri (Marka, Model, Yıl, Plaka)
  - Hasar tipi (Kaza, Arıza, Bakım)
  - Aciliyet seviyesi (Düşük, Normal, Yüksek, Acil)
  - Fotoğraf yükleme
  - İstenen tarih aralığı
- **Teklif Verme** (Servis):
  - Açık teklif taleplerini görüntüleme
  - Teklif tutarı belirleme
  - Tahmini süre belirleme
  - İndirim oranı
  - Garanti süresi
  - Açıklama ve notlar
- **Teklif Yönetimi** (Müşteri):
  - Gelen teklifleri görüntüleme
  - Teklifleri karşılaştırma
  - Teklif kabul/reddetme
  - Teklif kabul edildiğinde otomatik iş emri oluşturma
- **Teklif Listeleme**:
  - Tüm teklif taleplerini görüntüleme
  - Duruma göre filtreleme (Açık, Teklifler Alındı, Seçildi, İptal)
  - Aciliyet seviyesine göre filtreleme
  - Tarih aralığı filtreleme

#### 📍 API Endpoints:
```
POST   /api/quote-requests                          - Teklif talebi oluştur
GET    /api/quote-requests                           - Tüm talepleri listele
GET    /api/quote-requests/{id}                      - Talep detayı
GET    /api/quote-requests/open                      - Açık talepler (servisler için)
POST   /api/quote-requests/{id}/quotes               - Teklif ver
POST   /api/quote-requests/{id}/quotes/{quoteId}/accept - Teklifi kabul et
POST   /api/quote-requests/{id}/quotes/{quoteId}/reject - Teklifi reddet
```

#### 💡 Kullanım Senaryoları:
1. **Müşteri Teklif İster**: Müşteri hasar fotoğraflarıyla teklif talebi oluşturur
2. **Servisler Teklif Verir**: Birden fazla servis teklif verir
3. **Müşteri Seçer**: Müşteri en uygun teklifi seçer
4. **Otomatik İş Emri**: Seçilen tekliften otomatik iş emri oluşturulur

---

### 🔔 6. Bildirim ve İletişim Sistemi

#### ✅ Yapılabilenler:
- **Email Bildirimleri**: 
  - İş emri durum değişiklikleri
  - Yeni teklif bildirimleri
  - Stok alarm bildirimleri
  - Özel bildirimler
- **SMS Bildirimleri** (Netgsm entegrasyonu):
  - Kısa mesaj gönderimi
  - Durum güncellemeleri
- **WhatsApp Bildirimleri** (WhatsApp Business API):
  - Mesaj gönderimi
  - Medya gönderimi (fotoğraf, doküman)
- **Push Bildirimleri**: 
  - Web push bildirimleri
  - Mobil push bildirimleri
- **Bildirim Şablonları**: 
  - Önceden tanımlı şablonlar
  - Dinamik içerik doldurma
  - Çoklu dil desteği
- **Bildirim Geçmişi**: 
  - Tüm bildirimlerin kaydı
  - Okundu/okunmadı durumu
  - Filtreleme (tip, durum, tarih)

#### 📍 API Endpoints:
```
POST   /api/notifications              - Bildirim gönder
GET    /api/notifications/my-notifications - Kullanıcı bildirimleri
GET    /api/notifications/unread      - Okunmamış bildirimler
POST   /api/notifications/{id}/read    - Bildirimi okundu işaretle
```

#### 💡 Kullanım Senaryoları:
- İş emri durumu değiştiğinde müşteriye email/SMS/WhatsApp bildirimi
- Yeni teklif geldiğinde müşteriye bildirim
- Stok alarmında yöneticiye bildirim

---

### 📊 7. Real-Time Güncellemeler (SignalR)

#### ✅ Yapılabilenler:
- **Real-Time İş Emri Güncellemeleri**: 
  - Durum değişiklikleri anında bildirim
  - Müşteri ve personel aynı anda güncellemeleri görür
- **Real-Time Bildirimler**: 
  - Yeni bildirimler anında gelir
  - Okunmamış bildirim sayısı güncellenir
- **Hub Bağlantıları**:
  - `/hubs/notifications` - Bildirim hub'ı
  - `/hubs/workorders` - İş emri hub'ı

#### 💡 Kullanım Senaryoları:
- Müşteri web sayfasında iş emri durumunu canlı takip eder
- Personel durum güncellediğinde müşteri anında görür
- Bildirimler anında ekranda belirir

---

### 📦 8. Otomatik Stok Sipariş Sistemi

#### ✅ Yapılabilenler:
- **Otomatik Stok Kontrolü**: 
  - Her saat başı otomatik kontrol (Hosted Service)
  - Minimum stok seviyesi kontrolü
  - Kritik stok tespiti
- **Otomatik Sipariş Oluşturma**: 
  - Stok alarmından otomatik sipariş oluşturma
  - Tedarikçi seçimi
  - Miktar ve fiyat belirleme
- **Sipariş Yönetimi**: 
  - Bekleyen siparişleri görüntüleme
  - Sipariş onaylama/reddetme
  - Sipariş durumu takibi

#### 📍 API Endpoints:
```
GET    /api/stock-alerts/active        - Aktif stok alarmları
POST   /api/stock-alerts/{id}/resolve - Alarm çöz
POST   /api/auto-orders                - Otomatik sipariş oluştur
GET    /api/auto-orders/pending         - Bekleyen siparişler
POST   /api/auto-orders/{id}/approve    - Sipariş onayla
```

#### 💡 Kullanım Senaryoları:
- Stok minimum seviyenin altına düşer
- Otomatik alarm oluşur ve yöneticiye bildirim gider
- Yönetici otomatik sipariş oluşturur
- Sipariş onaylanır ve tedarikçiye gönderilir

---

### 🏢 9. Multi-Tenant (Çoklu Servis) Yönetimi

#### ✅ Yapılabilenler:
- **Servis (Client) Oluşturma**: 
  - Yeni servis kaydı
  - Servis bilgileri (Ad, Kod, İletişim)
- **Veri İzolasyonu**: 
  - Her servis sadece kendi verilerine erişir
  - Otomatik filtreleme (Global Query Filters)
  - Güvenli veri yönetimi
- **Servis Yönetimi**: 
  - Servis bilgilerini görüntüleme
  - Aktif/pasif durumu

#### 📍 API Endpoints:
```
POST   /api/clients        - Yeni servis oluştur
GET    /api/clients/{id}   - Servis detayı
```

---

### 📁 10. Dosya Yönetimi

#### ✅ Yapılabilenler:
- **Dosya Yükleme**: 
  - Fotoğraf yükleme
  - Doküman yükleme
- **Dosya İndirme**: 
  - Yüklenen dosyaları indirme
- **Dosya Listeleme**: 
  - Yüklenen dosyaları görüntüleme

#### 📍 API Endpoints:
```
POST   /api/files/upload   - Dosya yükle
GET    /api/files/{id}     - Dosya indir
GET    /api/files          - Dosya listesi
```

---

## 🎯 Kullanım Senaryoları (End-to-End)

### Senaryo 1: Yeni Araç Girişi ve İşlem Süreci

1. **Müşteri Araç Getirir**
   - Personel yeni iş emri oluşturur (`POST /api/workorders`)
   - Araç bilgileri, km, yakıt seviyesi kaydedilir
   - Giriş fotoğrafları çekilir ve yüklenir

2. **Arıza Tespiti**
   - Mekanik arızayı tespit eder
   - Gerekli parçalar eklenir (`POST /api/workorders/{id}/items`)
   - Stoktan otomatik düşüş yapılır
   - İşçilik kaydedilir (`POST /api/workorders/{id}/labor`)

3. **İşlem Takibi**
   - Durum güncellenir (`PUT /api/workorders/{id}/status`)
   - Müşteri SignalR ile real-time güncellemeleri görür
   - Fotoğraflar eklenir (`POST /api/workorders/{id}/photos`)

4. **Tamamlama ve Teslim**
   - İş emri tamamlanır (`POST /api/workorders/{id}/complete`)
   - Müşteriye bildirim gider (Email/SMS/WhatsApp)
   - Teslim edilir (`POST /api/workorders/{id}/deliver`)

### Senaryo 2: Teklif Sistemi ile İş Emri Oluşturma

1. **Müşteri Teklif İster**
   - Teklif talebi oluşturur (`POST /api/quote-requests`)
   - Hasar fotoğrafları yükler
   - Sorun açıklaması yazar

2. **Servisler Teklif Verir**
   - Birden fazla servis açık talepleri görür (`GET /api/quote-requests/open`)
   - Her servis teklif verir (`POST /api/quote-requests/{id}/quotes`)

3. **Müşteri Seçer**
   - Müşteri teklifleri karşılaştırır
   - En uygun teklifi seçer (`POST /api/quote-requests/{id}/quotes/{quoteId}/accept`)
   - Otomatik iş emri oluşturulur

4. **İşlem Başlar**
   - Seçilen servis iş emrini görür
   - Normal işlem süreci başlar

### Senaryo 3: Stok Alarm ve Otomatik Sipariş

1. **Stok Düşer**
   - Parça kullanıldıkça stok düşer
   - Minimum seviyenin altına düşer

2. **Alarm Oluşur**
   - Otomatik stok alarmı oluşur (Hosted Service)
   - Yöneticiye bildirim gider

3. **Otomatik Sipariş**
   - Yönetici alarmları görür (`GET /api/stock-alerts/active`)
   - Otomatik sipariş oluşturur (`POST /api/auto-orders`)
   - Sipariş onaylar (`POST /api/auto-orders/{id}/approve`)

---

## ⚠️ Eksik Özellikler (Şu An Kullanılamayan)

### 🔴 Kritik Eksikler:
1. **Muhasebe ve Faturalama**: 
   - Gelir-gider takibi yok
   - Fatura oluşturma yok
   - Ödeme takibi yok

2. **Müşteri ve Araç Yönetimi**: 
   - Müşteri CRUD işlemleri yok (sadece entity var)
   - Araç CRUD işlemleri yok (sadece entity var)

3. **Dashboard ve Raporlama**: 
   - Dashboard yok
   - Raporlar yok
   - İstatistikler yok

### 🟡 Orta Öncelikli Eksikler:
4. **Audit Logging**: 
   - İşlem logları yok
   - Kullanıcı aktivite takibi yok

5. **Export/Import**: 
   - Excel/PDF export yok
   - Toplu veri girişi yok

6. **AI Özellikleri**: 
   - Arıza tespiti yok
   - Fotoğraf analizi yok
   - Fiyat tahmini yok

---

## ✅ Sonuç: Sistem Kullanıma Hazır mı?

### **EVET, ama sınırlı kapsamda!** ✅

#### ✅ **Kullanıma Hazır Olanlar:**
- ✅ Personel yönetimi
- ✅ Parça ve stok yönetimi
- ✅ İş emri yönetimi (tam özellikli)
- ✅ Teklif sistemi (tam özellikli)
- ✅ Bildirim sistemi (Email, SMS, WhatsApp, Push)
- ✅ Real-time güncellemeler (SignalR)
- ✅ Stok alarm ve otomatik sipariş

#### ⚠️ **Eksik Olanlar (Kritik):**
- ❌ Müşteri yönetimi (CRUD)
- ❌ Araç yönetimi (CRUD)
- ❌ Muhasebe ve faturalama
- ❌ Dashboard ve raporlama

### 🎯 **Öneri:**
Sistem **MVP (Minimum Viable Product)** seviyesinde kullanıma hazır. Ancak **tam kullanım** için:
1. Müşteri ve Araç CRUD işlemleri eklenmeli
2. Muhasebe modülü eklenmeli
3. Dashboard eklenmeli

Bu 3 özellik eklendiğinde sistem **tam kullanıma hazır** olacaktır! 🚀

---

## 📞 Destek ve İletişim

Sorularınız için:
- API Dokümantasyonu: Swagger UI (`/swagger`)
- Hata Mesajları: Çoklu dil desteği (TR, EN, AR)
- Loglama: Detaylı hata logları

---

**Son Güncelleme**: 2025-01-29
**Versiyon**: 1.0.0 (MVP)

