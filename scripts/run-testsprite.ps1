# TestSprite oncesi: DB seed + TC dosyalari + yerel 40 test
# CALISTIRMA: Explorer'da .ps1'e cift tiklamayin (Notepad acar).
#   scripts\run-testsprite.cmd
#   veya: powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\run-testsprite.ps1
$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$tests = Join-Path $root "testsprite_tests"

Write-Host "==> API health"
try {
    $h = Invoke-WebRequest -Uri "http://localhost:5169/health" -UseBasicParsing -TimeoutSec 5
    if ($h.StatusCode -ne 200) { throw "API not healthy" }
} catch {
    Write-Error "API http://localhost:5169 ayakta degil. Once: dotnet run --project Core.Packages.WebAPI"
}

Set-Location $tests
Write-Host "==> ensure_db_seed.py"
python ensure_db_seed.py
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host "==> generate_mcp_tc_files.py"
python generate_mcp_tc_files.py

Write-Host "==> run_all_tc.py (yerel 40 test)"
python run_all_tc.py
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host ""
Write-Host "Yerel testler OK. TestSprite bulut icin:"
Write-Host "  node ...\testsprite-mcp\dist\index.js generateCodeAndExecute"
Write-Host "  (Cursor MCP: testsprite_generate_code_and_execute)"
