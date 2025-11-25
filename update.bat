@echo off
echo Trying to uninstall previous version
dotnet tool uninstall -g FH

echo Packing...
dotnet pack ./fh.csproj

echo Installing...
dotnet tool install -g FH --add-source ./nupkg

if %errorlevel% equ 0 (
    echo Update successfully!
)