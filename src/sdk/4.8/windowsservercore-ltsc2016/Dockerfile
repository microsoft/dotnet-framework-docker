# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:4.8-20211012-windowsservercore-ltsc2016

# Install NuGet CLI
ENV NUGET_VERSION=5.11.0
RUN mkdir "%ProgramFiles%\NuGet\latest" `
    && powershell -Command `
        $ProgressPreference = 'SilentlyContinue'; `
        [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri https://dist.nuget.org/win-x86-commandline/v$Env:NUGET_VERSION/nuget.exe `
            -OutFile $Env:ProgramFiles\NuGet\nuget.exe; `
    && mklink "%ProgramFiles%\NuGet\latest\nuget.exe" "%ProgramFiles%\NuGet\nuget.exe"

# Do not generate certificate
ENV DOTNET_GENERATE_ASPNET_CERTIFICATE=false

# Install VS components
RUN `
    # Install VS Test Agent
    powershell -Command `
        $ProgressPreference = 'SilentlyContinue'; `
        [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri https://download.visualstudio.microsoft.com/download/pr/45dfa82b-c1f8-4c27-a5a0-1fa7a864ae21/c7109010de610a88f4140b97d799dafc8ab15f648bc0ab71a66af0cdf626c658/vs_TestAgent.exe `
            -OutFile vs_TestAgent.exe `
    && start /w vs_TestAgent --quiet --norestart --nocache --wait `
    && powershell -Command "if ($err = dir $Env:TEMP -Filter dd_setup_*_errors.log | where Length -gt 0 | Get-Content) { throw $err }" `
    && del vs_TestAgent.exe `
    `
    # Install VS Build Tools
    && powershell -Command `
        $ProgressPreference = 'SilentlyContinue'; `
        Invoke-WebRequest `
            -UseBasicParsing `
            -Uri https://download.visualstudio.microsoft.com/download/pr/45dfa82b-c1f8-4c27-a5a0-1fa7a864ae21/b5795c5efd2e27d3dccab0e27661079a2179262bbd3ad15832c4a169fb317eb1/vs_BuildTools.exe `
            -OutFile vs_BuildTools.exe `
    && start /w vs_BuildTools ^ `
        --add Microsoft.Component.ClickOnce.MSBuild ^ `
        --add Microsoft.Net.Component.4.8.SDK ^ `
        --add Microsoft.VisualStudio.Component.WebDeploy ^ `
        --add Microsoft.VisualStudio.Web.BuildTools.ComponentGroup ^ `
        --add Microsoft.VisualStudio.Workload.MSBuildTools ^ `
        --add Microsoft.VisualStudio.Workload.NetCoreBuildTools ^ `
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
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update `
    `
    # Cleanup
    && (for /D %i in ("%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\*") do rmdir /S /Q "%i") `
    && (for %i in ("%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\*") do if not "%~nxi" == "vswhere.exe" del "%~i") `
    && powershell Remove-Item -Force -Recurse "%TEMP%\*" `
    && rmdir /S /Q "%ProgramData%\Package Cache"

ENV ROSLYN_COMPILER_LOCATION="C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn"

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
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; `
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
