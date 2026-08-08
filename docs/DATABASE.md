# PostgreSQL (Docker)

Local development uses PostgreSQL 16 in Docker.

## Bootstrap hesap (yalnızca boş DB)

| Alan | Değer |
|------|--------|
| E-posta | `admin@magiccar.com` |
| Şifre | `Admin123!` |
| Rol | SystemAdmin (`UserType` = 1) |

**Development:** API ilk açılışta `Customers` tablosu boşsa demo veriler (müşteri, araç, iş emri, parça, extended demo) ve `manager@magiccar.com` otomatik eklenir (`DatabaseSeed:AutoSeedDemoData`, varsayılan `true` Development’ta). Manuel: `POST /api/SeedData/run` (admin JWT).

## Quick start

```powershell
cd c:\Projects\MagicCarRepairAISupported

# Start database
docker compose up -d

# Drop volume, recreate DB, apply migrations
# (.ps1 dosyasına çift tıklamayın — Notepad açar; aşağıdaki yollardan birini kullanın)
.\scripts\reset-database.cmd
# veya:
# powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\reset-database.ps1

# Run API (applies seed on startup)
dotnet run --project Core.Packages.WebAPI\MagicCarRepairAISupported.WebAPI.csproj
```

**Not:** Explorer’da `reset-database.ps1`’e çift tıklamak veya dosyayı “açmak” genelde Notepad ile açar; script çalışmaz. Mutlaka **PowerShell / Terminal** içinden `reset-database.cmd` veya `powershell -File ...` kullanın.

## Connection string

Default in `Core.Packages.WebAPI/appsettings.json`:

```
Host=localhost;Port=5434;Database=MagicCarRepairDb;Username=magiccar;Password=magiccar_dev;Include Error Detail=true
```

Override with environment variable `ConnectionStrings__DefaultConnection`.

## Docker env

Copy `.env.docker.example` to `.env` to customize credentials (optional).

## Migrations

```powershell
dotnet ef migrations add YourMigrationName `
  --project Core.Packages.Persistence\MagicCarRepairAISupported.Persistence.csproj `
  --startup-project Core.Packages.WebAPI\MagicCarRepairAISupported.WebAPI.csproj

dotnet ef database update `
  --project Core.Packages.Persistence\MagicCarRepairAISupported.Persistence.csproj `
  --startup-project Core.Packages.WebAPI\MagicCarRepairAISupported.WebAPI.csproj
```
