# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2019-amd64

ENV COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # ngen .NET Fx
    %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update
