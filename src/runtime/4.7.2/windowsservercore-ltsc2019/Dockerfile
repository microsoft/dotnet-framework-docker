# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2019

# Apply latest patch
RUN curl -fSLo patch.msu http://download.windowsupdate.com/c/msdownload/update/software/secu/2020/07/windows10.0-kb4569776-x64_fb879f26b463abeea05426b15bbaba635b4ab79d.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && DISM /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb4569776-x64.cab `
    && rmdir /S /Q patch

ENV COMPLUS_NGenProtectedProcess_FeatureEnabled 0

RUN \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
