# MagicCarRepairAISupported - Tam Kapsamlı Oto Servis Yönetim Sistemi
## Proje Yol Haritası

> **Vizyon**: İşletme ve Müşteri odaklı, Clean Architecture prensiplerine uygun, kullanıcı dostu oto servis yönetim sistemi

---

## 📋 Proje Durumu

### ✅ Tamamlanan Altyapı
- [x] Multi-Tenant Architecture (Her servis izole)
- [x] Dinamik Çoklu Dil Hata Mesajları (TR, EN, AR)
- [x] Role-Permission Sistemi (Her servis kendi rolleri)
- [x] Authentication & Authorization
- [x] CQRS Pattern
- [x] Repository Pattern
- [x] Unit of Work
- [x] Global Query Filters

---

## 🎯 FAZ 1: Temel İşletme Yönetimi (Öncelik: YÜKSEK)
**Tahmini Süre**: 2-3 Hafta

### 1.1 Personel Yönetimi
**Dosyalar**: `Features/Employees/`

#### Entity'ler
- [ ] `Employee` entity
  - PersonelNo, Ad, Soyad, TC, Telefon, Email
  - Pozisyon (Mekanik, Kaporta, Boyacı, Yönetici, vb.)
  - Maaş Bilgisi
  - İşe Giriş Tarihi
  - Çalışma Durumu (Aktif/Pasif)
  - Uzmanlık Alanları (Json field)
  - Adres, Kan Grubu, Acil Durum İletişim

#### Features
- [ ] CreateEmployee (Command)
- [ ] UpdateEmployee (Command)
- [ ] DeleteEmployee (Command) - Soft delete
- [ ] GetAllEmployees (Query) - Pagination, filtering
- [ ] GetEmployeeById (Query)
- [ ] GetEmployeesByPosition (Query)
- [ ] AssignEmployeeToWorkOrder (Command)

#### API Endpoints
```
POST   /api/employees
PUT    /api/employees/{id}
DELETE /api/employees/{id}
GET    /api/employees
GET    /api/employees/{id}
GET    /api/employees/by-position/{position}
```

---

### 1.2 Parça Stok Yönetimi
**Dosyalar**: `Features/Parts/`

#### Entity'ler
- [ ] `Part` entity
  - Parça Kodu, Parça Adı
  - Kategori (Motor, Fren, Elektrik, vb.)
  - Marka (Orijinal/Emsal)
  - Tedarikçi Bilgisi
  - Alış Fiyatı, Satış Fiyatı
  - KDV Oranı
  - Minimum Stok Seviyesi
  - Kritik Stok Uyarısı
  - Barkod

- [ ] `PartStock` entity
  - PartId
  - Miktar
  - Konum (Raf No)
  - Son Güncelleme Tarihi

- [ ] `PartSupplier` entity (Tedarikçi)
  - Firma Adı, Yetkili Kişi
  - Telefon, Email, Adres
  - Ödeme Koşulları

- [ ] `StockMovement` entity (Stok Hareketleri)
  - Giriş/Çıkış
  - Miktar, Tarih
  - İşlem Yapan Personel
  - Açıklama

#### Features
- [ ] CreatePart (Command)
- [ ] UpdatePart (Command)
- [ ] UpdatePartStock (Command)
- [ ] GetAllParts (Query) - Pagination, search, filter
- [ ] GetPartById (Query)
- [ ] GetLowStockParts (Query) - Kritik stok uyarıları
- [ ] RecordStockMovement (Command)
- [ ] GetStockMovementHistory (Query)
- [ ] CreateSupplier (Command)
- [ ] UpdateSupplier (Command)

#### Business Rules
- [ ] Stok minimum seviyenin altına düşerse bildirim
- [ ] Parça çıkışı yapılırken stok kontrolü
- [ ] Fiyat geçmişi tutulması (Price History)

---

### 1.3 Gelişmiş Araç ve Müşteri Yönetimi
**Dosyalar**: `Features/WorkOrders/`

#### Entity'ler
- [ ] `WorkOrder` entity (İş Emri)
  - İş Emri No (Otomatik)
  - VehicleId, CustomerId
  - Giriş Tarihi/Saati
  - Tahmini Çıkış Tarihi
  - Gerçek Çıkış Tarihi
  - Durum (Beklemede, İşlemde, Tamamlandı, Teslim Edildi)
  - Öncelik (Düşük, Normal, Acil)
  - Km Bilgisi
  - Yakıt Seviyesi
  - Müşteri Şikayetleri (Json)
  - Özel İstekler (Json)
  - Toplam Tutar
  - İndirim
  - Ödeme Durumu

- [ ] `WorkOrderItem` entity (İş Emri Kalemleri)
  - WorkOrderId
  - Tip (Parça, İşçilik, Dış Hizmet)
  - PartId (opsiyonel)
  - Açıklama
  - Miktar
  - Birim Fiyat
  - İndirim
  - KDV
  - Toplam
  - Marka (Orijinal/Emsal)
  - Garanti Süresi

- [ ] `WorkOrderLabor` entity (İşçilik)
  - WorkOrderId
  - EmployeeId (Yapan personel)
  - İşlem Adı
  - Başlangıç Saati
  - Bitiş Saati
  - Süre (saat)
  - Saat Ücreti
  - Toplam Tutar

