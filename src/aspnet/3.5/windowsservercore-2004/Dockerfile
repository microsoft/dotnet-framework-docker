# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:3.5-20211012-windowsservercore-2004

RUN powershell -Command `
        $ErrorActionPreference = 'Stop'; `
        $ProgressPreference = 'SilentlyContinue'; `
        Add-WindowsFeature Web-Server; `
        Add-WindowsFeature Web-Asp-Net; `
        Remove-Item -Recurse C:\inetpub\wwwroot\*; `
        Invoke-WebRequest -Uri https://dotnetbinaries.blob.core.windows.net/servicemonitor/2.0.1.10/ServiceMonitor.exe -OutFile C:\ServiceMonitor.exe `
    && %windir%\System32\inetsrv\appcmd set apppool /apppool.name:DefaultAppPool /managedRuntimeVersion:v2.0 `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update

EXPOSE 80

ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]
