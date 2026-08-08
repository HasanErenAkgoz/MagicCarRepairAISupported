# Prints dotnet user-secrets commands for local Web API development.
# Does not store real secrets in the repository.
$ErrorActionPreference = "Stop"
$webApiDir = Join-Path $PSScriptRoot "..\Core.Packages.WebAPI"
Push-Location $webApiDir
Write-Host "Run from: $webApiDir" -ForegroundColor Cyan
Write-Host @"

dotnet user-secrets init
dotnet user-secrets set "TokenOptions:SecurityKey" "<min-32-char-jwt-key>"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5434;Database=MagicCarRepairDb;Username=magiccar;Password=magiccar_dev"
dotnet user-secrets set "AIOptions:ApiKey" "<openai-key>"
# Optional dev-only:
dotnet user-secrets set "SuperPassword:Enabled" "true"
dotnet user-secrets set "SuperPassword:Password" "<dev-password>"
dotnet user-secrets set "SuperPassword:AllowedEmails:0" "admin@magiccar.com"

"@ -ForegroundColor Yellow
Pop-Location
