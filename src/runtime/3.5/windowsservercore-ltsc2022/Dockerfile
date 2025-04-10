# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2022-amd64

ENV `
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true `
    COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # Install .NET Fx 3.5
    dism /Online /Add-Capability /CapabilityName:NetFx3 `
    `
    # Apply latest 3.5 patch
    && curl -fSLo patch.msu https://catalog.s.download.windowsupdate.com/d/msdownload/update/software/secu/2024/12/windows10.0-kb5049617-x64-ndp48_5adfae2ccbb4743d93898ff35010c77daa8f130f.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-KB5049617-x64-ndp48.cab `
    && rmdir /S /Q patch `
    `
    # Ngen top of assembly graph to optimize a set of frequently used assemblies
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    # To optimize 32-bit assemblies, uncomment the next line
    # && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update
