# Backend Gereksinimleri - Remember Me (Beni Hatırla) Özelliği

## Genel Bakış

"Beni Hatırla" özelliği, kullanıcının login bilgilerini ve oturumunu kalıcı olarak saklamasına olanak tanır. Bu özellik için backend tarafında **herhangi bir değişiklik gerekmez** çünkü bu tamamen frontend'de token saklama mantığı ile ilgilidir.

## Mevcut Durum

- Frontend'de "Beni Hatırla" checkbox'ı mevcut
- Token'lar `expo-secure-store` kullanılarak saklanıyor
- Backend zaten JWT token ve refresh token üretiyor
- Token'ların expiration süreleri backend'de belirleniyor

## Frontend Implementasyonu

### Remember Me = True (İşaretli)
- Token'lar `SecureStore`'da kalıcı olarak saklanır
- Uygulama kapanıp açılsa bile kullanıcı otomatik olarak giriş yapmış olur
- Token'lar cihazdan silinene kadar geçerlidir

### Remember Me = False (İşaretsiz)
- Token'lar sadece memory'de tutulur (state'de)
- Uygulama kapanıp açıldığında token'lar kaybolur
- Kullanıcı tekrar login olmak zorundadır

## Backend Tarafında Yapılması Gerekenler

**Hiçbir şey!** Backend zaten gerekli token'ları üretiyor ve döndürüyor.

## Opsiyonel İyileştirmeler (Gelecek için)

Eğer daha gelişmiş bir "Remember Me" özelliği istenirse, backend'de şunlar yapılabilir:

### 1. Token Expiration Sürelerini Ayarlama

**Mevcut Durum:**
- Access token ve refresh token'ların expiration süreleri backend'de belirleniyor
- Bu süreler tüm kullanıcılar için aynı

**Önerilen İyileştirme:**
- "Remember Me" seçiliyse daha uzun süreli token'lar üretilebilir
- Örnek:
  - Normal login: Access token 1 saat, Refresh token 7 gün
  - Remember Me: Access token 24 saat, Refresh token 30 gün

**Endpoint Değişikliği:**
```
POST /api/Auth/login
```

**Request Body:**
```json
{
  "email": "user@example.com",
  "password": "password123",
  "rememberMe": true  // Yeni alan
}
```

**Backend İşlemi:**
```csharp
public async Task<IActionResult> Login([FromBody] LoginRequest request)
{
    // ... mevcut login işlemleri ...
    
    var accessTokenExpiration = request.RememberMe 
        ? DateTime.UtcNow.AddDays(1)  // Remember Me: 24 saat
        : DateTime.UtcNow.AddHours(1); // Normal: 1 saat
    
    var refreshTokenExpiration = request.RememberMe
        ? DateTime.UtcNow.AddDays(30)  // Remember Me: 30 gün
        : DateTime.UtcNow.AddDays(7);  // Normal: 7 gün
    
    var accessToken = GenerateJwtToken(user, accessTokenExpiration);
    var refreshToken = GenerateRefreshToken(user, refreshTokenExpiration);
    
    // ... geri kalan kod ...
}
```

### 2. Device Tracking (Cihaz Takibi)

**Açıklama:**
- Her login'de cihaz bilgisi kaydedilebilir
- "Remember Me" seçiliyse cihaz "güvenilir cihaz" olarak işaretlenebilir
- Güvenilir cihazlardan gelen istekler için ekstra güvenlik kontrolleri atlanabilir

**Veritabanı Şeması:**
```sql
CREATE TABLE UserDevices (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    DeviceId NVARCHAR(255) NOT NULL, -- Unique device identifier
    DeviceName NVARCHAR(255) NULL, -- "iPhone 13", "Samsung Galaxy S21" etc.
    IsTrusted BIT NOT NULL DEFAULT 0, -- Remember Me ile işaretlenmiş mi?
    LastLoginAt DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    INDEX IX_UserDevices_UserId (UserId),
    INDEX IX_UserDevices_DeviceId (DeviceId)
)
```

**Endpoint Değişikliği:**
```json
{
  "email": "user@example.com",
  "password": "password123",
  "rememberMe": true,
  "deviceId": "unique-device-identifier", // Frontend'den gönderilecek
  "deviceName": "iPhone 13" // Opsiyonel
}
```

### 3. Session Management (Oturum Yönetimi)

**Açıklama:**
- Backend'de aktif session'ları takip edebilir
- "Remember Me" seçiliyse session daha uzun süre aktif kalır
- Kullanıcı istediğinde tüm session'ları görebilir ve iptal edebilir

**Veritabanı Şeması:**
```sql
CREATE TABLE UserSessions (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    TokenId NVARCHAR(255) NOT NULL, -- JWT JTI (JWT ID)
    DeviceId NVARCHAR(255) NULL,
    DeviceName NVARCHAR(255) NULL,
    IpAddress NVARCHAR(45) NULL,
    UserAgent NVARCHAR(500) NULL,
    IsRemembered BIT NOT NULL DEFAULT 0, -- Remember Me ile mi oluşturuldu?
    ExpiresAt DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    LastActivityAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    INDEX IX_UserSessions_UserId (UserId),
    INDEX IX_UserSessions_TokenId (TokenId)
)
```

## Güvenlik Notları

1. **Token Expiration:**
   - "Remember Me" token'ları bile mutlaka bir expiration süresi olmalı
   - Çok uzun süreli token'lar güvenlik riski oluşturur
   - Önerilen maksimum: 30-90 gün

2. **Refresh Token Rotation:**
   - Her refresh token kullanımında yeni bir refresh token üretilmeli
   - Eski refresh token invalidate edilmeli
   - Bu, token çalınması durumunda zararı sınırlar

3. **Device Tracking:**
   - Cihaz bilgileri hash'lenerek saklanmalı
   - GDPR uyumluluğu için kullanıcı cihaz bilgilerini silebilmeli

## Test Senaryoları

### Senaryo 1: Remember Me = True
1. Kullanıcı login olurken "Beni Hatırla" checkbox'ını işaretler
2. Login başarılı olur
3. Token'lar SecureStore'da saklanır
4. Uygulama kapatılıp açılır
5. Kullanıcı otomatik olarak giriş yapmış olur

### Senaryo 2: Remember Me = False
1. Kullanıcı login olurken "Beni Hatırla" checkbox'ını işaretlemez
2. Login başarılı olur
3. Token'lar sadece memory'de tutulur
4. Uygulama kapatılıp açılır
5. Kullanıcı login ekranını görür

### Senaryo 3: Token Expiration (Eğer backend'de süreler ayarlanırsa)
1. Remember Me = True ile login olunur
2. Token'lar 30 gün geçerli olur
3. 30 gün sonra token expire olur
4. Refresh token ile yeni token alınır
5. Refresh token da expire olursa kullanıcı tekrar login olmalıdır

## Öncelik ve Durum

**Öncelik:** Düşük (Opsiyonel iyileştirmeler için)  
**Durum:** Mevcut implementasyon çalışıyor

## Notlar

- **Mevcut durumda backend'de hiçbir değişiklik gerekmez**
- Frontend implementasyonu tamamlandı ve çalışıyor
- Opsiyonel iyileştirmeler gelecekte yapılabilir
- Token expiration süreleri backend'de zaten kontrol ediliyor

## İletişim

Sorularınız için frontend geliştirme ekibi ile iletişime geçin.
