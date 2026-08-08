# Backend Gereksinimleri - Two-Factor Authentication (2FA)

## Genel Bakış

Settings sayfasında kullanıcıların 2FA'yı açıp kapatabilmesi için backend'de iki ana endpoint gereklidir:
1. **Enable 2FA** - Kullanıcının 2FA'yı aktif etmesi
2. **Disable 2FA** - Kullanıcının 2FA'yı pasif etmesi

## Mevcut Durum

- Frontend'de `AuthUser` tipinde `requiresTwoFactor: boolean` alanı mevcut
- Login endpoint'inde 2FA kontrolü yapılıyor (`complete2FA` endpoint'i mevcut)
- Settings sayfasında 2FA toggle'ı eklendi ancak backend entegrasyonu bekleniyor

## 1. Enable 2FA Endpoint

### Endpoint
```
POST /api/Auth/enable-2fa
```

### Authorization
- **Required:** Bearer Token (JWT)
- **Roles:** Tüm authenticated kullanıcılar (Customer, SystemAdmin, Manager, Employee)

### Request Body
```json
{}
```

**Not:** Request body boş olabilir çünkü kullanıcı bilgisi JWT token'dan alınacak.

### Response (Başarılı)
```json
{
  "success": true,
  "message": "Two-factor authentication has been enabled successfully",
  "data": {
    "requiresTwoFactor": true,
    "qrCodeUrl": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAA...", // Optional: QR code for authenticator app
    "backupCodes": [ // Optional: Backup codes for recovery
      "1234-5678",
      "2345-6789",
      "3456-7890"
    ]
  }
}
```

### Response (Hata - Zaten Aktif)
```json
{
  "success": false,
  "message": "Two-factor authentication is already enabled",
  "data": null
}
```

### Response (Hata - Unauthorized)
```json
{
  "success": false,
  "message": "Unauthorized. Please log in again.",
  "data": null
}
```

### Backend İşlemleri

1. **Token Doğrulama**
   - JWT token'dan kullanıcı ID'sini al
   - Token'ın geçerliliğini kontrol et

2. **Kullanıcı Kontrolü**
   - Kullanıcının `requiresTwoFactor` durumunu kontrol et
   - Eğer zaten aktif ise hata döndür

3. **2FA Aktivasyonu**
   - Kullanıcının `requiresTwoFactor` alanını `true` yap
   - Veritabanında güncelle

4. **QR Code Oluşturma (Opsiyonel)**
   - Authenticator app (Google Authenticator, Microsoft Authenticator) için QR code oluştur
   - Format: `otpauth://totp/MagicCarRepair:{email}?secret={secret}&issuer=MagicCarRepair`
   - QR code'u base64 PNG olarak döndür

5. **Backup Codes Oluşturma (Opsiyonel)**
   - 8-10 adet tek kullanımlık backup code oluştur
   - Her code 8 karakter (örn: "1234-5678" formatında)
   - Bu kodları veritabanında hash'leyerek sakla
   - Kullanıcıya sadece bir kez göster (sonraki isteklerde gösterilmez)

### Veritabanı Güncellemesi

```sql
UPDATE Users
SET RequiresTwoFactor = 1
WHERE Id = @userId
```

### C# Implementation Örneği

```csharp
[HttpPost("enable-2fa")]
[Authorize]
public async Task<IActionResult> Enable2FA()
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrEmpty(userId))
    {
        return Unauthorized(new { success = false, message = "Unauthorized" });
    }

    var user = await _userRepository.GetByIdAsync(int.Parse(userId));
    if (user == null)
    {
        return NotFound(new { success = false, message = "User not found" });
    }

    if (user.RequiresTwoFactor)
    {
        return BadRequest(new { success = false, message = "Two-factor authentication is already enabled" });
    }

    // Generate secret for TOTP
    var secret = GenerateSecret();
    
    // Store secret in database (encrypted)
    user.TwoFactorSecret = EncryptSecret(secret);
    user.RequiresTwoFactor = true;
    await _userRepository.UpdateAsync(user);

    // Generate QR code (optional)
    var qrCodeUrl = GenerateQRCode(user.Email, secret);

    // Generate backup codes (optional)
    var backupCodes = GenerateBackupCodes();
    await SaveBackupCodes(userId, backupCodes);

    return Ok(new
    {
        success = true,
        message = "Two-factor authentication has been enabled successfully",
        data = new
        {
            requiresTwoFactor = true,
            qrCodeUrl = qrCodeUrl, // Optional
            backupCodes = backupCodes // Optional, show only once
        }
    });
}

private string GenerateSecret()
{
    // Generate 32-character base32 secret
    var bytes = new byte[20];
    using (var rng = RandomNumberGenerator.Create())
    {
        rng.GetBytes(bytes);
    }
    return Base32Encoding.ToString(bytes);
}

private string GenerateQRCode(string email, string secret)
{
    var issuer = "MagicCarRepair";
    var otpAuthUrl = $"otpauth://totp/{issuer}:{email}?secret={secret}&issuer={issuer}";
    
    // Use a QR code library (e.g., QRCoder for .NET)
    using (var qrGenerator = new QRCodeGenerator())
    {
        var qrCodeData = qrGenerator.CreateQrCode(otpAuthUrl, QRCodeGenerator.ECCLevel.Q);
        using (var qrCode = new QRCode(qrCodeData))
        {
            using (var bitmap = qrCode.GetGraphic(20))
            {
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    var base64 = Convert.ToBase64String(ms.ToArray());
                    return $"data:image/png;base64,{base64}";
                }
            }
        }
    }
}

private List<string> GenerateBackupCodes()
{
    var codes = new List<string>();
    for (int i = 0; i < 10; i++)
    {
        var code = $"{Random.Shared.Next(1000, 9999)}-{Random.Shared.Next(1000, 9999)}";
        codes.Add(code);
    }
    return codes;
}
```

