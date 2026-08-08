# Backend Gereksinimleri - Şifremi Unuttum (Forgot Password) - SMS/OTP Tabanlı

## Genel Bakış

Web sitesi olmadığı için şifre sıfırlama işlemi **SMS ile OTP (One-Time Password)** kullanılarak yapılacaktır.

Forgot Password özelliği için backend'de üç ana endpoint gereklidir:
1. **Forgot Password** - Telefon numarasına OTP kodu gönderme
2. **Verify OTP** - OTP kodunu doğrulama
3. **Reset Password** - OTP doğrulandıktan sonra yeni şifre belirleme

## 1. Forgot Password Endpoint (OTP Gönderme)

### Endpoint
```
POST /api/Auth/forgot-password
```

### Request Body
```json
{
  "phone": "+905551234567"
}
```

### Response (Başarılı)
```json
{
  "success": true,
  "message": "OTP code has been sent to your phone number",
  "data": {
    "otpExpiresIn": 300,
    "maskedPhone": "+90 5** *** ** **"
  }
}
```

### Response (Hata - Telefon numarası bulunamadı)
```json
{
  "success": false,
  "message": "User with this phone number not found",
  "data": null
}
```

### Backend İşlemleri

1. **Telefon Numarası Doğrulama**
   - Telefon numarası formatını kontrol et (uluslararası format: +905551234567)
   - Veritabanında bu telefon numarası ile kayıtlı kullanıcı var mı kontrol et
   - Telefon numarası kayıtlı değilse hata döndür (güvenlik için enumeration önleme uygulanabilir)

2. **OTP Kodu Oluşturma**
   - 6 haneli rastgele OTP kodu oluştur (örn: 123456, 789012)
   - OTP'yi hash'leyerek veritabanında sakla (plain text saklama!)
   - OTP'nin geçerlilik süresini belirle (örn: 5 dakika, 10 dakika)
   - OTP'nin kullanılıp kullanılmadığını takip etmek için bir flag ekle
   - Aynı kullanıcı için eski OTP'leri geçersiz kıl (yeni OTP isteğinde)

3. **SMS Gönderme**
   - Kullanıcının telefon numarasına OTP kodunu SMS olarak gönder
   - SMS içeriği:
     ```
     Magic Car Repair - Şifre Sıfırlama
     OTP kodunuz: 123456
     Bu kod 5 dakika geçerlidir.
     Eğer bu talebi siz yapmadıysanız, bu mesajı görmezden gelin.
     ```
   - SMS servisi entegrasyonu gerekli (Twilio, AWS SNS, Nexmo, vb.)

4. **Güvenlik Önlemleri**
   - Rate limiting: Aynı telefon numarasına çok fazla istek gönderilmesini engelle (örn: 3 istek/15 dakika)
   - Telefon numarası bulunamadığında bile başarılı yanıt döndür (security best practice - phone enumeration önleme)
   - OTP kodlarını hash'leyerek sakla (veritabanında plain text tutma)
   - OTP kodlarını belirli bir süre sonra otomatik olarak sil (örn: 10 dakika sonra)

### Veritabanı Şeması Önerisi

```sql
-- Password Reset OTP tablosu
CREATE TABLE password_reset_otps (
    id INT PRIMARY KEY AUTO_INCREMENT,
    user_id INT NOT NULL,
    phone_number VARCHAR(20) NOT NULL,
    otp_hash VARCHAR(255) NOT NULL,  -- Hash'lenmiş OTP (SHA-256, bcrypt, vb.)
    expires_at DATETIME NOT NULL,
    used BOOLEAN DEFAULT FALSE,
    attempts INT DEFAULT 0,  -- Yanlış deneme sayısı
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES users(id),
    INDEX idx_user_phone (user_id, phone_number),
    INDEX idx_phone_number (phone_number),
    INDEX idx_expires_at (expires_at)
);
```

