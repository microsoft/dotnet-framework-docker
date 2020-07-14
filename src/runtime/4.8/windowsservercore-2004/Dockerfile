# escape=`

FROM mcr.microsoft.com/windows/servercore:2004

# Apply latest patch
RUN curl -fSLo patch.msu http://download.windowsupdate.com/c/msdownload/update/software/secu/2020/06/windows10.0-kb4565627-x64-ndp48_a765b5e7b36d04b80565d097620218def98f1f9d.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && DISM /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb4565627-x64-ndp48.cab `
    && rmdir /S /Q patch

ENV `
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true `
    # Ngen workaround: https://github.com/microsoft/dotnet-framework-docker/issues/231
    COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # Ngen top of assembly graph to optimize a set of frequently used assemblies
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    # To optimize 32-bit assemblies, uncomment the next line
    # && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
