# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:3.5-windowsservercore-1803

# Install .NET 4.8 Fx
RUN curl -fSLo dotnet-framework-installer.exe https://download.visualstudio.microsoft.com/download/pr/7afca223-55d2-470a-8edc-6a1739ae3252/abd170b4b0ec15ad0222a809b761a036/ndp48-x86-x64-allos-enu.exe `
    && .\dotnet-framework-installer.exe /q `
    && del .\dotnet-framework-installer.exe `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\*

# ngen .NET Fx
RUN \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update

# Install NuGet CLI
ENV NUGET_VERSION 4.4.3
RUN mkdir "%ProgramFiles%\NuGet" `
    && curl -fSLo "%ProgramFiles%\NuGet\nuget.exe" https://dist.nuget.org/win-x86-commandline/v%NUGET_VERSION%/nuget.exe

# Install VS components
RUN `
    # Install VS Test Agent
    curl -fSLo vs_TestAgent.exe https://download.visualstudio.microsoft.com/download/pr/1d102ee6-9263-4353-8676-6430f0cc5998/1e779e07d0265e229adf69a6b67ebeecd24afe3179c1b15864f3fb81533aa89f/vs_TestAgent.exe `
    && start /w vs_TestAgent.exe --quiet --norestart --nocache --wait `
    && powershell -Command "if ($err = dir $Env:TEMP -Filter dd_setup_*_errors.log | where Length -gt 0 | Get-Content) { throw $err }" `
    && del vs_TestAgent.exe `
    `
    # Install VS Build Tools
    && curl -fSLo vs_BuildTools.exe https://download.visualstudio.microsoft.com/download/pr/1d102ee6-9263-4353-8676-6430f0cc5998/c0457643c8a80b008250148151a51cb6cc0aa2ccb57316db496628faa9b9a84f/vs_BuildTools.exe `
    # Installer won't detect DOTNET_SKIP_FIRST_TIME_EXPERIENCE if ENV is used, must use setx /M
    && setx /M DOTNET_SKIP_FIRST_TIME_EXPERIENCE 1 `
    && start /w vs_BuildTools.exe ^ `
        --add Microsoft.VisualStudio.Workload.MSBuildTools ^ `
        --add Microsoft.VisualStudio.Workload.NetCoreBuildTools ^ `
        --add Microsoft.Net.Component.4.8.SDK ^ `
        --add Microsoft.Component.ClickOnce.MSBuild ^ `
        --add Microsoft.VisualStudio.Component.WebDeploy ^ `
        --quiet --norestart --nocache --wait `
    && powershell -Command "if ($err = dir $Env:TEMP -Filter dd_setup_*_errors.log | where Length -gt 0 | Get-Content) { throw $err }" `
    && del vs_BuildTools.exe `
    `
    #Cleanup
    && rmdir /S /Q "%ProgramFiles(x86)%\Microsoft Visual Studio\Installer" `
    && powershell Remove-Item -Force -Recurse "%TEMP%\*" `
    && rmdir /S /Q "%ProgramData%\Package Cache"

# Install web targets
RUN curl -fSLo MSBuild.Microsoft.VisualStudio.Web.targets.zip https://dotnetbinaries.blob.core.windows.net/dockerassets/MSBuild.Microsoft.VisualStudio.Web.targets.2019.09.zip `
    && tar -zxf MSBuild.Microsoft.VisualStudio.Web.targets.zip -C "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Microsoft\VisualStudio\v16.0" `
    && del MSBuild.Microsoft.VisualStudio.Web.targets.zip

ENV ROSLYN_COMPILER_LOCATION "C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn"

# ngen assemblies queued by VS installers - must be done in cmd shell to avoid access issues
RUN `
    # Workaround for issue with 32 bit assemblies from .NET Framework 4.8 SDK being 64 bit ngen'ed
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\SecAnnotate.exe" `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\WinMDExp.exe" `
    `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    `
    # Workaround VS installer/ngen issues
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\TestAgent\Common7\IDE\VSWebLauncher.exe" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\csc.exe.config" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\csc.rsp" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\csi.exe.config" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\csi.rsp" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\Microsoft.CSharp.Core.targets" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\Microsoft.Managed.Core.targets" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\Microsoft.Managed.EditorConfig.targets" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\Microsoft.VisualBasic.Core.targets" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\vbc.exe.config" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\vbc.rsp" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\VBCSCompiler.exe.config" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\Microsoft.DiaSymReader.Native.amd64.dll" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn\Microsoft.DiaSymReader.Native.x86.dll" `
    `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update

# Set PATH in one layer to keep image size down.
RUN powershell setx /M PATH $(${Env:PATH} `
    + \";${Env:ProgramFiles}\NuGet\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\TestAgent\Common7\IDE\CommonExtensions\Microsoft\TestWindow\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft SDKs\ClickOnce\SignTool\")

# Install Targeting Packs
RUN powershell " `
    $ErrorActionPreference = 'Stop'; `
    $ProgressPreference = 'SilentlyContinue'; `
    @('4.0', '4.5.2', '4.6.2', '4.7.2', '4.8') `
    | %{ `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri https://dotnetbinaries.blob.core.windows.net/referenceassemblies/v${_}.zip `
            -OutFile referenceassemblies.zip; `
        Expand-Archive referenceassemblies.zip -DestinationPath \"${Env:ProgramFiles(x86)}\Reference Assemblies\Microsoft\Framework\.NETFramework\"; `
        Remove-Item -Force referenceassemblies.zip; `
    }"

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]
