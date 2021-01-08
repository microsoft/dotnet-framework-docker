# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:4.8-windowsservercore-20H2

RUN dism /Online /Quiet /Enable-Feature /All /FeatureName:IIS-WebServerRole /FeatureName:NetFx4Extended-ASPNET45 /FeatureName:IIS-ASPNET45 `
    && dism /Online /Quiet /Disable-Feature /FeatureName:IIS-WebServerManagementTools `
    && del /q "C:\inetpub\wwwroot\*" `
    && FOR /D %p IN ("C:\inetpub\wwwroot\*") DO rmdir "%p" /s /q `
    && curl -fSLo ServiceMonitor.exe https://dotnetbinaries.blob.core.windows.net/servicemonitor/2.0.1.10/ServiceMonitor.exe `
    && C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ngen.exe update `
    && C:\Windows\Microsoft.NET\Framework\v4.0.30319\ngen.exe update

# Install Roslyn compilers
RUN curl -fSLo microsoft.net.compilers.2.9.0.zip https://api.nuget.org/packages/microsoft.net.compilers.2.9.0.nupkg `
    && mkdir c:\RoslynCompilers `
    && tar -C c:\RoslynCompilers -zxf microsoft.net.compilers.2.9.0.zip `
    && del microsoft.net.compilers.2.9.0.zip `
    && C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ngen.exe install c:\RoslynCompilers\tools\csc.exe /ExeConfig:c:\RoslynCompilers\tools\csc.exe `
    && C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ngen.exe install c:\RoslynCompilers\tools\vbc.exe /ExeConfig:c:\RoslynCompilers\tools\vbc.exe `
    && C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ngen.exe install c:\RoslynCompilers\tools\VBCSCompiler.exe /ExeConfig:c:\RoslynCompilers\tools\VBCSCompiler.exe

ENV ROSLYN_COMPILER_LOCATION c:\RoslynCompilers\tools

EXPOSE 80

ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]