- [ ] `WorkOrderTimeline` entity (İşlem Geçmişi)
  - WorkOrderId
  - Tarih/Saat
  - Durum Değişikliği
  - Yapan Personel
  - Açıklama
  - Fotoğraflar (Json - file paths)

- [ ] `WorkOrderPhoto` entity
  - WorkOrderId
  - TimelineId (opsiyonel)
  - Fotoğraf Yolu
  - Açıklama
  - Tip (Giriş, İşlem, Çıkış)
  - Yükleme Tarihi

#### Features
- [ ] CreateWorkOrder (Command)
  - Araç girişi
  - Müşteri şikayetleri kayıt
  - Başlangıç fotoğrafları
- [ ] UpdateWorkOrder (Command)
- [ ] AddWorkOrderItem (Command) - Parça/İşçilik ekleme
- [ ] RemoveWorkOrderItem (Command)
- [ ] UpdateWorkOrderStatus (Command)
  - Timeline kaydı otomatik
  - Müşteriye bildirim
- [ ] AddWorkOrderLabor (Command)
- [ ] AddWorkOrderPhoto (Command)
- [ ] CalculateWorkOrderTotal (Command)
- [ ] CompleteWorkOrder (Command)
- [ ] DeliverWorkOrder (Command)
- [ ] GetWorkOrderById (Query)
- [ ] GetAllWorkOrders (Query) - Filter by status, date, customer
- [ ] GetWorkOrderTimeline (Query)
- [ ] GetActiveWorkOrders (Query)
- [ ] GetWorkOrdersByEmployee (Query)
- [ ] GenerateWorkOrderPDF (Query) - Proforma/Fatura

#### Business Rules
- [ ] İş emri oluştururken parça stok kontrolü
- [ ] Parça kullanıldığında stoktan otomatik düşme
- [ ] Fiyat hesaplama: (Parça + İşçilik + Dış Hizmet) - İndirim + KDV
- [ ] Durum değişikliklerinde timeline kaydı
- [ ] Teslimatta müşteri onayı

---

## 🎯 FAZ 2: Muhasebe ve Mali Yönetim (Öncelik: YÜKSEK)
**Tahmini Süre**: 2 Hafta

### 2.1 Gelir-Gider Takibi
**Dosyalar**: `Features/Accounting/`

#### Entity'ler
- [ ] `Income` entity (Gelir)
  - WorkOrderId (opsiyonel)
  - Gelir Türü (İş Emri, Dış Satış, vb.)
  - Tutar
  - Ödeme Yöntemi (Nakit, Kredi Kartı, Havale)
  - Tarih
  - Açıklama
  - Fatura No

- [ ] `Expense` entity (Gider)
  - Gider Türü (Personel Maaş, Kira, Elektrik, Su, Parça Alımı, vb.)
  - Tutar
  - Ödeme Yöntemi
  - Tarih
  - Tedarikçi
  - Fatura No
  - Açıklama

- [ ] `SalaryPayment` entity (Maaş Ödemeleri)
  - EmployeeId
  - Dönem (Ay/Yıl)
  - Brüt Maaş
  - SGK Kesintisi
  - Vergi Kesintisi
  - Net Maaş
  - Ödeme Tarihi
  - Ödeme Yöntemi

- [ ] `Tax` entity (Vergi)
  - Vergi Türü (KDV, Stopaj, Kurumlar, vb.)
  - Dönem
  - Tutar
  - Son Ödeme Tarihi
  - Ödeme Tarihi
  - Durum

#### Features
- [ ] RecordIncome (Command)
- [ ] RecordExpense (Command)
- [ ] PaySalary (Command)
- [ ] RecordTax (Command)
- [ ] GetIncomeReport (Query) - Tarih aralığı, filtreleme
- [ ] GetExpenseReport (Query)
- [ ] GetProfitLossReport (Query) - Kar-Zarar raporu
- [ ] GetMonthlySummary (Query)
- [ ] GetYearlySummary (Query)
- [ ] GetCashFlow (Query) - Nakit akışı
- [ ] GetTaxReport (Query)

#### Business Rules
- [ ] İş emri tamamlandığında otomatik gelir kaydı
- [ ] Personel maaşı ödendiğinde otomatik gider kaydı
- [ ] KDV hesaplaması otomatik
- [ ] Kar = (Satış - Alış) - İşçilik - Genel Giderler

---

### 2.2 Faturalama
**Dosyalar**: `Features/Invoices/`

#### Entity'ler
- [ ] `Invoice` entity
  - Fatura No (Otomatik)
  - Fatura Türü (Satış, Alış)
  - WorkOrderId (opsiyonel)
  - CustomerId/SupplierId
  - Fatura Tarihi
  - Vade Tarihi
  - Ara Toplam
  - KDV Tutarı
  - Toplam Tutar
  - Durum (Ödendi, Beklemede, Vadesi Geçti)
  - E-Fatura Entegrasyonu (gelecek)

- [ ] `InvoiceItem` entity
  - InvoiceId
  - Açıklama
  - Miktar
  - Birim Fiyat
  - KDV Oranı
  - Toplam

