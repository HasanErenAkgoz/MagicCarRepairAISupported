# Backend Multi-Language — Durum Raporu

> **Hazırlayan:** Frontend takımı
> **Tarih:** 2026-03-08
> **Konu:** Mobile uygulama için multi-language entegrasyonu — backend kod incelemesi sonuçları

---

## ✅ Zaten Yapılmış — Aksiyon Gerekmez

### 1. `GET /api/Translation/languages` → Public ✅

`TranslationController.cs` satır 48'de `[AllowAnonymous]` zaten mevcut. Login öncesi dil listesi alınabiliyor.

### 2. Dil Değiştirince `User.Language` DB'ye Kaydediliyor ✅

`change-language` endpoint'i hem cookie set ediyor hem de `UserManager.UpdateAsync()` ile `User.Language`'ı DB'ye yazıyor (satır 91–101).

### 3. Translation Seed Otomatik Çalışıyor ✅

`DatabaseSeedHostedService.cs` satır 68'de `SeedTranslationsAsync` çağrılıyor. Tablo boşsa otomatik seed ediyor, doluysa atlıyor.

---

## 🟡 ÖNEMLİ — Yapılması Gereken

### 4. `User.Language` JWT Claim'ine Eklenmeli

**Problem:** `TenantService.GetCurrentLanguage()` önce Claims'e bakıyor ama JWT token üretilirken `Language` claim'i eklenmiyor. Bu yüzden dil her zaman header ya da default (`tr`) üzerinden belirleniyor; kullanıcının DB'deki dil tercihi JWT'ye yansımıyor.

**Çözüm:** Token üretiminde `Language` claim'i ekle:

```csharp
// JWT token oluşturan yerde (TokenHelper / JwtHelper vb.)
claims.Add(new Claim("Language", user.Language ?? "tr"));
```

Böylece kullanıcı bir kez dil seçince sonraki token'larda bu tercih taşınır.

---

### 5. Dil Algılama Tutarsızlığı

**Mevcut durum:**

| Servis | Öncelik Sırası |
|--------|---------------|
| `TranslationService` | QueryString (`?lang=en`) → Header → Cookie → Default |
| `TenantService` | Claims → Header → Default |

**Problem:** Aynı istek iki farklı serviste farklı dil sonucu üretebilir. ExceptionHandlingMiddleware `TenantService` kullanırken translation endpoint'leri `TranslationService` kullanıyor.

**Çözüm:** Her iki serviste de aynı öncelik sırasını kullan:

```
Claims → Accept-Language Header → Cookie → Default (tr)
```

---

## 🟢 OPSİYONEL — İyi Olur

### 6. Almanca (de) Dışındaki Diller İçin Seed Verisi

`fr` ve `ar` `ErrorMessageService` kodunda referans var ama seed yok. Ya seed verisini ekle ya da bu dilleri kaldır.

### 7. Kullanıcı Kaydında `Language` Field'ı

Frontend müşteri kaydında kullanıcının seçtiği dili gönderiyor (`i18n.language || 'tr'`). Backend `RegisterCustomerRequest` içinde bu field'ı alıp `user.Language`'a atıyor mu kontrol et.

---

## 📋 Özet Tablo

| # | Öncelik | Konu | Etki |
|---|---------|------|------|
| 1 | ✅ Tamam | `languages` endpoint public | — |
| 2 | ✅ Tamam | `User.Language` DB'ye kaydediliyor | — |
| 3 | ✅ Tamam | Translation seed otomatik | — |
| 4 | 🟡 Önemli | Language claim JWT'ye eklenmeli | Dil tercihi token'a yansımıyor |
| 5 | 🟡 Önemli | Dil algılama tutarsızlığı | Farklı servislerde farklı dil |
| 6 | 🟢 Opsiyonel | fr/ar seed verisi | Karışıklık önlenir |
| 7 | 🟢 Opsiyonel | RegisterCustomer `Language` field'ı | Kayıtta dil kaydedilir |

---

## 🔌 Frontend'in Şu An Yaptıkları

- Her API isteğinde `Accept-Language: tr/en/de` header'ı otomatik gönderiliyor
- Auth sonrası `GET /api/Translation/all?language={lang}` çağrılıp i18next'e yükleniyor
- Dil değişince `POST /api/Translation/change-language` backend'e bildiriliyor
- `GET /api/Translation/languages` auth sonrası çağrılıp dil listesi cache'leniyor
- Login/Register ekranları yerel JSON dosyalarından çalışıyor (auth gerektirmiyor)
