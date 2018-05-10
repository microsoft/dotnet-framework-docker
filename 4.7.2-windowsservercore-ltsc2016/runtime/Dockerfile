# escape=`

FROM microsoft/windowsservercore:ltsc2016

# Install .NET 4.7.2
RUN powershell Invoke-WebRequest -Uri "https://download.microsoft.com/download/5/A/3/5A3607CA-53B1-4717-8845-4389B11931FA/NDP472-KB4054530-x86-x64-AllOS-ENU.exe" -OutFile dotnet-framework-installer.exe & `
    .\dotnet-framework-installer.exe /q & `
    del .\dotnet-framework-installer.exe

# ngen .NET Fx
ENV COMPLUS_NGenProtectedProcess_FeatureEnabled 0
RUN \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update & `
    \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
