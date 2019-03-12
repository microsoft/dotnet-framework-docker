# escape=`

FROM mcr.microsoft.com/windows/servercore:1803

# Install .NET Fx 3.5
RUN powershell -Command `
        $ErrorActionPreference = 'Stop'; `
        $ProgressPreference = 'SilentlyContinue'; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri "https://dotnetbinaries.blob.core.windows.net/dockerassets/microsoft-windows-netfx3-1803.zip" `
            -OutFile microsoft-windows-netfx3.zip; `
        Expand-Archive microsoft-windows-netfx3.zip; `
        Remove-Item -Force microsoft-windows-netfx3.zip; `
        Add-WindowsPackage -Online -PackagePath .\microsoft-windows-netfx3\microsoft-windows-netfx3-ondemand-package~31bf3856ad364e35~amd64~~.cab; `
        Remove-Item -Force -Recurse microsoft-windows-netfx3

# Apply latest patch
RUN powershell -Command `
        $ErrorActionPreference = 'Stop'; `
        $ProgressPreference = 'SilentlyContinue'; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri "http://download.windowsupdate.com/c/msdownload/update/software/secu/2019/03/windows10.0-kb4489868-x64_e5aaf33c58b19b162cbbbf559b9cbb5306df1a67.msu" `
            -OutFile patch.msu; `
        New-Item -Type Directory patch; `
        Start-Process expand -ArgumentList 'patch.msu', 'patch', '-F:*' -NoNewWindow -Wait; `
        Remove-Item -Force patch.msu; `
        Add-WindowsPackage -Online -PackagePath C:\patch\Windows10.0-KB4489868-x64.cab; `
        Remove-Item -Force -Recurse \patch

# ngen .NET 3.5 Fx
ENV COMPLUS_NGenProtectedProcess_FeatureEnabled 0
RUN \Windows\Microsoft.NET\Framework64\v2.0.50727\ngen update & `
    \Windows\Microsoft.NET\Framework\v2.0.50727\ngen update
