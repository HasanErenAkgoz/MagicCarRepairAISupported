# Admin ile POST /api/SeedData/run (API ayaktayken demo veri doldurur)
$ErrorActionPreference = "Stop"
$base = "http://localhost:5169"

Write-Host "==> Health"
$h = Invoke-WebRequest -Uri "$base/health" -UseBasicParsing -TimeoutSec 10
if ($h.Content -ne "Healthy") { throw "API not healthy" }

Write-Host "==> Login"
$login = Invoke-RestMethod -Uri "$base/api/Auth/login" -Method Post -ContentType "application/json" `
    -Body (@{ email = "admin@magiccar.com"; password = "Admin123!" } | ConvertTo-Json) -TimeoutSec 60
if (-not $login.success) { throw "Login failed" }
$token = $login.data.token
$headers = @{ Authorization = "Bearer $token" }

Write-Host "==> SeedData/run"
$seed = Invoke-RestMethod -Uri "$base/api/SeedData/run?clearExisting=false" -Method Post -Headers $headers -TimeoutSec 300
Write-Host ($seed | ConvertTo-Json)

Write-Host "==> Customers"
$customers = Invoke-RestMethod -Uri "$base/api/Customers" -Headers $headers -TimeoutSec 120
Write-Host "Count:" $customers.Count

Write-Host "==> Customer Id=1"
try {
    $c1 = Invoke-WebRequest -Uri "$base/api/Customers/1" -Headers $headers -TimeoutSec 30 -UseBasicParsing
    Write-Host "Customer 1 status:" $c1.StatusCode
} catch {
    Write-Host "Customer 1:" $_.Exception.Response.StatusCode.value__
}

Write-Host "Done."