#### Features
- [ ] CreateInvoice (Command)
- [ ] GenerateInvoiceFromWorkOrder (Command)
- [ ] UpdateInvoiceStatus (Command)
- [ ] GetInvoiceById (Query)
- [ ] GetAllInvoices (Query)
- [ ] GetOverdueInvoices (Query)
- [ ] PrintInvoice (Query) - PDF
- [ ] SendInvoiceByEmail (Command)
- [ ] GenerateInvoiceQRCode (Query) - Hızlı ödeme için QR kod
- [ ] ExportInvoiceToExcel (Query)
- [ ] E-Fatura Entegrasyonu (gelecek)

#### Ödeme Entegrasyonu
- [ ] Online Ödeme Gateway (İyzico, PayTR, Papara)
- [ ] Taksit Seçenekleri
- [ ] Ödeme Geçmişi Takibi
- [ ] Ödeme Bildirimleri (SMS/Email)
- [ ] Ödeme QR Kodu Oluşturma

---

## 🎯 FAZ 3: Müşteri Paneli ve Teklif Sistemi (Öncelik: YÜKSEK)
**Tahmini Süre**: 2 Hafta

### 3.1 Çoklu Servis Teklif Sistemi (Bidding System) (Öncelik: YÜKSEK - İnovatif)
**Dosyalar**: `Features/QuoteRequests/`

#### Entity'ler
- [ ] `QuoteRequest` entity (Müşteri Talebi)
  - Talep No (Otomatik)
  - CustomerId (Müşteri - opsiyonel, misafir kullanıcılar için)
  - VehicleId (Araç - opsiyonel, yeni araç için)
  - Araç Bilgileri (Marka, Model, Yıl, Plaka - eğer VehicleId yoksa)
  - Sorun Açıklaması (Text)
  - Hasar Tipi (Kaza, Arıza, Bakım, vb.)
  - Aciliyet Seviyesi
  - İstenen Tarih Aralığı
  - Durum (Açık, Teklifler Alındı, Teklif Seçildi, İptal)
  - Seçilen Teklif ID
  - Son Teklif Tarihi
  - Müşteri İletişim Bilgileri (Email, Telefon)

- [ ] `QuoteResponse` entity (Servis Teklifleri)
  - QuoteRequestId
  - ClientId (Teklif veren servis)
  - Teklif No (Otomatik)
  - Teklif Açıklaması
  - Tahmini Süre (Gün)
  - Teklif Tutarı
  - İndirim Oranı
  - Net Tutar
  - Garanti Süresi (Ay)
  - Durum (Beklemede, Kabul Edildi, Reddedildi, İptal)
  - Teklif Tarihi
  - Geçerlilik Tarihi
  - Notlar

- [ ] `QuoteRequestPhoto` entity (Talep Fotoğrafları)
  - QuoteRequestId
  - FilePath
  - UploadedFileId
  - PhotoType (Hasar, Arıza, Genel)
  - Açıklama

#### Features
- [ ] CreateQuoteRequest (Command) - Müşteri talep oluşturur
- [ ] UpdateQuoteRequest (Command)
- [ ] SubmitQuoteResponse (Command) - Servis teklif verir
- [ ] AcceptQuoteResponse (Command) - Müşteri teklif kabul eder
- [ ] RejectQuoteResponse (Command) - Müşteri teklif reddeder
- [ ] CancelQuoteRequest (Command)
- [ ] AddQuoteRequestPhoto (Command)
- [ ] GetAllQuoteRequests (Query) - Tüm talepler (servis görünümü)
- [ ] GetMyQuoteRequests (Query) - Müşterinin kendi talepleri
- [ ] GetQuoteRequestById (Query)
- [ ] GetQuoteResponsesByRequest (Query) - Bir talebe verilen teklifler
- [ ] GetQuoteResponsesByClient (Query) - Servisin verdiği teklifler
- [ ] GetOpenQuoteRequests (Query) - Açık talepler (teklif bekleyen)

#### Business Rules
- [ ] Müşteri misafir olarak da talep oluşturabilir (CustomerId opsiyonel)
- [ ] Her servis (Client) sadece bir kez teklif verebilir
- [ ] Teklif verildikten sonra QuoteRequest durumu güncellenir
- [ ] Teklif kabul edildiğinde otomatik WorkOrder oluşturulur
- [ ] Teklif reddedildiğinde müşteriye bildirim gönderilir
- [ ] Son Teklif Tarihi geçince QuoteRequest otomatik kapanır
- [ ] Müşteri teklif seçtiğinde diğer servislere otomatik red bildirimi

#### WorkOrder Entegrasyonu
- [ ] Teklif kabul edildiğinde otomatik WorkOrder oluştur
- [ ] QuoteRequest bilgilerini WorkOrder'a aktar
- [ ] Teklif tutarını WorkOrder'a set et
- [ ] Fotoğrafları WorkOrder'a kopyala

#### API Endpoints
```
# Müşteri Endpoints (Public veya Authenticated)
POST   /api/quote-requests                    - Talep oluştur
GET    /api/quote-requests/my-requests        - Kendi taleplerim
GET    /api/quote-requests/{id}               - Talep detayı
GET    /api/quote-requests/{id}/responses      - Teklifleri görüntüle
POST   /api/quote-requests/{id}/accept/{responseId} - Teklif kabul et
POST   /api/quote-requests/{id}/reject/{responseId} - Teklif reddet
POST   /api/quote-requests/{id}/photos         - Fotoğraf ekle
PUT    /api/quote-requests/{id}                - Talep güncelle
DELETE /api/quote-requests/{id}                - Talep iptal

# Servis Endpoints (Authenticated - Sadece kendi ClientId'si için)
GET    /api/quote-requests/open                - Açık talepler (teklif verebilecekler)
GET    /api/quote-requests                     - Tüm talepler
POST   /api/quote-responses                    - Teklif ver
PUT    /api/quote-responses/{id}                - Teklif güncelle
GET    /api/quote-responses/my-responses       - Verdiğim teklifler
GET    /api/quote-responses/{id}                - Teklif detayı
```

