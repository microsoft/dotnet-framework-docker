# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2019-amd64

ENV COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # Install .NET Fx 3.5
    curl -fSLo microsoft-windows-netfx3.zip https://dotnetbinaries.blob.core.windows.net/dockerassets/microsoft-windows-netfx3-ltsc2019.zip `
    && tar -zxf microsoft-windows-netfx3.zip `
    && del /F /Q microsoft-windows-netfx3.zip `
    && powershell Add-WindowsPackage -Online -NoRestart -PackagePath .\microsoft-windows-netfx3-ondemand-package~31bf3856ad364e35~amd64~~.cab `
    && del microsoft-windows-netfx3-ondemand-package~31bf3856ad364e35~amd64~~.cab `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\* `
    `
    # Apply latest patch
    && curl -fSLo patch.msu http://download.windowsupdate.com/d/msdownload/update/software/secu/2021/12/windows10.0-kb5008218-x64_66e07f2fc23728ca0b8f395df15da52546e45e45.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && powershell Add-WindowsPackage -Online -NoRestart -PackagePath C:\patch\windows10.0-kb5008218-x64.cab `
    && rmdir /S /Q patch `
    `
    # ngen .NET Fx
    && %windir%\Microsoft.NET\Framework64\v2.0.50727\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && %windir%\Microsoft.NET\Framework64\v2.0.50727\ngen update `
    && %windir%\Microsoft.NET\Framework\v2.0.50727\ngen update