## 2. Disable 2FA Endpoint

### Endpoint
```
POST /api/Auth/disable-2fa
```

### Authorization
- **Required:** Bearer Token (JWT)
- **Roles:** Tüm authenticated kullanıcılar (Customer, SystemAdmin, Manager, Employee)

### Request Body
```json
{
  "password": "currentPassword123" // Optional: Kullanıcının mevcut şifresini doğrulamak için
}
```

**Not:** Güvenlik için şifre doğrulaması önerilir ancak opsiyoneldir.

### Response (Başarılı)
```json
{
  "success": true,
  "message": "Two-factor authentication has been disabled successfully",
  "data": {
    "requiresTwoFactor": false
  }
}
```

### Response (Hata - Zaten Pasif)
```json
{
  "success": false,
  "message": "Two-factor authentication is already disabled",
  "data": null
}
```

### Response (Hata - Yanlış Şifre)
```json
{
  "success": false,
  "message": "Invalid password. Please try again.",
  "data": null
}
```

### Response (Hata - Unauthorized)
```json
{
  "success": false,
  "message": "Unauthorized. Please log in again.",
  "data": null
}
```

### Backend İşlemleri

1. **Token Doğrulama**
   - JWT token'dan kullanıcı ID'sini al
   - Token'ın geçerliliğini kontrol et

2. **Kullanıcı Kontrolü**
   - Kullanıcının `requiresTwoFactor` durumunu kontrol et
   - Eğer zaten pasif ise hata döndür

3. **Şifre Doğrulama (Opsiyonel ama Önerilir)**
   - Request body'de şifre varsa doğrula
   - Şifre yanlışsa hata döndür

4. **2FA Deaktivasyonu**
   - Kullanıcının `requiresTwoFactor` alanını `false` yap
   - `TwoFactorSecret` alanını temizle (veya null yap)
   - Backup codes'ları sil (veya invalidate et)
   - Veritabanında güncelle

### Veritabanı Güncellemesi

```sql
UPDATE Users
SET RequiresTwoFactor = 0,
    TwoFactorSecret = NULL
WHERE Id = @userId
```

### C# Implementation Örneği

```csharp
[HttpPost("disable-2fa")]
[Authorize]
public async Task<IActionResult> Disable2FA([FromBody] Disable2FARequest request)
{
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrEmpty(userId))
    {
        return Unauthorized(new { success = false, message = "Unauthorized" });
    }

    var user = await _userRepository.GetByIdAsync(int.Parse(userId));
    if (user == null)
    {
        return NotFound(new { success = false, message = "User not found" });
    }

    if (!user.RequiresTwoFactor)
    {
        return BadRequest(new { success = false, message = "Two-factor authentication is already disabled" });
    }

    // Optional: Verify password for security
    if (!string.IsNullOrEmpty(request?.Password))
    {
        var passwordValid = await _passwordHasher.VerifyHashedPasswordAsync(
            user, 
            user.PasswordHash, 
            request.Password
        );
        
        if (passwordValid == PasswordVerificationResult.Failed)
        {
            return BadRequest(new { success = false, message = "Invalid password. Please try again." });
        }
    }

    // Disable 2FA
    user.RequiresTwoFactor = false;
    user.TwoFactorSecret = null; // Clear secret
    await _userRepository.UpdateAsync(user);

    // Invalidate backup codes
    await InvalidateBackupCodes(userId);

    return Ok(new
    {
        success = true,
        message = "Two-factor authentication has been disabled successfully",
        data = new
        {
            requiresTwoFactor = false
        }
    });
}

public class Disable2FARequest
{
    public string? Password { get; set; }
}
```

## 3. Get 2FA Status Endpoint (Opsiyonel)

Kullanıcının mevcut 2FA durumunu kontrol etmek için bir GET endpoint'i de eklenebilir. Ancak bu bilgi zaten `AuthUser` objesinde mevcut olduğu için gerekli olmayabilir.

### Endpoint
```
GET /api/Auth/2fa-status
```

### Response
```json
{
  "success": true,
  "data": {
    "requiresTwoFactor": true,
    "isEnabled": true
  }
}
```

