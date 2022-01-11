# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2019-amd64

ENV `
    # Enable detection of running in a container
    COMPLUS_RUNNING_IN_CONTAINER=1 `
    COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # Install .NET Fx 4.8
    curl -fSLo dotnet-framework-installer.exe https://download.visualstudio.microsoft.com/download/pr/7afca223-55d2-470a-8edc-6a1739ae3252/abd170b4b0ec15ad0222a809b761a036/ndp48-x86-x64-allos-enu.exe `
    && .\dotnet-framework-installer.exe /q `
    && del .\dotnet-framework-installer.exe `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\* `
    `
    # Apply latest patch
    && curl -fSLo patch.msu http://download.windowsupdate.com/c/msdownload/update/software/secu/2021/12/windows10.0-kb5008878-x64-ndp48_1b3c7e40c5ba4b6325148980b4d939d5ae8d391d.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && powershell Add-WindowsPackage -Online -NoRestart -PackagePath C:\patch\windows10.0-kb5008878-x64-ndp48.cab `
    && rmdir /S /Q patch `
    `
    # Apply patch to provide support for container limits
    && curl -fSLo patch.msu https://download.microsoft.com/download/5/a/4/5a4425d3-eb10-47a8-afe0-b07a19b20b1c/Windows10.0-KB9008392-x64-NDP48.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && powershell Add-WindowsPackage -Online -NoRestart -PackagePath C:\patch\windows10.0-kb9008392-x64-ndp48.cab `
    && rmdir /S /Q patch `
    `
    # ngen .NET Fx
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update
