# Mobil Uygulama Repository Stratejisi

> **Sorun**: Mobil uygulama için mevcut repository'den mi devam edelim yoksa ayrı repository mi açalım?

---

## 🎯 İKİ SEÇENEK

### Seçenek 1: Monorepo (Tek Repository) ✅ ÖNERİLEN

**Yapı:**
```
MagicCarRepairAISupported/
├── Core.Packages.Application/      # Backend (.NET)
├── Core.Packages.Domain/           # Backend (.NET)
├── Core.Packages.Infrastructure/   # Backend (.NET)
├── Core.Packages.Persistence/      # Backend (.NET)
├── Core.Packages.WebAPI/           # Backend (.NET)
├── magic-car-repair-mobile/        # Mobil (React Native)
│   ├── src/
│   ├── App.tsx
│   ├── package.json
│   └── ...
└── docs/                           # Dokümantasyon
```

### Seçenek 2: Multi-Repo (Ayrı Repository)

**Yapı:**
```
GitHub:
├── MagicCarRepairAISupported/      # Backend repo
└── MagicCarRepairAISupported-Mobile/  # Mobil repo
```

---

## 📊 KARŞILAŞTIRMA TABLOSU

| Özellik | Monorepo (Tek Repo) | Multi-Repo (Ayrı Repo) |
|---------|---------------------|------------------------|
| **Kod Paylaşımı** | ✅ Kolay (shared types, docs) | ❌ Zor (copy-paste veya npm package) |
| **Senkronizasyon** | ✅ Otomatik (aynı commit) | ❌ Manuel (API değişiklikleri) |
| **CI/CD** | ⚠️ Tek pipeline (daha karmaşık) | ✅ Ayrı pipeline'lar (daha basit) |
| **Build Süresi** | ⚠️ Uzun (tüm proje build) | ✅ Kısa (sadece ilgili proje) |
| **Git History** | ✅ Tek history (kolay takip) | ❌ Ayrı history (takip zor) |
| **Dependency Yönetimi** | ✅ Tek yerden yönetim | ❌ Ayrı yönetim |
| **Takım Çalışması** | ⚠️ Çakışma riski (aynı repo) | ✅ Az çakışma (ayrı repo) |
| **Versiyonlama** | ✅ Tek versiyon | ❌ Ayrı versiyonlar |
| **Deployment** | ⚠️ Birlikte deploy | ✅ Bağımsız deploy |

---

## ✅ ÖNERİ: MONOREPO (Tek Repository)

### Neden Monorepo?

#### 1. **Kod Paylaşımı Kolay**
```
magic-car-repair-mobile/
├── src/
│   ├── types/
│   │   └── api.types.ts    # Backend API types (JULES_BACKEND_DOCUMENTATION.md'den)
│   └── ...
└── docs/
    ├── API_DOCUMENTATION.md
    └── JULES_BACKEND_DOCUMENTATION.md
```

#### 2. **Senkronizasyon Otomatik**
- Backend API değiştiğinde → Mobil uygulama hemen görebilir
- Dokümantasyon tek yerde → Güncelleme kolay
- Shared types → Tek yerden yönetim

#### 3. **Git History Tek Yerde**
```
Commit: "Add WorkOrder API endpoint"
├── Backend: Core.Packages.WebAPI/Controllers/WorkOrdersController.cs
└── Mobile: magic-car-repair-mobile/src/services/api/workOrder.service.ts
```
→ İlişkili değişiklikleri tek commit'te görebilirsin

#### 4. **Dokümantasyon Paylaşımı**
```
MagicCarRepairAISupported/
├── API_DOCUMENTATION.md           # Backend API docs
├── JULES_BACKEND_DOCUMENTATION.md # Jules için backend docs
├── CURSOR_SONNET_PROMPT.md        # Cursor için prompt
└── magic-car-repair-mobile/
    └── README.md                   # Mobil uygulama docs
```

#### 5. **CI/CD Basitleştirme**
```yaml
# .github/workflows/ci.yml
- name: Build Backend
  run: dotnet build
  
- name: Build Mobile
  run: cd magic-car-repair-mobile && npm install && npm run build
```

---

## 🚀 MONOREPO YAPISI (ÖNERİLEN)

```
MagicCarRepairAISupported/
├── Core.Packages.Application/      # Backend (.NET)
├── Core.Packages.Domain/           # Backend (.NET)
├── Core.Packages.Infrastructure/   # Backend (.NET)
├── Core.Packages.Persistence/      # Backend (.NET)
├── Core.Packages.WebAPI/           # Backend (.NET)
│
├── magic-car-repair-mobile/        # 🆕 Mobil Uygulama
│   ├── src/
│   │   ├── screens/
│   │   ├── components/
│   │   ├── services/
│   │   ├── navigation/
│   │   └── types/
│   ├── assets/
│   │   └── designs/                # Stitch tasarımları
│   ├── App.tsx
│   ├── package.json
│   ├── tsconfig.json
│   ├── app.json
│   └── README.md
│
├── docs/                           # Dokümantasyon
│   ├── API_DOCUMENTATION.md
│   ├── JULES_BACKEND_DOCUMENTATION.md
│   ├── CURSOR_SONNET_PROMPT.md
│   └── STITCH_MOBILE_APP_PROMPTS.md
│
├── Core.Packages.sln               # Backend solution
└── README.md                       # Ana README
```

