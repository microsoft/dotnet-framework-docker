# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2019-amd64

# Apply latest patch
RUN curl -fSLo patch.msu http://download.windowsupdate.com/d/msdownload/update/software/secu/2020/10/windows10.0-kb4578966-x64_af176f25626d3689abfa12586ca6c144aa5904b9.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && DISM /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb4578966-x64.cab `
    && rmdir /S /Q patch

ENV COMPLUS_NGenProtectedProcess_FeatureEnabled 0

RUN \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
