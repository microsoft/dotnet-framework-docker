# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2022-amd64

ENV `
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true `
    COMPLUS_RUNNING_IN_CONTAINER=1 `
    COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # Apply patch to provide support for container limits
    curl -fSLo patch.msu https://download.microsoft.com/download/5/a/4/5a4425d3-eb10-47a8-afe0-b07a19b20b1c/Windows10.0-KB9008395-x64-NDP48.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && powershell Add-WindowsPackage -Online -NoRestart -PackagePath C:\patch\windows10.0-kb9008395-x64-ndp48.cab `
    && rmdir /S /Q patch `
    `
    # Ngen top of assembly graph to optimize a set of frequently used assemblies
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    # To optimize 32-bit assemblies, uncomment the next line
    # && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update
