# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:4.8.1-20250513-windowsservercore-ltsc2025

RUN dism /Online /Quiet /Enable-Feature /All /FeatureName:IIS-WebServerRole /FeatureName:NetFx4Extended-ASPNET45 /FeatureName:IIS-ASPNET45 `
    && dism /Online /Quiet /Disable-Feature /FeatureName:IIS-WebServerManagementTools `
    && del /q "C:\inetpub\wwwroot\*" `
    && for /D %p IN ("C:\inetpub\wwwroot\*") DO rmdir "%p" /s /q `
    && curl -fSLo ServiceMonitor.exe https://github.com/microsoft/IIS.ServiceMonitor/releases/download/v2.0.1.10/ServiceMonitor.exe `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update

# Install 2.9.0 Roslyn compilers
RUN curl -fSLo microsoft.net.compilers.2.9.0.zip https://api.nuget.org/packages/microsoft.net.compilers.2.9.0.nupkg `
    && mkdir C:\RoslynCompilers `
    && tar -C C:\RoslynCompilers -zxf microsoft.net.compilers.2.9.0.zip `
    && del microsoft.net.compilers.2.9.0.zip `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen install C:\RoslynCompilers\tools\csc.exe /ExeConfig:C:\RoslynCompilers\tools\csc.exe `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen install C:\RoslynCompilers\tools\vbc.exe /ExeConfig:C:\RoslynCompilers\tools\vbc.exe `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen install C:\RoslynCompilers\tools\VBCSCompiler.exe /ExeConfig:C:\RoslynCompilers\tools\VBCSCompiler.exe

# Install 3.6.0 Roslyn compilers
RUN curl -fSLo microsoft.net.compilers.3.6.0.zip https://api.nuget.org/packages/microsoft.net.compilers.3.6.0.nupkg `
    && mkdir C:\RoslynCompilers-3.6.0 `
    && tar -C C:\RoslynCompilers-3.6.0 -zxf microsoft.net.compilers.3.6.0.zip `
    && del microsoft.net.compilers.3.6.0.zip `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen install C:\RoslynCompilers-3.6.0\tools\csc.exe /ExeConfig:C:\RoslynCompilers-3.6.0\tools\csc.exe `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen install C:\RoslynCompilers-3.6.0\tools\vbc.exe /ExeConfig:C:\RoslynCompilers-3.6.0\tools\vbc.exe `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen install C:\RoslynCompilers-3.6.0\tools\VBCSCompiler.exe /ExeConfig:C:\RoslynCompilers-3.6.0\tools\VBCSCompiler.exe

ENV ROSLYN_COMPILER_LOCATION=C:\RoslynCompilers-3.6.0\tools

EXPOSE 80

ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]
