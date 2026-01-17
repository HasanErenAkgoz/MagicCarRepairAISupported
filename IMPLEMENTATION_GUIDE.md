# Multi-Tenant ve Dinamik Hata Mesajları - Uygulama Rehberi

## 🎯 Özet
Bu proje (MagicCarRepairAISupported) artık **Multi-Tenant (Çok Kiracılı)** mimari ve **Dinamik Çoklu Dil Hata Mesajları** desteğine sahiptir.

### Önemli Özellikler:
1. **Multi-Tenant Yapı**: Her oto servis ayrı bir "Client" (Kiracı)
2. **Veri İzolasyonu**: Her servis sadece kendi verilerini görebilir
3. **Dinamik Çeviriler**: Hata mesajları veritabanından gelir (TR, EN, AR)
4. **Otomatik Filtreleme**: Tüm sorgular otomatik olarak ClientId'ye göre filtrelenir

---

## 📊 Veritabanı Yapısı

### Client (Tenant) Tablosu
Her oto servisi temsil eder:
```sql
- Id (Primary Key)
- Name (Servis Adı)
- Code (Benzersiz Kod - örn: "SERVIS001")
- ContactEmail, ContactPhone
- IsActive
- SubscriptionStartDate, SubscriptionEndDate
```

### ErrorMessage Tablosu
Çoklu dil desteği için:
```sql
- Id (Primary Key)
- ErrorCode (örn: "VEHICLE_KM_NEGATIVE")
- Language (tr, en, ar)
- Message (Çevrilmiş mesaj)
- Unique Index: (ErrorCode, Language)
```

### Multi-Tenant Entity'ler
Aşağıdaki entity'ler ClientId içerir:
- **User** (Kullanıcılar)
- **Customer** (Müşteriler)
- **Vehicle** (Araçlar)

---

## 🔧 Migration Oluşturma

```powershell
# Migration ekle
dotnet ef migrations add AddMultiTenantAndErrorMessages --project MagicCarRepairAISupported.Persistence --startup-project MagicCarRepairAISupported.WebAPI

# Veritabanını güncelle
dotnet ef database update --project MagicCarRepairAISupported.Persistence --startup-project MagicCarRepairAISupported.WebAPI
```

---

## 🚀 Kullanım Örnekleri

### 1. Multi-Tenant Kullanımı

#### HTTP Header ile Client Belirtme
```http
GET /api/customers
Headers:
  X-Client-Id: 1
  Accept-Language: tr
```

#### JWT Token ile (Önerilen)
Token'a ClientId claim'i ekleyin:
```csharp
var claims = new List<Claim>
{
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    new Claim("ClientId", user.ClientId.ToString()),
    new Claim("Language", user.Language)
};
```

### 2. Yeni Client (Servis) Oluşturma

```http
POST /api/clients
Content-Type: application/json
{
  "name": "ABC Oto Servis",
  "code": "ABC001",
  "description": "Kadıköy şubesi",
  "contactEmail": "info@abcotoservis.com",
  "contactPhone": "+90 555 123 4567",
  "subscriptionStartDate": "2025-01-01",
  "subscriptionEndDate": "2026-01-01"
}
```

### 3. Dinamik Hata Mesajları

#### Kod İçinde Kullanım
```csharp
// Service/Handler içinde
public class VehicleService
{
    private readonly IErrorMessageService _errorMessageService;
    
    public async Task UpdateKilometers(int vehicleId, long newKm)
    {
        if (newKm < 0)
        {
            // Veritabanından dile göre mesaj getir
            var message = await _errorMessageService.GetMessageAsync(
                "VEHICLE_KM_NEGATIVE",
                parameters: new { 
                    LicensePlate = "34ABC123", 
                    NewKilometers = newKm 
                }
            );
            
            throw new DomainException("VEHICLE_KM_NEGATIVE", 
                new { LicensePlate = "34ABC123", NewKilometers = newKm });
        }
    }
}
```

#### Domain Exception ile
```csharp
// Vehicle entity içinde
public void UpdateKilometers(long newKilometers)
{
    if (newKilometers < 0)
    {
        throw new DomainException(
            "VEHICLE_KM_NEGATIVE",
            new { 
                NewKilometers = newKilometers,
                LicensePlate = this.LicensePlate
            }
        );
    }
    
    this.Kilometers = newKilometers;
}
```

### 4. Hata Mesajı Çevirisi Ekleme

