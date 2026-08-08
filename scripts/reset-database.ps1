# Drops PostgreSQL volume, recreates container, applies EF migrations.
# Super Admin seed runs when you start the Web API.
param(
    [switch]$SkipDocker,
    [string]$ProjectRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
)

$ErrorActionPreference = "Stop"
Set-Location $ProjectRoot

Write-Host "==> Magic Car Repair - database reset (PostgreSQL)" -ForegroundColor Cyan

if (-not $SkipDocker) {
    Write-Host "Stopping containers and removing volume..." -ForegroundColor Yellow
    docker compose down -v
    Write-Host "Starting PostgreSQL..." -ForegroundColor Yellow
    docker compose up -d
    Write-Host "Waiting for PostgreSQL health..." -ForegroundColor Yellow
    $deadline = (Get-Date).AddMinutes(2)
    do {
        $status = docker inspect -f "{{.State.Health.Status}}" magiccarrepair-postgres 2>$null
        if ($status -eq "healthy") { break }
        if ((Get-Date) -gt $deadline) {
            throw "PostgreSQL container did not become healthy in time."
        }
        Start-Sleep -Seconds 2
    } while ($true)
}

$webApi = Join-Path $ProjectRoot "Core.Packages.WebAPI\MagicCarRepairAISupported.WebAPI.csproj"
$persistence = Join-Path $ProjectRoot "Core.Packages.Persistence\MagicCarRepairAISupported.Persistence.csproj"

Write-Host "Applying EF Core migrations..." -ForegroundColor Yellow
dotnet ef database update `
    --project $persistence `
    --startup-project $webApi

Write-Host ""
Write-Host "Done. Start the API to run bootstrap seed:" -ForegroundColor Green
$runApi = "dotnet run --project Core.Packages.WebAPI\MagicCarRepairAISupported.WebAPI.csproj"
Write-Host "  $runApi"
