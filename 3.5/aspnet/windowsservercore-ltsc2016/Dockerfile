# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:3.5-windowsservercore-ltsc2016

RUN powershell -Command `
        $ErrorActionPreference = 'Stop'; `
        $ProgressPreference = 'SilentlyContinue'; `
        Add-WindowsFeature Web-Server; `
        Add-WindowsFeature Web-Asp-Net; `
        Remove-Item -Recurse C:\inetpub\wwwroot\*; `
        Invoke-WebRequest -Uri https://dotnetbinaries.blob.core.windows.net/servicemonitor/2.0.1.6/ServiceMonitor.exe -OutFile C:\ServiceMonitor.exe `
    && %windir%\System32\inetsrv\appcmd set apppool /apppool.name:DefaultAppPool /managedRuntimeVersion:v2.0 `
    &C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ngen.exe update; `
    &C:\Windows\Microsoft.NET\Framework\v4.0.30319\ngen.exe update

EXPOSE 80

ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]
