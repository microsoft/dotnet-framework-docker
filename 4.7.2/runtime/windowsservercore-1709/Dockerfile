# escape=`

FROM mcr.microsoft.com/windows/servercore:1709

# Install .NET 4.7.2
RUN powershell Invoke-WebRequest -Uri "https://download.microsoft.com/download/6/E/4/6E48E8AB-DC00-419E-9704-06DD46E5F81D/NDP472-KB4054530-x86-x64-AllOS-ENU.exe" -OutFile dotnet-framework-installer.exe & `
    .\dotnet-framework-installer.exe /q & `
    del .\dotnet-framework-installer.exe & `
    powershell Remove-Item -Force -Recurse ${Env:TEMP}\*

# Apply latest patch
RUN powershell -Command `
        $ErrorActionPreference = 'Stop'; `
        $ProgressPreference = 'SilentlyContinue'; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri "http://download.windowsupdate.com/c/msdownload/update/software/secu/2019/03/windows10.0-kb4489886-x64_8efbb3174d842f21830bc4d0dc050eea0e584a83.msu" `
            -OutFile patch.msu; `
        New-Item -Type Directory patch; `
        Start-Process expand -ArgumentList 'patch.msu', 'patch', '-F:*' -NoNewWindow -Wait; `
        Remove-Item -Force patch.msu; `
        Add-WindowsPackage -Online -PackagePath C:\patch\Windows10.0-KB4489886-x64.cab; `
        Remove-Item -Force -Recurse \patch

# ngen .NET Fx
ENV COMPLUS_NGenProtectedProcess_FeatureEnabled 0
RUN \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update & `
    \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
