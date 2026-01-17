# JULES.GOOGLE PROMPT - MagicCarRepairAI Mobil Uygulama

> **Bu dosya Google Jules (jules.google) için hazırlanmış prompt'tur.**  
> **Jules**: GitHub entegreli kodlama ajanı - Kod yazma, test yazma, bug fix, feature ekleme

---

## 🎯 JULES İÇİN ÖNEMLİ BİLGİLER

### Jules Nedir?
- Google'ın **autonomous coding agent**'ı
- GitHub ile entegre çalışır
- Kod yazma, test yazma, bug fix, feature ekleme yapar
- Plan üretir, PR açar, sen onaylarsın
- **ÖNEMLİ**: Tasarımdan direkt mobil uygulama üretmez, kod yazar

### Jules'in Güçlü Yönleri:
✅ Kod yazma (React Native, TypeScript)
✅ Test yazma
✅ Bug fix
✅ API entegrasyonu
✅ GitHub PR oluşturma
✅ Kod kalitesi kontrolü

### Jules'in Sınırlamaları:
⚠️ Tasarımdan direkt mobil uygulama üretmez
⚠️ Build/APK/IPA oluşturmaz (kod yazar, sen build edersin)
⚠️ Görsel tasarım analizi sınırlı

---

## 🚀 JULES İLE ÇALIŞMA STRATEJİSİ

### Yaklaşım 1: Tasarım → Kod → Jules (ÖNERİLEN)

