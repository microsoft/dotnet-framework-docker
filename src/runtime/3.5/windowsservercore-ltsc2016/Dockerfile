# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2016-amd64

ENV COMPLUS_NGenProtectedProcess_FeatureEnabled=0

RUN `
    # Enable Windows Update service
    sc config wuauserv start= auto `
    # Install .NET Fx 3.5
    && dism /Online /Enable-Feature /FeatureName:NetFx3 /All `
    # Disable Windows Update service
    && sc config wuauserv start= disabled `
    `
    # Apply latest patch
    && powershell -Command `
        $ProgressPreference = 'SilentlyContinue'; `
        [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri https://catalog.s.download.windowsupdate.com/d/msdownload/update/software/secu/2025/03/windows10.0-kb5053594-x64_d126a8ab63952fdac4b4de0672f75fe453c884d2.msu `
            -OutFile patch.msu; `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-KB5055521-x64.cab `
    && rmdir /S /Q patch `
    `
    # ngen .NET Fx
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework64\v2.0.50727\ngen update `
    && %windir%\Microsoft.NET\Framework\v2.0.50727\ngen update