#### AI Entegrasyonu
- [ ] AI Destekli Teklif Önerisi - Fotoğraf ve açıklamaya göre otomatik teklif önerisi
- [ ] AI Destekli Teklif Optimizasyonu - Rekabetçi fiyat önerisi
- [ ] AI Destekli Sorun Analizi - Fotoğraflardan sorun tespiti

### 3.2 Müşteri Portal
**Dosyalar**: `Features/CustomerPortal/`

#### Features
- [ ] CustomerLogin (Command)
- [ ] GetMyVehicles (Query)
- [ ] GetMyWorkOrders (Query)
- [ ] GetWorkOrderDetails (Query)
  - Timeline görünümü
  - Kullanılan parçalar
  - Fotoğraflar
  - Durum
- [ ] GetWorkOrderPhotos (Query)
- [ ] DownloadInvoice (Query)
- [ ] RequestMaintenanceReminder (Command)
- [ ] UpdateMyProfile (Command)
- [ ] GetMaintenanceHistory (Query) - Araç bakım geçmişi

#### UI Özellikleri
- [ ] Timeline Component (Vertical timeline)
- [ ] Photo Gallery Component
- [ ] Part Details Component (Orijinal/Emsal badge)
- [ ] Status Tracker (Araç durumu: Girdi, İşlemde, Hazır, vb.)
- [ ] Push Notification (Durum değişikliğinde)
- [ ] QR Code ile hızlı erişim

---

## 🎯 FAZ 4: Raporlama ve Analizler (Öncelik: ORTA)
**Tahmini Süre**: 1 Hafta

### 4.1 Dashboard ve Raporlar
**Dosyalar**: `Features/Reports/`

#### Features
- [ ] GetDashboardSummary (Query)
  - Bugünkü iş emirleri
  - Bekleyen işler
  - Günlük ciro
  - Kritik stoklar
  - Vadesi yaklaşan faturalar

- [ ] GetWorkOrderStatistics (Query)
  - Tamamlanma süreleri
  - Ortalama işlem süresi
  - Personel performansı

- [ ] GetPartUsageReport (Query)
  - En çok kullanılan parçalar
  - Parça bazlı kar marjları

- [ ] GetCustomerAnalytics (Query)
  - Müşteri sadakati
  - Tekrar gelme oranları
  - Müşteri değeri (CLV)

- [ ] GetFinancialCharts (Query)
  - Aylık gelir-gider grafiği
  - Kar trendi
  - Kategori bazlı harcamalar

- [ ] ExportReport (Query) - Excel, PDF

---

## 🎯 FAZ 5: İleri Özellikler (Öncelik: DÜŞÜK)
**Tahmini Süre**: 2 Hafta

### 5.1 Randevu Sistemi
- [ ] Appointment entity
- [ ] Online randevu alma
- [ ] SMS/Email hatırlatıcı
- [ ] Takvim entegrasyonu

### 5.2 Tedarikçi Entegrasyonu
- [ ] Tedarikçi portalı
- [ ] Otomatik sipariş
- [ ] Fiyat karşılaştırma

### 5.3 Mobil Uygulama
- [ ] React Native / Flutter
- [ ] Personel için mobil app
- [ ] Müşteri için mobil app
- [ ] Fotoğraf yükleme (mobil)

### 5.4 Bakım Hatırlatıcıları
- [ ] Periyodik bakım takibi
- [ ] Otomatik müşteri bilgilendirme
- [ ] Email/SMS entegrasyonu

### 5.5 Bildirim ve İletişim Sistemi (Öncelik: YÜKSEK)
**Dosyalar**: `Features/Notifications/`

#### Bildirim Türleri
- [ ] **Email Bildirimleri**
  - WorkOrder durum değişiklikleri
  - Fatura gönderimi
  - Randevu hatırlatıcıları
  - Stok alarmları
  - Teklif bildirimleri

- [ ] **SMS Bildirimleri**
  - SMS Gateway entegrasyonu (Netgsm, İleti Merkezi, vb.)
  - WorkOrder durum bildirimleri
  - Randevu hatırlatıcıları
  - Ödeme hatırlatıcıları
  - Acil durum bildirimleri

- [ ] **Push Bildirimleri**
  - Mobil uygulama için
  - Web push notifications
  - Real-time bildirimler

- [ ] **WhatsApp Business API**
  - Otomatik bildirimler (WorkOrder durumu, fatura)
  - Chatbot desteği
  - Fotoğraf/video gönderimi
  - Onay istekleri (interaktif butonlar)

#### Features
- [ ] SendNotification (Command) - Genel bildirim gönder
- [ ] SendSMS (Command)
- [ ] SendEmail (Command) - Zaten var, geliştirilebilir
- [ ] SendWhatsAppMessage (Command)
- [ ] SendPushNotification (Command)
- [ ] GetNotificationHistory (Query)
- [ ] MarkNotificationAsRead (Command)
- [ ] NotificationSettings (Command) - Kullanıcı bildirim tercihleri

