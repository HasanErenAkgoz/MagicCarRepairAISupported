# Secrets and local configuration

Never commit real API keys, SMTP passwords, or JWT signing keys. Use **User Secrets** (recommended) or a local `appsettings.Development.json` (gitignored).

## Quick setup (development)

```powershell
cd c:\Projects\MagicCarRepairAISupported
Copy-Item Core.Packages.WebAPI\appsettings.Development.example.json Core.Packages.WebAPI\appsettings.Development.json
# Edit appsettings.Development.json with your values

cd Core.Packages.WebAPI
dotnet user-secrets init
dotnet user-secrets set "TokenOptions:SecurityKey" "YourSuperSecretKeyForJwtTokenSigning_MustBeAtLeast32Chars!"
dotnet user-secrets set "AIOptions:ApiKey" "YOUR_OPENAI_KEY"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5434;Database=MagicCarRepairDb;Username=magiccar;Password=magiccar_dev"
```

Or run: `.\scripts\setup-user-secrets.ps1` (prints commands; does not store secrets in repo).

## Configuration keys

| Key | Purpose |
|-----|---------|
| `ConnectionStrings:DefaultConnection` | PostgreSQL (see [DATABASE.md](./DATABASE.md)) |
| `TokenOptions:SecurityKey` | JWT signing (min 32 chars) |
| `AIOptions:ApiKey` | OpenAI / AI provider |
| `EmailSettings:*` | SMTP for password reset / notifications |
| `SuperPassword:*` | Dev-only master password — **disable in production** |
| `Iyzico:ApiKey` / `SecretKey` | Payments |

## Environment variables

ASP.NET Core binds env vars with `__` for nesting:

```powershell
$env:ConnectionStrings__DefaultConnection = "Host=localhost;Port=5434;..."
$env:TokenOptions__SecurityKey = "..."
```

## Production

- Set secrets in your host (Azure App Settings, Docker secrets, etc.).
- Set `SuperPassword:Enabled` to `false`.
- Rotate any key that was ever committed to git.

## Files in git

| File | Committed |
|------|-----------|
| `appsettings.Example.json` | Yes (template) |
| `appsettings.Development.example.json` | Yes (template) |
| `appsettings.json` | No (gitignored) |
| `appsettings.Development.json` | No (gitignored) |