## Güvenlik Önlemleri

1. **Rate Limiting**
   - Enable/disable işlemleri için rate limiting uygulanmalı
   - Örnek: 5 dakikada en fazla 3 deneme

2. **Audit Logging**
   - 2FA enable/disable işlemleri loglanmalı
   - Kullanıcı ID, timestamp, IP adresi kaydedilmeli

3. **Secret Encryption**
   - `TwoFactorSecret` veritabanında şifrelenmiş olarak saklanmalı
   - AES-256 veya benzeri güçlü şifreleme kullanılmalı

4. **Backup Codes Security**
   - Backup codes hash'lenerek saklanmalı
   - Her code sadece bir kez kullanılabilir olmalı
   - Kullanıldıktan sonra invalidate edilmeli

5. **Password Verification**
   - Disable işlemi için şifre doğrulaması önerilir
   - Güvenlik açısından kritik bir işlem olduğu için

## Veritabanı Şeması

### Users Tablosu (Mevcut)
```sql
-- Zaten mevcut olması gereken alan
RequiresTwoFactor BIT NOT NULL DEFAULT 0

-- Eklenmesi gereken alanlar (opsiyonel)
TwoFactorSecret NVARCHAR(MAX) NULL -- Encrypted secret
TwoFactorEnabledAt DATETIME2 NULL
```

### BackupCodes Tablosu (Yeni - Opsiyonel)
```sql
CREATE TABLE BackupCodes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    CodeHash NVARCHAR(256) NOT NULL, -- Hashed backup code
    IsUsed BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UsedAt DATETIME2 NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    INDEX IX_BackupCodes_UserId (UserId)
)
```

## Frontend Entegrasyonu

### API Endpoint'leri
```typescript
// src/constants/api.ts
export const AUTH_ENDPOINTS = {
  // ... mevcut endpoint'ler
  enable2FA: `${API_BASE_URL}/Auth/enable-2fa`,
  disable2FA: `${API_BASE_URL}/Auth/disable-2fa`,
} as const;
```

### AuthService Güncellemesi
```typescript
// src/services/authService.ts
export const authService = {
  // ... mevcut metodlar
  
  async enable2FA(token: string): Promise<ApiDataResult<{ requiresTwoFactor: boolean; qrCodeUrl?: string; backupCodes?: string[] }>> {
    return postJson<ApiDataResult<{ requiresTwoFactor: boolean; qrCodeUrl?: string; backupCodes?: string[] }>>(
      AUTH_ENDPOINTS.enable2FA,
      {},
      token
    );
  },

  async disable2FA(token: string, password?: string): Promise<ApiDataResult<{ requiresTwoFactor: boolean }>> {
    return postJson<ApiDataResult<{ requiresTwoFactor: boolean }>>(
      AUTH_ENDPOINTS.disable2FA,
      password ? { password } : {},
      token
    );
  },
};
```

## Test Senaryoları

### Enable 2FA Test Senaryoları

1. **Başarılı Aktivasyon**
   - Authenticated kullanıcı enable endpoint'ini çağırır
   - 2FA başarıyla aktif edilir
   - Response'da `requiresTwoFactor: true` döner

2. **Zaten Aktif**
   - 2FA zaten aktif olan kullanıcı tekrar enable çağırır
   - Hata mesajı döner: "Two-factor authentication is already enabled"

3. **Unauthorized**
   - Token olmadan veya geçersiz token ile çağrı
   - 401 Unauthorized döner

### Disable 2FA Test Senaryoları

1. **Başarılı Deaktivasyon (Şifre ile)**
   - Authenticated kullanıcı doğru şifre ile disable çağırır
   - 2FA başarıyla pasif edilir
   - Response'da `requiresTwoFactor: false` döner

2. **Başarılı Deaktivasyon (Şifre olmadan)**
   - Authenticated kullanıcı şifre olmadan disable çağırır
   - Backend şifre doğrulaması opsiyonel ise başarılı olur

3. **Yanlış Şifre**
   - Authenticated kullanıcı yanlış şifre ile disable çağırır
   - Hata mesajı döner: "Invalid password. Please try again."

4. **Zaten Pasif**
   - 2FA zaten pasif olan kullanıcı tekrar disable çağırır
   - Hata mesajı döner: "Two-factor authentication is already disabled"

5. **Unauthorized**
   - Token olmadan veya geçersiz token ile çağrı
   - 401 Unauthorized döner

## Öncelik ve Durum

**Öncelik:** Orta  
**Durum:** Beklemede

## Notlar

- QR code ve backup codes opsiyonel özelliklerdir
- Eğer bu özellikler eklenmeyecekse, response'dan çıkarılabilir
- Frontend şu anda sadece toggle state'ini yönetiyor, backend entegrasyonu tamamlandıktan sonra API çağrıları eklenecek
- Mevcut login akışında 2FA kontrolü zaten yapılıyor (`complete2FA` endpoint'i mevcut)

## İletişim

Sorularınız için frontend geliştirme ekibi ile iletişime geçin.
