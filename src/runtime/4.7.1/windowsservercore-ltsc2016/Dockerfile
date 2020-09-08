# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2016-amd64

# Install .NET 4.7.1
RUN powershell -Command `
        $ProgressPreference = 'SilentlyContinue'; `
        [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri "https://download.microsoft.com/download/9/E/6/9E63300C-0941-4B45-A0EC-0008F96DD480/NDP471-KB4033342-x86-x64-AllOS-ENU.exe" `
            -OutFile dotnet-framework-installer.exe `
    && start /w .\dotnet-framework-installer.exe /q `
    && del .\dotnet-framework-installer.exe `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\*

# Apply latest patch
RUN powershell -Command `
        $ProgressPreference = 'SilentlyContinue'; `
        [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri "http://download.windowsupdate.com/d/msdownload/update/software/secu/2020/09/windows10.0-kb4577015-x64_0e838646b432d7b5360fffd0f12365ca181d475d.msu" `
            -OutFile patch.msu; `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb4577015-x64.cab `
    && rmdir /S /Q patch

# ngen .NET Fx
ENV COMPLUS_NGenProtectedProcess_FeatureEnabled 0
RUN \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
