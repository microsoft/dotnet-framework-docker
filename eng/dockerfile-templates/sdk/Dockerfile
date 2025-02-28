{{
    set sdkVersion to when(PRODUCT_VERSION = "4.8.1", "4.8.1", "4.8")
}}# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:{{PRODUCT_VERSION}}-{{VARIABLES[cat(PRODUCT_VERSION, "-", OS_VERSION_NUMBER, "-Runtime-DateStamp")]}}-{{OS_VERSION}}

ENV `
    # Do not generate certificate
    DOTNET_GENERATE_ASPNET_CERTIFICATE=false `
    {{if OS_VERSION_NUMBER != "ltsc2019":# Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true `
    }}# NuGet version to install
    NUGET_VERSION={{VARIABLES[cat("nuget|version")]}} `
    # Install location of Roslyn
    ROSLYN_COMPILER_LOCATION="C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\Roslyn"
{{if OS_VERSION_NUMBER = "ltsc2019" && PRODUCT_VERSION = "3.5"
:
RUN `
    # Install .NET {{sdkVersion}} Fx
    curl -fSLo dotnet-framework-installer.exe {{VARIABLES[cat(sdkVersion, "|url")]}} `
    && .\dotnet-framework-installer.exe /q `
    && del .\dotnet-framework-installer.exe `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\* `
    `
    # Apply latest patch
    && curl -fSLo patch.msu {{VARIABLES[cat("lcu|", OS_VERSION_NUMBER, "|", sdkVersion)]}} `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-{{VARIABLES[cat("kb|", OS_VERSION_NUMBER, "|", sdkVersion)]}}-x64-ndp48{{if PRODUCT_VERSION = "4.8.1":1}}.cab `
    && rmdir /S /Q patch `
    `
    # ngen .NET Fx
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update
}}
# Install NuGet CLI
RUN mkdir "%ProgramFiles%\NuGet\latest" `
    && curl -fSLo "%ProgramFiles%\NuGet\nuget.exe" https://dist.nuget.org/win-x86-commandline/v%NUGET_VERSION%/nuget.exe `
    && mklink "%ProgramFiles%\NuGet\latest\nuget.exe" "%ProgramFiles%\NuGet\nuget.exe"

# Install VS components
RUN `
    # Install VS Test Agent
    curl -fSLo vs_TestAgent.exe {{VARIABLES["vs|testAgentUrl"]}} `
    && start /w vs_TestAgent --quiet --norestart --nocache --wait --installPath "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\TestAgent" `
    && powershell -Command "if ($err = dir $Env:TEMP -Filter dd_setup_*_errors.log | where Length -gt 0 | Get-Content) { throw $err }" `
    && del vs_TestAgent.exe `
    `
    # Install VS Build Tools
    && curl -fSLo vs_BuildTools.exe {{VARIABLES["vs|buildToolsUrl"]}} `
    && start /w vs_BuildTools @^ `
        --installPath "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\BuildTools" @^ `
        --add Microsoft.Component.ClickOnce.MSBuild @^ `
        --add Microsoft.Net.Component.{{sdkVersion}}.SDK @^ `
        --add Microsoft.NetCore.Component.Runtime.8.0 @^ `
        --add Microsoft.NetCore.Component.Runtime.9.0 @^ `
        --add Microsoft.NetCore.Component.SDK @^ `
        --add Microsoft.VisualStudio.Component.NuGet.BuildTools @^ `
        --add Microsoft.VisualStudio.Component.WebDeploy @^ `
        --add Microsoft.VisualStudio.Web.BuildTools.ComponentGroup @^ `
        --add Microsoft.VisualStudio.Workload.MSBuildTools @^ `
        --quiet --norestart --nocache --wait `
    && powershell -Command "if ($err = dir $Env:TEMP -Filter dd_setup_*_errors.log | where Length -gt 0 | Get-Content) { throw $err }" `
    && del vs_BuildTools.exe `
    `
    # Trigger dotnet first run experience by running arbitrary cmd
    && "%ProgramFiles%\dotnet\dotnet" help `
    `
    # Workaround for issues with 64-bit ngen
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX {{sdkVersion}} Tools\SecAnnotate.exe" `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX {{sdkVersion}} Tools\WinMDExp.exe" `
    `
    # ngen assemblies queued by VS installers
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
{{if OS_VERSION_NUMBER = "ltsc2019"
:    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update `
}}    `
    # Cleanup
    && (for /D %i in ("%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\*") do rmdir /S /Q "%i") `
    && (for %i in ("%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\*") do if not "%~nxi" == "vswhere.exe" del "%~i") `
    && powershell Remove-Item -Force -Recurse "%TEMP%\*" `
    && rmdir /S /Q "%ProgramData%\Package Cache"

# Set PATH in one layer to keep image size down.
RUN powershell setx /M PATH $(${Env:PATH} `
    + \";${Env:ProgramFiles}\NuGet\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft Visual Studio\2022\TestAgent\Common7\IDE\CommonExtensions\Microsoft\TestWindow\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\amd64\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft SDKs\Windows\v10.0A\bin\NETFX {{sdkVersion}} Tools\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft SDKs\ClickOnce\SignTool\")

{{InsertTemplate("Dockerfile.install-reference-assemblies")}}{{
if OS_VERSION_NUMBER = "ltsc2019"
:

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]}}
