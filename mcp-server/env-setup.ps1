# .env dosyası oluşturma script'i
# Bu script .env dosyasını oluşturur (eğer yoksa)

$envContent = @"
# Magic Car Repair MCP Server Configuration
# Stitch API Configuration
STITCH_API_KEY=AQ.Ab8RN6J5zVub6x873XJ66i0X2rOxB78FVApB-Bn6_SqqoXZXoA

# Backend API Configuration
API_BASE_URL=http://localhost:5169
API_TOKEN=

# Logging
LOG_LEVEL=info
"@

$envPath = Join-Path $PSScriptRoot ".env"

if (-not (Test-Path $envPath)) {
    $envContent | Out-File -FilePath $envPath -Encoding utf8 -NoNewline
    Write-Host ".env file created: $envPath" -ForegroundColor Green
} else {
    Write-Host ".env file already exists: $envPath" -ForegroundColor Yellow
    Write-Host "Existing content preserved. Edit manually to update." -ForegroundColor Yellow
}