---

## 📝 MONOREPO AVANTAJLARI (Senin Durumun İçin)

### 1. **Backend API Değişiklikleri**
```
Backend'de API değişti:
POST /api/workorders → POST /api/work-orders

Monorepo'da:
✅ Mobil uygulama hemen görebilir
✅ Tek commit'te her ikisini de güncelleyebilirsin
✅ Dokümantasyon tek yerde güncellenir

Multi-Repo'da:
❌ Mobil repo'yu manuel güncellemek gerekir
❌ İki ayrı commit yapmak gerekir
❌ Senkronizasyon sorunları olabilir
```

### 2. **Dokümantasyon Paylaşımı**
```
Monorepo'da:
✅ API_DOCUMENTATION.md → Her ikisi de kullanabilir
✅ JULES_BACKEND_DOCUMENTATION.md → Mobil geliştirme için hazır
✅ CURSOR_SONNET_PROMPT.md → Mobil geliştirme için hazır

Multi-Repo'da:
❌ Dokümantasyonu kopyalamak gerekir
❌ Güncellemeleri manuel senkronize etmek gerekir
```

### 3. **Type Definitions**
```
Monorepo'da:
✅ Backend API types → Mobil uygulamada kullanılabilir
✅ Shared types → Tek yerden yönetim

Multi-Repo'da:
❌ Types'ı manuel kopyalamak gerekir
❌ Senkronizasyon sorunları
```

---

## ⚠️ MONOREPO DEZAVANTAJLARI

### 1. **Build Süresi**
- Backend build → ~30 saniye
- Mobil build → ~60 saniye
- Toplam → ~90 saniye

**Çözüm:** CI/CD'de conditional build kullan:
```yaml
- name: Build Backend
  if: contains(github.event.head_commit.message, 'backend')
  
- name: Build Mobile
  if: contains(github.event.head_commit.message, 'mobile')
```

### 2. **Repository Büyüklüğü**
- Backend: ~50 MB
- Mobil: ~100 MB (node_modules)
- Toplam: ~150 MB

**Çözüm:** `.gitignore` ile `node_modules` ignore et

### 3. **Git Çakışmaları**
- Aynı repo'da çalışırken çakışma riski

**Çözüm:** Branch stratejisi kullan:
```
main
├── backend/
│   └── feature/new-api
└── mobile/
    └── feature/new-screen
```

---

## 🎯 SONUÇ VE ÖNERİ

### ✅ ÖNERİLEN: MONOREPO (Tek Repository)

**Neden?**
1. ✅ Kod paylaşımı kolay
2. ✅ Senkronizasyon otomatik
3. ✅ Dokümantasyon tek yerde
4. ✅ Git history tek yerde
5. ✅ Tek commit'te ilgili değişiklikler

**Ne Zaman Multi-Repo?**
- Farklı takımlar çalışıyorsa
- Bağımsız deployment gerekiyorsa
- Repository çok büyükse (>500 MB)
- Farklı teknoloji stack'ler (örn: Flutter + React Native)

---

## 🚀 UYGULAMA ADIMLARI

### Adım 1: Monorepo Yapısını Oluştur

```bash
# Mevcut repository'de
mkdir magic-car-repair-mobile
cd magic-car-repair-mobile

# Expo projesi oluştur
npx create-expo-app@latest . --template blank-typescript
```

### Adım 2: .gitignore Güncelle

```gitignore
# Backend
bin/
obj/
*.user
*.suo

# Mobile
magic-car-repair-mobile/node_modules/
magic-car-repair-mobile/.expo/
magic-car-repair-mobile/dist/
magic-car-repair-mobile/.expo-shared/
```

### Adım 3: README Güncelle

```markdown
# MagicCarRepairAI - Oto Servis Yönetim Sistemi

## Proje Yapısı

- `Core.Packages.*/` - Backend (.NET Core)
- `magic-car-repair-mobile/` - Mobil Uygulama (React Native)
- `docs/` - Dokümantasyon
```

### Adım 4: CI/CD Güncelle (Opsiyonel)

```yaml
# .github/workflows/ci.yml
name: CI

on: [push, pull_request]

jobs:
  backend:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Build Backend
        run: dotnet build Core.Packages.sln
  
  mobile:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup Node
        uses: actions/setup-node@v3
      - name: Build Mobile
        run: |
          cd magic-car-repair-mobile
          npm install
          npm run build
```

---

## 💡 SONUÇ

**Öneri: Monorepo (Tek Repository)**

Senin durumun için en mantıklısı:
- ✅ Tek kişi/ küçük takım
- ✅ Backend ve mobil birlikte geliştirilecek
- ✅ Dokümantasyon paylaşımı önemli
- ✅ API değişiklikleri sık olacak

**Başlangıç:**
1. Mevcut repository'de `magic-car-repair-mobile/` klasörü oluştur
2. Expo projesi oluştur
3. `.gitignore` güncelle
4. README güncelle

**Başarılar! 🚀**
