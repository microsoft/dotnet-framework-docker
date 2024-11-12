# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:3.5-20241112-windowsservercore-ltsc2022

RUN dism /Online /Quiet /Enable-Feature /All /FeatureName:IIS-WebServerRole /FeatureName:IIS-ASPNET `
    && dism /Online /Quiet /Disable-Feature /FeatureName:IIS-WebServerManagementTools `
    && del /q "C:\inetpub\wwwroot\*" `
    && for /D %p IN ("C:\inetpub\wwwroot\*") DO rmdir "%p" /s /q `
    && curl -fSLo ServiceMonitor.exe https://dotnetbinaries.blob.core.windows.net/servicemonitor/2.0.1.10/ServiceMonitor.exe `
    && %windir%\System32\inetsrv\appcmd set apppool /apppool.name:DefaultAppPool /managedRuntimeVersion:v2.0 `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update

EXPOSE 80

ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]
