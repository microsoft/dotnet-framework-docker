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
    && curl -fSLo patch.msu https://catalog.s.download.windowsupdate.com/d/msdownload/update/software/secu/2022/04/windows10.0-kb5013626-x64-ndp48_e2b4283a00b2e97030507f071c3ecc6a2a352064.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb5013626-x64-ndp48.cab `
    && rmdir /S /Q patch `
    `
    # Apply patch to provide support for container limits
    && curl -fSLo patch.msu https://download.microsoft.com/download/f/8/8/f883b65c-c6fe-4ce5-9c42-3f79aac2a834/Windows10.0-KB9008392-x64-NDP48.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb9008392-x64-ndp48.cab `
    && rmdir /S /Q patch `
    `
    # ngen .NET Fx
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update
