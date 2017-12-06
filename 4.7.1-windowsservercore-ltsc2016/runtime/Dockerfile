# escape=`

FROM microsoft/windowsservercore:ltsc2016

RUN powershell Invoke-WebRequest -Uri "https://download.microsoft.com/download/9/E/6/9E63300C-0941-4B45-A0EC-0008F96DD480/NDP471-KB4033342-x86-x64-AllOS-ENU.exe" -OutFile dotnet-framework-installer.exe & `
    .\dotnet-framework-installer.exe /q & `
    del .\dotnet-framework-installer.exe & `
    set COMPLUS_NGenProtectedProcess_FeatureEnabled=0 & `
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update & `
    \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
