# escape=`

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:3.5-20220712-windowsservercore-ltsc2019

ENV `
    # Do not generate certificate
    DOTNET_GENERATE_ASPNET_CERTIFICATE=false `
    # NuGet version to install
    NUGET_VERSION=6.2.1 `
    # Install location of Roslyn
    ROSLYN_COMPILER_LOCATION="C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\Roslyn"

RUN `
    # Install .NET 4.8 Fx
    curl -fSLo dotnet-framework-installer.exe https://download.visualstudio.microsoft.com/download/pr/7afca223-55d2-470a-8edc-6a1739ae3252/abd170b4b0ec15ad0222a809b761a036/ndp48-x86-x64-allos-enu.exe `
    && .\dotnet-framework-installer.exe /q `
    && del .\dotnet-framework-installer.exe `
    && powershell Remove-Item -Force -Recurse ${Env:TEMP}\* `
    `
    # Apply latest patch
    && curl -fSLo patch.msu https://catalog.s.download.windowsupdate.com/d/msdownload/update/software/updt/2022/04/windows10.0-kb5013888-x64-ndp48_91410482e639b883cf0680e0287fce33248a49b1.msu `
    && mkdir patch `
    && expand patch.msu patch -F:* `
    && del /F /Q patch.msu `
    && dism /Online /Quiet /Add-Package /PackagePath:C:\patch\windows10.0-kb5013888-x64-ndp48.cab `
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
    curl -fSLo vs_TestAgent.exe https://download.visualstudio.microsoft.com/download/pr/dc2793e9-7b80-4f11-9e33-85833e8921a6/129ac716764b517709d969cad9e89c4472451c0c7c746f4f1e3141ddcef885c8/vs_TestAgent.exe `
    && start /w vs_TestAgent --quiet --norestart --nocache --wait `
    && powershell -Command "if ($err = dir $Env:TEMP -Filter dd_setup_*_errors.log | where Length -gt 0 | Get-Content) { throw $err }" `
    && del vs_TestAgent.exe `
    `
    # Install VS Build Tools
    && curl -fSLo vs_BuildTools.exe https://download.visualstudio.microsoft.com/download/pr/dc2793e9-7b80-4f11-9e33-85833e8921a6/06293b7db26d35bf19253f8059bf9feeb97d34236c02970afd1931f5e4da6da7/vs_BuildTools.exe `
    && start /w vs_BuildTools ^ `
        --add Microsoft.Component.ClickOnce.MSBuild ^ `
        --add Microsoft.Net.Component.4.8.SDK ^ `
        --add Microsoft.NetCore.Component.Runtime.3.1 ^ `
        --add Microsoft.NetCore.Component.Runtime.5.0 ^ `
        --add Microsoft.NetCore.Component.Runtime.6.0 ^ `
        --add Microsoft.NetCore.Component.SDK ^ `
        --add Microsoft.VisualStudio.Component.NuGet.BuildTools ^ `
        --add Microsoft.VisualStudio.Component.WebDeploy ^ `
        --add Microsoft.VisualStudio.Web.BuildTools.ComponentGroup ^ `
        --add Microsoft.VisualStudio.Workload.MSBuildTools ^ `
        --quiet --norestart --nocache --wait `
    && powershell -Command "if ($err = dir $Env:TEMP -Filter dd_setup_*_errors.log | where Length -gt 0 | Get-Content) { throw $err }" `
    && del vs_BuildTools.exe `
    `
    # Trigger dotnet first run experience by running arbitrary cmd
    && "%ProgramFiles%\dotnet\dotnet" help `
    `
    # Workaround for issues with 32-bit ngen
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\TestAgent\Common7\IDE\Extensions\TestPlatform\testhost.arm64.exe" `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\TestAgent\Common7\IDE\Extensions\TestPlatform\testhost.net452.arm64.exe" `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\TestAgent\Common7\IDE\Extensions\TestPlatform\testhost.net46.arm64.exe" `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\TestAgent\Common7\IDE\Extensions\TestPlatform\testhost.net461.arm64.exe" `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\TestAgent\Common7\IDE\Extensions\TestPlatform\testhost.net462.arm64.exe" `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\TestAgent\Common7\IDE\Extensions\TestPlatform\testhost.net47.arm64.exe" `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\TestAgent\Common7\IDE\Extensions\TestPlatform\testhost.net471.arm64.exe" `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\TestAgent\Common7\IDE\Extensions\TestPlatform\testhost.net472.arm64.exe" `
    && %windir%\Microsoft.NET\Framework\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\TestAgent\Common7\IDE\Extensions\TestPlatform\testhost.net48.arm64.exe" `
    `
    # Workaround for issues with 64-bit ngen
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\SecAnnotate.exe" `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\WinMDExp.exe" `
    && %windir%\Microsoft.NET\Framework64\v4.0.30319\ngen uninstall "%ProgramFiles(x86)%\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\arm64\MSBuild.exe" `
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

# Set PATH in one layer to keep image size down.
RUN powershell setx /M PATH $(${Env:PATH} `
    + \";${Env:ProgramFiles}\NuGet\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft Visual Studio\2022\TestAgent\Common7\IDE\CommonExtensions\Microsoft\TestWindow\" `
    + \";${Env:ProgramFiles(x86)}\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\" `
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
