# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:4.7.2-20211214-windowsservercore-ltsc2016

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

RUN Add-WindowsFeature Web-Server; `
    Add-WindowsFeature NET-Framework-45-ASPNET; `
    Add-WindowsFeature Web-Asp-Net45; `
    Remove-Item -Recurse C:\inetpub\wwwroot\*; `
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
    Invoke-WebRequest -Uri https://dotnetbinaries.blob.core.windows.net/servicemonitor/2.0.1.10/ServiceMonitor.exe -OutFile C:\ServiceMonitor.exe; `
    &$Env:windir\Microsoft.NET\Framework64\v4.0.30319\ngen update; `
    &$Env:windir\Microsoft.NET\Framework\v4.0.30319\ngen update

# Install 2.9.0 Roslyn compilers
RUN [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
    Invoke-WebRequest https://api.nuget.org/packages/microsoft.net.compilers.2.9.0.nupkg -OutFile C:\microsoft.net.compilers.2.9.0.zip; `
    Expand-Archive -Path C:\microsoft.net.compilers.2.9.0.zip -DestinationPath C:\RoslynCompilers; `
    Remove-Item C:\microsoft.net.compilers.2.9.0.zip -Force; `
    &$Env:windir\Microsoft.NET\Framework64\v4.0.30319\ngen install C:\RoslynCompilers\tools\csc.exe /ExeConfig:C:\RoslynCompilers\tools\csc.exe | `
    &$Env:windir\Microsoft.NET\Framework64\v4.0.30319\ngen install C:\RoslynCompilers\tools\vbc.exe /ExeConfig:C:\RoslynCompilers\tools\vbc.exe | `
    &$Env:windir\Microsoft.NET\Framework64\v4.0.30319\ngen install C:\RoslynCompilers\tools\VBCSCompiler.exe /ExeConfig:C:\RoslynCompilers\tools\VBCSCompiler.exe | `
    &$Env:windir\Microsoft.NET\Framework\v4.0.30319\ngen install C:\RoslynCompilers\tools\csc.exe /ExeConfig:C:\RoslynCompilers\tools\csc.exe | `
    &$Env:windir\Microsoft.NET\Framework\v4.0.30319\ngen install C:\RoslynCompilers\tools\vbc.exe /ExeConfig:C:\RoslynCompilers\tools\vbc.exe | `
    &$Env:windir\Microsoft.NET\Framework\v4.0.30319\ngen install C:\RoslynCompilers\tools\VBCSCompiler.exe  /ExeConfig:C:\RoslynCompilers\tools\VBCSCompiler.exe

# Install 3.6.0 Roslyn compilers
RUN [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
    Invoke-WebRequest https://api.nuget.org/packages/microsoft.net.compilers.3.6.0.nupkg -OutFile C:\microsoft.net.compilers.3.6.0.zip; `
    Expand-Archive -Path C:\microsoft.net.compilers.3.6.0.zip -DestinationPath C:\RoslynCompilers-3.6.0; `
    Remove-Item C:\microsoft.net.compilers.3.6.0.zip -Force; `
    &$Env:windir\Microsoft.NET\Framework64\v4.0.30319\ngen install C:\RoslynCompilers-3.6.0\tools\csc.exe /ExeConfig:C:\RoslynCompilers-3.6.0\tools\csc.exe | `
    &$Env:windir\Microsoft.NET\Framework64\v4.0.30319\ngen install C:\RoslynCompilers-3.6.0\tools\vbc.exe /ExeConfig:C:\RoslynCompilers-3.6.0\tools\vbc.exe | `
    &$Env:windir\Microsoft.NET\Framework64\v4.0.30319\ngen install C:\RoslynCompilers-3.6.0\tools\VBCSCompiler.exe /ExeConfig:C:\RoslynCompilers-3.6.0\tools\VBCSCompiler.exe | `
    &$Env:windir\Microsoft.NET\Framework\v4.0.30319\ngen install C:\RoslynCompilers-3.6.0\tools\csc.exe /ExeConfig:C:\RoslynCompilers-3.6.0\tools\csc.exe | `
    &$Env:windir\Microsoft.NET\Framework\v4.0.30319\ngen install C:\RoslynCompilers-3.6.0\tools\vbc.exe /ExeConfig:C:\RoslynCompilers-3.6.0\tools\vbc.exe | `
    &$Env:windir\Microsoft.NET\Framework\v4.0.30319\ngen install C:\RoslynCompilers-3.6.0\tools\VBCSCompiler.exe  /ExeConfig:C:\RoslynCompilers-3.6.0\tools\VBCSCompiler.exe

ENV ROSLYN_COMPILER_LOCATION=C:\RoslynCompilers-3.6.0\tools

EXPOSE 80

ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]
