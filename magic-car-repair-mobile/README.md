# MagicCarRepairAI - Mobil Uygulama

> **React Native (Expo) ile geliştirilmiş Oto Servis Yönetim Sistemi mobil uygulaması**

---

## 📱 Proje Bilgileri

- **Platform**: React Native (Expo SDK 54+)
- **Language**: TypeScript
- **State Management**: Redux Toolkit
- **Navigation**: React Navigation
- **HTTP Client**: Axios
- **Storage**: AsyncStorage
- **Backend API**: .NET Core Web API (`http://localhost:5000/api`)

---

## 🚀 Kurulum

### Gereksinimler

- Node.js 18+ 
- npm veya yarn
- Expo CLI (global): `npm install -g expo-cli`
- Expo Go app (telefonda test için)

### Kurulum Adımları

```bash
# Bağımlılıkları yükle
npm install

# Veya yarn kullanıyorsanız
yarn install
```

---

## 🏃 Çalıştırma

### Development Mode

```bash
# Expo development server'ı başlat
npm start

# Android emulator'da çalıştır
npm run android

# iOS simulator'da çalıştır (macOS gerekli)
npm run ios

# Web browser'da çalıştır
npm run web
```

### Telefonda Test Etme

1. Expo Go uygulamasını indir (App Store / Play Store)
2. `npm start` komutunu çalıştır
3. QR kodu telefonunla tara
4. Uygulama telefonunda açılacak

---

## 📁 Proje Yapısı

```
magic-car-repair-mobile/
├── src/
│   ├── screens/              # Ekranlar
│   │   ├── Auth/
│   │   ├── Dashboard/
│   │   ├── WorkOrders/
│   │   └── ...
│   ├── components/           # Reusable component'ler
│   ├── services/            # API servisleri
│   │   └── api/
│   ├── navigation/          # Navigation yapısı
│   ├── store/               # Redux store
│   ├── types/               # TypeScript type definitions
│   ├── utils/               # Utility fonksiyonlar
│   └── theme/               # Tema (renkler, typography)
├── assets/
│   └── designs/             # Stitch tasarım dosyaları
├── App.tsx                  # Ana component
├── package.json
└── tsconfig.json
```

---

## 🔌 Backend API Entegrasyonu

Backend API dokümantasyonu için:
- `../API_DOCUMENTATION.md` - Genel API dokümantasyonu
- `../JULES_BACKEND_DOCUMENTATION.md` - Jules için backend dokümantasyonu

### API Configuration

```typescript
// src/services/api/client.ts
export const API_CONFIG = {
  BASE_URL: 'http://localhost:5000/api', // Development
  // BASE_URL: 'https://api.magiccarrepair.com/api', // Production
};
```

---

## 🎨 Tasarım

Tasarım dosyaları `assets/designs/` klasöründe bulunur.

- Stitch tasarımları buraya eklenebilir
- Tasarım referansları için `../STITCH_MOBILE_APP_PROMPTS.md` dosyasına bak

---

## 📦 Gerekli Paketler

### Temel Paketler (Zaten Yüklü)
- `expo` - Expo framework
- `react` - React library
- `react-native` - React Native
- `typescript` - TypeScript

### Eklenmesi Gereken Paketler

```bash
# State Management
npm install @reduxjs/toolkit react-redux

# Navigation
npm install @react-navigation/native @react-navigation/native-stack @react-navigation/bottom-tabs
npm install react-native-screens react-native-safe-area-context

# HTTP Client
npm install axios

# Storage
npm install @react-native-async-storage/async-storage

# UI Components (Opsiyonel)
npm install react-native-paper
# veya
npm install react-native-elements

# Charts (Opsiyonel)
npm install react-native-chart-kit

# Image Picker
npm install expo-image-picker

# Barcode Scanner
npm install expo-barcode-scanner
```

---

## 🔐 Authentication

JWT Bearer Token kullanılır. Token AsyncStorage'a kaydedilir.

```typescript
// src/services/api/auth.service.ts
await authService.login({ email, password });
// Token otomatik olarak AsyncStorage'a kaydedilir
```

---

## 🏢 Multi-Tenant

Her istekte `X-Client-Id` header'ı gönderilir:

```typescript
// Otomatik olarak API client tarafından eklenir
headers: {
  'Authorization': `Bearer ${token}`,
  'X-Client-Id': clientId
}
```

---

## 📝 Geliştirme Notları

### Type Definitions

Backend API type definitions için `src/types/api.ts` dosyasına bak.
Bu dosya `JULES_BACKEND_DOCUMENTATION.md` dosyasındaki type definitions'a göre oluşturulacak.

### Error Handling

Tüm API çağrıları `try-catch` ile sarılmalı ve error handling utility kullanılmalı:

```typescript
import { showErrorAlert } from '../utils/errorHandler';

try {
  const data = await apiService.getData();
} catch (error) {
  showErrorAlert(error);
}
```

---

## 🧪 Test

```bash
# Test çalıştır (test framework eklendikten sonra)
npm test
```

---

## 📚 Dokümantasyon

- **Backend API**: `../API_DOCUMENTATION.md`
- **Jules Backend Docs**: `../JULES_BACKEND_DOCUMENTATION.md`
- **Cursor Sonnet Prompt**: `../CURSOR_SONNET_PROMPT.md`
- **Stitch Prompts**: `../STITCH_MOBILE_APP_PROMPTS.md`

---

## 🚀 Build & Deploy

### Development Build

```bash
# Expo development build
expo build:android
expo build:ios
```

### Production Build

```bash
# EAS Build (Expo Application Services)
eas build --platform android
eas build --platform ios
```

---

## 📞 Destek

Sorular için:
- Backend API: `../API_DOCUMENTATION.md`
- Backend Docs: `../JULES_BACKEND_DOCUMENTATION.md`

---

**Başarılar! 🚀**
