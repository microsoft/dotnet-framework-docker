# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2025-amd64

ENV `
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true `
    COMPLUS_RUNNING_IN_CONTAINER=1 `
    COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # Apply latest patch
    curl -fSLo patch.msu https://catalog.sf.dl.delivery.mp.microsoft.com/filestreamingservice/files/6ce99b67-e730-4486-8380-935d60b274e7/public/windows11.0-kb5050577-x64-ndp481_587d1c1f9937f98d194e97ceff613dab9606f7f4.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows11.0-KB5050577-x64-ndp481.cab `
    && rmdir /S /Q patch `
    `
    # Ngen top of assembly graph to optimize a set of frequently used assemblies
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    # To optimize 32-bit assemblies, uncomment the next line
    # && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update
