# Firebase & Google Maps Kurulum Rehberi

Bu rehber; Firebase projesini sıfırdan kurmanı, `google-services.json` dosyasını almanı ve Google Maps API Key'ini aktive etmeni adım adım anlatır.

---

## Adım 1 — Firebase Projesi Oluştur

1. Tarayıcında [https://console.firebase.google.com](https://console.firebase.google.com) adresine git.
2. Google hesabınla giriş yap.
3. **"Add project"** (Proje ekle) butonuna tıkla.
4. Proje adı gir → örneğin: `MagicCarRepair`
5. Google Analytics sorusu çıkarsa: **Enable** seçebilirsin (opsiyonel), **Continue** de.
6. **"Create project"** tıkla → birkaç saniye bekle → **Continue**.

---

## Adım 2 — Android Uygulamasını Ekle

Firebase proje konsolunda sol menüde **Project Overview** sayfasındayken:

1. Ortadaki **Android ikonu** `</>` veya Android logosuna tıkla.
2. **Android package name** alanına yaz:
   ```
   com.magiccarrepair.mobile
   ```
   > Bu değer `app.json` → `android.package` ile birebir aynı olmalı.
3. **App nickname** (opsiyonel): `Magic Car Repair Mobile`
4. **Debug signing certificate SHA-1** — şimdilik boş bırak.
5. **"Register app"** tıkla.

---

## Adım 3 — google-services.json'ı İndir

1. Kayıt sonrası **"Download google-services.json"** butonu çıkacak.
2. Dosyayı indir.
3. İndirilen `google-services.json` dosyasını projenin **kök dizinine** koy:
   ```
   MagicCarRepairMobile/
   ├── app.json
   ├── google-services.json   ← buraya
   ├── package.json
   └── ...
   ```
4. Firebase konsolunda **"Next"** → **"Next"** → **"Continue to console"** tıkla.

---

## Adım 4 — Google Cloud Console'da Projeyi Aç

Firebase projesi oluşturunca arka planda bir Google Cloud projesi de otomatik oluşur. Şimdi oraya geçeceğiz.

1. [https://console.cloud.google.com](https://console.cloud.google.com) adresine git.
2. Sol üstte proje seçici dropdown'unu tıkla.
3. Az önce oluşturduğun **MagicCarRepair** projesini seç.

---

## Adım 5 — Maps SDK for Android'i Aktive Et

1. Sol menüden **"APIs & Services"** → **"Library"** tıkla.
2. Arama kutusuna: `Maps SDK for Android` yaz.
3. Çıkan sonuca tıkla → **"Enable"** butonuna bas.
4. Birkaç saniye bekle → aktive oldu.

---

## Adım 6 — Google Maps API Key Oluştur

1. Sol menüden **"APIs & Services"** → **"Credentials"** tıkla.
2. Üstte **"+ Create Credentials"** → **"API Key"** seç.
3. Yeni bir API Key oluşturuldu — **kopyala** (örn: `AIzaSyAbc123...`).

### API Key'i Kısıtla (Önerilir)
Güvenlik için key'i kısıtlayabilirsin:
1. Oluşturulan key'e tıkla → **"Edit API key"**.
2. **Application restrictions** → **"Android apps"** seç.
3. **"Add an item"** → package name: `com.magiccarrepair.mobile` gir → SHA-1 şimdilik boş bırakabilirsin.
4. **API restrictions** → **"Restrict key"** → **"Maps SDK for Android"** seç.
5. **Save**.

---

## Adım 7 — app.json'ı Güncelle

`MagicCarRepairMobile/app.json` dosyasını aç, `YOUR_GOOGLE_MAPS_API_KEY` yerine kopyaladığın key'i yaz:

```json
"android": {
  "package": "com.magiccarrepair.mobile",
  "googleServicesFile": "./google-services.json",
  "config": {
    "googleMaps": {
      "apiKey": "AIzaSyAbc123..."
    }
  },
  ...
}
```

---

## Adım 8 — Yeniden Build Al

API key app.json'a kaydedildikten sonra native build'in yenilenmesi gerekiyor:

```bash
# Proje dizininde terminal aç
cd C:\Users\hasan\source\repos\MagicCarRepairMobile

npx expo run:android
```

> Build tamamlandıktan sonra harita butonu çalışmaya başlayacak.

---

## Kontrol Listesi

| Adım | Durum |
|------|-------|
| Firebase projesi oluşturuldu | ☐ |
| Android uygulaması eklendi (`com.magiccarrepair.mobile`) | ☐ |
| `google-services.json` proje köküne koyuldu | ☐ |
| Maps SDK for Android aktive edildi | ☐ |
| API Key oluşturuldu | ☐ |
| `app.json`'a API Key yazıldı | ☐ |
| `npx expo run:android` çalıştırıldı | ☐ |

---

## Sorun Giderme

### Harita gri/boş görünüyor
- API Key yanlış veya eksik
- Maps SDK for Android aktive edilmemiş
- `npx expo run:android` ile rebuild yapılmamış

### `google-services.json` hatası
- Dosyanın proje kökünde (app.json ile aynı klasörde) olduğundan emin ol
- Package name'in `com.magiccarrepair.mobile` ile tam eşleştiğini kontrol et

### "API Key not valid" hatası
- Google Cloud Console'da Maps SDK for Android'in aktive olduğunu doğrula
- Birkaç dakika bekle (aktifleşmesi zaman alabilir)
