# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2016-amd64

# Install .NET Fx 3.5
RUN powershell -Command `
        $ErrorActionPreference = 'Stop'; `
        $ProgressPreference = 'SilentlyContinue'; `
        [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri "https://dotnetbinaries.blob.core.windows.net/dockerassets/microsoft-windows-netfx3-ltsc2016.zip" `
            -OutFile microsoft-windows-netfx3.zip; `
        Expand-Archive microsoft-windows-netfx3.zip; `
    && del microsoft-windows-netfx3.zip `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\microsoft-windows-netfx3\microsoft-windows-netfx3-ondemand-package.cab `
    && rmdir /S /Q microsoft-windows-netfx3 `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\*

# Apply latest patch
RUN powershell -Command `
        $ProgressPreference = 'SilentlyContinue'; `
        [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri "http://download.windowsupdate.com/c/msdownload/update/software/secu/2020/10/windows10.0-kb4580346-x64_1b5d07a6251f5794fe6a6c0069d39d302c5412d3.msu" `
            -OutFile patch.msu; `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb4580346-x64.cab `
    && rmdir /S /Q patch

# ngen .NET Fx
ENV COMPLUS_NGenProtectedProcess_FeatureEnabled 0
RUN \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework64\v2.0.50727\ngen update `
    && \Windows\Microsoft.NET\Framework\v2.0.50727\ngen update
