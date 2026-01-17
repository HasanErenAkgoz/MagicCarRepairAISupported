# Yüklenecek Paketler

Bu dosya, mobil uygulama geliştirmeye başlamadan önce yüklenecek paketleri listeler.

## 📦 Gerekli Paketler

### State Management
```bash
npm install @reduxjs/toolkit react-redux
```

### Navigation
```bash
npm install @react-navigation/native @react-navigation/native-stack @react-navigation/bottom-tabs
npm install react-native-screens react-native-safe-area-context
```

### HTTP Client
```bash
npm install axios
```

### Storage
```bash
npm install @react-native-async-storage/async-storage
```

### UI Components (Opsiyonel - Birini seç)
```bash
# React Native Paper (Material Design)
npm install react-native-paper react-native-vector-icons

# VEYA React Native Elements
npm install react-native-elements react-native-vector-icons
```

### Charts (Opsiyonel)
```bash
npm install react-native-chart-kit react-native-svg
```

### Image Picker
```bash
npm install expo-image-picker
```

### Barcode Scanner
```bash
npm install expo-barcode-scanner
```

### Date Picker
```bash
npm install @react-native-community/datetimepicker
```

## 🚀 Tek Komutla Tümünü Yükle

```bash
npm install @reduxjs/toolkit react-redux @react-navigation/native @react-navigation/native-stack @react-navigation/bottom-tabs react-native-screens react-native-safe-area-context axios @react-native-async-storage/async-storage react-native-paper react-native-vector-icons react-native-chart-kit react-native-svg expo-image-picker expo-barcode-scanner @react-native-community/datetimepicker
```

## 📝 Notlar

- Paketler yüklendikten sonra `npx expo install` komutunu çalıştır (Expo uyumluluğu için)
- iOS için: `cd ios && pod install` (macOS gerekli)
- Android için: Genellikle otomatik çalışır
