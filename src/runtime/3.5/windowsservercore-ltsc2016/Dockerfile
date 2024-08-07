# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2016-amd64

ENV COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # Install .NET Fx 3.5
    powershell -Command `
        $ErrorActionPreference = 'Stop'; `
        $ProgressPreference = 'SilentlyContinue'; `
        [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri https://dotnetbinaries.blob.core.windows.net/dockerassets/microsoft-windows-netfx3-ltsc2016.zip `
            -OutFile microsoft-windows-netfx3.zip; `
        Expand-Archive microsoft-windows-netfx3.zip; `
    && del microsoft-windows-netfx3.zip `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\microsoft-windows-netfx3\microsoft-windows-netfx3-ondemand-package.cab `
    && rmdir /S /Q microsoft-windows-netfx3 `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\* `
    `
    # Apply latest patch
    && powershell -Command `
        $ProgressPreference = 'SilentlyContinue'; `
        [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri https://catalog.s.download.windowsupdate.com/d/msdownload/update/software/secu/2024/07/windows10.0-kb5040434-x64_d54a2d3165f2f751ceee94f5a92b8ae70a82b77e.msu `
            -OutFile patch.msu; `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb5040434-x64.cab `
    && rmdir /S /Q patch `
    `
    # ngen .NET Fx
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework64\v2.0.50727\ngen update `
    && %windir%\Microsoft.NET\Framework\v2.0.50727\ngen update
