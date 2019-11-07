# escape=`

FROM mcr.microsoft.com/windows/servercore:1909

# Enable detection of running in a container
ENV DOTNET_RUNNING_IN_CONTAINER true `
    # Ngen workaround: https://github.com/microsoft/dotnet-framework-docker/issues/231
    COMPLUS_NGenProtectedProcess_FeatureEnabled 0

RUN \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
