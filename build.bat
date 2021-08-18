@echo off
echo Attempting to build

nuget restore
devenv Discord-QR-Token-Stealer.sln /build

pause