**Önemli Notlar:**
- OTP'yi **asla plain text** olarak saklamayın, mutlaka hash'leyin
- OTP hash'leme için SHA-256 veya bcrypt kullanabilirsiniz
- `attempts` alanı ile yanlış deneme sayısını takip edin (3 denemeden sonra OTP'yi geçersiz kılın)

## 2. Verify OTP Endpoint (OTP Doğrulama)

### Endpoint
```
POST /api/Auth/verify-reset-otp
```

### Request Body
```json
{
  "phone": "+905551234567",
  "otp": "123456"
}
```

### Response (Başarılı)
```json
{
  "success": true,
  "message": "OTP verified successfully",
  "data": {
    "resetToken": "abc123def456...",  // Geçici reset token (OTP doğrulandıktan sonra)
    "resetTokenExpiresIn": 600  // 10 dakika
  }
}
```

### Response (Hata - Geçersiz OTP)
```json
{
  "success": false,
  "message": "Invalid or expired OTP code",
  "data": null
}
```

### Response (Hata - Çok fazla deneme)
```json
{
  "success": false,
  "message": "Too many failed attempts. Please request a new OTP code.",
  "data": null
}
```

### Backend İşlemleri

1. **OTP Doğrulama**
   - Telefon numarası ile kullanıcıyı bul
   - Kullanıcı için en son oluşturulan OTP'yi bul
   - OTP'nin süresinin dolup dolmadığını kontrol et
   - OTP'nin daha önce kullanılıp kullanılmadığını kontrol et
   - Yanlış deneme sayısını kontrol et (3'ten fazla ise hata döndür)

2. **OTP Karşılaştırma**
   - Gelen OTP'yi hash'le
   - Veritabanındaki hash'lenmiş OTP ile karşılaştır
   - Eşleşirse geçici bir reset token oluştur (bu token reset password için kullanılacak)
   - OTP'yi "used" olarak işaretle

3. **Reset Token Oluşturma**
   - OTP doğrulandıktan sonra, reset password için geçici bir token oluştur
   - Bu token'ın geçerlilik süresi kısa olmalı (örn: 10 dakika)
   - Token'ı veritabanında sakla veya JWT olarak imzala

4. **Güvenlik Önlemleri**
   - Yanlış deneme sayısını artır
   - 3 yanlış denemeden sonra OTP'yi geçersiz kıl ve yeni OTP iste
   - Rate limiting uygula

## 3. Reset Password Endpoint

### Endpoint
```
POST /api/Auth/reset-password
```

### Request Body
```json
{
  "resetToken": "abc123def456...",  // Verify OTP'den dönen token
  "phone": "+905551234567",
  "newPassword": "NewSecurePassword123!"
}
```

### Response (Başarılı)
```json
{
  "success": true,
  "message": "Password has been reset successfully",
  "data": null
}
```

### Response (Hata - Geçersiz token)
```json
{
  "success": false,
  "message": "Invalid or expired reset token",
  "data": null
}
```

### Backend İşlemleri

1. **Token Doğrulama**
   - ResetToken'ın veritabanında var olup olmadığını kontrol et
   - Token'ın süresinin dolup dolmadığını kontrol et
   - Token'ın daha önce kullanılıp kullanılmadığını kontrol et
   - Token'ın ilgili telefon numarasına ait olup olmadığını kontrol et

2. **Şifre Validasyonu**
   - Yeni şifrenin minimum uzunluk gereksinimlerini karşıladığını kontrol et (örn: en az 8 karakter)
   - Şifre karmaşıklık kurallarını kontrol et (büyük harf, küçük harf, rakam, özel karakter)
   - Kullanıcının önceki şifrelerinden farklı olduğunu kontrol et (opsiyonel ama önerilir)

3. **Şifre Güncelleme**
   - Yeni şifreyi hash'le (bcrypt, Argon2, vb.)
   - Kullanıcının şifresini güncelle
   - Reset token'ı veya OTP'yi "used" olarak işaretle
   - Tüm aktif oturumları sonlandır (güvenlik için - kullanıcı yeniden giriş yapmalı)
   - Kullanıcıya SMS ile şifre değişikliği bildirimi gönder (opsiyonel ama önerilir)

4. **Güvenlik Önlemleri**
   - Token'ı tek kullanımlık yap (kullanıldıktan sonra geçersiz kıl)
   - Rate limiting uygula
   - Şifre değişikliği için e-posta bildirimi gönder

## 4. Ek Özellikler (Opsiyonel ama Önerilir)

### OTP Yeniden Gönderme
Eğer kullanıcı OTP'yi almadıysa veya süresi dolduysa:
```
POST /api/Auth/resend-reset-otp
{
  "phone": "+905551234567"
}
```
- Eski OTP'yi geçersiz kıl
- Yeni OTP oluştur ve SMS gönder
- Rate limiting: 5 dakika içinde maksimum 3 kez yeniden gönderme

### OTP İptal Etme
Kullanıcı şifre sıfırlama talebini iptal edebilir:
```
POST /api/Auth/cancel-reset-password
{
  "phone": "+905551234567"
}
```
- Kullanıcının aktif OTP'lerini geçersiz kıl

### Şifre Sıfırlama Geçmişi
Güvenlik için şifre sıfırlama işlemlerini logla:
- Hangi kullanıcı
- Ne zaman
- Hangi IP adresinden
- Başarılı/Başarısız

## 5. SMS Şablonu Örnekleri

### OTP Gönderme SMS'i
```
Magic Car Repair - Şifre Sıfırlama

OTP kodunuz: 123456
Bu kod 5 dakika geçerlidir.

Eğer bu talebi siz yapmadıysanız, bu mesajı görmezden gelin.

Magic Car Repair
```

### Şifre Değişikliği Bildirimi SMS'i (Opsiyonel)
```
Magic Car Repair

Şifreniz başarıyla değiştirildi.
Eğer bu işlemi siz yapmadıysanız, lütfen derhal bizimle iletişime geçin.

Magic Car Repair
```

## 6. SMS Servisi Entegrasyonu

### Önerilen SMS Servisleri

1. **Twilio** (https://www.twilio.com)
   - Güvenilir ve yaygın kullanılan
   - Türkiye desteği var
   - API dokümantasyonu iyi

2. **AWS SNS** (https://aws.amazon.com/sns/)
   - AWS ekosistemi içinde
   - Ölçeklenebilir
   - Türkiye desteği sınırlı olabilir

3. **Nexmo/Vonage** (https://www.vonage.com)
   - Global kapsama
   - İyi fiyatlandırma

4. **Türkiye Yerel Servisler**
   - Netgsm
   - İleti Merkezi
   - Related Digital

### SMS Gönderme Örneği (Twilio)

```csharp
// C# .NET örneği
using Twilio;
using Twilio.Rest.Api.V2010.Account;

var accountSid = "YOUR_ACCOUNT_SID";
var authToken = "YOUR_AUTH_TOKEN";
TwilioClient.Init(accountSid, authToken);

var message = MessageResource.Create(
    body: $"Magic Car Repair - Şifre Sıfırlama\n\nOTP kodunuz: {otpCode}\nBu kod 5 dakika geçerlidir.",
    from: new Twilio.Types.PhoneNumber("+1234567890"), // Twilio telefon numaranız
    to: new Twilio.Types.PhoneNumber(phoneNumber)
);
```

## 7. Güvenlik Checklist

- [ ] OTP kodları hash'lenerek saklanıyor (asla plain text!)
- [ ] OTP kodları belirli bir süre sonra expire oluyor (5-10 dakika)
- [ ] OTP kodları tek kullanımlık
- [ ] Yanlış OTP denemeleri sınırlandırılıyor (max 3 deneme)
- [ ] Rate limiting uygulanıyor (3 OTP isteği/15 dakika)
- [ ] Telefon numarası enumeration önleniyor (phone bulunamadığında da başarılı yanıt - opsiyonel)
- [ ] Şifreler güvenli bir şekilde hash'leniyor (bcrypt, Argon2)
- [ ] HTTPS kullanılıyor
- [ ] CORS ayarları doğru yapılandırılmış
- [ ] Input validation yapılıyor
- [ ] SQL injection koruması var
- [ ] XSS koruması var
- [ ] SMS servisi güvenli bir şekilde entegre edilmiş
- [ ] SMS gönderim hataları loglanıyor

## 8. Test Senaryoları

1. ✅ Geçerli telefon numarası ile OTP isteği
2. ✅ Geçersiz telefon numarası formatı
3. ✅ Kayıtlı olmayan telefon numarası
5. ✅ Geçerli OTP ile doğrulama
6. ✅ Yanlış OTP ile doğrulama denemesi
7. ✅ Süresi dolmuş OTP ile doğrulama
8. ✅ Çok fazla yanlış OTP denemesi (3+ deneme)
9. ✅ Geçerli reset token ile şifre sıfırlama
10. ✅ Geçersiz reset token ile şifre sıfırlama
11. ✅ Zayıf şifre ile şifre sıfırlama denemesi
12. ✅ Rate limiting testi (çok fazla OTP isteği)
13. ✅ SMS gönderim başarısızlığı durumu
14. ✅ OTP yeniden gönderme
15. ✅ Aynı OTP'nin iki kez kullanılması

## 9. API Örnekleri

### cURL - Forgot Password (OTP Gönderme)
```bash
curl -X POST http://localhost:5169/api/Auth/forgot-password \
  -H "Content-Type: application/json" \
  -d '{
    "phone": "+905551234567"
  }'
```

### cURL - Verify OTP
```bash
curl -X POST http://localhost:5169/api/Auth/verify-reset-otp \
  -H "Content-Type: application/json" \
  -d '{
    "phone": "+905551234567",
    "otp": "123456"
  }'
```

### cURL - Reset Password (Reset Token ile)
```bash
curl -X POST http://localhost:5169/api/Auth/reset-password \
  -H "Content-Type: application/json" \
  -d '{
    "resetToken": "abc123def456...",
    "phone": "+905551234567",
    "newPassword": "NewSecurePassword123!"
  }'
```

### cURL - Resend OTP
```bash
curl -X POST http://localhost:5169/api/Auth/resend-reset-otp \
  -H "Content-Type: application/json" \
  -d '{
    "phone": "+905551234567"
  }'
```

## 10. Akış Diyagramı

```
1. Kullanıcı "Şifremi Unuttum" ekranında telefon numarası girer
   ↓
2. POST /api/Auth/forgot-password
   ↓
3. Backend: Telefon numarasını doğrula, kullanıcıyı bul, OTP oluştur
   ↓
4. SMS ile OTP gönder (örn: "OTP kodunuz: 123456")
   ↓
5. Kullanıcı OTP'yi girer
   ↓
6. POST /api/Auth/verify-reset-otp
   ↓
7. Backend: OTP doğrula, reset token oluştur
   ↓
8. Kullanıcı yeni şifresini girer
   ↓
9. POST /api/Auth/reset-password (resetToken + yeni şifre)
   ↓
10. Backend: Şifreyi güncelle, oturumları sonlandır
    ↓
11. Kullanıcı yeni şifresi ile giriş yapar
```

## 11. Notlar

- **SMS Servisi Gerekli**: Twilio, AWS SNS, Nexmo veya yerel bir SMS servisi entegrasyonu yapılmalı
- **OTP Güvenliği**: OTP kodları mutlaka hash'lenerek saklanmalı, asla plain text tutulmamalı
- **Rate Limiting**: SMS maliyetlerini kontrol etmek ve spam'i önlemek için rate limiting kritik
- **Telefon Numarası**: Kullanıcı kayıt sırasında telefon numarası zorunlu olmalı
- **Mobile App**: Forgot Password ekranında OTP girişi için bir ekran gerekli (Stitch'te tasarlanabilir)
- **Production**: SMS gönderimi için async job queue (RabbitMQ, Redis Queue, vb.) kullanılması önerilir
- **Maliyet**: SMS gönderimi ücretli olduğu için gereksiz SMS gönderiminden kaçınılmalı
- **Alternatif**: Eğer SMS maliyeti yüksekse, e-posta ile OTP gönderme de düşünülebilir
