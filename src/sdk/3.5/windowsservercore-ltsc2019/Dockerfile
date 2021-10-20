# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:3.5-20211012-windowsservercore-ltsc2019

ENV `
    # Do not generate certificate
    DOTNET_GENERATE_ASPNET_CERTIFICATE=false `
    # NuGet version to install
    NUGET_VERSION=5.11.0 `
    # Install location of Roslyn
    ROSLYN_COMPILER_LOCATION="C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\Roslyn"

RUN `
    # Install .NET 4.8 Fx
    curl -fSLo dotnet-framework-installer.exe https://download.visualstudio.microsoft.com/download/pr/7afca223-55d2-470a-8edc-6a1739ae3252/abd170b4b0ec15ad0222a809b761a036/ndp48-x86-x64-allos-enu.exe `
    && .\dotnet-framework-installer.exe /q `
    && del .\dotnet-framework-installer.exe `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\* `
    `
    # Apply latest patch
    && curl -fSLo patch.msu http://download.windowsupdate.com/d/msdownload/update/software/updt/2021/08/windows10.0-kb5005540-x64-ndp48_ddca726ba3efa8935b549d9840b190c5efd78d60.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb5005540-x64-ndp48.cab `
    && rmdir /S /Q patch `
    `
    # ngen .NET Fx
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen update `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen update

# Install NuGet CLI
RUN mkdir "%ProgramFiles%\NuGet\latest" `
    && curl -fSLo "%ProgramFiles%\NuGet\nuget.exe" https://dist.nuget.org/win-x86-commandline/v%NUGET_VERSION%/nuget.exe `
    && mklink "%ProgramFiles%\NuGet\latest\nuget.exe" "%ProgramFiles%\NuGet\nuget.exe"

# Install VS components
RUN `
    # Install VS Test Agent
    curl -fSLo vs_TestAgent.exe https://download.visualstudio.microsoft.com/download/pr/45dfa82b-c1f8-4c27-a5a0-1fa7a864ae21/c7109010de610a88f4140b97d799dafc8ab15f648bc0ab71a66af0cdf626c658/vs_TestAgent.exe `
    && start /w vs_TestAgent --quiet --norestart --nocache --wait `
    && powershell -Command "if ($err = dir $Env:TEMP -Filter dd_setup_*_errors.log | where Length -gt 0 | Get-Content) { throw $err }" `
    && del vs_TestAgent.exe `
    `
    # Install VS Build Tools
    && curl -fSLo vs_BuildTools.exe https://download.visualstudio.microsoft.com/download/pr/45dfa82b-c1f8-4c27-a5a0-1fa7a864ae21/b5795c5efd2e27d3dccab0e27661079a2179262bbd3ad15832c4a169fb317eb1/vs_BuildTools.exe `
    && start /w vs_BuildTools ^ `
        --add Microsoft.VisualStudio.Workload.MSBuildTools ^ `
        --add Microsoft.VisualStudio.Workload.NetCoreBuildTools ^ `
        --add Microsoft.Net.Component.4.8.SDK ^ `
        --add Microsoft.Component.ClickOnce.MSBuild ^ `
        --add Microsoft.VisualStudio.Component.WebDeploy ^ `
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

# Install web targets
RUN curl -fSLo MSBuild.Microsoft.VisualStudio.Web.targets.zip https://dotnetbinaries.blob.core.windows.net/dockerassets/MSBuild.Microsoft.VisualStudio.Web.targets.2021.08.zip `
    && tar -zxf MSBuild.Microsoft.VisualStudio.Web.targets.zip -C "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\BuildTools\MSBuild\Microsoft\VisualStudio\v16.0" `
    && del MSBuild.Microsoft.VisualStudio.Web.targets.zip

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
