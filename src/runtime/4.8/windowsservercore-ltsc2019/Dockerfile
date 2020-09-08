# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2019-amd64

# Install .NET 4.8
RUN curl -fSLo dotnet-framework-installer.exe https://download.visualstudio.microsoft.com/download/pr/7afca223-55d2-470a-8edc-6a1739ae3252/abd170b4b0ec15ad0222a809b761a036/ndp48-x86-x64-allos-enu.exe `
    && .\dotnet-framework-installer.exe /q `
    && del .\dotnet-framework-installer.exe `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\*

# Apply latest patch
RUN curl -fSLo patch.msu http://download.windowsupdate.com/d/msdownload/update/software/secu/2020/08/windows10.0-kb4576483-x64-ndp48_c9221807b795c7c804afcc7555e77a93c779b70f.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && DISM /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb4576483-x64-ndp48.cab `
    && rmdir /S /Q patch

# ngen .NET Fx
ENV COMPLUS_NGenProtectedProcess_FeatureEnabled 0
RUN \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
