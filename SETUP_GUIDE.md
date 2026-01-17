# MagicCarRepairAISupported - Kurulum ve Konfigürasyon Rehberi

> **Versiyon**: 1.0  
> **Son Güncelleme**: 2024

---

## 📋 İçindekiler

1. [Gereksinimler](#gereksinimler)
2. [Kurulum Adımları](#kurulum-adımları)
3. [Veritabanı Yapılandırması](#veritabanı-yapılandırması)
4. [Konfigürasyon](#konfigürasyon)
5. [Seed Data](#seed-data)
6. [Development vs Production](#development-vs-production)
7. [Sorun Giderme](#sorun-giderme)

---

## 🔧 Gereksinimler

### Yazılım Gereksinimleri

- **.NET 9.0 SDK** veya üzeri
- **SQL Server** (LocalDB, Express, veya Full SQL Server)
- **Visual Studio 2022** veya **VS Code** (Opsiyonel, geliştirme için)
- **Redis** (Opsiyonel, cache için - production'da önerilir)

### .NET SDK Kurulumu

1. [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) indirin ve kurun
2. Kurulumu doğrulayın:
   ```bash
   dotnet --version
   ```
   Çıktı: `9.0.x` veya üzeri olmalı

### SQL Server Kurulumu

#### Seçenek 1: SQL Server LocalDB (Development)
- Visual Studio ile birlikte gelir
- Veya [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads) indirip kurun

#### Seçenek 2: SQL Server Express/Full (Production)
- [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads) indirin
- Kurulum sırasında "Mixed Mode Authentication" seçin
- Instance name'i not edin (genellikle `SQLEXPRESS` veya `MSSQLSERVER`)

### Redis Kurulumu (Opsiyonel)

#### Windows
1. [Redis for Windows](https://github.com/microsoftarchive/redis/releases) indirin
2. Veya Docker kullanın:
   ```bash
   docker run -d -p 6379:6379 redis
   ```

#### Linux
```bash
sudo apt-get install redis-server
sudo systemctl start redis
```

---

## 🚀 Kurulum Adımları

### 1. Projeyi İndirin

```bash
git clone <repository-url>
cd MagicCarRepairAISupported
```

### 2. Bağımlılıkları Yükleyin

```bash
dotnet restore
```

### 3. Veritabanı Bağlantı String'ini Ayarlayın

`Core.Packages.WebAPI/appsettings.json` dosyasını açın ve `ConnectionStrings` bölümünü düzenleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MagicCarRepairAISupportedDb;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

**SQL Server Express için:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=MagicCarRepairAISupportedDb;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

**SQL Server (Remote) için:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your-server-name;Database=MagicCarRepairAISupportedDb;User Id=your-username;Password=your-password;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

### 4. Migration'ları Uygulayın

```bash
# Migration'ları oluştur (eğer yeni migration varsa)
dotnet ef migrations add InitialCreate --project Core.Packages.Persistence --startup-project Core.Packages.WebAPI

# Veritabanını oluştur ve migration'ları uygula
dotnet ef database update --project Core.Packages.Persistence --startup-project Core.Packages.WebAPI
```

**Not:** İlk çalıştırmada veritabanı otomatik oluşturulur ve seed data yüklenir.

### 5. Uygulamayı Çalıştırın

```bash
cd Core.Packages.WebAPI
dotnet run
```

Veya Visual Studio'da:
- `Core.Packages.WebAPI` projesini startup project olarak ayarlayın
- F5 ile çalıştırın

### 6. Swagger UI'ı Kontrol Edin

Tarayıcıda şu adresi açın:
```
https://localhost:5001/swagger
```

veya

```
http://localhost:5000/swagger
```

---

## 🗄️ Veritabanı Yapılandırması

### Migration Yönetimi

#### Yeni Migration Oluşturma

```bash
dotnet ef migrations add MigrationName --project Core.Packages.Persistence --startup-project Core.Packages.WebAPI
```

#### Migration'ları Uygulama

```bash
dotnet ef database update --project Core.Packages.Persistence --startup-project Core.Packages.WebAPI
```

#### Belirli Bir Migration'a Geri Dönme

```bash
dotnet ef database update PreviousMigrationName --project Core.Packages.Persistence --startup-project Core.Packages.WebAPI
```

#### Tüm Migration'ları Geri Alma

```bash
dotnet ef database update 0 --project Core.Packages.Persistence --startup-project Core.Packages.WebAPI
```

### Veritabanı Yedekleme

```bash
# SQL Server Management Studio (SSMS) kullanarak
# Veya komut satırından:
sqlcmd -S (localdb)\mssqllocaldb -d MagicCarRepairAISupportedDb -Q "BACKUP DATABASE MagicCarRepairAISupportedDb TO DISK='C:\backup\MagicCarRepairAISupportedDb.bak'"
```

---

## ⚙️ Konfigürasyon

### appsettings.json

Ana konfigürasyon dosyası: `Core.Packages.WebAPI/appsettings.json`

#### ConnectionStrings

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MagicCarRepairAISupportedDb;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

#### TokenOptions (JWT)

```json
{
  "TokenOptions": {
    "Audience": "MagicCarRepairAISupported",
    "Issuer": "MagicCarRepairAISupported",
    "AccessTokenExpiration": 60,
    "SecurityKey": "MySuperSecretKeyThatShouldBeAtLeast32CharactersLong!123"
  }
}
```

**Önemli:** Production'da `SecurityKey` değerini güçlü ve gizli bir değerle değiştirin!

#### Redis (Opsiyonel)

```json
{
  "Redis": {
    "ConnectionString": "localhost:6379",
    "InstanceName": "MagicCarRepairAISupported",
    "UseSsl": false,
    "Password": ""
  }
}
```

**Production için:**
```json
{
  "Redis": {
    "ConnectionString": "your-redis-server:6379",
    "InstanceName": "MagicCarRepairAISupported",
    "UseSsl": true,
    "Password": "your-redis-password"
  }
}
```

#### AIOptions

```json
{
  "AIOptions": {
    "Provider": "Mock",
    "ApiKey": "",
    "AzureEndpoint": "",
    "AzureDeploymentName": "",
    "Model": "gpt-4-turbo-preview",
    "VisionModel": "gpt-4-vision-preview",
    "BaseUrl": "https://api.openai.com/v1",
    "MaxRetryAttempts": 3,
    "TimeoutSeconds": 60
  }
}
```

**OpenAI Kullanımı:**
```json
{
  "AIOptions": {
    "Provider": "OpenAI",
    "ApiKey": "sk-...",
    "Model": "gpt-4-turbo-preview",
    "VisionModel": "gpt-4-vision-preview",
    "BaseUrl": "https://api.openai.com/v1",
    "MaxRetryAttempts": 3,
    "TimeoutSeconds": 60
  }
}
```

**Azure OpenAI Kullanımı:**
```json
{
  "AIOptions": {
    "Provider": "AzureOpenAI",
    "ApiKey": "your-azure-api-key",
    "AzureEndpoint": "https://your-resource.openai.azure.com/",
    "AzureDeploymentName": "gpt-4",
    "Model": "gpt-4",
    "VisionModel": "gpt-4-vision",
    "MaxRetryAttempts": 3,
    "TimeoutSeconds": 60
  }
}
```

**Mock (Development):**
```json
{
  "AIOptions": {
    "Provider": "Mock"
  }
}
```

Mock provider kullanıldığında AI servisleri gerçek API çağrısı yapmaz, test verileri döner.

### appsettings.Development.json

Development ortamı için özel ayarlar:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MagicCarRepairAISupportedDb_Dev;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

### Environment Variables (Production)

Production'da hassas bilgileri environment variable olarak ayarlayın:

```bash
# Windows
set ConnectionStrings__DefaultConnection="Server=prod-server;Database=MagicCarRepairAISupportedDb;..."
set TokenOptions__SecurityKey="your-super-secret-key-here"
set AIOptions__ApiKey="sk-..."

# Linux/Mac
export ConnectionStrings__DefaultConnection="Server=prod-server;Database=MagicCarRepairAISupportedDb;..."
export TokenOptions__SecurityKey="your-super-secret-key-here"
export AIOptions__ApiKey="sk-..."
```

---

## 🌱 Seed Data

### Otomatik Seed Data

İlk migration uygulandığında otomatik olarak seed data yüklenir:

1. **Client (Tenant) Data**
   - Demo Client (ClientId: 1)
   - Test Client (ClientId: 2)

2. **Error Messages**
   - 60+ hata mesajı
   - 3 dilde (TR, EN, AR)

3. **Permissions**
   - Global permissions (ClientId: 0)
   - Tüm CRUD işlemleri için izinler

4. **Roles**
   - Her client için örnek roller

5. **Users**
   - Demo client için örnek kullanıcılar

### Seed Data Kontrolü

Veritabanında seed data'nın yüklenip yüklenmediğini kontrol edin:

```sql
-- Client kontrolü
SELECT * FROM Clients;

-- Error message kontrolü
SELECT COUNT(*) FROM ErrorMessages;

-- Permission kontrolü
SELECT COUNT(*) FROM Permissions;
```

### Seed Data'yı Yeniden Yükleme

Eğer seed data'yı yeniden yüklemek isterseniz:

1. Veritabanını silin:
   ```bash
   dotnet ef database drop --project Core.Packages.Persistence --startup-project Core.Packages.WebAPI
   ```

2. Migration'ları tekrar uygulayın:
   ```bash
   dotnet ef database update --project Core.Packages.Persistence --startup-project Core.Packages.WebAPI
   ```

---

## 🔄 Development vs Production

### Development Ortamı

**Özellikler:**
- LocalDB kullanımı
- Mock AI services
- Detaylı logging
- Swagger UI aktif
- CORS açık (AllowAllOrigins)

**appsettings.Development.json:**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
    }
  }
}
```

### Production Ortamı

**Özellikler:**
- Full SQL Server
- Real AI services (OpenAI/Azure)
- Minimal logging
- Swagger UI kapalı (opsiyonel)
- CORS kısıtlamalı
- HTTPS zorunlu

**appsettings.Production.json:**
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-server;Database=MagicCarRepairAISupportedDb;..."
  },
  "AIOptions": {
    "Provider": "OpenAI",
    "ApiKey": "${AI_API_KEY}"
  }
}
```

**Production Checklist:**
- [ ] `SecurityKey` değiştirildi
- [ ] `ConnectionString` production server'a ayarlandı
- [ ] `AIOptions:ApiKey` ayarlandı (eğer AI kullanılacaksa)
- [ ] `Redis:ConnectionString` ayarlandı (eğer Redis kullanılacaksa)
- [ ] HTTPS zorunlu yapıldı
- [ ] CORS policy kısıtlandı
- [ ] Swagger UI kapatıldı (opsiyonel)
- [ ] Logging seviyesi ayarlandı
- [ ] Environment variables ayarlandı

---

## 🐛 Sorun Giderme

### Veritabanı Bağlantı Sorunları

**Hata:** `Cannot open database "MagicCarRepairAISupportedDb"`

**Çözüm:**
1. SQL Server'ın çalıştığından emin olun
2. Connection string'i kontrol edin
3. Veritabanının oluşturulduğundan emin olun:
   ```bash
   dotnet ef database update --project Core.Packages.Persistence --startup-project Core.Packages.WebAPI
   ```

### Migration Sorunları

**Hata:** `There is already an object named 'X' in the database`

**Çözüm:**
1. Migration geçmişini kontrol edin:
   ```bash
   dotnet ef migrations list --project Core.Packages.Persistence --startup-project Core.Packages.WebAPI
   ```
2. Eğer migration'lar uygulanmışsa, yeni migration oluşturmayın
3. Eğer migration'lar uygulanmamışsa:
   ```bash
   dotnet ef database update --project Core.Packages.Persistence --startup-project Core.Packages.WebAPI
   ```

### Port Çakışması

**Hata:** `Address already in use`

**Çözüm:**
1. `launchSettings.json` dosyasında port değiştirin
2. Veya çalışan uygulamayı durdurun

### Redis Bağlantı Sorunları

**Hata:** `Redis connection failed`

**Çözüm:**
1. Redis'in çalıştığından emin olun:
   ```bash
   redis-cli ping
   ```
   Yanıt: `PONG` olmalı
2. Connection string'i kontrol edin
3. Redis kullanmıyorsanız, `AddDistributedMemoryCache` kullanılır (fallback)

### AI Service Sorunları

**Hata:** `AI service unavailable`

**Çözüm:**
1. `AIOptions:Provider` değerini kontrol edin
2. `AIOptions:ApiKey` ayarlı mı kontrol edin (Mock değilse)
3. Mock provider kullanıyorsanız sorun olmaz
4. API key'in geçerli olduğundan emin olun

### Multi-Tenant Sorunları

**Hata:** `CLIENT_ID_REQUIRED`

**Çözüm:**
1. Her istekte `X-Client-Id` header'ı gönderin
2. Veya JWT token içinde `ClientId` claim'i olduğundan emin olun

---

## 📦 Build ve Deploy

### Build

```bash
dotnet build Core.Packages.sln
```

### Publish (Production)

```bash
dotnet publish Core.Packages.WebAPI/Core.Packages.WebAPI.csproj -c Release -o ./publish
```

### Docker (Opsiyonel)

Dockerfile örneği:

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Core.Packages.WebAPI/Core.Packages.WebAPI.csproj", "Core.Packages.WebAPI/"]
# ... diğer projeler
RUN dotnet restore "Core.Packages.WebAPI/Core.Packages.WebAPI.csproj"
COPY . .
WORKDIR "/src/Core.Packages.WebAPI"
RUN dotnet build "Core.Packages.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Core.Packages.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Core.Packages.WebAPI.dll"]
```

---

## ✅ Kurulum Doğrulama

### 1. Veritabanı Kontrolü

```sql
-- Tabloların oluşturulduğunu kontrol et
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';

-- Seed data kontrolü
SELECT COUNT(*) FROM Clients;
SELECT COUNT(*) FROM ErrorMessages;
```

### 2. API Kontrolü

```bash
# Swagger UI'ı aç
https://localhost:5001/swagger

# Health check (eğer varsa)
curl https://localhost:5001/api/health
```

### 3. Authentication Kontrolü

```bash
# Login test
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@example.com","password":"password"}'
```

### 4. Multi-Tenant Kontrolü

```bash
# Client ID ile istek
curl -X GET https://localhost:5001/api/customers \
  -H "Authorization: Bearer {token}" \
  -H "X-Client-Id: 1"
```

---

## 🔐 Güvenlik Notları

### Production Checklist

- [ ] `SecurityKey` güçlü ve gizli bir değer
- [ ] `ConnectionString` production veritabanına ayarlı
- [ ] HTTPS zorunlu
- [ ] CORS policy kısıtlandı
- [ ] Swagger UI kapatıldı (opsiyonel)
- [ ] Environment variables kullanılıyor
- [ ] Logging seviyesi uygun ayarlandı
- [ ] Error messages production'da detaylı bilgi içermiyor

---

## 📞 Destek

Sorun yaşarsanız:

1. `GAPS_AND_ROADMAP.md` dosyasına bakın
2. `USER_MANUAL.md` dosyasına bakın
3. `API_DOCUMENTATION.md` dosyasına bakın
4. Log dosyalarını kontrol edin

---

**Son Güncelleme**: 2024