1. **Stitch tasarımlarını al** (PNG veya Figma)
2. **Locofy.ai veya Cursor ile temel kod üret** (UI component'leri)
3. **Jules'e ver**: "Bu React Native kodunu geliştir, backend API'leri entegre et"

### Yaklaşım 2: Sıfırdan Jules ile Başla

1. **Jules'e detaylı prompt ver** (bu dosyadaki prompt)
2. **Jules kod yazsın** (React Native + Backend entegrasyonu)
3. **Sen build et** (Expo EAS Build veya React Native CLI)

---

## 📝 JULES'E VERİLECEK PROMPT

### Başlangıç Prompt'u:

```
Sen bir React Native (Expo) mobil uygulama geliştirme uzmanısın. 
MagicCarRepairAI - Oto Servis Yönetim Sistemi için mobil uygulama geliştiriyoruz.

PROJE BİLGİLERİ:
- Proje: Oto Servis Yönetim Sistemi
- Platform: React Native (Expo SDK 50+)
- Backend: .NET Core Web API
- Base URL: http://localhost:5000/api
- Authentication: JWT Bearer Token
- Multi-Tenant: X-Client-Id header gerekli

GÖREV:
1. Expo projesi oluştur (npx create-expo-app)
2. Gerekli paketleri yükle (package.json'a ekle)
3. Proje yapısını kur (src/screens, src/components, src/services, vb.)
4. Backend API'leri ile entegrasyon yap
5. Modern, kullanıcı dostu tasarım uygula

BAŞLANGIÇ:
Phase 1: Setup & Authentication modülünden başla.
Tüm dosyaları oluştur, backend API'leri entegre et, test et.

Detaylı gereksinimler için CURSOR_SONNET_PROMPT.md dosyasına bak.
```

---

## 🎨 TASARIM GEREKSİNİMLERİ (Jules'e Verilecek)

### Renk Paleti:
- Primary (Mavi): #3B82F6 → #2563EB (Gradient) - %55 ağırlık
- Accent (Turuncu): #FF6B35 → #FF8C42 (Gradient) - %20 ağırlık
- Background: #F9FAFB
- Surface: #FFFFFF
- Text Primary: #1A1A1A
- Success: #10B981
- Error: #EF4444
- Warning: #F59E0B

### Tasarım Prensipleri:
- Modern, kullanıcı dostu
- Glassmorphism efektleri
- Gradient backgrounds
- Smooth animations (60fps)
- Card-based design
- Dark mode desteği

---

## 📁 PROJE YAPISI (Jules Oluşturmalı)

```
magic-car-repair-mobile/
├── src/
│   ├── screens/
│   ├── components/
│   ├── services/
│   ├── store/
│   ├── navigation/
│   ├── theme/
│   └── types/
├── App.tsx
├── package.json
├── tsconfig.json
└── app.json
```

---

## 🔐 PHASE 1: AUTHENTICATION (Jules'e Verilecek Detaylı Prompt)

```
PHASE 1: Authentication Modülü

1. Expo projesi oluştur:
   - npx create-expo-app magic-car-repair-mobile --template blank-typescript
   - Gerekli paketleri yükle (package.json'a ekle)

2. API Client Setup (src/services/api/client.ts):
   - Axios instance oluştur
   - Base URL: http://localhost:5000/api
   - Request interceptor: Token ve X-Client-Id ekle
   - Response interceptor: 401 durumunda logout

3. Auth Service (src/services/api/auth.service.ts):
   - login(email, password)
   - register(data)
   - forgotPassword(email)
   - resetPassword(email, token, newPassword)

4. Redux Store Setup:
   - Redux Toolkit kur
   - authSlice oluştur
   - login, register, logout actions
   - Token AsyncStorage'a kaydet

5. Login Screen (src/screens/Auth/LoginScreen.tsx):
   - Modern tasarım (glassmorphism, gradient)
   - Email input (ikon, placeholder)
   - Password input (göz ikonu ile göster/gizle)
   - "Beni Hatırla" checkbox
   - "Şifremi Unuttum" linki
   - "Giriş Yap" butonu (turuncu gradient)
   - Loading state
   - Error handling
   - Animations (fade in, slide up)

6. Register Screen (src/screens/Auth/RegisterScreen.tsx):
   - Multi-step form (3 adım)
   - Progress indicator
   - Form validation

7. Navigation Setup:
   - React Navigation kur
   - AppNavigator (auth check)
   - AuthNavigator (login, register)
   - MainNavigator (bottom tabs)

8. Theme Setup:
   - colors.ts (renk paleti)
   - typography.ts (font sizes)
   - spacing.ts (padding, margin)

BACKEND API:
POST /api/auth/login
Request: { email, password }
Response: { success, data: { token, user } }

POST /api/auth/register
Request: { email, password, firstName, lastName, clientId, userType }
Response: { success, data: { token, user } }

KOD STANDARTLARI:
- TypeScript kullan
- Functional components
- Hooks (useState, useEffect, useSelector, useDispatch)
- Error handling (try-catch)
- Loading states
- Form validation

TEST:
- Login flow test et
- Token storage test et
- Navigation test et
```

---

## 📊 PHASE 2: DASHBOARD (Jules'e Verilecek)

```
PHASE 2: Dashboard Modülü

1. Dashboard API Service:
   GET /api/dashboard/statistics
   Response: { totalWorkOrders, activeWorkOrders, todayRevenue, pendingApprovals }

2. Admin Dashboard Screen:
   - Hoş geldiniz bölümü
   - 4 istatistik kartı (horizontal scrollable)
   - Grafikler (Line Chart, Pie Chart)
   - Son aktiviteler (timeline)
   - Pull-to-refresh

3. Redux Slice:
   - dashboardSlice oluştur
   - getStatistics action

4. Components:
   - StatCard component
   - LineChart component
   - PieChart component
   - ActivityTimeline component

TASARIM:
- Modern kartlar (glassmorphism)
- Gradient backgrounds
- Smooth animations
- Loading skeleton screens
```

---

## 🔧 PHASE 3: WORK ORDERS (Jules'e Verilecek)

```
PHASE 3: Work Orders Modülü

1. Work Order API Service:
   GET /api/workorders?pageNumber=1&pageSize=20&status=Active
   GET /api/workorders/{id}
   POST /api/workorders
   PUT /api/workorders/{id}/status
   DELETE /api/workorders/{id}

2. Work Order List Screen:
   - Modern arama bar
   - Filtre chips
   - Work order kartları
   - Swipe actions
   - Pull-to-refresh
   - Infinite scroll
   - FAB (Yeni İş Emri)

3. Work Order Detail Screen:
   - Tab navigation (Genel, Parçalar, Timeline, Fotoğraflar)
   - Müşteri bilgileri
   - Araç bilgileri
   - İş emri bilgileri
   - Quick actions

4. Create Work Order Screen:
   - Multi-step form (4 adım)
   - Müşteri & Araç seçimi
   - İş emri bilgileri
   - Fotoğraf ekleme
   - Özet & Onay

TASARIM:
- Modern kartlar
- Durum badge'leri (renkli)
- Swipe gestures
- Empty states
```

---

## 📋 JULES İÇİN IMPLEMENTATION PLAN

### Adım 1: Proje Setup
```
1. Expo projesi oluştur
2. Paketleri yükle
3. Proje yapısını kur
4. TypeScript config
5. ESLint/Prettier setup
```

### Adım 2: Core Infrastructure
```
1. API client setup
2. Redux store setup
3. Navigation setup
4. Theme system
5. Storage utilities
```

### Adım 3: Authentication
```
1. Auth API service
2. Auth Redux slice
3. Login Screen
4. Register Screen
5. Navigation guards
```

### Adım 4: Dashboard
```
1. Dashboard API service
2. Dashboard Screen
3. Charts integration
4. Statistics cards
```

### Adım 5: Work Orders
```
1. Work Order API service
2. List Screen
3. Detail Screen
4. Create Screen
```

### Adım 6: Diğer Modüller
```
1. Customers
2. Vehicles
3. Parts
4. Appointments
5. Invoices
```

---

## 🎯 JULES'E VERİLECEK ÖRNEK PROMPT (Tam)

```
Sen bir React Native (Expo) mobil uygulama geliştirme uzmanısın.

PROJE: MagicCarRepairAI - Oto Servis Yönetim Sistemi

GÖREV:
Bu GitHub repository'sinde (mevcut repo veya yeni repo) 
React Native (Expo) mobil uygulama geliştir.

TEKNİK GEREKSİNİMLER:
- Platform: React Native (Expo SDK 50+)
- Language: TypeScript
- State Management: Redux Toolkit
- Navigation: React Navigation
- HTTP Client: Axios
- Storage: AsyncStorage
- UI Library: React Native Paper

BACKEND API:
- Base URL: http://localhost:5000/api
- Authentication: JWT Bearer Token
- Multi-Tenant: X-Client-Id header

TASARIM GEREKSİNİMLERİ:
- Modern, kullanıcı dostu
- Renk Paleti: %55 Mavi (#3B82F6), %20 Turuncu (#FF6B35)
- Glassmorphism efektleri
- Gradient backgrounds
- Smooth animations
- Dark mode desteği

IMPLEMENTATION PLAN:
1. Expo projesi oluştur ve paketleri yükle
2. Proje yapısını kur (src/screens, components, services, store)
3. API client ve Redux store setup
4. Authentication modülü (Login, Register)
5. Dashboard modülü
6. Work Orders modülü
7. Diğer modüller (Customers, Vehicles, Parts, vb.)

DETAYLI GEREKSİNİMLER:
CURSOR_SONNET_PROMPT.md dosyasındaki tüm detayları takip et.

BAŞLANGIÇ:
Phase 1: Setup & Authentication'dan başla.
Tüm dosyaları oluştur, backend API'leri entegre et, test et.
PR oluştur ve onay için bekle.
```

---

## ✅ JULES İLE ÇALIŞMA ADIMLARI

### 1. GitHub Repository Hazırla
```
- Yeni repo oluştur: magic-car-repair-mobile
- Jules'e erişim ver
- README.md ekle
```

### 2. Jules'e Prompt Ver
```
- Yukarıdaki prompt'u Jules'e ver
- Plan oluşturmasını bekle
- Plan'ı kontrol et
- Onayla
```

### 3. Jules Kod Yazsın
```
- Jules otomatik kod yazar
- PR oluşturur
- Sen kontrol et
- Merge et
```

### 4. Test Et
```
- Expo ile test et
- Backend API'leri kontrol et
- Sorunları düzelt
```

### 5. Build Et
```
- Expo EAS Build ile APK/IPA oluştur
- Telefona yükle
```

---

## 🎯 JULES İÇİN ÖZEL NOTLAR

### Jules'in Güçlü Yönleri:
- ✅ Kod yazma (React Native, TypeScript)
- ✅ Test yazma
- ✅ API entegrasyonu
- ✅ GitHub PR oluşturma
- ✅ Kod kalitesi

### Jules'e Özel Talimatlar:
- Her modül için ayrı PR oluştur
- Test yaz (unit tests, integration tests)
- Error handling ekle
- Loading states ekle
- TypeScript strict mode kullan
- ESLint kurallarına uy
- Code comments ekle

---

## 📝 JULES'E VERİLECEK KISA PROMPT (Hızlı Başlangıç)

```
React Native (Expo) mobil uygulama geliştir:

PROJE: Oto Servis Yönetim Sistemi
BACKEND: .NET Core Web API (http://localhost:5000/api)
AUTH: JWT Bearer Token

GEREKSİNİMLER:
1. Expo projesi oluştur (TypeScript)
2. Redux Toolkit + React Navigation kur
3. Authentication modülü (Login, Register)
4. Dashboard modülü
5. Work Orders modülü

TASARIM: Modern, %55 Mavi + %20 Turuncu renk paleti

Detaylar: CURSOR_SONNET_PROMPT.md dosyasında.

Başla!
```

---

## 🚀 SONUÇ

**Jules kullanmak için:**

1. ✅ GitHub repository hazırla
2. ✅ Jules'e erişim ver
3. ✅ Yukarıdaki prompt'u ver
4. ✅ Plan'ı kontrol et ve onayla
5. ✅ Jules kod yazsın
6. ✅ Test et ve build et

**Jules'in Avantajları:**
- Otomatik kod yazma
- Test yazma
- GitHub PR oluşturma
- Kod kalitesi kontrolü

**Jules'in Sınırlamaları:**
- Tasarım analizi sınırlı
- Build/APK oluşturmaz (kod yazar)
- Manuel build gerekir

**Öneri:** Jules'i kod yazma için kullan, tasarım için Cursor + Claude Sonnet kullan!

---

**Başarılar! 🚀**
