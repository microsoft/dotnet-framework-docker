# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:3.5-windowsservercore-2009

RUN dism /Online /Quiet /Enable-Feature /All /FeatureName:IIS-WebServerRole /FeatureName:IIS-ASPNET `
    && dism /Online /Quiet /Disable-Feature /FeatureName:IIS-WebServerManagementTools `
    && del /q "C:\inetpub\wwwroot\*" `
    && FOR /D %p IN ("C:\inetpub\wwwroot\*.*") DO rmdir "%p" /s /q `
    && curl -fSLo ServiceMonitor.exe https://dotnetbinaries.blob.core.windows.net/servicemonitor/2.0.1.10/ServiceMonitor.exe `
    && %windir%\System32\inetsrv\appcmd set apppool /apppool.name:DefaultAppPool /managedRuntimeVersion:v2.0 `
    && C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ngen.exe update `
    && C:\Windows\Microsoft.NET\Framework\v4.0.30319\ngen.exe update

EXPOSE 80

ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]
