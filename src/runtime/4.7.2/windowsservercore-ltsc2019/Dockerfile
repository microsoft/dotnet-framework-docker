# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2019-amd64

ENV COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # Apply latest patch
    curl -fSLo patch.msu http://download.windowsupdate.com/c/msdownload/update/software/updt/2020/11/windows10.0-kb4586875-x64_a14cffeb8033542a5379d4c5a9c0c6fa17c3f939.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && DISM /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb4586875-x64.cab `
    && rmdir /S /Q patch `
    `
    # ngen .NET Fx
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