#### Business Rules
- [ ] Müşteri bildirim tercihlerine göre gönderim
- [ ] Bildirim şablonları (Template system)
- [ ] Bildirim geçmişi saklama
- [ ] Başarısız bildirimler için retry mekanizması

---

### 5.6 Müşteri Değerlendirme ve Rating Sistemi (Öncelik: ORTA)
**Dosyalar**: `Features/Ratings/`

#### Entity'ler
- [ ] `ServiceRating` entity
  - WorkOrderId
  - CustomerId
  - ClientId (Değerlendirilen servis)
  - Rating (1-5 yıldız)
  - Hizmet Kalitesi (1-5)
  - Fiyat Uygunluğu (1-5)
  - Zamanında Teslim (1-5)
  - Personel Davranışı (1-5)
  - Yorum (Text)
  - Fotoğraflar (opsiyonel)
  - Durum (Onaylandı, Beklemede, Reddedildi)
  - Yanıt (Servis yanıtı)

- [ ] `ServiceReview` entity (Detaylı inceleme)
  - ServiceRatingId
  - Kategori bazlı değerlendirme
  - Öneri/Dilek

#### Features
- [ ] CreateRating (Command) - Müşteri değerlendirme yapar
- [ ] UpdateRating (Command)
- [ ] ReplyToRating (Command) - Servis yanıt verir
- [ ] GetRatingsByClient (Query) - Servisin tüm değerlendirmeleri
- [ ] GetRatingsByWorkOrder (Query)
- [ ] GetAverageRating (Query) - Servis ortalama puanı
- [ ] GetRatingStatistics (Query) - Puan dağılımı, trend analizi
- [ ] ModerateRating (Command) - Admin onay/red

#### Business Rules
- [ ] WorkOrder tamamlandıktan sonra rating yapılabilir
- [ ] Her WorkOrder için bir rating
- [ ] Servis yanıt verebilir
- [ ] Sahte rating tespiti (AI destekli)
- [ ] Rating ortalaması otomatik hesaplanır

---

### 5.7 QR Code ve Barcode Sistemi (Öncelik: ORTA)
**Dosyalar**: `Features/QRCode/`

#### QR Code Kullanım Alanları
- [ ] WorkOrder QR Code - Hızlı erişim, müşteri takibi
- [ ] Invoice QR Code - Hızlı ödeme, fatura görüntüleme
- [ ] Part QR Code - Stok takibi, hızlı arama
- [ ] Vehicle QR Code - Araç dosyası hızlı erişim
- [ ] Employee QR Code - Personel kimlik kartı

#### Barcode Sistemi
- [ ] Part Barcode - Parça barkod okuma
- [ ] Barcode Scanner entegrasyonu
- [ ] Stok giriş/çıkış barcode ile

#### Features
- [ ] GenerateQRCode (Command) - QR kod oluştur
- [ ] ScanQRCode (Query) - QR kod okuma
- [ ] GenerateBarcode (Command) - Barcode oluştur
- [ ] ScanBarcode (Query) - Barcode okuma
- [ ] GetQRCodeInfo (Query) - QR kod bilgisi

---

### 5.8 Real-time Updates (SignalR) (Öncelik: YÜKSEK)
**Dosyalar**: `Features/Realtime/`

#### Real-time Özellikler
- [ ] WorkOrder durum güncellemeleri
- [ ] Yeni teklif bildirimleri
- [ ] Stok alarm bildirimleri
- [ ] Chat sistemi (Müşteri-Servis)
- [ ] Dashboard canlı güncellemeleri
- [ ] Bildirim sistemi

#### Features
- [ ] WorkOrderStatusHub - WorkOrder durum güncellemeleri
- [ ] NotificationHub - Bildirim hub'ı
- [ ] ChatHub - Mesajlaşma hub'ı
- [ ] DashboardHub - Dashboard güncellemeleri

---

### 5.9 Export/Import Sistemi (Öncelik: ORTA)
**Dosyalar**: `Features/ExportImport/`

#### Export Özellikleri
- [ ] WorkOrder Export (Excel, PDF, CSV)
- [ ] Part Stock Export (Excel)
- [ ] Customer Export (Excel)
- [ ] Invoice Export (Excel, PDF)
- [ ] Report Export (Excel, PDF)
- [ ] Bulk Export (Toplu export)

#### Import Özellikleri
- [ ] Part Import (Excel) - Toplu parça ekleme
- [ ] Customer Import (Excel) - Toplu müşteri ekleme
- [ ] Stock Import (Excel) - Stok güncelleme
- [ ] Price List Import (Excel) - Fiyat listesi güncelleme

#### Features
- [ ] ExportToExcel (Query)
- [ ] ExportToPDF (Query)
- [ ] ExportToCSV (Query)
- [ ] ImportFromExcel (Command)
- [ ] ValidateImportData (Command) - Import öncesi doğrulama
- [ ] GetImportTemplate (Query) - Import şablonu indir

---

### 5.10 Stok Alarm ve Otomatik Sipariş Sistemi (Öncelik: YÜKSEK)
**Dosyalar**: `Features/StockAlerts/`

#### Stok Alarm Sistemi
- [ ] Minimum Stok Seviyesi Alarmı
- [ ] Kritik Stok Alarmı
- [ ] Stok Bitiş Tarihi Alarmı
- [ ] Otomatik Bildirim (Email, SMS)

