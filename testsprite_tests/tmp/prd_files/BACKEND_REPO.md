# Backend API (MagicCarRepairAISupported)

Mobil uygulamanın konuştuğu .NET API ayrı repoda / klasörde:

| | |
|--|--|
| **Kök dizin** | `c:\Projects\MagicCarRepairAISupported` |
| **Web API projesi** | `Core.Packages.WebAPI\MagicCarRepairAISupported.WebAPI.csproj` |
| **Varsayılan URL** | `http://localhost:5169` (HTTP), `https://localhost:7216` (HTTPS) |
| **SignalR chat** | `{SERVER_BASE_URL}/hubs/chat` |

## Veritabanı (PostgreSQL + Docker)

```powershell
cd c:\Projects\MagicCarRepairAISupported
docker compose up -d
.\scripts\reset-database.cmd
```

Varsayılan bağlantı: `localhost:5434` (makinede 5432/5433 doluysa). Ayrıntılar: backend repo `docs/DATABASE.md`.

## Çalıştırma

```powershell
cd c:\Projects\MagicCarRepairAISupported
dotnet run --project Core.Packages.WebAPI\MagicCarRepairAISupported.WebAPI.csproj
```

Mobil tarafta aynı makinede geliştirme için `EXPO_PUBLIC_API_HOST` = bilgisayarın LAN IPv4 adresi (emülatörde otomatik `10.0.2.2`).

## Mimari (kısa)

- **Controllers:** `Core.Packages.WebAPI\Controllers\`
- **İş kuralları:** `Core.Packages.Application\Features\` (MediatR)
- **Çok kiracı:** `ITenantService` → JWT `ClientId` claim (+ isteğe bağlı `X-Client-Id` header)
- **JWT claim’leri:** `UserType`, `ClientId`, `Language`, roller/permission’lar

Yetkilendirme denetim listesi ve kod incelemesi özeti: [BACKEND_AUTH_AUDIT.md](./BACKEND_AUTH_AUDIT.md).
