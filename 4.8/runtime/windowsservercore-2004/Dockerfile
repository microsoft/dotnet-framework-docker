# escape=`

FROM mcr.microsoft.com/windows/servercore:2004

ENV `
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true `
    # Ngen workaround: https://github.com/microsoft/dotnet-framework-docker/issues/231
    COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # Ngen top of assembly graph to optimize a set of frequently used assemblies
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update
    # To optimize 32-bit assemblies, uncomment the next line and add an escape character (`) to the end of the previous line
    # && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    # && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
