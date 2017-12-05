FROM microsoft/windowsservercore:ltsc2016

RUN powershell Invoke-WebRequest -Uri "https://download.microsoft.com/download/D/D/3/DD35CC25-6E9C-484B-A746-C5BE0C923290/NDP47-KB3186497-x86-x64-AllOS-ENU.exe" -OutFile dotnet-framework-installer.exe & \
    .\dotnet-framework-installer.exe /q & \ 
    del .\dotnet-framework-installer.exe & \
    set COMPLUS_NGenProtectedProcess_FeatureEnabled=0 & \
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update & \
    \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
