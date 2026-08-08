# MagicCarRepairMobile — Tam Yapılandırma Rehberi

Bu belge projenin çalışması için gereken tüm konfigürasyonları, API key'lerini ve servis bağlantılarını adım adım açıklar.

---

## İçindekiler

1. [Genel Bakış](#1-genel-bakış)
2. [Mobil Uygulama — Expo / React Native](#2-mobil-uygulama--expo--react-native)
3. [Backend — .NET](#3-backend--net)
4. [Firebase / FCM Push Notification](#4-firebase--fcm-push-notification)
5. [OpenAI (AI Özellikleri)](#5-openai-ai-özellikleri)
6. [E-posta (SMTP)](#6-e-posta-smtp)
7. [SMS — Netgsm](#7-sms--netgsm)
8. [Ödeme — Iyzico](#8-ödeme--iyzico)
9. [Veritabanı](#9-veritabanı)
10. [JWT / Güvenlik](#10-jwt--güvenlik)
11. [EAS Build (Expo Application Services)](#11-eas-build-expo-application-services)
12. [Production Checklist](#12-production-checklist)

---

## 1. Genel Bakış

| Katman | Teknoloji | Port |
|--------|-----------|------|
| Mobil | Expo / React Native | — |
| Backend | ASP.NET Core 9 | 5169 |
| Veritabanı | SQL Server (LocalDB geliştirme) | 1433 |
| Cache | Redis (opsiyonel) | 6379 |
| Realtime | SignalR | 5169/hubs/* |

---

## 2. Mobil Uygulama — Expo / React Native

### 2.1 Backend API URL'ini Ayarla

**Önerilen:** `app.json` → `expo.extra.apiHost` ve `expo.extra.apiPort` (bilgisayarın **Wi‑Fi IPv4** adresi; telefonun IP’si değil).

```json
"extra": {
  "apiHost": "192.168.1.23",
  "apiPort": "5169"
}
```

`src/constants/api.ts` bu değerleri `expo-constants` ile okur; isteğe bağlı override: ortam değişkenleri `EXPO_PUBLIC_API_HOST` / `EXPO_PUBLIC_API_PORT`.

**Kendi IP'ni bulmak için:**
```bash
# Windows
ipconfig
# Örnek çıktı: IPv4 Address: 192.168.1.23
```

> **Not:** Backend ve telefon aynı Wi-Fi ağında olmalı.
>
> **Emülatör için:** `10.0.2.2` Android emülatöründe `localhost` karşılığıdır, değiştirme.
>
> **Production:** `apiHost` olarak gerçek API domain’i veya EAS ortam değişkenleri kullan.

---

### 2.1b Development build kurulumu (bir kez — zorunlu)

Bu proje **Expo Go ile tam çalışmaz** (`expo-dev-client` + native modüller var). Metro’dan **“Opening on Android”** veya `a` tuşu, telefonda **kurulu development build** arar.

**Telefonda henüz `Magic Car Repair` (paket: `com.magiccarrepair.mobile`) yoksa önce şunu çalıştır:**

```bash
npm run android
# veya: npx expo run:android
```

- **USB ile gerçek cihaz:** Geliştirici seçeneklerinde USB debugging açık olsun; ilk kurulumda kablo pratik olur.
- **Emülatör:** Android Studio emülatörü açıkken aynı komut emülatöre kurar.

> `No development build ... is installed` hatası = bu adımı atladın veya uygulamayı sildin. `npm run android` ile tekrar kur.

---

### 2.1c Kablosuz test (USB kablosu olmadan — build kurulduktan sonra)

Telefon ve bilgisayar **aynı Wi‑Fi** ağındayken günlük geliştirmede **USB gerekmez**:

1. `npm start` (veya `npm run start:lan`) — Metro bilgisayarının IP’sinde (örn. `192.168.1.23:8081`) dinler.
2. Telefonda **development build** uygulamasını aç (Expo Go değil) — QR ile veya uygulama içinden “Enter URL” ile `exp://...` adresine bağlan.
3. **Windows Güvenlik Duvarı** ilk seferde Node/8081’e izin sorarsa **İzin ver** (aksi halde telefon Metro’ya ulaşamaz).

**Farklı ağdaysan** (ör. telefon mobil veri, PC ev Wi‑Fi’i): tünel kullan:

```bash
npm run start:tunnel
```

İlk kullanımda Expo tünel paketini indirmeyi sorabilir; onayla. Tünel, internet üzerinden daha yavaş ama kablo/Wi‑Fi kısıtı olmadan çalışır.

> **Not:** `npm run android` ile APK bir kez kurulduktan sonra çoğu gün sadece Metro (`npm start`) + aynı ağ veya `start:tunnel` yeterli.

---

### 2.2 app.json Ayarları

**Dosya:** `app.json`

```json
{
  "expo": {
    "name": "MagicCarRepairMobile",
    "slug": "MagicCarRepairMobile",
    "android": {
      "package": "com.magiccarrepair.mobile",
      "googleServicesFile": "./google-services.json"
    },
    "ios": {
      "bundleIdentifier": "com.magiccarrepair.mobile"
    },
    "extra": {
      "eas": {
        "projectId": "48a63986-4464-455b-92db-8d28fbaa521e"
      }
    }
  }
}
```

> **projectId:** Bu ID Expo hesabına bağlı. Değiştirme — projeni `eas init` ile ilk kez başlattığında otomatik atandı.

#### Google Maps (`react-native-maps`) — Android

`app.json` içinde `android.config.googleMaps.apiKey` tanımlı olsa bile, **bare** `android/` klasöründe `AndroidManifest.xml` içine `com.google.android.geo.API_KEY` meta-data’sının yazılması gerekir; aksi halde uygulama harita açılırken çöker (`API key not found`).

**Dosya:** `android/app/src/main/AndroidManifest.xml` → `<application>` içinde:

```xml
<meta-data android:name="com.google.android.geo.API_KEY" android:value="GOOGLE_MAPS_API_KEY"/>
```

Anahtarı [Google Cloud Console](https://console.cloud.google.com/) → APIs & Services → Credentials → Maps SDK for Android ile kısıtla (paket adı `com.magiccarrepair.mobile` + debug/release SHA-1).

`npx expo prebuild --clean` sonrası manifest sıfırlanırsa bu satırı tekrar kontrol et.

---

### 2.3 EAS Build Profilleri

**Dosya:** `eas.json` (proje kökünde olmalı)

```json
{
  "cli": {
    "version": ">= 18.4.0",
    "appVersionSource": "remote"
  },
  "build": {
    "development": {
      "developmentClient": true,
      "distribution": "internal"
    },
    "preview": {
      "distribution": "internal",
      "android": {
        "buildType": "apk"
      }
    },
    "production": {
      "autoIncrement": true
    }
  }
}
```

---

## 3. Backend — .NET

### 3.1 appsettings.Development.json (Geliştirme)

**Dosya:** `Core.Packages.WebAPI/appsettings.Development.json`

Aşağıdaki template'i kullan. Boş bırakılan alanları doldur:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MagicCarRepairAISupportedDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  },
  "JwtSettings": {
    "SecurityKey": "DEV_SADECE_LOCALHOST_ICIN_DEGISTIR_MIN32CHAR!!",
    "Issuer": "MagicCarRepairAISupported",
    "Audience": "MagicCarRepairAISupported",
    "AccessTokenExpiration": 60
  },
  "AIOptions": {
    "Provider": "OpenAI",
    "ApiKey": "sk-proj-BURAYA_OPENAI_KEY_YAZAR",
    "Model": "gpt-4o",
    "VisionModel": "gpt-4o"
  },
  "EmailSettings": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SmtpUsername": "GMAIL_ADRESIN@gmail.com",
    "SmtpPassword": "GMAIL_UYGULAMA_SIFRESI",
    "FromEmail": "noreply@magiccarrepair.com",
    "FromName": "MagicCarRepair AI",
    "EnableSsl": true
  },
  "SmsSettings": {
    "Provider": "Netgsm",
    "IsEnabled": false,
    "Netgsm": {
      "Username": "NETGSM_KULLANICI_ADI",
      "Password": "NETGSM_SIFRE",
      "SenderNumber": "MAGICCAR"
    }
  },
  "FCM": {
    "ServiceAccountPath": "./firebase-service-account.json",
    "ProjectId": "FIREBASE_PROJE_ID"
  },
  "PaymentSettings": {
    "Iyzico": {
      "ApiKey": "IYZICO_API_KEY",
      "SecretKey": "IYZICO_SECRET_KEY",
      "BaseUrl": "https://sandbox.iyzipay.com"
    }
  },
  "FileStorageSettings": {
    "StorageType": "Local",
    "LocalPath": "wwwroot/uploads",
    "MaxFileSizeInMB": 10
  },
  "RedisSettings": {
    "ConnectionString": "localhost:6379",
    "InstanceName": "MagicCarRepairAISupported"
  }
}
```

---

## 4. Firebase / FCM Push Notification

Push notification için iki şey gerekli:
- **Mobil:** `google-services.json` (Android cihaza ulaşabilmek için)
- **Backend:** `firebase-service-account.json` (sunucudan push göndermek için)

### 4.1 Firebase Projesi Oluştur

1. [console.firebase.google.com](https://console.firebase.google.com) → **Add project**
2. Proje adı: `magic-car-repair` → Devam et
3. Google Analytics: İstersen açık bırak → Projeyi oluştur

---

### 4.2 Android Uygulaması Ekle (Mobil için)

1. Firebase Console → projen → **"Add app"** → Android simgesi
2. **Android package name:** `com.magiccarrepair.mobile`
3. App nickname: `Magic Car Repair Mobile`
4. **"Register app"**
5. **`google-services.json`** indir
6. Dosyayı mobil projenin kök dizinine koy:
   ```
   MagicCarRepairMobile/
     google-services.json   ← buraya
     app.json
   ```
7. Rebuild:
   ```bash
   npx expo run:android
   ```

---

### 4.3 Firebase Admin SDK (Backend için — sunucudan push göndermek)

1. Firebase Console → projen → **Project Settings** (dişli ikon)
2. **"Service accounts"** sekmesi
3. **"Generate new private key"** → JSON dosyasını indir
4. Dosyayı backend projesine koy:
   ```
   Core.Packages.WebAPI/
     firebase-service-account.json   ← buraya
   ```

   **Backend kök yolu (Windows):**
   ```
   c:\Projects\MagicCarRepairAISupported\Core.Packages.WebAPI\
     firebase-service-account.json
   ```

   > **Mobil repo (`MagicCarRepairMobile`):** Aynı dosyayı geliştirme / EAS referansı için proje kökünde `firebase-service-account.json` adıyla da tutabilirsin; sonra backend’e yukarıdaki yola **kopyala** (Git’e ekleme).

   > ⚠️ Bu dosyayı asla Git'e commit etme! `.gitignore`'a ekle:
   > ```
   > firebase-service-account.json
   > ```

5. `appsettings.Development.json` içine ekle:
   ```json
   "FCM": {
     "ServiceAccountPath": "./firebase-service-account.json",
     "ProjectId": "magic-car-repair-279aa"
   }
   ```
   Project ID'yi Firebase Console'da **Project settings → General → Project ID** altında bulursun. (Bu projede kullanılan ID: `magic-car-repair-279aa`.)

---

### 4.4 EAS ile FCM Credentials (Production Build için)

EAS üzerinden production push notification için:

```bash
npx eas credentials
```

→ **Android** → **Production** → **FCM V1 Service Account Key** →
Firebase'den indirdiğin JSON dosyasının yolunu ver.

---

## 5. OpenAI (AI Özellikleri)

Kullanılan AI özellikleri: Arıza teşhisi, parça önerisi, açıklama üretimi, fotoğraf analizi.

### 5.1 OpenAI API Key Al

1. [platform.openai.com/api-keys](https://platform.openai.com/api-keys) → **Create new secret key**
2. Key'i kopyala (`sk-proj-...` ile başlar)

### 5.2 Backend'e Ekle

`appsettings.Development.json`:
```json
"AIOptions": {
  "Provider": "OpenAI",
  "ApiKey": "sk-proj-BURAYA_YAZAR",
  "Model": "gpt-4o",
  "VisionModel": "gpt-4o"
}
```

**Model Karşılaştırması:**

| Model | Hız | Kalite | Maliyet/1M token |
|-------|-----|--------|-----------------|
| `gpt-3.5-turbo` | Çok hızlı | Temel | ~$0.50 |
| `gpt-4o-mini` | Hızlı | İyi | ~$0.15 |
| `gpt-4o` | Orta | En iyi | ~$5.00 |
| `gpt-4-turbo` | Yavaş | Çok iyi | ~$10.00 |

> **Öneri:** Başlangıç için `gpt-4o-mini` — hem ucuz hem kaliteli.

### 5.3 Azure OpenAI Kullanmak İstersen

```json
"AIOptions": {
  "Provider": "AzureOpenAI",
  "ApiKey": "AZURE_OPENAI_KEY",
  "AzureEndpoint": "https://KAYNAĞIN_ADI.openai.azure.com/",
  "AzureDeploymentName": "gpt-4o",
  "Model": "gpt-4o"
}
```

### 5.4 Provider Seçimi Nasıl Çalışır

`ServiceRegistration.cs:74` içinde:
```csharp
var aiProvider = configuration["AIOptions:Provider"] ?? "Mock";

if (aiProvider == "OpenAI" || aiProvider == "AzureOpenAI")
    // Gerçek OpenAI servisleri
else
    // Mock servisler (AI key yoksa buna düşer)
```

`Provider = "Mock"` veya `ApiKey` boşsa otomatik mock'a düşer — uygulama çökmez.

---

## 6. E-posta (SMTP)

Kullanım: Şifre sıfırlama maili, iş emri bildirimi, fatura gönderimi.

### 6.1 Gmail ile Kurulum

1. Gmail hesabında **2 Adımlı Doğrulama** aktif olmalı
2. [myaccount.google.com/apppasswords](https://myaccount.google.com/apppasswords) → **Uygulama şifresi** oluştur
3. App: "Mail", Device: "Windows Computer" → **Generate**
4. Çıkan 16 haneli kodu kopyala

`appsettings.Development.json`:
```json
"EmailSettings": {
  "SmtpServer": "smtp.gmail.com",
  "SmtpPort": 587,
  "SmtpUsername": "senin@gmail.com",
  "SmtpPassword": "abcd efgh ijkl mnop",
  "FromEmail": "noreply@magiccarrepair.com",
  "FromName": "MagicCarRepair AI",
  "EnableSsl": true
}
```

> **Not:** Normal Gmail şifreni değil, uygulama şifresini kullan.

### 6.2 Production için (SendGrid)

```json
"EmailSettings": {
  "SmtpServer": "smtp.sendgrid.net",
  "SmtpPort": 587,
  "SmtpUsername": "apikey",
  "SmtpPassword": "SG.SENDGRID_API_KEY",
  "FromEmail": "noreply@magiccarrepair.com",
  "FromName": "MagicCarRepair AI"
}
```

---

## 7. SMS — Netgsm

Kullanım: OTP gönderimi, iş emri bildirimi.

### 7.1 Netgsm Hesabı

1. [netgsm.com.tr](https://netgsm.com.tr) → Kayıt ol / Giriş yap
2. Kullanıcı adı ve şifre bilgilerin panelden görünür
3. Başlık (sender) için başlık başvurusu yap (örn: `MAGICCAR`)

`appsettings.Development.json`:
```json
"SmsSettings": {
  "Provider": "Netgsm",
  "IsEnabled": true,
  "Netgsm": {
    "Username": "NETGSM_KULLANICI_NO",
    "Password": "NETGSM_SIFRE",
    "SenderNumber": "MAGICCAR",
    "ApiUrl": "https://api.netgsm.com.tr/sms/send/get"
  }
}
```

> Geliştirme sırasında `"IsEnabled": false` bırak, maliyeti önler.

---

## 8. Ödeme — Iyzico

Kullanım: Mobil ödeme, taksitli ödeme.

### 8.1 Iyzico Sandbox (Test)

1. [iyzico.com](https://iyzico.com) → **Hesap Aç**
2. **Merchant Panel** → **Ayarlar** → **API Key / Secret Key** kopyala
3. Sandbox ve production için ayrı key'ler var

`appsettings.Development.json`:
```json
"PaymentSettings": {
  "Iyzico": {
    "ApiKey": "sandbox-IYZICO_API_KEY",
    "SecretKey": "sandbox-IYZICO_SECRET_KEY",
    "BaseUrl": "https://sandbox.iyzipay.com"
  }
}
```

Production için:
```json
"BaseUrl": "https://api.iyzipay.com"
```

---

## 9. Veritabanı

### 9.1 Geliştirme — SQL Server LocalDB

Herhangi bir kurulum gerekmez. Visual Studio ile birlikte LocalDB gelir.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MagicCarRepairAISupportedDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

Migration'ları uygulamak için:
```bash
cd Core.Packages.WebAPI
dotnet ef database update
```

### 9.2 Production — SQL Server

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SQL_SUNUCU_ADRESI;Database=MagicCarRepairDb;User Id=SA_KULLANICI;Password=GUCLU_SIFRE;TrustServerCertificate=True"
}
```

### 9.3 Redis (Cache — Opsiyonel)

Redis yoksa uygulama in-memory cache'e düşer, çalışmaya devam eder.

**Redis kurulumu (Windows):**
```bash
# WSL veya Docker ile
docker run -d -p 6379:6379 redis:alpine
```

```json
"RedisSettings": {
  "ConnectionString": "localhost:6379",
  "InstanceName": "MagicCarRepairAISupported"
}
```

---

## 10. JWT / Güvenlik

### 10.1 SecurityKey

`appsettings.json` içindeki mevcut key **geliştirme için** kullanılabilir ama **production'da mutlaka değiştirilmeli.**

```json
"JwtSettings": {
  "SecurityKey": "EN_AZ_32_KARAKTER_OLMALI_GUCLU_RASTGELE_BIR_KEY_YAZAR!!",
  "Issuer": "MagicCarRepairAISupported",
  "Audience": "MagicCarRepairAISupported",
  "AccessTokenExpiration": 60
}
```

**Güçlü key üretmek için:**
```bash
# PowerShell
[Convert]::ToBase64String([System.Security.Cryptography.RandomNumberGenerator]::GetBytes(64))
```

### 10.2 .NET User Secrets (Production'da Önerilir)

Key'leri `appsettings.json`'a yazmak yerine User Secrets kullan:

```bash
cd Core.Packages.WebAPI
dotnet user-secrets init
dotnet user-secrets set "JwtSettings:SecurityKey" "GUCLU_KEY_BURAYA"
dotnet user-secrets set "AIOptions:ApiKey" "sk-proj-..."
dotnet user-secrets set "PaymentSettings:Iyzico:ApiKey" "IYZICO_KEY"
```

---

## 11. EAS Build (Expo Application Services)

### 11.1 İlk Kurulum

```bash
# EAS CLI kur
npm install -g eas-cli

# Expo hesabına giriş
eas login

# Projeyi EAS'a bağla (zaten bağlı, atla)
# eas init --id 48a63986-4464-455b-92db-8d28fbaa521e
```

### 11.2 Build Alma

```bash
# Development build (fiziksel cihazda test için)
eas build --profile development --platform android

# APK (dağıtım için)
eas build --profile preview --platform android

# Production
eas build --profile production --platform android
```

### 11.3 FCM Credentials EAS'a Yükleme

```bash
eas credentials

# Sırasıyla seç:
# → Android
# → production
# → Push Notifications: Upload FCM V1 service account key
# → firebase-service-account.json dosyasının yolunu ver
```

### 11.4 OTA Update (Uygulamayı Store'a Göndermeden Güncelleme)

```bash
eas update --branch production --message "Hata düzeltmesi"
```

---

## 12. Production Checklist

Canlıya almadan önce bu maddelerin tamamı yapılmış olmalı:

### Güvenlik
- [ ] `JwtSettings:SecurityKey` — güçlü, rastgele, en az 64 karakter
- [ ] `appsettings.json` içinde gerçek şifre/key yok (hepsi env var veya user secrets'ta)
- [ ] `firebase-service-account.json` `.gitignore`'da
- [ ] `google-services.json` `.gitignore`'da (EAS bunu yönetir)
- [ ] CORS wildcard (`*`) kaldırıldı, sadece mobil domain izin verildi
- [ ] Super admin şifresi değiştirildi

### Servisler
- [ ] **Firebase** — `google-services.json` EAS'a yüklendi (`eas credentials`)
- [ ] **FCM Backend** — `firebase-service-account.json` sunucuda mevcut
- [ ] **OpenAI** — `ApiKey` dolu, `Provider: "OpenAI"` seçili
- [ ] **E-posta** — SMTP credentials dolu, test maili gönderildi
- [ ] **SMS** — `IsEnabled: true`, Netgsm credentials dolu
- [ ] **Iyzico** — Production key'ler girildi, `BaseUrl` production URL'i
- [ ] **Veritabanı** — Production SQL Server connection string girildi
- [ ] **Redis** — Production Redis bağlantısı yapılandırıldı (opsiyonel)

### Mobil
- [ ] `src/constants/api.ts` içinde `LOCAL_HOST` production domain'e çekildi
- [ ] `app.json` içinde `googleServicesFile` referansı doğru
- [ ] EAS build profili `production` ile build alındı

### Backend
- [ ] `dotnet ef database update` production'da çalıştırıldı
- [ ] `FileStorageSettings` production storage (Azure Blob / S3) yapılandırıldı
- [ ] Logging level production'da `Warning` seviyesine çekildi

---

## Hızlı Başlangıç (Sadece Geliştirme)

Minimum kurulum ile uygulamayı çalıştırmak için:

```bash
# 1. Backend — sadece bunlar yeterli
# appsettings.Development.json içinde:
# - ConnectionStrings:DefaultConnection → LocalDB (değiştirme)
# - AIOptions:Provider → "Mock" (OpenAI key olmadan)
# - Diğer her şey boş bırakılabilir

# 2. Backend'i başlat
cd Core.Packages.WebAPI
dotnet run

# 3. Mobil — IP adresini güncelle
# src/constants/api.ts → LOCAL_HOST = '192.168.1.XX' (kendi IP'n)

# 4. Mobil başlat (bu projede expo-dev-client + native modüller var → Expo Go değil development build)
npm start
# veya: npx expo start --dev-client
# Telefonda `expo run:android` ile kurulu **development build** uygulamasını aç; Expo Go ile bu hata alınır:
# "dev server cannot open custom runtimes ... (target: expo)"

# Push notification olmadan, AI mock ile, ödeme olmadan çalışır.
```

---

*Son güncelleme: 2026-03-22*