#### Otomatik Sipariş Sistemi
- [ ] Minimum stok seviyesine ulaşınca otomatik sipariş
- [ ] Tedarikçi seçimi (en ucuz, en hızlı)
- [ ] Sipariş onay mekanizması
- [ ] Sipariş takibi

#### Features
- [ ] SetStockAlert (Command) - Stok alarm seviyesi belirle
- [ ] GetStockAlerts (Query) - Aktif alarmlar
- [ ] CreateAutoOrder (Command) - Otomatik sipariş oluştur
- [ ] GetAutoOrderHistory (Query)
- [ ] ApproveAutoOrder (Command) - Sipariş onayla

---

### 5.11 Audit Logging ve Güvenlik (Öncelik: YÜKSEK)
**Dosyalar**: `Features/Audit/`

#### Audit Log Özellikleri
- [ ] Tüm kritik işlemlerin loglanması
- [ ] Kullanıcı aktivite takibi
- [ ] Veri değişiklik geçmişi
- [ ] Silinen kayıtların saklanması (Soft Delete)
- [ ] IP adresi ve cihaz bilgisi

#### Features
- [ ] GetAuditLogs (Query) - Log kayıtları
- [ ] GetUserActivity (Query) - Kullanıcı aktivitesi
- [ ] GetDataChangeHistory (Query) - Veri değişiklik geçmişi
- [ ] ExportAuditLogs (Query) - Log export

---

### 5.12 Entegrasyonlar
- [ ] E-Fatura entegrasyonu
- [ ] Muhasebe yazılımı entegrasyonu (Logo, Nebim, vb.)
- [ ] Google Maps (araç konumu takibi)
- [ ] Google My Business (otomatik yorum cevaplama)

### 5.13 AI Destekli Özellikler (Öncelik: YÜKSEK - İnovatif)
**Dosyalar**: `Features/AI/`

#### AI Özellikleri
- [ ] **AI Destekli Arıza Tespiti**
  - Müşteri şikayetlerinden (doğal dil) arıza tahmini
  - Sesli şikayet analizi (opsiyonel)
  - Arıza olasılık skorları
  - Önerilen parça ve işçilik listesi
  - Tahmini süre ve maliyet tahmini

- [ ] **AI Destekli Fotoğraf Analizi**
  - Hasar fotoğraflarından otomatik hasar tespiti
  - Hasar şiddeti skorlama
  - Gerekli parça ve işçilik önerisi
  - Sigorta hasarı için otomatik rapor oluşturma
  - Fotoğraf kalitesi kontrolü

- [ ] **AI Destekli Fiyat Tahmini**
  - İş emri için otomatik fiyat önerisi
  - Piyasa fiyat analizi
  - Müşteri geçmişine göre özelleştirilmiş fiyat
  - İndirim önerileri (müşteri sadakatine göre)

- [ ] **AI Destekli Bakım Önerileri**
  - Araç km, yaş ve marka/model bazlı bakım önerileri
  - Periyodik bakım hatırlatıcıları
  - Önleyici bakım önerileri
  - Parça ömrü tahmini

- [ ] **AI Destekli Parça Önerisi**
  - Arıza türüne göre parça önerisi
  - Alternatif parça önerileri (Orijinal/Emsal)
  - Uyumluluk kontrolü
  - Fiyat-performans analizi

- [ ] **AI Chatbot (Müşteri Desteği)**
  - 7/24 müşteri desteği
  - İş emri durumu sorgulama
  - Randevu alma
  - Genel sorulara cevap
  - Çoklu dil desteği (TR, EN, AR)

- [ ] **AI Destekli Randevu Optimizasyonu**
  - En uygun randevu saatlerini önerme
  - Personel yüküne göre optimizasyon
  - Müşteri tercihlerine göre öneri
  - Trafik ve mesafe analizi

- [ ] **AI Destekli Stok Tahmini**
  - Gelecekteki parça ihtiyacı tahmini
  - Mevsimsel talep analizi
  - Otomatik sipariş önerileri
  - Stok maliyeti optimizasyonu

- [ ] **AI Destekli Müşteri Analizi**
  - Müşteri segmentasyonu (VIP, Normal, Risk)
  - Müşteri değeri (CLV) tahmini
  - Churn (kayıp) riski analizi
  - Kişiselleştirilmiş kampanya önerileri

- [ ] **AI Destekli Personel Performans Analizi**
  - İş tamamlama süreleri analizi
  - Kalite skorları
  - Verimlilik önerileri
  - Eğitim ihtiyacı tespiti

#### Teknik Gereksinimler
- [ ] OpenAI API veya Azure OpenAI entegrasyonu
- [ ] Computer Vision API (fotoğraf analizi için)
- [ ] NLP (Natural Language Processing) kütüphanesi
- [ ] ML Model eğitimi (opsiyonel - kendi verilerimizle)
- [ ] Vector Database (opsiyonel - semantic search için)

