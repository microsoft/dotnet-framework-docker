# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2019

# Install .NET Fx 3.5
RUN powershell -Command `
        $ErrorActionPreference = 'Stop'; `
        $ProgressPreference = 'SilentlyContinue'; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri "https://dotnetbinaries.blob.core.windows.net/dockerassets/microsoft-windows-netfx3-1809.zip" `
            -OutFile microsoft-windows-netfx3.zip; `
        Expand-Archive microsoft-windows-netfx3.zip; `
        Remove-Item -Force microsoft-windows-netfx3.zip; `
        Add-WindowsPackage -Online -PackagePath .\microsoft-windows-netfx3\microsoft-windows-netfx3-ondemand-package~31bf3856ad364e35~amd64~~.cab; `
        Remove-Item -Force -Recurse microsoft-windows-netfx3; `
        Remove-Item -Force -Recurse ${Env:TEMP}\*

# Apply latest patch
RUN curl -SL --output patch.msu http://download.windowsupdate.com/d/msdownload/update/software/secu/2019/05/windows10.0-kb4495590-x64_d746e135ff19912a8fd20a53bcc89fd20cbd3b6a.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && DISM /Online /Quiet /Add-Package /PackagePath:C:\patch\Windows10.0-kb4495590-x64.cab `
    && rmdir /S /Q patch

# ngen .NET 3.5 Fx
ENV COMPLUS_NGenProtectedProcess_FeatureEnabled 0
RUN \Windows\Microsoft.NET\Framework64\v2.0.50727\ngen update & `
    \Windows\Microsoft.NET\Framework\v2.0.50727\ngen update
