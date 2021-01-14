# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2016-amd64

ENV COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # ngen .NET Fx
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