#### API Endpoints
```
POST   /api/ai/diagnose              - Arıza tespiti (text/voice)
POST   /api/ai/analyze-photo         - Fotoğraf analizi
POST   /api/ai/estimate-price        - Fiyat tahmini
GET    /api/ai/maintenance-suggestions/{vehicleId} - Bakım önerileri
POST   /api/ai/suggest-parts         - Parça önerisi
POST   /api/ai/chat                  - Chatbot
GET    /api/ai/optimize-appointments - Randevu optimizasyonu
GET    /api/ai/stock-forecast        - Stok tahmini
GET    /api/ai/customer-analysis/{customerId} - Müşteri analizi
GET    /api/ai/employee-performance/{employeeId} - Personel analizi
```

#### Business Rules
- [ ] AI önerileri "öneri" olarak işaretlenmeli (zorunlu değil)
- [ ] Tüm AI işlemleri loglanmalı (audit)
- [ ] AI sonuçları manuel onay gerektirmeli (kritik işlemler için)
- [ ] AI model versiyonları takip edilmeli
- [ ] Kullanıcı feedback'i toplanmalı (AI öğrenmesi için)

### 5.14 Sigorta ve Kasko Entegrasyonu (Öncelik: ORTA)
**Dosyalar**: `Features/Insurance/`

#### Entity'ler
- [ ] `InsuranceCompany` entity (Sigorta/Kasko Firması)
  - Firma Adı, Kod
  - İletişim Bilgileri (Telefon, Email, Adres)
  - API Endpoint (opsiyonel - entegrasyon için)
  - API Key/Token (opsiyonel)
  - Desteklenen Sigorta Tipleri (Json)
  - Aktif/Pasif Durumu

- [ ] `InsurancePolicy` entity (Sigorta/Kasko Poliçesi)
  - Poliçe No
  - VehicleId, CustomerId
  - InsuranceCompanyId
  - Sigorta Tipi (Kasko, Trafik Sigortası, Muafiyet, vb.)
  - Başlangıç Tarihi, Bitiş Tarihi
  - Prim Tutarı
  - Teminat Tutarı
  - Muafiyet Oranı
  - Durum (Aktif, Süresi Dolmuş, İptal)
  - Poliçe Dosyası (UploadedFile)

- [ ] `InsuranceClaim` entity (Sigorta Hasarı)
  - Hasar Dosya No
  - WorkOrderId (İlgili iş emri)
  - InsurancePolicyId
  - Hasar Tarihi
  - Hasar Açıklaması
  - Hasar Tutarı
  - Onaylanan Tutar
  - Durum (Başvuru, İnceleme, Onaylandı, Reddedildi, Ödendi)
  - Onay Tarihi
  - Ödeme Tarihi
  - Red Nedeni
  - Fotoğraflar (Json - file paths)

#### Features
- [ ] CreateInsuranceCompany (Command)
- [ ] UpdateInsuranceCompany (Command)
- [ ] CreateInsurancePolicy (Command)
- [ ] UpdateInsurancePolicy (Command)
- [ ] RenewInsurancePolicy (Command) - Poliçe yenileme
- [ ] CreateInsuranceClaim (Command) - Hasar başvurusu
- [ ] UpdateInsuranceClaimStatus (Command) - Hasar durumu güncelleme
- [ ] ProcessInsuranceClaim (Command) - Hasar işleme
- [ ] GetAllInsuranceCompanies (Query)
- [ ] GetInsurancePoliciesByVehicle (Query)
- [ ] GetInsurancePoliciesByCustomer (Query)
- [ ] GetInsuranceClaimsByWorkOrder (Query)
- [ ] GetExpiringPolicies (Query) - Süresi yaklaşan poliçeler
- [ ] GetInsuranceClaimById (Query)

#### Business Rules
- [ ] Poliçe süresi dolmadan önce hatırlatma (30, 15, 7 gün önceden)
- [ ] WorkOrder oluştururken aktif sigorta kontrolü
- [ ] Hasar başvurusu yapılırken poliçe geçerlilik kontrolü
- [ ] Sigorta ödemesi yapıldığında WorkOrder'a otomatik kayıt
- [ ] Muafiyet hesaplama (Hasar Tutarı * Muafiyet Oranı)

#### API Endpoints
```
POST   /api/insurance/companies
PUT    /api/insurance/companies/{id}
GET    /api/insurance/companies
GET    /api/insurance/companies/{id}

POST   /api/insurance/policies
PUT    /api/insurance/policies/{id}
POST   /api/insurance/policies/{id}/renew
GET    /api/insurance/policies
GET    /api/insurance/policies/vehicle/{vehicleId}
GET    /api/insurance/policies/customer/{customerId}
GET    /api/insurance/policies/expiring

POST   /api/insurance/claims
PUT    /api/insurance/claims/{id}/status
POST   /api/insurance/claims/{id}/process
GET    /api/insurance/claims
GET    /api/insurance/claims/workorder/{workOrderId}
GET    /api/insurance/claims/{id}
```

#### WorkOrder Entegrasyonu
- [ ] WorkOrder oluştururken sigorta bilgisi ekleme
- [ ] WorkOrder'a sigorta hasarı bağlama
- [ ] Sigorta onayı sonrası WorkOrder durumu güncelleme
- [ ] Sigorta ödemesi takibi

---

## 📐 Teknik Mimari Güncellemeleri

### Database Schema Updates
- [ ] Yeni tablolar için migration'lar
- [ ] İndeksler ve performans optimizasyonu
- [ ] View'lar (raporlar için)
- [ ] Stored procedures (complex queries)

