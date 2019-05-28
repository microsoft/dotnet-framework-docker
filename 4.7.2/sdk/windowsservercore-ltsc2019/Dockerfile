# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:4.7.2-windowsservercore-ltsc2019

# Install NuGet CLI
ENV NUGET_VERSION 4.4.1
RUN mkdir "%ProgramFiles%\NuGet" `
    && curl -fSLo "%ProgramFiles%\NuGet\nuget.exe" https://dist.nuget.org/win-x86-commandline/v%NUGET_VERSION%/nuget.exe

# Install VS components
RUN `
    # Install VS Test Agent
    curl -fSLo vs_TestAgent.exe https://download.visualstudio.microsoft.com/download/pr/4eef3cca-9da8-417a-889f-53212671e83b/50a7f7a24b21910a7cbe093fa150fd31/vs_testagent.exe `
    && start /w vs_TestAgent.exe --quiet --norestart --nocache --wait `
    && del vs_TestAgent.exe `
    `
    # Install VS Build Tools
    && curl -fSLo vs_BuildTools.exe https://download.visualstudio.microsoft.com/download/pr/4da568ff-b8aa-43ab-92ec-a8129370b49a/6fb89b999fed6f395622e004bfe442eb/vs_buildtools.exe `
    # Installer won't detect DOTNET_SKIP_FIRST_TIME_EXPERIENCE if ENV is used, must use setx /M
    && setx /M DOTNET_SKIP_FIRST_TIME_EXPERIENCE 1 `
    && start /w vs_BuildTools.exe ^ `
        --add Microsoft.VisualStudio.Workload.MSBuildTools ^ `
        --add Microsoft.VisualStudio.Workload.NetCoreBuildTools ^ `
        --add Microsoft.Net.Component.4.7.2.SDK ^ `
        --add Microsoft.Component.ClickOnce.MSBuild ^ `
        --quiet --norestart --nocache --wait `
    && del vs_BuildTools.exe `
    `
    # Cleanup
    && rmdir /S /Q "%ProgramFiles(x86)%\Microsoft Visual Studio\Installer" `
    && powershell Remove-Item -Force -Recurse "%TEMP%\*" `
    && rmdir /S /Q "%ProgramData%\Package Cache"

# Install web targets
RUN curl -fSLo MSBuild.Microsoft.VisualStudio.Web.targets.zip https://dotnetbinaries.blob.core.windows.net/dockerassets/MSBuild.Microsoft.VisualStudio.Web.targets.2019.05.zip `
    && tar -zxf MSBuild.Microsoft.VisualStudio.Web.targets.zip -C "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Microsoft\VisualStudio\v16.0" `
    && del MSBuild.Microsoft.VisualStudio.Web.targets.zip

ENV ROSLYN_COMPILER_LOCATION "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn"

# ngen assemblies queued by VS installers - must be done in cmd shell to avoid access issues
RUN `
    # Workaround for issue with 32 bit assemblies from Microsoft.Net.Component.4.7.2.SDK being 64 bit ngen'ed
    \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.7.2 Tools\SecAnnotate.exe" `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.7.2 Tools\WinMDExp.exe" `
    `
    && \Windows\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    `
    # Workaround VS installer/ngen issues
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\TestAgent\Common7\IDE\VSWebLauncher.exe" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\TestAgent\Common7\IDE\CommonExtensions\Microsoft\Editor\Microsoft.VisualStudio.Diff.CommandLineSwitch.pkgdef" `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\TestAgent\Common7\IDE\CommonExtensions\Microsoft\Editor\Microsoft.VisualStudio.Diff.pkgdef" `
    `
    && \Windows\Microsoft.NET\Framework\v4.0.30319\ngen update

# Set PATH in one layer to keep image size down.
RUN powershell setx /M PATH $(${Env:PATH} `
    + \";${Env:ProgramFiles}\NuGet\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\TestAgent\Common7\IDE\CommonExtensions\Microsoft\TestWindow\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.7.2 Tools\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft SDKs\ClickOnce\SignTool\")

# Install Targeting Packs
RUN powershell " `
    $ErrorActionPreference = 'Stop'; `
    $ProgressPreference = 'SilentlyContinue'; `
    @('4.0', '4.5.2', '4.6.2', '4.7.2') `
    | %{ `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri https://dotnetbinaries.blob.core.windows.net/referenceassemblies/v${_}.zip `
            -OutFile referenceassemblies.zip; `
        Expand-Archive referenceassemblies.zip -DestinationPath \"${Env:ProgramFiles(x86)}\Reference Assemblies\Microsoft\Framework\.NETFramework\"; `
        Remove-Item -Force referenceassemblies.zip; `
    }"

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]
