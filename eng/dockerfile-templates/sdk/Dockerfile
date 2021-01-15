# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:{{PRODUCT_VERSION}}-{{OS_VERSION}}
{{if OS_VERSION_NUMBER = "ltsc2019" && PRODUCT_VERSION = "3.5"
:
# Install .NET 4.8 Fx
RUN curl -fSLo dotnet-framework-installer.exe https://download.visualstudio.microsoft.com/download/pr/7afca223-55d2-470a-8edc-6a1739ae3252/abd170b4b0ec15ad0222a809b761a036/ndp48-x86-x64-allos-enu.exe `
    && .\dotnet-framework-installer.exe /q `
    && del .\dotnet-framework-installer.exe `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\*

# ngen .NET Fx
RUN %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update
}}
# Install NuGet CLI
ENV NUGET_VERSION={{VARIABLES[cat("nuget|", OS_VERSION_NUMBER, "|version")]}}
RUN mkdir "%ProgramFiles%\NuGet\latest" `
    && curl -fSLo "%ProgramFiles%\NuGet\nuget.exe" https://dist.nuget.org/win-x86-commandline/v%NUGET_VERSION%/nuget.exe `
    && curl -fSLo "%ProgramFiles%\NuGet\latest\nuget.exe" https://dist.nuget.org/win-x86-commandline/v{{VARIABLES["nuget|version"]}}/nuget.exe

# Install VS components
RUN `
    # Install VS Test Agent
    curl -fSLo vs_TestAgent.exe {{VARIABLES["vs|testAgentUrl"]}} `
    && start /w vs_TestAgent --quiet --norestart --nocache --wait `
    && powershell -Command "if ($err = dir $Env:TEMP -Filter dd_setup_*_errors.log | where Length -gt 0 | Get-Content) { throw $err }" `
    && del vs_TestAgent.exe `
    `
    # Install VS Build Tools
    && curl -fSLo vs_BuildTools.exe {{VARIABLES["vs|buildToolsUrl"]}} `
    && start /w vs_BuildTools @^ `
        --add Microsoft.VisualStudio.Workload.MSBuildTools @^ `
        --add Microsoft.VisualStudio.Workload.NetCoreBuildTools @^ `
        --add Microsoft.Net.Component.4.8.SDK @^ `
        --add Microsoft.Component.ClickOnce.MSBuild @^ `
        --add Microsoft.VisualStudio.Component.WebDeploy @^ `
        --quiet --norestart --nocache --wait `
    && powershell -Command "if ($err = dir $Env:TEMP -Filter dd_setup_*_errors.log | where Length -gt 0 | Get-Content) { throw $err }" `
    && del vs_BuildTools.exe `
    `
    # Trigger dotnet first run experience by running arbitrary cmd
    && "%ProgramFiles%\dotnet\dotnet" help `
    `
    # Workaround for issues with 64-bit ngen
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\SecAnnotate.exe" `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\WinMDExp.exe" `
    `
    # ngen assemblies queued by VS installers
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
{{if OS_VERSION_NUMBER = "ltsc2019" || OS_VERSION_NUMBER = "1909"
:    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update `
}}    `
    # Cleanup
    && (for /D %i in ("%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\*") do rmdir /S /Q "%i") `
    && (for %i in ("%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\*") do if not "%~nxi" == "vswhere.exe" del "%~i") `
    && powershell Remove-Item -Force -Recurse "%TEMP%\*" `
    && rmdir /S /Q "%ProgramData%\Package Cache"

# Install web targets
RUN curl -fSLo MSBuild.Microsoft.VisualStudio.Web.targets.zip {{VARIABLES["vs|webTargetsUrl"]}} `
    && tar -zxf MSBuild.Microsoft.VisualStudio.Web.targets.zip -C "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Microsoft\VisualStudio\v16.0" `
    && del MSBuild.Microsoft.VisualStudio.Web.targets.zip

ENV {{if OS_VERSION_NUMBER != "ltsc2019":DOTNET_USE_POLLING_FILE_WATCHER=true `
    }}ROSLYN_COMPILER_LOCATION="C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn"

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
    @@('4.0', '4.5.2', '4.6.2', '4.7.2', '4.8') `
    | %{ `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri https://dotnetbinaries.blob.core.windows.net/referenceassemblies/v${_}.zip `
            -OutFile referenceassemblies.zip; `
        Expand-Archive referenceassemblies.zip -DestinationPath \"${Env:ProgramFiles(x86)}\Reference Assemblies\Microsoft\Framework\.NETFramework\"; `
        Remove-Item -Force referenceassemblies.zip; `
    }"{{
if OS_VERSION_NUMBER = "ltsc2019" || OS_VERSION_NUMBER = "1909" || OS_VERSION_NUMBER = "2004"
:

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]}}