```sql
-- Türkçe
INSERT INTO ErrorMessages (ErrorCode, Language, Message, Status, CreatedDate)
VALUES ('CUSTOM_ERROR', 'tr', 'Özel hata mesajı: {PropertyName}', 1, GETDATE());

-- İngilizce
INSERT INTO ErrorMessages (ErrorCode, Language, Message, Status, CreatedDate)
VALUES ('CUSTOM_ERROR', 'en', 'Custom error message: {PropertyName}', 1, GETDATE());

-- Arapça
INSERT INTO ErrorMessages (ErrorCode, Language, Message, Status, CreatedDate)
VALUES ('CUSTOM_ERROR', 'ar', 'رسالة خطأ مخصصة: {PropertyName}', 1, GETDATE());
```

---

## 🔐 Multi-Tenant Güvenlik

### Global Query Filter
**Otomatik olarak çalışır!** Manuel filtrelemeye gerek yok:

```csharp
// ❌ YANLIŞ - Manuel filtreleme gerekmez
var customers = await _context.Customers
    .Where(c => c.ClientId == currentClientId)
    .ToListAsync();

// ✅ DOĞRU - Otomatik filtrelenir
var customers = await _context.Customers.ToListAsync();
```

### Filter'ı Devre Dışı Bırakma (Sadece Admin İşlemleri)
```csharp
// Tüm client'ların verilerine erişim (dikkatli kullanın!)
var allCustomers = await _context.Customers
    .IgnoreQueryFilters()
    .ToListAsync();
```

---

## 📝 Örnek Senaryolar

### Senaryo 1: Çok Serviste Kayıtlı Müşteri
```
Müşteri: Ahmet Yılmaz
- ABC Oto Servis (ClientId: 1) -> 2 aracı var
- XYZ Oto Servis (ClientId: 2) -> 1 aracı var

ABC Servis kullanıcısı login olunca:
- Sadece ClientId=1 olan kayıtları görür
- Ahmet'in sadece 2 aracını görür
```

### Senaryo 2: Farklı Dillerde Hata Mesajları
```
Türk kullanıcı (Accept-Language: tr):
"Şifre en az 6 karakter olmalıdır."

İngiliz kullanıcı (Accept-Language: en):
"Password must be at least 6 characters."

Arap kullanıcı (Accept-Language: ar):
"يجب أن تكون كلمة المرور 6 أحرف على الأقل."
```

---

## 🎨 Seed Data

Sistem ilk çalıştırıldığında otomatik olarak:
- 2 demo client oluşturulur (DEMO001, TEST001)
- 60+ hata mesajı 3 dilde eklenir (TR, EN, AR)

---

## 🔍 API Endpoint'leri

### Client (Tenant) Yönetimi
- `POST /api/clients` - Yeni servis oluştur
- `GET /api/clients/{id}` - Servis detayı

### Hata Mesajları
- `GET /api/errormessages/{errorCode}?language=tr` - Hata mesajı al

---

## ⚠️ Önemli Notlar

1. **Her zaman ClientId gönderilmeli**: Header veya Token ile
2. **Seed data otomatik yüklenir**: İlk migration'da
3. **Cache kullanılır**: Hata mesajları 60 dk cache'lenir
4. **Language default: tr**: Belirtilmezse Türkçe döner
5. **Global filter otomatik**: Manuel WHERE ClientId = X gerekmez!

---

## 🛠️ Geliştirme İpuçları

### Yeni Hata Mesajı Eklerken
1. Önce error code belirle (örn: `ORDER_NOT_FOUND`)
2. Messages sınıfına ekle: `public static string OrderNotFound = "ORDER_NOT_FOUND";`
3. Veritabanına çevirileri ekle (TR, EN, AR)
4. Kodda kullan: `throw new DomainException(Messages.OrderNotFound);`

### Yeni Multi-Tenant Entity Eklerken
1. `IClientEntity` interface'ini implement et
2. `public int ClientId { get; set; }` property'si ekle
3. Configuration'da index ekle
4. Migration oluştur

---

## 📞 Destek

Sorun yaşarsanız:
1. Migration'ları kontrol edin
2. ConnectionString'i kontrol edin
3. Seed data yüklenmiş mi kontrol edin: `SELECT * FROM ErrorMessages`
4. ClientId gönderiliyor mu kontrol edin

---

**Başarılar! 🚀**

