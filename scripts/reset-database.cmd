@echo off
setlocal
cd /d "%~dp0.."
powershell.exe -NoProfile -ExecutionPolicy Bypass -File "%~dp0reset-database.ps1" %*
if errorlevel 1 exit /b 1
endlocal