### API Enhancements
- [ ] Swagger documentation güncellemeleri
- [ ] API versioning (v1, v2)
- [ ] Rate limiting
- [ ] Response caching

### Security
- [ ] Fine-grained permissions
- [ ] Audit logging (tüm işlemler loglanacak)
- [ ] Data encryption (hassas bilgiler)
- [ ] GDPR compliance

### Performance
- [ ] Database query optimization
- [ ] Redis caching (yoğun sorgular için)
- [ ] Image optimization
- [ ] Background jobs (Hangfire)

### Testing
- [ ] Unit tests (her feature için)
- [ ] Integration tests
- [ ] E2E tests
- [ ] Load testing

---

## 📦 Seed Data Eklemeleri

### Pozisyonlar
```csharp
- Genel Müdür
- Servis Müdürü
- Ön Büro / Danışman
- Usta Başı
- Mekanik
- Elektrikçi
- Kaporta Ustası
- Boyacı
- Yıkamacı
```

### Parça Kategorileri
```csharp
- Motor Parçaları
- Fren Sistemi
- Süspansiyon
- Elektrik/Elektronik
- Kaporta
- İç Aksesuar
- Dış Aksesuar
- Filtreler
- Yağlar
```

### İş Emri Durumları
```csharp
- Randevu Alındı
- Araç Girişi Yapıldı
- Arıza Tespit Edildi
- Parça Bekliyor
- İşlem Başladı
- İşlem Devam Ediyor
- Kalite Kontrol
- Yıkama
- Teslime Hazır
- Teslim Edildi
```

### Dinamik Hata Mesajları (Ek)
```csharp
PART_OUT_OF_STOCK: "tr" => "Parça stokta yok: {PartName}"
WORKORDER_ALREADY_COMPLETED: "tr" => "İş emri zaten tamamlanmış"
INSUFFICIENT_PERMISSION: "tr" => "Bu işlem için yetkiniz yok"
EMPLOYEE_ALREADY_ASSIGNED: "tr" => "Personel zaten başka bir işe atanmış"
INSURANCE_POLICY_EXPIRED: "tr" => "Sigorta poliçesi süresi dolmuş"
INSURANCE_CLAIM_ALREADY_EXISTS: "tr" => "Bu iş emri için zaten sigorta hasarı kaydı var"
AI_DIAGNOSIS_FAILED: "tr" => "AI arıza tespiti başarısız oldu, lütfen manuel kontrol yapın"
AI_PHOTO_ANALYSIS_FAILED: "tr" => "Fotoğraf analizi başarısız oldu"
AI_ESTIMATE_UNAVAILABLE: "tr" => "Fiyat tahmini şu anda kullanılamıyor"
```

### Sigorta Firmaları (Seed Data)
```csharp
- Allianz Sigorta
- Anadolu Sigorta
- Axa Sigorta
- Groupama Sigorta
- HDI Sigorta
- Mapfre Sigorta
- Neova Sigorta
- Ray Sigorta
- Unico Sigorta
- Ziraat Sigorta
```

---

## 🎨 Frontend Önerileri (İleride)

### Tech Stack
- **React** veya **Angular** (SPA)
- **TailwindCSS** veya **Material UI**
- **Chart.js** / **D3.js** (grafikler için)
- **React Query** (data fetching)
- **Zustand** veya **Redux** (state management)

### Sayfalar
1. Dashboard (İşletme)
2. İş Emri Yönetimi
3. Personel Yönetimi
4. Stok Yönetimi
5. Muhasebe
6. Raporlar
7. Ayarlar
8. Müşteri Portalı (ayrı subdomain)

---

## 📅 Öncelik Sıralaması

### Şimdi Yapılacaklar (Sprint 1)
1. ✅ Personel Yönetimi (1.1)
2. ✅ Parça Stok Yönetimi (1.2)
3. ✅ Gelişmiş İş Emri Sistemi (1.3)

### Sonra Yapılacaklar (Sprint 2)
4. ✅ Muhasebe ve Mali Yönetim (2.1, 2.2)
5. ✅ Çoklu Servis Teklif Sistemi (3.1) - İnovatif, müşteri çekici özellik
6. ✅ AI Destekli Özellikler (5.6) - İnovatif ve rekabet avantajı sağlar
7. ✅ Sigorta ve Kasko Entegrasyonu (5.7)

### Daha Sonra (Sprint 3)
12. ✅ Müşteri Paneli (3.2)
13. ✅ Müşteri Değerlendirme Sistemi (5.6) - Rating ve Review
14. ✅ QR Code ve Barcode Sistemi (5.7) - Hızlı erişim ve takip
15. ✅ Export/Import Sistemi (5.9) - Excel, PDF, CSV
16. ✅ Raporlama (4.1)

### İleride (Sprint 4+)
7. ✅ İleri Özellikler (5.x)

---

## 📚 Dokümantasyon

- [x] ROADMAP.md (bu dosya)
- [ ] API_DOCUMENTATION.md
- [ ] DEPLOYMENT_GUIDE.md
- [ ] USER_MANUAL.md
- [ ] DEVELOPER_GUIDE.md

---

## 🚀 Başlangıç

Şimdi **FAZ 1.1 - Personel Yönetimi** ile başlayabiliriz!

**Komut**:
```bash
# Entity oluştur
# Features/Employees klasörünü oluştur
# Commands ve Queries ekle
```

Hazır mısınız? 🎯

