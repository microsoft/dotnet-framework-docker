FROM microsoft/dotnet-framework:4.7.1-windowsservercore

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Install NuGet CLI
RUN New-Item -Type Directory $Env:ProgramFiles\NuGet; \
    Invoke-WebRequest -UseBasicParsing https://dist.nuget.org/win-x86-commandline/v4.3.0/nuget.exe -OutFile $Env:ProgramFiles\NuGet\nuget.exe

# Install VS Test Agent
RUN Invoke-WebRequest -UseBasicParsing https://download.visualstudio.microsoft.com/download/pr/100232757/8a386d27295953ee79281fd1f1832e2d/vs_TestAgent.exe -OutFile vs_TestAgent.exe; \
    Start-Process vs_TestAgent.exe -ArgumentList '--passive', '--norestart' -NoNewWindow -Wait; \
    Remove-Item -Force vs_TestAgent.exe; \
# Install VS Build Tools
    Invoke-WebRequest -UseBasicParsing https://download.visualstudio.microsoft.com/download/pr/100196686/e64d79b40219aea618ce2fe10ebd5f0d/vs_BuildTools.exe -OutFile vs_BuildTools.exe; \
    Start-Process vs_BuildTools.exe -ArgumentList '--add', 'Microsoft.VisualStudio.Workload.MSBuildTools', '--passive', '--norestart' -NoNewWindow -Wait; \
    Remove-Item -Force vs_buildtools.exe
# Remove VS Installer
    # TODO: Waiting for clarity on the correct way to remove these.
    # The following hack frees up ~250 MB
    #Remove-Item -Force -Recurse $(${Env:ProgramFiles(x86)} + '\Microsoft Visual Studio\Installer'); \
    #Get-Item -Path $(${Env:ProgramData} + '\Microsoft\VisualStudio\Packages\*') -Exclude '_*' | Remove-Item -Force -Recurse

# Set PATH in one layer to keep image size down.
RUN setx /M PATH $(${Env:PATH} \
    + ';' + ${Env:ProgramFiles} + '\NuGet' \
    + ';' + ${Env:ProgramFiles(x86)} + '\Microsoft Visual Studio\2017\TestAgent\Common7\IDE\CommonExtensions\Microsoft\TestWindow' \
    + ';' + ${Env:ProgramFiles(x86)} + '\Microsoft Visual Studio\2017\BuildTools\MSBuild\15.0\Bin')

# Install Targeting Pack
RUN Invoke-WebRequest -UseBasicParsing https://dotnetweb.blob.core.windows.net/installer/NDP471-TargetingPack.exe -OutFile NDP471-TargetingPack.exe; \
    Start-Process NDP471-TargetingPack.exe -ArgumentList '/install', '/quiet' -NoNewWindow -Wait; \
    Remove-Item -Force NDP471-TargetingPack.exe
