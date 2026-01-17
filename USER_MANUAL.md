# MagicCarRepairAISupported - Kullanıcı Kılavuzu

> **Versiyon**: 1.0  
> **Son Güncelleme**: 2024

---

## 📖 İçindekiler

1. [Giriş ve Genel Bakış](#giriş-ve-genel-bakış)
2. [Hızlı Başlangıç](#hızlı-başlangıç)
3. [Modül Bazlı Kullanım Kılavuzu](#modül-bazlı-kullanım-kılavuzu)
4. [API Endpoint Referansı](#api-endpoint-referansı)
5. [Yaygın Senaryolar](#yaygın-senaryolar)
6. [Çoklu Dil Desteği](#çoklu-dil-desteği)
7. [Güvenlik ve Yetkilendirme](#güvenlik-ve-yetkilendirme)

---

## 🎯 Giriş ve Genel Bakış

### Projenin Amacı

**MagicCarRepairAISupported**, oto servis işletmeleri için kapsamlı bir yönetim sistemidir. Sistem şu ana özelliklere sahiptir:

- **Multi-Tenant Architecture**: Her oto servis izole bir ortamda çalışır
- **İş Emri Yönetimi**: Araç girişinden teslimata kadar tüm süreç
- **Stok Yönetimi**: Parça stok takibi ve otomatik alarmlar
- **Muhasebe**: Gelir-gider takibi ve faturalama
- **Sigorta Yönetimi**: Poliçe ve hasar takibi
- **Müşteri Değerlendirmeleri**: Rating ve review sistemi
- **AI Destekli Özellikler**: Arıza tespiti, fotoğraf analizi, fiyat tahmini
- **Teklif Sistemi**: Çoklu servis teklif verme platformu
- **Müşteri Portalı**: Müşterilerin kendi işlerini takip edebilmesi

### Temel Özellikler

1. **İşletme Yönetimi**
   - Personel yönetimi
   - Müşteri yönetimi
   - Araç yönetimi

2. **İş Emri Yönetimi**
   - İş emri oluşturma ve takibi
   - Parça ve işçilik ekleme
   - Fotoğraf yükleme
   - Timeline takibi

3. **Stok Yönetimi**
   - Parça yönetimi
   - Stok hareketleri
   - Kritik stok uyarıları
   - Otomatik sipariş

4. **Muhasebe ve Faturalama**
   - Gelir-gider kayıtları
   - Fatura oluşturma
   - Ödeme yönetimi
   - Raporlar

5. **Sigorta Yönetimi**
   - Sigorta firması yönetimi
   - Poliçe yönetimi
   - Hasar başvuruları

6. **Değerlendirme Sistemi**
   - Müşteri değerlendirmeleri
   - Servis yanıtları
   - Moderation

### Sistem Gereksinimleri

- **.NET 9.0** veya üzeri
- **SQL Server** (LocalDB veya Full SQL Server)
- **Redis** (Opsiyonel, cache için)
- **OpenAI API Key** (AI özellikleri için, opsiyonel)

---

## 🚀 Hızlı Başlangıç

### 1. İlk Kurulum

Detaylı kurulum için `SETUP_GUIDE.md` dosyasına bakın.

### 2. İlk Client (Tenant) Oluşturma

Sisteme ilk oto servisi eklemek için:

```http
POST /api/clients
Content-Type: application/json
Authorization: Bearer {token}

{
  "name": "ABC Oto Servis",
  "code": "ABC001",
  "description": "Kadıköy şubesi",
  "contactEmail": "info@abcotoservis.com",
  "contactPhone": "+90 555 123 4567",
  "subscriptionStartDate": "2024-01-01",
  "subscriptionEndDate": "2025-01-01"
}
```

**Yanıt:**
```json
{
  "success": true,
  "data": {
    "id": 1,
    "name": "ABC Oto Servis",
    "code": "ABC001",
    "isActive": true
  }
}
```

### 3. İlk Kullanıcı Kaydı

```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "admin@abcotoservis.com",
  "password": "SecurePassword123!",
  "firstName": "Ahmet",
  "lastName": "Yılmaz",
  "clientId": 1,
  "userType": "Manager"
}
```

### 4. Giriş Yapma

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@abcotoservis.com",
  "password": "SecurePassword123!"
}
```

**Yanıt:**
```json
{
  "success": true,
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "expiration": "2024-01-02T10:00:00Z",
    "user": {
      "id": 1,
      "email": "admin@abcotoservis.com",
      "firstName": "Ahmet",
      "lastName": "Yılmaz"
    }
  }
}
```

**Not**: Sonraki tüm isteklerde `Authorization: Bearer {token}` header'ını kullanın.

### 5. İlk Müşteri Ekleme

```http
POST /api/customers
Content-Type: application/json
Authorization: Bearer {token}
X-Client-Id: 1

{
  "firstName": "Mehmet",
  "lastName": "Demir",
  "email": "mehmet@example.com",
  "phone": "+90 555 987 6543",
  "address": "İstanbul, Kadıköy"
}
```

### 6. İlk Araç Ekleme

```http
POST /api/vehicles
Content-Type: application/json
Authorization: Bearer {token}
X-Client-Id: 1

{
  "customerId": 1,
  "licensePlate": "34ABC123",
  "brand": "Toyota",
  "model": "Corolla",
  "year": 2020,
  "color": "Beyaz",
  "kilometers": 50000
}
```

### 7. İlk İş Emri Oluşturma

```http
POST /api/workorders
Content-Type: application/json
Authorization: Bearer {token}
X-Client-Id: 1

{
  "vehicleId": 1,
  "customerId": 1,
  "assignedEmployeeId": 1,
  "priority": "Normal",
  "description": "Motor arızası",
  "customerComplaints": "Araç çalışmıyor"
}
```

---

## 📚 Modül Bazlı Kullanım Kılavuzu

### 1. İşletme Yönetimi

#### 1.1 Personel Yönetimi

**Personel Ekleme:**
```http
POST /api/employees
Authorization: Bearer {token}
X-Client-Id: 1

{
  "employeeNo": "EMP001",
  "firstName": "Ali",
  "lastName": "Veli",
  "position": "Mechanic",
  "phone": "+90 555 111 2233",
  "email": "ali@abcotoservis.com",
  "hireDate": "2024-01-01",
  "employmentStatus": "Active"
}
```

**Personel Listeleme:**
```http
GET /api/employees?position=Mechanic&pageNumber=1&pageSize=10
Authorization: Bearer {token}
X-Client-Id: 1
```

**Personel Güncelleme:**
```http
PUT /api/employees/{id}
Authorization: Bearer {token}
X-Client-Id: 1

{
  "firstName": "Ali",
  "lastName": "Veli",
  "position": "SeniorMechanic",
  "phone": "+90 555 111 2233"
}
```

**Personel Silme (Soft Delete):**
```http
DELETE /api/employees/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### 1.2 Müşteri Yönetimi

**Müşteri Ekleme:**
```http
POST /api/customers
Authorization: Bearer {token}
X-Client-Id: 1

{
  "firstName": "Ayşe",
  "lastName": "Kaya",
  "email": "ayse@example.com",
  "phone": "+90 555 222 3344",
  "address": "İstanbul, Üsküdar"
}
```

**Müşteri Listeleme:**
```http
GET /api/customers?searchTerm=ayse&pageNumber=1&pageSize=10
Authorization: Bearer {token}
X-Client-Id: 1
```

**Müşteri Detayı:**
```http
GET /api/customers/{id}
Authorization: Bearer {token}
X-Client-Id: 1
```

#### 1.3 Araç Yönetimi

**Araç Ekleme:**
```http
POST /api/vehicles
Authorization: Bearer {token}
X-Client-Id: 1

{
  "customerId": 1,
  "licensePlate": "34XYZ789",
  "brand": "BMW",
  "model": "520i",
  "year": 2019,
  "color": "Siyah",
  "kilometers": 75000
}
```

**Müşteri Araçlarını Listeleme:**
```http
GET /api/vehicles?customerId=1
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### 2. İş Emri Yönetimi

#### 2.1 İş Emri Oluşturma

```http
POST /api/workorders
Authorization: Bearer {token}
X-Client-Id: 1

{
  "vehicleId": 1,
  "customerId": 1,
  "assignedEmployeeId": 1,
  "priority": "Normal",
  "description": "Fren balata değişimi",
  "customerComplaints": "Frenler tutmuyor",
  "estimatedCompletionDate": "2024-01-15T17:00:00Z"
}
```

**Yanıt:**
```json
{
  "success": true,
  "data": {
    "id": 1,
    "workOrderNumber": "WO-2024-001",
    "status": "VehicleEntered",
    "priority": "Normal",
    "entryDate": "2024-01-10T10:00:00Z"
  }
}
```

#### 2.2 Parça Ekleme

```http
POST /api/workorders/{id}/items
Authorization: Bearer {token}
X-Client-Id: 1

{
  "itemType": "Part",
  "partId": 5,
  "quantity": 2,
  "unitPrice": 250.00,
  "discount": 0,
  "description": "Fren balata seti"
}
```

#### 2.3 İşçilik Ekleme

```http
POST /api/workorders/{id}/labors
Authorization: Bearer {token}
X-Client-Id: 1

{
  "employeeId": 1,
  "workDescription": "Fren balata değişimi",
  "startTime": "2024-01-10T14:00:00Z",
  "endTime": "2024-01-10T16:00:00Z",
  "hourlyRate": 150.00
}
```

#### 2.4 Fotoğraf Ekleme

```http
POST /api/workorders/{id}/photos
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: multipart/form-data

{
  "file": [binary data],
  "description": "Fren balata aşınmış",
  "photoType": "Process"
}
```

#### 2.5 Durum Güncelleme

```http
PUT /api/workorders/{id}/status
Authorization: Bearer {token}
X-Client-Id: 1

{
  "status": "InProgress",
  "notes": "İşlem başladı"
}
```

**Mevcut Durumlar:**
- `VehicleEntered` - Araç girişi yapıldı
- `InProgress` - İşlem başladı
- `WaitingForParts` - Parça bekleniyor
- `QualityControl` - Kalite kontrol
- `Completed` - Tamamlandı
- `Delivered` - Teslim edildi
- `Cancelled` - İptal edildi

#### 2.6 İş Emrini Tamamlama

```http
POST /api/workorders/{id}/complete
Authorization: Bearer {token}
X-Client-Id: 1

{
  "completionNotes": "Tüm işlemler tamamlandı",
  "actualCompletionDate": "2024-01-12T16:00:00Z"
}
```

#### 2.7 İş Emrini Teslim Etme

```http
POST /api/workorders/{id}/deliver
Authorization: Bearer {token}
X-Client-Id: 1

{
  "deliveryNotes": "Müşteriye teslim edildi",
  "deliveryDate": "2024-01-12T17:00:00Z"
}
```

#### 2.8 İş Emri Listeleme

```http
GET /api/workorders?status=InProgress&customerId=1&pageNumber=1&pageSize=10
Authorization: Bearer {token}
X-Client-Id: 1
```

#### 2.9 İş Emri Timeline

```http
GET /api/workorders/{id}/timeline
Authorization: Bearer {token}
X-Client-Id: 1
```

**Yanıt:**
```json
{
  "success": true,
  "data": [
    {
      "id": 1,
      "date": "2024-01-10T10:00:00Z",
      "status": "VehicleEntered",
      "description": "Araç girişi yapıldı",
      "employeeName": "Ali Veli"
    },
    {
      "id": 2,
      "date": "2024-01-10T14:00:00Z",
      "status": "InProgress",
      "description": "İşlem başladı",
      "employeeName": "Ali Veli"
    }
  ]
}
```

---

### 3. Stok Yönetimi

#### 3.1 Parça Ekleme

```http
POST /api/parts
Authorization: Bearer {token}
X-Client-Id: 1

{
  "partCode": "FRB-001",
  "name": "Fren Balata Seti",
  "category": "Brake",
  "brandType": "Original",
  "purchasePrice": 200.00,
  "salePrice": 250.00,
  "taxRate": 20,
  "minimumStockLevel": 10,
  "barcode": "1234567890123"
}
```

#### 3.2 Parça Listeleme

```http
GET /api/parts?category=Brake&searchTerm=fren&pageNumber=1&pageSize=10
Authorization: Bearer {token}
X-Client-Id: 1
```

#### 3.3 Stok Hareketi Kaydetme

```http
POST /api/stock-movements
Authorization: Bearer {token}
X-Client-Id: 1

{
  "partId": 5,
  "movementType": "In",
  "quantity": 20,
  "notes": "Tedarikçiden gelen sipariş"
}
```

**Hareket Tipleri:**
- `In` - Stok girişi
- `Out` - Stok çıkışı
- `Adjustment` - Stok düzeltmesi
- `Return` - İade

#### 3.4 Stok Alarmları

```http
GET /api/stock-alerts/active
Authorization: Bearer {token}
X-Client-Id: 1
```

**Yanıt:**
```json
{
  "success": true,
  "data": [
    {
      "partId": 5,
      "partName": "Fren Balata Seti",
      "currentStock": 5,
      "minimumStockLevel": 10,
      "alertLevel": "Critical"
    }
  ]
}
```

#### 3.5 Otomatik Sipariş Oluşturma

```http
POST /api/auto-orders
Authorization: Bearer {token}
X-Client-Id: 1

{
  "partId": 5,
  "supplierId": 1,
  "quantity": 20,
  "notes": "Kritik stok seviyesi"
}
```

---

### 4. Muhasebe ve Faturalama

#### 4.1 Gelir Kaydı

```http
POST /api/incomes
Authorization: Bearer {token}
X-Client-Id: 1

{
  "workOrderId": 1,
  "incomeType": "WorkOrder",
  "amount": 5000.00,
  "paymentMethod": "CreditCard",
  "date": "2024-01-12",
  "description": "İş emri geliri",
  "invoiceNumber": "INV-2024-001"
}
```

#### 4.2 Gider Kaydı

```http
POST /api/expenses
Authorization: Bearer {token}
X-Client-Id: 1

{
  "expenseType": "PartPurchase",
  "amount": 2000.00,
  "paymentMethod": "BankTransfer",
  "date": "2024-01-10",
  "supplierId": 1,
  "description": "Parça alımı",
  "invoiceNumber": "SUP-2024-001"
}
```

#### 4.3 Fatura Oluşturma

```http
POST /api/invoices
Authorization: Bearer {token}
X-Client-Id: 1

{
  "invoiceType": "Sales",
  "workOrderId": 1,
  "customerId": 1,
  "invoiceDate": "2024-01-12",
  "dueDate": "2024-02-12",
  "description": "İş emri faturası",
  "items": [
    {
      "description": "Fren balata değişimi",
      "quantity": 1,
      "unitPrice": 250.00,
      "taxRate": 20
    },
    {
      "description": "İşçilik",
      "quantity": 2,
      "unitPrice": 150.00,
      "taxRate": 20
    }
  ]
}
```

#### 4.4 İş Emrinden Fatura Oluşturma

```http
POST /api/invoices/from-workorder/{workOrderId}
Authorization: Bearer {token}
X-Client-Id: 1

{
  "invoiceDate": "2024-01-12",
  "dueDate": "2024-02-12"
}
```

#### 4.5 Fatura PDF İndirme

```http
GET /api/invoices/{id}/pdf
Authorization: Bearer {token}
X-Client-Id: 1
```

#### 4.6 Fatura QR Kodu

```http
GET /api/invoices/{id}/qrcode?size=300
Authorization: Bearer {token}
X-Client-Id: 1
```

#### 4.7 Fatura Email Gönderme

```http
POST /api/invoices/{id}/send-email
Authorization: Bearer {token}
X-Client-Id: 1

{
  "email": "customer@example.com",
  "subject": "Faturanız",
  "message": "Faturanız ektedir."
}
```

---

### 5. Sigorta Yönetimi

#### 5.1 Sigorta Firması Ekleme

```http
POST /api/insurance/companies
Authorization: Bearer {token}
X-Client-Id: 1

{
  "companyName": "Allianz Sigorta",
  "companyCode": "ALL001",
  "contactPhone": "+90 212 123 4567",
  "contactEmail": "info@allianz.com",
  "address": "İstanbul",
  "isActive": true
}
```

#### 5.2 Poliçe Oluşturma

```http
POST /api/insurance/policies
Authorization: Bearer {token}
X-Client-Id: 1

{
  "policyNumber": "POL-2024-001",
  "vehicleId": 1,
  "customerId": 1,
  "insuranceCompanyId": 1,
  "insuranceType": "Comprehensive",
  "startDate": "2024-01-01",
  "endDate": "2025-01-01",
  "premiumAmount": 10000.00,
  "coverageAmount": 200000.00,
  "deductiblePercentage": 10
}
```

#### 5.3 Poliçe Yenileme

```http
POST /api/insurance/policies/{id}/renew
Authorization: Bearer {token}
X-Client-Id: 1

{
  "newEndDate": "2026-01-01",
  "premiumAmount": 11000.00
}
```

#### 5.4 Süresi Yaklaşan Poliçeler

```http
GET /api/insurance/policies/expiring?daysBeforeExpiration=30
Authorization: Bearer {token}
X-Client-Id: 1
```

#### 5.5 Hasar Başvurusu

```http
POST /api/insurance/claims
Authorization: Bearer {token}
X-Client-Id: 1

{
  "claimNumber": "CLM-2024-001",
  "insurancePolicyId": 1,
  "workOrderId": 1,
  "incidentDate": "2024-01-10",
  "description": "Kaza hasarı",
  "claimAmount": 50000.00,
  "photos": "[]"
}
```

#### 5.6 Hasar Durumu Güncelleme

```http
PUT /api/insurance/claims/{id}/status
Authorization: Bearer {token}
X-Client-Id: 1

{
  "status": "Approved",
  "approvedAmount": 45000.00,
  "notes": "Hasar onaylandı"
}
```

---

### 6. Değerlendirme Sistemi

#### 6.1 Değerlendirme Oluşturma

```http
POST /api/ratings
Authorization: Bearer {token}
X-Client-Id: 1

{
  "workOrderId": 1,
  "rating": 5,
  "serviceQuality": 5,
  "priceValue": 4,
  "onTimeDelivery": 5,
  "staffBehavior": 5,
  "comment": "Mükemmel hizmet!",
  "photos": "[]"
}
```

**Not**: Sadece `Delivered` durumundaki iş emirleri için değerlendirme yapılabilir.

#### 6.2 Değerlendirmeye Yanıt Verme

```http
POST /api/ratings/{id}/reply
Authorization: Bearer {token}
X-Client-Id: 1

{
  "reply": "Teşekkür ederiz! Memnuniyetiniz bizim için önemli."
}
```

#### 6.3 Değerlendirme Moderation

```http
PUT /api/ratings/{id}/moderate
Authorization: Bearer {token}
X-Client-Id: 1

{
  "status": "Approved",
  "rejectionReason": null
}
```

**Durumlar:**
- `Pending` - Onay bekliyor
- `Approved` - Onaylandı
- `Rejected` - Reddedildi

#### 6.4 Servis Değerlendirmeleri

```http
GET /api/ratings?clientId=1&pageNumber=1&pageSize=10
Authorization: Bearer {token}
X-Client-Id: 1
```

#### 6.5 Ortalama Puan

```http
GET /api/ratings/average
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### 7. Teklif Sistemi

#### 7.1 Teklif Talebi Oluşturma

```http
POST /api/quote-requests
Authorization: Bearer {token}

{
  "customerId": 1,
  "vehicleId": 1,
  "problemDescription": "Motor çalışmıyor",
  "damageType": "Breakdown",
  "urgencyLevel": "High",
  "desiredDateRange": "2024-01-15",
  "contactEmail": "customer@example.com",
  "contactPhone": "+90 555 123 4567"
}
```

#### 7.2 Açık Teklif Talepleri

```http
GET /api/quote-requests/open?pageNumber=1&pageSize=10
Authorization: Bearer {token}
X-Client-Id: 1
```

#### 7.3 Teklif Verme

```http
POST /api/quote-responses
Authorization: Bearer {token}
X-Client-Id: 1

{
  "quoteRequestId": 1,
  "description": "Motor arızası tespiti ve onarım",
  "estimatedDays": 3,
  "quoteAmount": 5000.00,
  "discountPercentage": 10,
  "warrantyMonths": 12,
  "notes": "Orijinal yedek parça kullanılacak"
}
```

#### 7.4 Teklif Kabul Etme

```http
POST /api/quote-requests/{id}/accept/{responseId}
Authorization: Bearer {token}
```

**Not**: Teklif kabul edildiğinde otomatik olarak iş emri oluşturulur.

---

### 8. Müşteri Portalı

#### 8.1 Kendi Araçlarım

```http
GET /api/customer-portal/vehicles
Authorization: Bearer {token}
```

#### 8.2 Kendi İş Emirlerim

```http
GET /api/customer-portal/work-orders?activeOnly=true
Authorization: Bearer {token}
```

#### 8.3 İş Emri Detayı

```http
GET /api/customer-portal/work-orders/{id}
Authorization: Bearer {token}
```

#### 8.4 Bakım Geçmişi

```http
GET /api/customer-portal/maintenance-history?vehicleId=1
Authorization: Bearer {token}
```

---

### 9. Dashboard ve Raporlar

#### 9.1 Dashboard İstatistikleri

```http
GET /api/dashboard/stats
Authorization: Bearer {token}
X-Client-Id: 1
```

**Yanıt:**
```json
{
  "success": true,
  "data": {
    "todayWorkOrders": 5,
    "pendingWorkOrders": 3,
    "dailyRevenue": 25000.00,
    "criticalStockItems": 2,
    "overdueInvoices": 1
  }
}
```

#### 9.2 Gelir-Gider Grafiği

```http
GET /api/dashboard/income-expense-chart?startDate=2024-01-01&endDate=2024-01-31
Authorization: Bearer {token}
X-Client-Id: 1
```

#### 9.3 İş Emri Durum Grafiği

```http
GET /api/dashboard/workorder-status-chart
Authorization: Bearer {token}
X-Client-Id: 1
```

#### 9.4 Son Aktiviteler

```http
GET /api/dashboard/recent-activities?limit=10
Authorization: Bearer {token}
X-Client-Id: 1
```

#### 9.5 En Çok Harcama Yapan Müşteriler

```http
GET /api/dashboard/top-customers?limit=10
Authorization: Bearer {token}
X-Client-Id: 1
```

---

### 10. AI Özellikleri

#### 10.1 Arıza Tespiti

```http
POST /api/ai/diagnose
Authorization: Bearer {token}
X-Client-Id: 1

{
  "description": "Araç çalışırken motor sesi geliyor, vızıltı gibi",
  "vehicleInfo": {
    "brand": "Toyota",
    "model": "Corolla",
    "year": 2020
  }
}
```

**Yanıt:**
```json
{
  "success": true,
  "data": {
    "diagnosis": "Alternatör problemi olabilir",
    "confidence": 0.85,
    "suggestedParts": [
      {
        "partName": "Alternatör",
        "probability": 0.85
      }
    ],
    "estimatedCost": 2000.00,
    "estimatedTime": "2-3 saat"
  }
}
```

#### 10.2 Fotoğraf Analizi

```http
POST /api/ai/analyze-photo
Authorization: Bearer {token}
X-Client-Id: 1
Content-Type: multipart/form-data

{
  "file": [binary data],
  "analysisType": "Damage"
}
```

#### 10.3 Fiyat Tahmini

```http
POST /api/ai/estimate-price
Authorization: Bearer {token}
X-Client-Id: 1

{
  "workOrderId": 1,
  "description": "Fren balata değişimi"
}
```

#### 10.4 Bakım Önerileri

```http
GET /api/ai/maintenance-suggestions/{vehicleId}
Authorization: Bearer {token}
X-Client-Id: 1
```

---

## 🔌 API Endpoint Referansı

### Base URL
```
http://localhost:5000/api
```

### Authentication

Tüm endpoint'ler (login ve register hariç) `Authorization` header'ı gerektirir:

```
Authorization: Bearer {token}
```

### Multi-Tenant

Her istekte `X-Client-Id` header'ı gönderilmelidir:

```
X-Client-Id: 1
```

**Alternatif**: JWT token içinde `ClientId` claim'i varsa header'a gerek yoktur.

### Dil Desteği

Hata mesajları için dil belirtmek:

```
Accept-Language: tr
```

**Desteklenen Diller:**
- `tr` - Türkçe (varsayılan)
- `en` - İngilizce
- `ar` - Arapça

### Hata Yanıt Formatı

```json
{
  "success": false,
  "errorCode": "VEHICLE_NOT_FOUND",
  "message": "Araç bulunamadı",
  "details": {
    "VehicleId": 999
  }
}
```

---

## 📋 Yaygın Senaryolar

### Senaryo 1: Yeni Araç Girişi ve İş Emri Oluşturma

1. **Müşteri Kontrolü**
   ```http
   GET /api/customers?searchTerm=mehmet
   ```
   Müşteri yoksa oluştur.

2. **Araç Kontrolü**
   ```http
   GET /api/vehicles?customerId=1
   ```
   Araç yoksa oluştur.

3. **İş Emri Oluşturma**
   ```http
   POST /api/workorders
   {
     "vehicleId": 1,
     "customerId": 1,
     "description": "Periyodik bakım"
   }
   ```

4. **Fotoğraf Yükleme**
   ```http
   POST /api/workorders/{id}/photos
   ```

5. **Parça ve İşçilik Ekleme**
   ```http
   POST /api/workorders/{id}/items
   POST /api/workorders/{id}/labors
   ```

6. **Durum Güncelleme**
   ```http
   PUT /api/workorders/{id}/status
   {
     "status": "InProgress"
   }
   ```

### Senaryo 2: İş Emri Tamamlama ve Fatura Oluşturma

1. **İş Emrini Tamamlama**
   ```http
   POST /api/workorders/{id}/complete
   ```

2. **Fatura Oluşturma**
   ```http
   POST /api/invoices/from-workorder/{workOrderId}
   ```

3. **Fatura PDF İndirme**
   ```http
   GET /api/invoices/{id}/pdf
   ```

4. **Fatura Email Gönderme**
   ```http
   POST /api/invoices/{id}/send-email
   ```

5. **Ödeme Alma**
   ```http
   POST /api/payments/initialize
   {
     "invoiceId": 1,
     "amount": 5000.00
   }
   ```

6. **İş Emrini Teslim Etme**
   ```http
   POST /api/workorders/{id}/deliver
   ```

### Senaryo 3: Stok Alarmı ve Sipariş

1. **Stok Alarmlarını Kontrol Etme**
   ```http
   GET /api/stock-alerts/active
   ```

2. **Otomatik Sipariş Oluşturma**
   ```http
   POST /api/auto-orders
   {
     "partId": 5,
     "supplierId": 1,
     "quantity": 20
   }
   ```

3. **Sipariş Onaylama**
   ```http
   POST /api/auto-orders/{id}/approve
   ```

4. **Stok Girişi**
   ```http
   POST /api/stock-movements
   {
     "partId": 5,
     "movementType": "In",
     "quantity": 20
   }
   ```

---

## 🌍 Çoklu Dil Desteği

### Dil Değiştirme

API isteklerinde `Accept-Language` header'ı kullanın:

```http
GET /api/workorders
Accept-Language: en
Authorization: Bearer {token}
```

### Hata Mesajları

Hata mesajları otomatik olarak seçilen dile göre döner:

**Türkçe:**
```json
{
  "errorCode": "VEHICLE_NOT_FOUND",
  "message": "Araç bulunamadı"
}
```

**İngilizce:**
```json
{
  "errorCode": "VEHICLE_NOT_FOUND",
  "message": "Vehicle not found"
}
```

**Arapça:**
```json
{
  "errorCode": "VEHICLE_NOT_FOUND",
  "message": "المركبة غير موجودة"
}
```

---

## 🔐 Güvenlik ve Yetkilendirme

### Multi-Tenant Yapı

Her oto servis (Client) izole bir veri ortamında çalışır. Bir servisin verilerine başka bir servis erişemez.

### Role ve Permission Sistemi

Sistemde şu roller tanımlıdır:
- **Admin** - Tam yetki
- **Manager** - Üst düzey işlemler
- **Employee** - Sınırlı yetkiler
- **Customer** - Sadece kendi verilerine erişim

### Veri İzolasyonu

Tüm entity'ler `ClientId` ile filtrelenir. Global query filter'lar otomatik olarak `WHERE ClientId = X` ekler.

### JWT Token

Token içinde şu bilgiler bulunur:
- User ID
- Client ID
- Email
- Roles
- Permissions

---

## 📞 Destek

Sorularınız için:
- Dokümantasyon: `README.md`, `ROADMAP.md`
- API Dokümantasyonu: `API_DOCUMENTATION.md`
- Kurulum: `SETUP_GUIDE.md`
- Eksiklikler: `GAPS_AND_ROADMAP.md`

---

**Son Güncelleme**: 2024
