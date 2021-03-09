# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2019-amd64

ENV COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # Apply latest patch
    curl -fSLo patch.msu http://download.windowsupdate.com/c/msdownload/update/software/secu/2021/01/windows10.0-kb4601060-x64_b645458f512391ae20e17e21f01d24183b5076c9.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb4601060-x64.cab `
    && rmdir /S /Q patch `
    `
    # ngen .NET Fx
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update
