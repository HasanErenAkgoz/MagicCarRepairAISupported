# MagicCarRepairAI - Oto Servis Yönetim Sistemi

> **Tam kapsamlı, AI destekli oto servis yönetim sistemi - Backend (.NET Core) + Mobil Uygulama (React Native)**

---

## 📋 Proje Yapısı

Bu proje **monorepo** yapısında organize edilmiştir:

```
MagicCarRepairAISupported/
├── Core.Packages.Application/      # Backend - Application Layer (.NET)
├── Core.Packages.Domain/           # Backend - Domain Layer (.NET)
├── Core.Packages.Infrastructure/   # Backend - Infrastructure Layer (.NET)
├── Core.Packages.Persistence/     # Backend - Persistence Layer (.NET)
├── Core.Packages.WebAPI/           # Backend - Web API (.NET)
│
├── magic-car-repair-mobile/        # 🆕 Mobil Uygulama (React Native + Expo)
│   ├── src/
│   ├── assets/
│   └── package.json
│
└── docs/                            # Dokümantasyon
    ├── API_DOCUMENTATION.md
    ├── JULES_BACKEND_DOCUMENTATION.md
    ├── CURSOR_SONNET_PROMPT.md
    └── STITCH_MOBILE_APP_PROMPTS.md
```

---

## 🚀 Hızlı Başlangıç

### Backend (.NET Core)

```bash
# Solution'ı build et
dotnet build Core.Packages.sln

# Web API'yi çalıştır
cd Core.Packages.WebAPI
dotnet run
```

**Backend API**: `https://localhost:7216`  
**Swagger UI**: `https://localhost:7216/swagger`

### Mobil Uygulama (React Native)

```bash
# Mobil uygulama klasörüne git
cd magic-car-repair-mobile

# Bağımlılıkları yükle
npm install

# Development server'ı başlat
npm start
```

**Detaylar**: `magic-car-repair-mobile/README.md`

---

## 📚 Dokümantasyon

### Backend Dokümantasyonu
- **API Dokümantasyonu**: [`API_DOCUMENTATION.md`](API_DOCUMENTATION.md)
- **Jules Backend Docs**: [`JULES_BACKEND_DOCUMENTATION.md`](JULES_BACKEND_DOCUMENTATION.md)
- **Setup Guide**: [`SETUP_GUIDE.md`](SETUP_GUIDE.md)

### Mobil Uygulama Dokümantasyonu
- **Mobil Uygulama README**: [`magic-car-repair-mobile/README.md`](magic-car-repair-mobile/README.md)
- **Cursor Sonnet Prompt**: [`CURSOR_SONNET_PROMPT.md`](CURSOR_SONNET_PROMPT.md)
- **Stitch Prompts**: [`STITCH_MOBILE_APP_PROMPTS.md`](STITCH_MOBILE_APP_PROMPTS.md)

### Geliştirme Dokümantasyonu
- **Repository Stratejisi**: [`MOBILE_REPOSITORY_STRATEGY.md`](MOBILE_REPOSITORY_STRATEGY.md)
- **Project Status**: [`PROJECT_STATUS.md`](PROJECT_STATUS.md)
- **Roadmap**: [`ROADMAP.md`](ROADMAP.md)

---

## 🏗️ Mimari

### Backend (Clean Architecture)
- **Domain**: Entities, Enums, Interfaces, Repositories
- **Application**: Features (CQRS), Services, DTOs
- **Infrastructure**: Services Implementation, Middleware
- **Persistence**: DbContext, Configurations, Seed Data, Repositories
- **WebAPI**: Controllers, Program.cs

### Mobil Uygulama (React Native)
- **Screens**: Ekranlar (Auth, Dashboard, WorkOrders, vb.)
- **Components**: Reusable component'ler
- **Services**: API servisleri
- **Navigation**: React Navigation
- **Store**: Redux Toolkit
- **Types**: TypeScript type definitions

---

## 🔐 Authentication

- **Backend**: JWT Bearer Token
- **Mobil**: Token AsyncStorage'a kaydedilir
- **Multi-Tenant**: `X-Client-Id` header ile

---

## 🏢 Multi-Tenant Yapı

Her Client (oto servis) izole veriye sahiptir:
- Global Query Filters otomatik `WHERE ClientId = X` ekler
- Her istekte `X-Client-Id` header'ı gerekli
- Token içinde `ClientId` claim'i varsa header'a gerek yok

---

## 🎨 Özellikler

### Backend
- ✅ Multi-Tenant Architecture
- ✅ CQRS Pattern (MediatR)
- ✅ Clean Architecture
- ✅ AI Services (OpenAI/Azure OpenAI)
- ✅ SignalR Real-time Notifications
- ✅ Multi-language Support (TR, EN, AR)
- ✅ Role-Permission System
- ✅ File Upload Service
- ✅ Barcode System

### Mobil Uygulama
- ✅ React Native (Expo)
- ✅ TypeScript
- ✅ Redux Toolkit
- ✅ React Navigation
- ✅ Axios API Client
- ✅ AsyncStorage

---

## 📦 Teknolojiler

### Backend
- .NET 9.0
- Entity Framework Core
- MediatR (CQRS)
- AutoMapper
- Swagger/OpenAPI
- SignalR
- Redis (Cache)
- Azure Blob Storage (File Storage)

### Mobil
- React Native 0.81.5
- Expo SDK 54+
- TypeScript 5.9+
- Redux Toolkit
- React Navigation
- Axios

---

## 🧪 Test

### Backend Tests
```bash
dotnet test Core.Packages.Application.Tests
dotnet test Core.Packages.WebAPI.Tests
```

### Mobil Tests
```bash
cd magic-car-repair-mobile
npm test
```

---

## 📝 Geliştirme Notları

### Backend
- Clean Architecture prensiplerine uygun
- CQRS pattern kullanılıyor
- Multi-tenant yapı aktif
- Global query filters otomatik çalışıyor

### Mobil
- Backend API dokümantasyonuna göre geliştiriliyor
- Type definitions `JULES_BACKEND_DOCUMENTATION.md` dosyasından alınacak
- Stitch tasarımları `assets/designs/` klasörüne eklenecek

---

## 🚀 Deployment

### Backend
- IIS / Azure App Service
- Docker container
- Kubernetes

### Mobil
- Expo EAS Build
- Google Play Store
- Apple App Store

---

## 📞 Destek

Sorular için dokümantasyon dosyalarına bak:
- Backend API: `API_DOCUMENTATION.md`
- Mobil Geliştirme: `magic-car-repair-mobile/README.md`
- Setup: `SETUP_GUIDE.md`

---

## 📄 Lisans

Bu proje lisanslıdır. Detaylar için `LICENSE.txt` dosyasına bak.

---

**Başarılar! 🚀**
