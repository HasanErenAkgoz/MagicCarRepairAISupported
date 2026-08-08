@echo off
setlocal
cd /d "%~dp0.."
powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0seed-db-via-api.ps1" %*
if errorlevel 1 exit /b 1
endlocal
