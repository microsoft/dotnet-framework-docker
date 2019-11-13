# escape=`

FROM mcr.microsoft.com/windows/servercore:1909

# Enable detection of running in a container
ENV DOTNET_RUNNING_IN_CONTAINER true `
    # Ngen workaround: https://github.com/microsoft/dotnet-framework-docker/issues/231
    COMPLUS_NGenProtectedProcess_FeatureEnabled 0

# Install .NET Fx 3.5
RUN curl -fSLo microsoft-windows-netfx3.zip https://dotnetbinaries.blob.core.windows.net/dockerassets/microsoft-windows-netfx3-1909.zip `
    && tar -zxf microsoft-windows-netfx3.zip `
    && del /F /Q microsoft-windows-netfx3.zip `
    && DISM /Online /Quiet /Add-Package /PackagePath:.\microsoft-windows-netfx3-ondemand-package~31bf3856ad364e35~amd64~~.cab `
    && del microsoft-windows-netfx3-ondemand-package~31bf3856ad364e35~amd64~~.cab `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\*

# ngen .NET 3.5 Fx
RUN \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.Tpm.Commands, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35, processorArchitecture=amd64" `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "Microsoft.VisualC, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=msil" `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "Microsoft.VisualC, Version=10.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=msil" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
