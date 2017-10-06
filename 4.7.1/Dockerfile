FROM microsoft/windowsservercore

RUN powershell Invoke-WebRequest -Uri "https://download.microsoft.com/download/5/B/D/5BD1CB75-43AA-4408-9147-B75FCDA6B062/NDP471-DevPack-ENU.exe" -OutFile dotnet-framework-installer.exe & \
    .\dotnet-framework-installer.exe /q & \ 
    del .\dotnet-framework-installer.exe & \
    set COMPLUS_NGenProtectedProcess_FeatureEnabled=0 & \
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen install System.Web & \
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update & \
    \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update
    