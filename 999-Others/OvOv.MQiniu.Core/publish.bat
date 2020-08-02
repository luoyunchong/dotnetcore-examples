
@echo off

dotnet clean
dotnet restore
dotnet build

DEL /F/Q/S "D:\Publishes\Qiniu" > NUL && RMDIR /Q/S "D:\Publishes\Qiniu"

::dotnet publish -c Release -r win-x64   --self-contained true -o "D:\Publishes\Qiniu\win-x64"
::dotnet publish -c Release -r win-x86   --self-contained true -o "D:\Publishes\Qiniu\win-x86"
::dotnet publish -c Release -r osx-x64   --self-contained true -o "D:\Publishes\Qiniu\osx-x64"
dotnet publish -c Release -r linux-x64 --self-contained false -o "D:\Publishes\Qiniu\linux-x64"