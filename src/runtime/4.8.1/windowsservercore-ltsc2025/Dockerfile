# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2025-amd64

ENV `
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true `
    COMPLUS_RUNNING_IN_CONTAINER=1 `
    COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # Apply latest patch
    curl -fSLo patch.msu https://catalog.sf.dl.delivery.mp.microsoft.com/filestreamingservice/files/66baa9bc-4fa3-4f4b-97b1-af6dc17c64d9/public/windows11.0-kb5056579-x64-ndp481_a062e62bdedd9a2a4b0cbf4e26ff1652a240f1ee.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows11.0-KB5056579-x64-ndp481.cab `
    && rmdir /S /Q patch `
    `
    # Ngen top of assembly graph to optimize a set of frequently used assemblies
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    # To optimize 32-bit assemblies, uncomment the next line
    # && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen install "Microsoft.PowerShell.Utility.Activities, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update
